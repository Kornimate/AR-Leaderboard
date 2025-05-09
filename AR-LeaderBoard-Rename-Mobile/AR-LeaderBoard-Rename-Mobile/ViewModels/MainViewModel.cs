using AR_LeaderBoard_Rename_Mobile.Interfaces;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace AR_LeaderBoard_Rename_Mobile.ViewModels
{
    public class MainViewModel : ObservableObject
    {
        private readonly IMainModel _model;
        private readonly IConfiguration _config;

        public event EventHandler<string>? ShowNotification;

        private string _oldTeamName;
        public string OldTeamName
        {
            get => _oldTeamName;
            set
            {
                if (value != _oldTeamName)
                {
                    _oldTeamName = value;
                    OnPropertyChanged(nameof(OldTeamName));
                }
            }
        }

        private string _newTeamName;
        public string NewTeamName
        {
            get => _newTeamName;
            set
            {
                if (value != _newTeamName)
                {
                    _newTeamName = value;
                    OnPropertyChanged(nameof(NewTeamName));
                }
            }
        }

        private string _url;
        public string Url
        {
            get => _url;
            set
            {
                if (value != _url)
                {
                    _url = value;
                    OnPropertyChanged(nameof(Url));
                }
            }
        }

        public ICommand SendRequestCommand { get; set; }
        public ICommand GetHelpForTeamNameCommand { get; set; }
        public MainViewModel(IMainModel model, IConfiguration config)
        {
            _model = model;
            _config = config;

            _model.RequestSent += NotifyAboutResultOfRequest;
            _model.EntriesReceivedAsString += SetOldTeamNameToSuggestion;

            SendRequestCommand = new AsyncRelayCommand(SendRequest);
            GetHelpForTeamNameCommand = new AsyncRelayCommand(GetHelpForTeamName);

            _url = $"{_config["WebApiBaseAddress"]}";
            _oldTeamName = string.Empty;
            _newTeamName = string.Empty;
        }

        private async Task GetHelpForTeamName()
        {
            await _model.GetNumberOfEntries(Url);
        }

        private async Task SendRequest()
        {
            List<string> errorMessageComponents = [];
            string createdMessage = string.Empty;

            if (OldTeamName == string.Empty)
            {
                errorMessageComponents.Add("Old Team Name");
            }

            if (NewTeamName == string.Empty)
            {
                errorMessageComponents.Add("New Team Name");
            }

            if (errorMessageComponents.Count != 0)
            {
                createdMessage = String.Join(" and ", errorMessageComponents) + $" value{(errorMessageComponents.Count == 2 ? "s" : "")} must be set";
                ShowNotification?.Invoke(this, createdMessage);
                return;
            }

            await _model.SendRequest(Url, OldTeamName, NewTeamName);
        }

        private void NotifyAboutResultOfRequest(object? sender, bool success)
        {
            ShowNotification?.Invoke(this, success ? "Request was successful" : "Request was NOT successful");
            if (success)
            {
                OldTeamName = string.Empty;
                NewTeamName = string.Empty;
            }
        }

        private void SetOldTeamNameToSuggestion(object? sender, int teamNumber)
        {
            OldTeamName = $"Team{teamNumber + 1}";
        }
    }
}
