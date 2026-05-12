using Microsoft.Extensions.Logging;
using ApiDeepSeekl.InterfaceService_;
using ApiDeepSeekl.Service;
using ApiDeepSeekl.ViewModel;
using ApiDeepSeekl.InterfisRepotisiory;
using ApiDeepSeekl.Repotisiory;
namespace ApiDeepSeekl
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
            builder.Services.AddSingleton<ICatService, CatService>();
            builder.Services.AddSingleton<IUserRepotisiory, UserRepotisiory>();
            builder.Services.AddSingleton<IFactRepotisiory, FactRepotisiory>();
            builder.Services.AddSingleton<IUserService, UserService>();
            builder.Services.AddTransient<MainPage>();
            builder.Services.AddTransient<MainPageViewModel>();

#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
