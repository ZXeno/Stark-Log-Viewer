namespace Stark.Models
{
    using System;

    public class LogModel : IComparable
    {
        public string Category { get; set; }
        public string CategoryString { get; set; }
        public string ComputerName { get; set; }
        public string Data { get; set; }
        public string EventCode { get; set; }
        public string EventIdentifier { get; set; }
        public string EventType { get; set; }
        public string InsertionStrings { get; set; }
        public string Logfile { get; set; }
        public string Message { get; set; }
        public string RecordNumber { get; set; }
        public string SourceName { get; set; }
        public DateTime TimeGenerated { get; set; }
        public DateTime TimeWritten { get; set; }
        public string Type { get; set; }
        public string User { get; set; }

        public int CompareTo(object obj)
        {
            if (obj is LogModel model)
            {
                return this.TimeGenerated.CompareTo(model.TimeGenerated);
            }
            else if (obj == null)
            {
                return 1;
            }
            else
            {
                throw new ArgumentException("Object is not a LogModel");
            }
        }

        public override string ToString()
        {
            return "{" + this.Logfile + "," + this.TimeGenerated + "," + this.Type + "," + this.SourceName + "," + this.Message + this.EventCode + "}";
        }

        /// <summary>
        /// Returns the entire log formatted as a string. "f" for Full "b" for brief.
        /// </summary>
        /// <returns>The log formatted as a string as defined by format</returns>
        public string ToString(string format)
        {
            switch(format)
            {
                case "f":
                    return this.VerboseFormat();
                case "b":
                    return this.BriefFormat();
                default:
                    return this.ToString();
            }
        }

        private string VerboseFormat()
        {
            return "Categrory: " + this.Category + Environment.NewLine +
                   "CategoryString: " + this.CategoryString + Environment.NewLine +
                   "ComputerName: " + this.ComputerName + Environment.NewLine +
                   "Data: " + this.Data + Environment.NewLine +
                   "EventCode: " + this.EventCode + Environment.NewLine +
                   "EventIdentifier: " + this.EventIdentifier + Environment.NewLine +
                   "EventType: " + this.EventType + Environment.NewLine +
                   "InsertionStrings: " + this.InsertionStrings + Environment.NewLine +
                   "Logfile: " + this.Logfile + Environment.NewLine +
                   "Message: " + this.Message + Environment.NewLine +
                   "RecordNumber: " + this.RecordNumber + Environment.NewLine +
                   "SourceName: " + this.SourceName + Environment.NewLine +
                   "TimeGenerated: " + this.TimeGenerated + Environment.NewLine +
                   "TimeWritten: " + this.TimeWritten + Environment.NewLine +
                   "Type: " + this.Type + Environment.NewLine +
                   "User: " + this.User;
        }

        private string BriefFormat()
        {
            return "Logfile: " + this.Logfile + Environment.NewLine +
                   "TimeGenerated: " + this.TimeGenerated + Environment.NewLine +
                   "Type: " + this.Type + Environment.NewLine +
                   "SourceName: " + this.SourceName + Environment.NewLine +
                   "Message: " + this.Message + Environment.NewLine +
                   "EventCode: " + this.EventCode;
        }
    }
}
