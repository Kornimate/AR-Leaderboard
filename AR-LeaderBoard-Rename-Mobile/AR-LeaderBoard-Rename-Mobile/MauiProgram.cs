using AR_LeaderBoard_Rename_Mobile.Interfaces;
using AR_LeaderBoard_Rename_Mobile.Models;
using AR_LeaderBoard_Rename_Mobile.ViewModels;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Reflection;

namespace AR_LeaderBoard_Rename_Mobile
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();

            using var stream = Assembly
                                .GetExecutingAssembly()
                                .GetManifestResourceStream("AR_LeaderBoard_Rename_Mobile.appsettings.json");

            var resources = Assembly.GetExecutingAssembly().GetManifestResourceNames();

            var config = new ConfigurationBuilder()
                                .AddJsonStream(stream!)
                                .Build();

            builder.Configuration.AddConfiguration(config);

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

            builder.Services.AddTransient<MainViewModel>();
            builder.Services.AddTransient<IMainModel, MainModel>();
            builder.Services.AddTransient<MainPage>();

            return builder.Build();
        }
    }
}
