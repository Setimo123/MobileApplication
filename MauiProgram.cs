using Microsoft.Extensions.Logging;
using CommunityToolkit.Maui;
using Consultation.Repository.Repository.IRepository;
using Consultation.Repository;
using Consultation.Service.IService;
using Consultation.Service;
using Consultation.Domain;
using Microsoft.AspNetCore.Identity;
using UM_Consultation_App_MAUI.ViewModels;
using Consultation.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using UM_Consultation_App_MAUI.Views.StudentView;

namespace UM_Consultation_App_MAUI
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder.UseMauiApp<App>().ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            }).UseMauiCommunityToolkit();

            builder.Services.AddDbContext<AppDbContext>(opt =>
            {
                opt.UseSqlServer("Server=tcp:consultationserver.database.windows.net,1433;" +
                "Initial Catalog=ConsultationDatabase;" +
                "Persist Security Info=False;" +
                "User ID=ConsultationDB;" +
                "Password=MyServerAdmin123!;" +
                "MultipleActiveResultSets=False;" +
                "Encrypt=True;" +
                "TrustServerCertificate=False;" +
                "Connection Timeout=30;");
            });


            //For services and repository
            builder.Services.AddTransient<IAuthRepository, UserRepository>();
            builder.Services.AddTransient<IAuthService, AuthService>();
            builder.Services.AddTransient<IConsultationRequestServices, ConsultationRequestServices>();

            // Password Hasher
            builder.Services.AddSingleton<IPasswordHasher<Users>, PasswordHasher<Users>>();

            // ViewModels
            builder.Services.AddTransient<LoginViewModel>();
            builder.Services.AddTransient<HomeViewModel>();

            //Common Pages
            builder.Services.AddTransient<LoginPage>();

            //Student Pages
            builder.Services.AddTransient<HomePage>();



#if DEBUG
            builder.Logging.AddDebug();
#endif
            return builder.Build();
        }
    }
}