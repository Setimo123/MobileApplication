using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Consultation.Domain;
using Consultation.Service;
using Consultation.Service.IService;
using Consultation.Services.Service;
using Consultation.Services.Service.IService;
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

        [ObservableProperty]
        private bool isPasswordHidden = true;

        [ObservableProperty]
        private string passwordhashed = "eyeclosed.png";


        [RelayCommand]
        private void TogglePasswordVisibility()
        {
            IsPasswordHidden = !IsPasswordHidden;
            UpdateIcon();
        }

        private void UpdateIcon()
        {
            passwordhashed = IsPasswordHidden ? "eyeclosed.png" : "eyeopen.png";
        }

        public LoginViewModel(IAuthService authService,IStudentServices studetnServices,
            IFacultyServices facultyServices)
        {
            _facultyServices = facultyServices;
            _authService = authService;
            _studentServices = studetnServices;
        }


        //Command for the log-in button
        [RelayCommand]
        private async Task ClickLogIn()
        {
            Users studentUsers = await _authService.Login(Email, Password, "Student");
            Users facultyUsers = await _authService.Login(Email, Password, "Faculty");

            if (studentUsers != null)
            {
                await StudentInformation(studentUsers.UMID);
                await Shell.Current.GoToAsync($"///Student");
                AccountVerification = true;
                return;
            }
            else if (facultyUsers != null)
            {
                await FacultyInformation(facultyUsers.UMID);
                await Shell.Current.GoToAsync("///FacultyHomePage");
                AccountVerification = false;
                return;
            }
            else
            {
                App.Current.MainPage.DisplayAlert("Message", $"Invalid Credential", "OK");
                return;
            }
        }

        //Command to route the create account
        [RelayCommand]
        private async Task CreateAccountClick()
        {
            //Add role and phone number into the UI
           //_authService.CreateAccount(Email,Password,);
            await Shell.Current.GoToAsync("CreateAccountPage");
        }



        private async Task StudentInformation(string userUMIDNumber)
        {
            Student = await _studentServices.GetStudentInformation(userUMIDNumber);
        }

        private async Task FacultyInformation(string userUMIDNumber)
        {
            Faculty = await _facultyServices.GetFacultyInformation(userUMIDNumber);
        }

        //Read only types or the Interface caller
        private readonly IAuthService _authService;

        private readonly IStudentServices _studentServices;

        private readonly IFacultyServices _facultyServices;

        //Statics function so all pages has the flexibility to access data that only needed
        public static Student Student { get; set; } = new Student();
        public static Faculty Faculty { get; set; } = new Faculty();
        public static bool AccountVerification { get; set; }
        public string userUMIDNumber { get; set; }
    }
}
