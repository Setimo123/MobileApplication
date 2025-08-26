using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Consultation.Domain;
using Consultation.Service.IService;
using System.Windows.Input;
using UM_Consultation_App_MAUI.Views;

namespace UM_Consultation_App_MAUI.ViewModels
{
    public partial class LoginViewModel : ObservableObject
    {
        
        [ObservableProperty]
        private string email;

        [ObservableProperty]
        private string password;


        //Constructor
        public LoginViewModel(IAuthService authService)
        {
            _authService = authService;
        }

        public LoginViewModel()
        {
            userUMIDNumber = studentUsers.UMID; 
        }

        //Command for the log-in button
        [RelayCommand]
        private async Task ClickLogIn()
        {
            studentUsers = await _authService.Login(Email, Password, "Student");
            facultyUsers = await _authService.Login(Email, Password, "Faculty");

            if (studentUsers != null)
            {
                await Shell.Current.GoToAsync("//Student");
                AccountVerification = true;
                return;
            }
            else if (facultyUsers != null)
            {
                await Shell.Current.GoToAsync("ConsultationListPage");
                AccountVerification = false;
                return;
            }
            else
            {
                App.Current.MainPage.DisplayAlert("Error Message", $"Navigation error: Invalid Credential", "OK");
                return;
            }
        }

        //Read only types or the Interface caller
        private readonly IAuthService _authService;

        //Statics function so all pages has the flexibility to access data that only needed
        public static Users studentUsers { get; set; } = new Users();
        public static Users facultyUsers { get; set; } = new Users();
        
        public static bool AccountVerification { get; set; }
        public string userUMIDNumber { get; set; }
    }
}
