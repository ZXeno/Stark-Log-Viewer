namespace Stark.DataAccessLayer
{
    using Stark.Factories;
    using Stark.Models;
    using System;
    using System.Collections.Concurrent;
    using System.Collections.Generic;
    using System.Linq;
    using System.Management;
    using System.Threading.Tasks;

    public class LogGrabberService
    {
        private readonly WmiService wmi;

        public LogGrabberService(WmiService wmi)
        {
            this.wmi = wmi;
        }

        public async Task<List<LogModel>> GetRemoteEventLogDataAsync(DateTime start, DateTime end, string serverFQDN, string messagequeryvalue, string username, string password, LogTypeEnum logType)
        {
            ConnectionOptions connops = new ConnectionOptions();

            if (!string.IsNullOrEmpty(username) && !string.IsNullOrEmpty(password))
            {
                connops = new ConnectionOptions()
                {
                    Password = password,
                    Username = username,
                };
            }

            ManagementScope remote = this.wmi.ConnectToRemoteWmi(serverFQDN, WellKnownStrings.WmiRootNamespace, connops);

            return await ExecuteQuery(remote, start, end, messagequeryvalue, logType);
        }

        public async Task<List<LogModel>> GetEventLogDataAsync(DateTime start, DateTime end, string messagequeryvalue, LogTypeEnum logType)
        {
            ManagementScope remote = this.wmi.ConnectToRemoteWmi(WellKnownStrings.Localhost, WellKnownStrings.WmiRootNamespace, new ConnectionOptions());

            return await ExecuteQuery(remote, start, end, messagequeryvalue, logType);
        }

        private async Task<List<LogModel>> ExecuteQuery(ManagementScope remote, DateTime start, DateTime end, string messagequeryvalue, LogTypeEnum logType)
        {
            if (remote != null)
            {
                return await Task.Run(() =>
                {
                    string timeBasedQuery = this.BuildTimeBasedQueryFromDates(start, end);
                    string logfileName = this.GetLogFileName(logType);

                    ObjectQuery query = new ObjectQuery($"SELECT * FROM Win32_NTLogEvent where (logfile='{logfileName}' and Message like '%{messagequeryvalue}%'{timeBasedQuery})");
                    ManagementObjectSearcher searcher = new ManagementObjectSearcher(remote, query);

                    ManagementObjectCollection querycollection = searcher.Get();

                    ConcurrentBag<LogModel> bag = new ConcurrentBag<LogModel>();

                    if (querycollection?.Count == 0)
                    {
                        return bag.ToList();
                    }

                    ManagementBaseObject[] resultarray = new ManagementBaseObject[querycollection.Count];
                    querycollection.CopyTo(resultarray, 0);

                    Parallel.ForEach(resultarray, (mbo) =>
                    {
                        bag.Add(LogModelFactory.BuildItemFromManagementBaseObject(mbo));
                    });

                    return bag.ToList();
                });
            }
            else
            {
                return null;
            }
        }

        private string GetLogFileName(LogTypeEnum logType)
        {
            switch (logType)
            {
                case LogTypeEnum.SelectOne:
                    throw new NotSupportedException("A log type must be selected.");
                case LogTypeEnum.Application:
                    return logType.ToString();
                case LogTypeEnum.Security:
                    return logType.ToString();
                case LogTypeEnum.Setup:
                    return logType.ToString();
                case LogTypeEnum.System:
                    return logType.ToString();
            }

            return LogTypeEnum.Application.ToString();
        }

        private string BuildTimeBasedQueryFromDates(DateTime start, DateTime end)
        {
            string convertedstartdate = DateTimeToManagementObjectDateTime(start.ToUniversalTime());
            string convertedenddate = DateTimeToManagementObjectDateTime(end.ToUniversalTime());

            // Because this is used directly in the building of the query, put a space the front.
            return $" and TimeGenerated >= '{convertedstartdate}' and TimeGenerated <= '{convertedenddate}'";
        }

        private string DateTimeToManagementObjectDateTime(DateTime date)
        {
            return date.ToString("yyyyMMddHHmmss.ffffff±zzz");
        }
    }
}
