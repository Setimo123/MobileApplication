using CommunityToolkit.Mvvm.ComponentModel;
using Consultation.Domain;
using Consultation.Domain.Enum;
using Consultation.Repository.Repository.IRepository;
using Consultation.Service;
using Consultation.Service.IService;
using Consultation.Services.Service.IService;
using Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UM_Consultation_App_MAUI.MvvmHelper;


namespace UM_Consultation_App_MAUI.ViewModels
{
    public partial class HomeViewModel : ObservableObject
    {

        [ObservableProperty]
        private string studentname;

        [ObservableProperty]
        private string umid;

        [ObservableProperty]
        private string schoolyear;

        [ObservableProperty]
        private string program;

        [ObservableProperty]
        private string useryearlevel;

        [ObservableProperty]
        private string pendingconsultation;


        public HomeViewModel()
        {
            DisplayStudentUserInformation();
        }

        private async void DisplayStudentUserInformation()
        {
            try
            {
                Student StudentInfo = LoginViewModel.Student;
                if (StudentInfo == null)
                {
                    Studentname = "No name";
                    return;
                }

                UserInformation(StudentInfo);
                Pendingconsultation = StudentInfo.
                                      ConsultationRequests.
                                      Where(c => c.Status  == Status.Pending).Count().ToString();

            }
            catch (Exception ex)
            {
                Helper.DisplayMessage($"{ex.Message}");
            }
        }


        private void UserInformation(Student studentInfo)
        {
            List<string> fullname = Helper.StringSplitter(' ', studentInfo.StudentName);

            //Use the LINQ for accessing the data in a list<string> 

            string firstNames = string.Empty;
            string lastName = fullname[fullname.Count - 1];

            for (int i = 0; i < fullname.Count - 1; i++)
            {
                firstNames += fullname[i] + " ";
            }

            Studentname = $"{lastName},{firstNames.TrimEnd()}";

            Umid = studentInfo.StudentUMID;
            Schoolyear = $"{Helper.GetSemesterName(studentInfo.SchoolYear.Semester)} " +
                $"{studentInfo.SchoolYear.Year1}-{studentInfo.SchoolYear.Year2}";

            Useryearlevel = $"{Helper.GetYearLevelName(studentInfo.yearLevel)} Bachelors of ";
            Program = $"{studentInfo.Program.Description}";
        }
    }
}
