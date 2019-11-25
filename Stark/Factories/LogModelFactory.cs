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
                Category = mbo["Category"]?.ToString(),
                CategoryString = mbo["CategoryString"]?.ToString(),
                ComputerName = mbo["ComputerName"]?.ToString(),
                Data = mbo["Data"]?.ToString(),
                EventCode = mbo["EventCode"]?.ToString(),
                EventIdentifier = mbo["EventIdentifier"]?.ToString(),
                EventType = mbo["EventType"]?.ToString(),
                InsertionStrings = mbo["InsertionStrings"]?.ToString(),
                Logfile = mbo["Logfile"]?.ToString(),
                Message = mbo["Message"]?.ToString(),
                RecordNumber = mbo["Message"]?.ToString(),
                SourceName = mbo["SourceName"]?.ToString(),
                TimeGenerated = ManagementDateTimeConverter.ToDateTime(mbo["TimeGenerated"]?.ToString()),
                TimeWritten = ManagementDateTimeConverter.ToDateTime(mbo["TimeWritten"]?.ToString()),
                Type = mbo["Type"]?.ToString(),
                User = mbo["User"]?.ToString(),
            };
        }
    }
}
