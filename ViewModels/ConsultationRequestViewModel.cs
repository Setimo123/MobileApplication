using CommunityToolkit.Mvvm.ComponentModel;
using Consultation.Domain;
using Consultation.Service.IService;
using Microsoft.VisualStudio.RpcContracts.Logging;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UM_Consultation_App_MAUI.Helper;

namespace UM_Consultation_App_MAUI.ViewModels
{
    public partial class ConsultationRequestViewModel : ObservableObject
    {
        [ObservableProperty]
        private string username;

        [ObservableProperty]
        private string userumid;

        [ObservableProperty]
        private string instructorname;

        [ObservableProperty]
        private string coursecode;

        [ObservableProperty]
        private EnrolledCourse? studentenrollecourse;

        [ObservableProperty]
        private ObservableCollection<string> enrolledcourseslist = new ObservableCollection<string>();

        [ObservableProperty]
        private string? selectedsubject;


        private List<EnrolledCourse> mapper = new List<EnrolledCourse>();

        private LoginViewModel login = new LoginViewModel();

        private readonly IAuthService _authService;

        public ConsultationRequestViewModel(IAuthService authService)
        {
            _authService = authService;
            username = LoginViewModel.studentUsers.UserName;
            userumid = LoginViewModel.studentUsers.UMID;
            LoadEnrolledCourses();
           
        }

        //Display the combobox or picker
        private async Task LoadEnrolledCourses()
        {
            //Access the student Enrolled Courses

            //Create a foreach loop 
            var student = await _authService.GetStudentInformation(login.userUMIDNumber);
           
            //Foreach loop for adding into the picker
            foreach (var x in student.SchoolYear.EnrolledCourses)
            {
                mapper.Add(x);
                if (student.StudentID == x.StudentID)
                {
                    enrolledcourseslist.Add(x.CourseName);   
                }
            }
        }

        partial void OnSelectedsubjectChanged(string value)
        {
            foreach (var i in enrolledcourseslist)
            {
                foreach (var j in mapper)
                {
                    if (value == j.CourseName)
                    {
                        Instructorname = j.Faculty.FacultyName;
                        Coursecode = j.CourseCode;  
                    }
                }
            }         
        }
    }
}
