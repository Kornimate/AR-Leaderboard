using AR_LeaderBoard_Rename_Mobile.Interfaces;
using AR_LeaderBoard_Rename_Mobile.Models;
using AR_LeaderBoard_Rename_Mobile.ViewModels;
using Microsoft.Extensions.Logging;

namespace AR_LeaderBoard_Rename_Mobile
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

#if DEBUG
    		builder.Logging.AddDebug();
#endif

            builder.Services.AddSingleton<MainViewModel>();
            
            builder.Services.AddSingleton<IMainModel, MainModel>();

            return builder.Build();
        }
    }
}
