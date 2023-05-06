using BFZ.AtividadeExtensionistaII.IoC;
using BlazorBindings.Maui;
using Syncfusion.Licensing;

namespace BFZ.AtividadeExtensionistaII.App;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        SyncfusionLicenseProvider.RegisterLicense("MTc5OTQ2MUAzMjMxMmUzMTJlMzQzMUlaS3RpVldPZ3BXWTdOVnpyM0tkNk5yRDRrcjRXK1NCWlRoUjk1NlJZK1E9;MTc5OTQ2MkAzMjMxMmUzMTJlMzQzMVE4QjRRTkVMSzNGM3Z6ZXdLeEg3R0ZlQkRJa3NyQld4Q0xqSjk2MTF2eE09");

        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<BlazorBindingsApplication<AppShell>>()
            .UseMauiBlazorBindings()
            .ConfigureFonts(fonts => { fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular"); });

        builder.Services.AddDepedencyInjection();
        
        return builder.Build();
    }
}