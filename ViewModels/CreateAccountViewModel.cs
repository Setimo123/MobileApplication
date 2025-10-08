using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Consultation.Service.IService;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UM_Consultation_App_MAUI.ViewModels
{
    public partial class CreateAccountViewModel : ObservableObject
    {
        private readonly IAuthService _authService;
        public CreateAccountViewModel(IAuthService authService)
        {
            _authService = authService;
            AddUserType();
        }
        public ObservableCollection<string> Usertypes { get; } = new ObservableCollection<string>();

        [ObservableProperty]
        private string email;

        [ObservableProperty]
        private string password;

        [ObservableProperty]
        private string confirmpassword;

        [ObservableProperty]
        private string usertype;

        [ObservableProperty]
        private string phonenumber;




        private string selectedUserType;

        public string SelectedUserType
        {
            get => selectedUserType;
            set => SetProperty(ref selectedUserType, value);
        }


        [RelayCommand]
        private async Task CreateAccout()
        {
            //Add condition that textbox is empty
            if (Password != Confirmpassword)
            { 
                MvvmHelper.Helper.DisplayMessage("Password did not match");
                return;
            }

            if (string.IsNullOrWhiteSpace(Email) 
                || string.IsNullOrWhiteSpace(Password) 
                || string.IsNullOrWhiteSpace(Confirmpassword) 
                || string.IsNullOrWhiteSpace(Phonenumber)
                || string.IsNullOrWhiteSpace(SelectedUserType))
            {
                MvvmHelper.Helper.DisplayMessage("Please fill in all fields");
                return;
            }

            try
            {
                string email = Email.Split('.')[2];
                int UMID = int.Parse(email.Split('.')[2].Substring(0, 6));

                if (Email.Count(c => c == '.') != 3)
                {
                    MvvmHelper.Helper.DisplayMessage("Incorrect Email Format");
                    return;
                }

                await _authService.CreateAccount(Email, UMID.ToString(), 
                    Password,
                    UserTypeChecker(), 
                    Phonenumber, 
                    UMID.ToString());
            }
            catch (FormatException)
            {
                MvvmHelper.Helper.DisplayMessage("Incorrect Email Format");
            }
        }


        private Consultation.Domain.Enum.UserType UserTypeChecker()
        {
            if(SelectedUserType == "Student")
            {
                return Consultation.Domain.Enum.UserType.Student;
            }
            if (SelectedUserType == "Faculty")
            {
                return Consultation.Domain.Enum.UserType.Faculty;
            }

            return Consultation.Domain.Enum.UserType.Admin;
        }

        private void AddUserType()
        {
            Usertypes.Add("Student");
            Usertypes.Add("Faculty");
        }

    }
}
