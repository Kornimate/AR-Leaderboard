using AR_LeaderBoard_Rename_Mobile.Interfaces;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace AR_LeaderBoard_Rename_Mobile.ViewModels
{
    public class MainViewModel : ObservableObject
    {
        private readonly IMainModel _model;

        public event EventHandler<string>? ShowNotification;

        private string _oldTeamName;
        public string OldTeamName
        {
            get => _oldTeamName;
            set
            {
                if(value !=  _oldTeamName)
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
        public MainViewModel(IMainModel model)
        {
            _model = model;

            _model.RequestSent += NotifyAboutResultOfRequest;

            SendRequestCommand = new RelayCommand(SendRequest);

            Url = "https://matekorni-001-site1.jtempurl.com/api/rename";
        }

        private void NotifyAboutResultOfRequest(object? sender, bool success)
        {
            ShowNotification?.Invoke(this, success ? "Request was successful", "Request was NOT successful");
        }

        private void SendRequest()
        {
            _model.SendRequest(Url, OldTeamName, NewTeamName);
        }
    }
}
