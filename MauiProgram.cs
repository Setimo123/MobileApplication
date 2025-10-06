using Microsoft.Extensions.Logging;
using CommunityToolkit.Maui;
using UM_Consultation_App_MAUI.ViewModels;
using UM_Consultation_App_MAUI.Views.FacultyView;
using Consultation.Domain;
using Consultation.Repository.Repository.IRepository;
using Consultation.Repository;
using Consultation.Service.IService;
using Consultation.Service;
using UM_Consultation_App_MAUI.Views.StudentView;
using Microsoft.AspNetCore.Identity;
using Consultation.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using UM_Consultation_App_MAUI.Views.Common;
using Consultation.Services.Service.IService;
using Consultation.Services.Service;
using Consultation.Repository.Repository;
using UM_Consultation_App_MAUI.Views;
using Shiny;
using Microsoft.Maui.Hosting;
using Microsoft.Maui.Controls.Hosting;
using Shiny.Infrastructure;


namespace UM_Consultation_App_MAUI
{
     
    public static class MauiProgram
    {

        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder.UseMauiApp<App>()
                .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            }).UseMauiCommunityToolkit();

            builder.Services.AddDbContext<AppDbContext>(opt =>
            {
                var cs = "Server=consultationdb.mysql.database.azure.com;" +
                         "Port=3306;" +
                         "Database=consultationdatabase;" +
                         "User Id=ConsultationDb;" +
                         "Password=MyServerAdmin123!;" +
                         "SslMode=Required;";
                opt.UseMySql(cs, new MySqlServerVersion(new Version(8, 0, 36)), my =>
                my.EnableRetryOnFailure(
                 maxRetryCount: 5,
                    maxRetryDelay: TimeSpan.FromSeconds(10),
                 errorNumbersToAdd: null));
            });



            //For services and repository
            builder.Services.AddTransient<IAuthRepository, UserRepository>();
            builder.Services.AddTransient<IStudentRepository, StudentRepository>();
            builder.Services.AddTransient<IAuthService, AuthService>();
            builder.Services.AddTransient<IConsultationRequestServices, ConsultationRequestServices>();
            builder.Services.AddTransient<IStudentServices, StudentServices>();
            builder.Services.AddTransient<Consultation.Repository.Repository.IRepository.IFacultyRepository, FacultyRepository>();
            builder.Services.AddTransient<Consultation.Services.Service.IService.IFacultyServices, FacultyServices>();

            // Password Hasher
            builder.Services.AddSingleton<IPasswordHasher<Users>, PasswordHasher<Users>>();
          

            // ViewModels
            builder.Services.AddTransient<LoginViewModel>();
            builder.Services.AddTransient<HomeViewModel>();
            builder.Services.AddTransient<RequestViewModel>();
            builder.Services.AddTransient<RequestConsultationViewModel>();
            builder.Services.AddTransient<MenuViewModel>();
            builder.Services.AddTransient<ResponseViewModel>();
            builder.Services.AddTransient<FacultyCLPViewModel>();
            builder.Services.AddTransient<FacultyRequestViewModel>();
           

            //Common Pages
            builder.Services.AddTransient<LoginPage>();
            builder.Services.AddTransient<MenuPage>();
            builder.Services.AddTransient<CreateAccountPage>();


            //Student Pages
            builder.Services.AddTransient<HomePage>();
            builder.Services.AddTransient<RequestPage>();
            builder.Services.AddTransient<RequestConsultationPage>();
            builder.Services.AddTransient<ResponsePage>();

            //Faculty Pages
            builder.Services.AddTransient<RequestListPage>();
            builder.Services.AddTransient<ConsultationListPage>();

#if DEBUG
            builder.Logging.AddDebug();
#endif
            return builder.Build();
        }
    }
}