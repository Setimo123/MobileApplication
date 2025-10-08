using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Consultation.Domain;
using Consultation.Infrastructure.Data;
using Consultation.Service;
using Consultation.Service.IService;
using Consultation.Services.Service.IService;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using UM_Consultation_App_MAUI.MvvmHelper.Interface;

namespace UM_Consultation_App_MAUI.ViewModels
{
    //Gi add ra nako kay na libog ko kung asa butang ang viewmodel para sa RequestConsultationPage.
    // balhin ra nako ni kung makabalo nako asa ang ViewModel sa RequestConsultationPage.
    public partial class RequestConsultationViewModel : ObservableObject
    {
        [ObservableProperty]
        private string studentname;

        [ObservableProperty]
        private string studentumid;

        [ObservableProperty]
        private string coursecode;

        [ObservableProperty]
        private string courseinstructor;

        [ObservableProperty]
        private string starttime;

        [ObservableProperty]
        private string endtime;

        [ObservableProperty]
        private string selectedcourses;

        public ObservableCollection<StudentEnrolledCourses> AvailableCourses { get; } = new ObservableCollection<StudentEnrolledCourses>();
        private readonly ILoadingServices _loadingScreen;
        public RequestConsultationViewModel(IStudentServices studetnServices,
            IConsultationRequestServices consultationRequestServices, ILoadingServices loadingservices)
        {
            _consultationRequestServices = consultationRequestServices;
            _studentServices = studetnServices;
            _loadingScreen = loadingservices;
            DisplayUserInformationOption();
            DispalyUserInformation();
        }

        private void DispalyUserInformation()
        {
            List<string> fullname = MvvmHelper.Helper.StringSplitter(' ', StudentInfo.StudentName);

            //Use the LINQ for accessing the data in a list<string> 

            string firstNames = string.Empty;
            string lastName = fullname[fullname.Count - 1];

            //Reusable pwede siya ma separate method
            for (int i = 0; i < fullname.Count - 1; i++)
            {
                firstNames += fullname[i] + " ";
            }

            //Display name
            Studentname = $"{lastName}, {firstNames}";
            Studentumid = StudentInfo.StudentUMID;
        }

        [RelayCommand]
        private async void DisplayUserInformationOption()
        {
            //Student list 
            var StudentCourses = await
                _studentServices.GetStudentEnrolledCourses
                (StudentInfo.StudentID, RequestViewModel.Semester);

            //Display data into the Combobox
            foreach (var x in StudentCourses)
            {
                AvailableCourses.Add(new StudentEnrolledCourses(x.CourseName));
            }
        }

        public ICommand CourseCodeCommand => new Command<object>(ec =>
        {
             if (ec is EnrolledCourse courses)
                {
                    Courseinstructor = courses.Faculty?.FacultyName;
                    Coursecode = courses.CourseCode;
                  
                }
        }
       );

        [RelayCommand]
        public async Task FileConsultationClick()
        {
            try
            {
                _loadingScreen.Show();
                await Task.Delay(1000);
                //if (string.IsNullOrWhiteSpace(studentname)
                //    || string.IsNullOrWhiteSpace(courseinstructor)
                //    || string.IsNullOrWhiteSpace(starttime)
                //    || string.IsNullOrWhiteSpace(endtime)
                //    || string.IsNullOrWhiteSpace(selectedcourses))
                //{
                //    await _consultationRequestServices.AddConsultation(
                //        StudentInfo.StudentID,
                //        Coursecode,
                //        DateTime.Now,
                //        TimeSpan.Parse(Starttime),
                //        TimeSpan.Parse(Endtime),
                //        Consultation.Domain.Enum.Status.Upcoming,
                //        string.Empty);
                //    MvvmHelper.Helper.DisplayMessage("Successfully Added Consultation Request");
                //    AvailableCourses.Clear();
                //}
                //else
                //{
                //    MvvmHelper.Helper.DisplayMessage("Please fill in all the fields");
                //}

            }
            finally
            {
                _loadingScreen.Hide();
            }


            MvvmHelper.Helper.DisplayMessage("This feature is under development");  

        }

        [RelayCommand]
        public async Task BackNavigation()
        {
            AvailableCourses.Clear();
            await Shell.Current.GoToAsync("///Student");
        }

        private readonly Student StudentInfo = LoginViewModel.Student;
        private readonly IStudentServices _studentServices;
        private readonly IConsultationRequestServices _consultationRequestServices;
    }

    public partial class StudentEnrolledCourses : ObservableObject
    {

        [ObservableProperty]
        private string courseName;
     
        public StudentEnrolledCourses(string coursename)
        {
            CourseName = coursename;
           
        }
    }
}
