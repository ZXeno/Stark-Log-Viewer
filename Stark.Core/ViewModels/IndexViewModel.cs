namespace Stark.ViewModels
{
    using Stark.DataAccessLayer;
    using Stark.Models;
    using Stark.MVVM;
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public class IndexViewModel : ViewModelBase
    {
        private readonly LogGrabberService loggrabber;
        private List<LogViewModel> logViewModels;
        private string targetServer;
        private string filterText;
        private DateTime filterStartDate;
        private DateTime filterEndDate;
        private bool useExternalCredentials;
        private string userName;
        private string password;
        private bool isLoading;
        private LogTypeEnum logType;
        private List<string> errors;

        public IndexViewModel(LogGrabberService loggrabber)
        {
            this.loggrabber = loggrabber;
            this.logViewModels = new List<LogViewModel>();
            this.targetServer = "localhost";
            this.filterStartDate = DateTime.Now.AddDays(-7);
            this.filterEndDate = DateTime.Now;
            this.errors = new List<string>();
        }

        public string TargetServer
        {
            get => this.targetServer;
            set
            {
                this.targetServer = value;
                this.OnPropertyChanged();
            }
        }

        public string FilterText
        {
            get => this.filterText;
            set
            {
                this.filterText = value;
                this.OnPropertyChanged();
            }
        }

        public DateTime FilterStartDate
        {
            get => this.filterStartDate; set
            {
                this.filterStartDate = value;
                this.OnPropertyChanged();
            }
        }

        public DateTime FilterEndDate
        {
            get => this.filterEndDate;
            set
            {
                this.filterEndDate = value;
                this.OnPropertyChanged();
            }
        }

        public bool UseExternalCredentials
        {
            get => this.useExternalCredentials; set
            {
                this.useExternalCredentials = value;
                this.OnPropertyChanged();
            }
        }

        public string UserName
        {
            get => this.userName;
            set
            {
                this.userName = value;
                this.OnPropertyChanged();
            }
        }

        public string Password
        {
            get => this.password;
            set
            {
                this.password = value;
                this.OnPropertyChanged();
            }
        }

        public List<LogViewModel> LogViewModels
        {
            get => this.logViewModels;
            set
            {
                this.logViewModels = value;
                this.OnPropertyChanged();
            }
        }

        public bool IsLoading
        {
            get => this.isLoading;
            set
            {
                this.isLoading = value;
                this.OnPropertyChanged();
            }
        }

        public LogTypeEnum LogType
        {
            get => this.logType;
            set
            {
                this.logType = value;
                this.OnPropertyChanged();
            }
        }

        public List<string> Errors
        {
            get => this.errors;
            set
            {
                this.errors = value;
                this.OnPropertyChanged();
            }
        }

        public async Task GetLogListAsync()
        {
            this.ValidateInputFields();

            if (this.Errors.Count > 0)
            {
                return;
            }

            this.LogViewModels.Clear();
            this.IsLoading = true;
            List<LogModel> collectedLogs = new List<LogModel>();

            try
            {
                if (this.UseExternalCredentials
                    && (!string.IsNullOrEmpty(this.TargetServer)
                    || this.TargetServer.Equals("localhost")
                    || this.targetServer.Equals("127.0.0.1")))
                {
                    collectedLogs = await this.loggrabber.GetRemoteEventLogDataAsync(this.FilterStartDate, this.FilterEndDate, this.TargetServer, this.FilterText, this.UserName, this.Password, this.LogType);
                }
                else
                {
                    collectedLogs = await this.loggrabber.GetEventLogDataAsync(this.FilterStartDate, this.FilterEndDate, this.FilterText, this.LogType);
                }

                if (collectedLogs.Count > 0)
                {
                    foreach (LogModel log in collectedLogs)
                    {
                        this.LogViewModels.Add(new LogViewModel(log));
                    }
                }

                this.LogViewModels.Sort((x, y) => x.TimeGenerated.CompareTo(y.TimeGenerated));
            }
            catch (Exception ex)
            {
                this.Errors.Add($"An error was thrown while searching: {ex.Message}");
            }

            this.IsLoading = false;
        }

        private void ValidateInputFields()
        {
            this.Errors.Clear();

            if (string.IsNullOrEmpty(this.TargetServer))
            {
                Errors.Add("Target Server cannot be empty!");
            }

            if (this.LogType == LogTypeEnum.SelectOne)
            {
                Errors.Add("You must select a Log Type to query!");
            }

            if (this.UseExternalCredentials)
            {
                if (string.IsNullOrEmpty(this.UserName))
                {
                    Errors.Add("You must provide a username!");
                }

                if (string.IsNullOrEmpty(this.Password))
                {
                    Errors.Add("Password cannot be blank!");
                }
            }
        }
    }
}
