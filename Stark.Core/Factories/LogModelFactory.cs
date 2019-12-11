namespace Stark.Factories
{
    using Stark.Models;
    using System.Management;

    public class LogModelFactory
    {
        public static LogModel BuildItemFromManagementBaseObject(ManagementBaseObject mbo)
        {
            return new LogModel()
            {
                Category = mbo["Category"]?.ToString().Trim(),
                CategoryString = mbo["CategoryString"]?.ToString().Trim(),
                ComputerName = mbo["ComputerName"]?.ToString().Trim(),
                Data = mbo["Data"]?.ToString().Trim(),
                EventCode = mbo["EventCode"]?.ToString().Trim(),
                EventIdentifier = mbo["EventIdentifier"]?.ToString().Trim(),
                EventType = mbo["EventType"]?.ToString().Trim(),
                InsertionStrings = mbo["InsertionStrings"]?.ToString().Trim(),
                Logfile = mbo["Logfile"]?.ToString().Trim(),
                Message = mbo["Message"]?.ToString().Trim(),
                RecordNumber = mbo["RecordNumber"]?.ToString().Trim(),
                SourceName = mbo["SourceName"]?.ToString().Trim(),
                TimeGenerated = ManagementDateTimeConverter.ToDateTime(mbo["TimeGenerated"]?.ToString().Trim()),
                TimeWritten = ManagementDateTimeConverter.ToDateTime(mbo["TimeWritten"]?.ToString().Trim()),
                Type = mbo["Type"]?.ToString().Trim(),
                User = mbo["User"]?.ToString().Trim(),
            };
        }
    }
}
