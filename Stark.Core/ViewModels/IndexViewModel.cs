namespace Stark.ViewModels
{
    using Stark.DataAccessLayer;
    using Stark.Models;
    using Stark.MVVM;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text.Json;
    using System.Text.Json.Serialization;
    using System.Threading.Tasks;

    public class IndexViewModel : ViewModelBase
    {
        private readonly ILogGrabberService loggrabber;
        private readonly IFileService fileService;
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
        private string currentSortProperty;
        private bool sortDirectionDown = true;

        public IndexViewModel(ILogGrabberService loggrabber, IFileService fileService)
        {
            this.loggrabber = loggrabber;
            this.fileService = fileService;
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

        public string CurrentSortProperty
        {
            get => this.currentSortProperty;
            set
            {
                this.currentSortProperty = value;
                this.OnPropertyChanged();
            }
        }

        public bool SortDirectionDown
        {
            get => this.sortDirectionDown;
            set
            {
                this.sortDirectionDown = value;
                this.OnPropertyChanged();
            }
        }

        public async Task GetLogListAsync()
        {
            this.ValidateInputFields();

            if (this.Errors.Count > 0)
            {
                this.Errors.Clear();
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

                if (collectedLogs?.Count > 0)
                {
                    foreach (LogModel log in collectedLogs)
                    {
                        this.LogViewModels.Add(new LogViewModel(log));
                    }
                }

                await this.SortByProperty(this.CurrentSortProperty, true);
            }
            catch (Exception ex)
            {
                this.Errors.Add($"An error was thrown while searching: {ex.Message}");
            }

            this.IsLoading = false;
        }

        public async Task SortByProperty(string propertyName, bool ignoreToggle = false)
        {
            if (string.IsNullOrEmpty(propertyName))
            {
                return;
            }

            string prevSortProperty = this.CurrentSortProperty;
            this.CurrentSortProperty = propertyName;

            if (!ignoreToggle && this.CurrentSortProperty.Equals(prevSortProperty, StringComparison.InvariantCultureIgnoreCase))
            {
                this.SortDirectionDown = !this.SortDirectionDown;
            }

            this.IsLoading = true;
            await Task.Run(() =>
            {
                lock (this.LogViewModels)
                {
                    switch (this.CurrentSortProperty)
                    {
                        case nameof(LogModel.TimeGenerated):
                            if (this.sortDirectionDown)
                            {
                                this.LogViewModels.Sort((x, y) => x.TimeGenerated.CompareTo(y.TimeGenerated));
                            }
                            else
                            {
                                this.LogViewModels.Sort((x, y) => y.TimeGenerated.CompareTo(x.TimeGenerated));
                            }
                            break;
                        case nameof(LogModel.Type):
                            if (this.sortDirectionDown)
                            {
                                this.LogViewModels.Sort((x, y) => x.Type.CompareTo(y.Type));
                            }
                            else
                            {
                                this.LogViewModels.Sort((x, y) => y.Type.CompareTo(x.Type));
                            }
                            break;
                        case nameof(LogModel.SourceName):
                            if (this.sortDirectionDown)
                            {
                                this.LogViewModels.Sort((x, y) => x.SourceName.CompareTo(y.SourceName));
                            }
                            else
                            {
                                this.LogViewModels.Sort((x, y) => y.SourceName.CompareTo(x.SourceName));
                            }
                            break;
                        case nameof(LogModel.EventCode):
                            if (this.sortDirectionDown)
                            {
                                this.LogViewModels.Sort((x, y) => x.EventCode.CompareTo(y.EventCode));
                            }
                            else
                            {
                                this.LogViewModels.Sort((x, y) => y.EventCode.CompareTo(x.EventCode));
                            }
                            break;
                        case nameof(LogModel.Message):
                            if (this.sortDirectionDown)
                            {
                                this.LogViewModels.Sort((x, y) => x.Message.CompareTo(y.Message));
                            }
                            else
                            {
                                this.LogViewModels.Sort((x, y) => y.Message.CompareTo(x.Message));
                            }
                            break;
                        default:
                            this.LogViewModels.Sort((x, y) => y.TimeGenerated.CompareTo(x.TimeGenerated));
                            break;
                    }
                }

                this.IsLoading = false;
                this.OnPropertyChanged(nameof(this.LogViewModels));
            });
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

        public async Task ExportResultsAsync(string targetPath)
        {
            if (string.IsNullOrWhiteSpace(targetPath))
            {
                this.Errors.Add("Cannot save to empty or null location.");
                return;
            }

            string filename = targetPath.Substring(targetPath.LastIndexOf('\\') + 1);
            string targetDirectory = targetPath.Substring(0, targetPath.LastIndexOf('\\'));
            string fileContents = await LogFormatter.FormatLogsByExtensionAsync(filename.Substring(filename.LastIndexOf('.') + 1), this.LogViewModels);
            await this.fileService.WriteTextToFileAsync(targetDirectory, filename, fileContents);
        }
    }
}
