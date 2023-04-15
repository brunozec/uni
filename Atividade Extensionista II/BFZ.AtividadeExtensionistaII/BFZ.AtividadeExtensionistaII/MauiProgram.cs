using Microsoft.Extensions.Logging;
using BFZ.AtividadeExtensionistaII.IoC;
using Syncfusion.Maui.Core.Hosting;

namespace BFZ.AtividadeExtensionistaII;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .ConfigureSyncfusionCore()
            .ConfigureFonts(fonts => { fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular"); });

        builder.Services.AddMauiBlazorWebView();

    #if DEBUG
        builder.Services.AddBlazorWebViewDeveloperTools();
        builder.Logging.AddDebug();
    #endif

        builder.Services.AddDepedencyInjection();

        return builder.Build();
    }
}