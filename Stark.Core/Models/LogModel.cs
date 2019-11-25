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
    }
}
