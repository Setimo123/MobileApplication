using CommunityToolkit.Mvvm.ComponentModel;
using Consultation.Domain;
using Consultation.Domain.Enum;
using Consultation.Repository.Repository.IRepository;
using Consultation.Service;
using Consultation.Service.IService;
using Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UM_Consultation_App_MAUI.Helper;

namespace UM_Consultation_App_MAUI.ViewModels
{
    public partial class HomeViewModel : ObservableObject
    {
        private readonly IAuthRepository _userServices;
        private readonly IConsultationRequestServices _consultationRequestServices;

        [ObservableProperty]
        private string username;

        [ObservableProperty]
        private string umid;

        [ObservableProperty]
        private string schoolyear;

        [ObservableProperty]
        private string useryearlevel;

        [ObservableProperty]
        private string pendingconsultation;



        public HomeViewModel(IAuthRepository userServices, IConsultationRequestServices consultationRequestServices)
        {
            _userServices = userServices;
            _consultationRequestServices = consultationRequestServices;
            DisplayStudentUserInformation();
        }
        private async void DisplayStudentUserInformation()
        {
            Student studentInfo = await _userServices.GetStudentInformation(new LoginViewModel().userUMIDNumber);
            List<ConsultationRequest> consultationStatus = await _consultationRequestServices.GetStudentPendingList(studentInfo.StudentUMID, Status.Pending);
            if (studentInfo == null)
            {
                Username = "No name";
                return;
            }
       
            UserInformation(studentInfo);

            Pendingconsultation = consultationStatus.Where(c => c.Status == Status.Pending).Count().ToString();
  
        }

        private void UserInformation(Student studentInfo)
        {
            List<string> fullname = MvvmHelper.stringSplitter(' ', studentInfo.StudentName);

            //Use the LINQ for accessing the data in a list<string> 

            string firstNames = string.Empty;
            string lastName = fullname[fullname.Count-1];    

            for (int i = 0; i < fullname.Count - 1; i++)
            {
                firstNames += fullname[i] + " ";
            }

            Username = $"{lastName},{firstNames.TrimEnd()}";    

            Umid = studentInfo.StudentUMID;
            Schoolyear = $"{MvvmHelper.GetDisplayName(studentInfo.SchoolYear.Semester)} " +
                $"{studentInfo.SchoolYear.Year1}-{studentInfo.SchoolYear.Year2}";

            Useryearlevel = $"BS {studentInfo.Program.Description} {studentInfo.yearLevel.ToString()}";

        }
    }
}
