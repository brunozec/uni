using Microsoft.Extensions.Logging;
using BFZ.AtividadeExtensionistaII.IoC;
using Syncfusion.Blazor;
using Syncfusion.Licensing;
using Syncfusion.Maui.Core.Hosting;

namespace BFZ.AtividadeExtensionistaII;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        SyncfusionLicenseProvider.RegisterLicense("MTc5OTQ2MUAzMjMxMmUzMTJlMzQzMUlaS3RpVldPZ3BXWTdOVnpyM0tkNk5yRDRrcjRXK1NCWlRoUjk1NlJZK1E9;MTc5OTQ2MkAzMjMxMmUzMTJlMzQzMVE4QjRRTkVMSzNGM3Z6ZXdLeEg3R0ZlQkRJa3NyQld4Q0xqSjk2MTF2eE09");

        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .ConfigureSyncfusionCore()
            .ConfigureFonts(fonts => { fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular"); });

        builder.Services.AddMauiBlazorWebView();

        builder.Services.AddSyncfusionBlazor();

    #if DEBUG
        builder.Services.AddBlazorWebViewDeveloperTools();
        builder.Logging.AddDebug();
    #endif

        builder.Services.AddDepedencyInjection();

        return builder.Build();
    }
}