namespace Stark.ViewModels
{
    using Stark.DataAccessLayer;
    using Stark.Models;
    using Stark.MVVM;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Threading.Tasks;

    public class IndexViewModel : ViewModelBase
    {
        private readonly LogGrabberService loggrabber;
        private List<LogModel> logModels;
        private string targetServer;
        private string filterText;
        private DateTime filterStartDate;
        private DateTime filterEndDate;
        private bool useExternalCredentials;
        private string userName;
        private string password;
        private bool isLoading;

        public IndexViewModel(LogGrabberService loggrabber)
        {
            this.loggrabber = loggrabber;
            this.logModels = new List<LogModel>();
            this.targetServer = "localhost";
            this.filterStartDate = DateTime.Now.AddDays(-7);
            this.filterEndDate = DateTime.Now;
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

        public List<LogModel> LogModels
        {
            get => this.logModels;
            set
            {
                this.logModels = value;
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

        public async Task GetLogListAsync()
        {
            this.IsLoading = true;
            if (this.UseExternalCredentials
                && (!string.IsNullOrEmpty(this.TargetServer)
                || this.TargetServer.Equals("localhost")
                || this.targetServer.Equals("127.0.0.1")))
            {
                this.LogModels = await this.loggrabber.GetRemoteEventLogDataAsync(this.FilterStartDate, this.FilterEndDate, this.TargetServer, this.FilterText, this.UserName, this.Password);
            }
            else
            {
                this.LogModels = await this.loggrabber.GetEventLogDataAsync(this.FilterStartDate, this.FilterEndDate, this.FilterText);
            }

            this.LogModels.Sort();
            this.IsLoading = false;
        }
    }
}
