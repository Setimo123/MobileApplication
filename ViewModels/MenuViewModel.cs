using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Consultation.Domain;
using Consultation.Service;
using Consultation.Service.IService;
using Microsoft.Maui.Storage;
using Microsoft.VisualStudio.Telemetry;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UM_Consultation_App_MAUI.ViewModels
{
    public partial class MenuViewModel : ObservableObject
    {

        public MenuViewModel()
        {
            DisplayStudentInformation();
        }

        [ObservableProperty]
        private string studentname;

        [ObservableProperty]
        private string course;


        [ObservableProperty]
        private string email;


       private async void DisplayStudentInformation()
       {
            Student student = LoginViewModel.Student;
            Faculty faculty = LoginViewModel.Faculty;

            if (student == null || faculty == null) return;

            if (LoginViewModel.AccountVerification == true)
            {
                Studentname = student.StudentName;
                Email = student.Email;
                Course = student.Program.Description;
                return;
            }
            if (LoginViewModel.AccountVerification == false)
            {
                Studentname = faculty.FacultyName;
                Email = faculty.FacultyEmail;
                Course = faculty.Program.Description;
                return;
            }
        }

        [RelayCommand]
        public async void LogoutButton()
        {
            //navigate back to the log-in page
            Application.Current.MainPage = new AppShell();

            await Shell.Current.GoToAsync("//LoginPage");

        }
    }
}
