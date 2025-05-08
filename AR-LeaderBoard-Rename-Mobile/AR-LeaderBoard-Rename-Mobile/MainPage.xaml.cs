using AR_LeaderBoard_Rename_Mobile.ViewModels;

namespace AR_LeaderBoard_Rename_Mobile
{
    public partial class MainPage : ContentPage
    {
        private readonly MainViewModel _vm;

        public MainPage(MainViewModel vm)
        {
            InitializeComponent();

            _vm = vm;

            _vm.ShowNotification += async(sender, message) => await ShowAppNotification(sender, message);

            BindingContext = _vm;
        }

        private async Task ShowAppNotification(object? sender, string message)
        {
            await DisplayAlert("Request", message, "OK");
        }
    }

}
