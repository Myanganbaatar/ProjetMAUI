using Microsoft.Extensions.Logging;
using BarsboldApp.Services;   
using BarsboldApp.ViewModels;
using BarsboldApp.Views;

namespace BarsboldApp;

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

        builder.Services.AddSingleton<ApiService>();
        builder.Services.AddTransient<MainViewModel>();
        builder.Services.AddTransient<PaysViewModel>();
        builder.Services.AddTransient<AccueilPage>();
        builder.Services.AddTransient<ViewModels.ParametresViewModel>();
        builder.Services.AddTransient<Views.ParametresPage>();
        builder.Services.AddTransient<ProfilPage>();

        return builder.Build();
    }
}