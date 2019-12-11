namespace Stark.ViewModels
{
    using Stark.Models;
    using Stark.Extensions;
    using System;

    public class LogViewModel
    {
        private const int ShortMessageLength = 100;

        public LogViewModel(LogModel logModel)
        {
            this.LogModel = logModel;
            this.ShowReadMoreAndLessButtons = this.Message.Length > ShortMessageLength;
            this.ShowReadMoreText = true; // won't show if we don't show the buttons anyway
        }

        public DateTime TimeGenerated
        {
            get
            {
                return this.LogModel?.TimeGenerated ?? DateTime.MinValue;
            }
        }

        public string Type
        {
            get
            {
                return this.LogModel?.Type ?? string.Empty;
            }
        }

        public string SourceName
        {
            get
            {
                return this.LogModel?.SourceName ?? string.Empty;
            }
        }

        public string EventCode
        {
            get
            {
                return this.LogModel?.EventCode ?? string.Empty;
            }
        }

        public string Message
        {
            get
            {
                return this.LogModel?.Message ?? string.Empty;
            }
        }

        public bool ShowReadMoreAndLessButtons { get; private set; }

        public bool ShowReadMoreText { get; set; }

        public LogModel LogModel { get; private set; }

        public string GetMessageString()
        {
            if (ShowReadMoreText)
            {
                return this.LogModel.GetShortenedMessageString(ShortMessageLength);
            }

            return this.LogModel?.Message;
        }
    }
}
