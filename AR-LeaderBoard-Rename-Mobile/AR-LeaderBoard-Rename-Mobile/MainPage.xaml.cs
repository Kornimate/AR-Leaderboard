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

            _vm.ShowNotification += ShowAppNotification;

            BindingContext = _vm;
        }

        private void ShowAppNotification(object? sender, string e)
        {
            //TODO
        }
    }

}
