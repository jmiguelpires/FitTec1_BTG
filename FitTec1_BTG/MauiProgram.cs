using CommunityToolkit.Maui;
using CommunityToolkit.Maui.Services;
using FitTec1_BTG.Services.Abstractions;
using FitTec1_BTG.Services.Implementations;
using FitTec1_BTG.View;
using FitTec1_BTG.ViewModel;
using Microsoft.Extensions.Logging;

namespace FitTec1_BTG
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .UseMauiCommunityToolkit()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

            //Registros de páginas e viewmodels
            builder.Services.AddSingleton<INavigationService, NavigationService>();
            builder.Services.AddSingleton<IPopupService, PopupService>();
            builder.Services.AddSingleton<IClienteRepository>(sp =>
            {
                var dbPath = Path.Combine(FileSystem.AppDataDirectory, "FitTec1BTG.db3");
                return new ClienteRepository(dbPath);
            });

            //MainPage
            builder.Services.AddTransient<MainPage>();
            builder.Services.AddTransient<MainPageViewModel>();

            //CadastroCliente
            builder.Services.AddTransient<CadastroCliente>();
            builder.Services.AddTransient<CadastroClienteViewModel>();

            //EditaCliente
            builder.Services.AddTransient<EditaCliente>();
            builder.Services.AddTransient<EditaClienteViewModel>();

#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
