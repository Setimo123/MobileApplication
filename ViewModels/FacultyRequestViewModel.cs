using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Consultation.Domain;
using Consultation.Repository.Repository.IRepository;
using Microsoft.VisualStudio.Telemetry;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace UM_Consultation_App_MAUI.ViewModels
{
    //ViewModel for all pages in FacultyView

    public partial class FacultyRequestViewModel : ObservableObject
    {
        public ObservableCollection<RequestList> PendingRequests { get; set; } 
            = new ObservableCollection<RequestList>();
        public ObservableCollection<RequestList> ConsultationsList { get; set; } 
            = new ObservableCollection<RequestList>();


        private readonly IFacultyRepository _faculty;
        public FacultyRequestViewModel(IFacultyRepository faculty)
        {
            _faculty = faculty;
            DisplayConsultataion();
        }


        private async void DisplayConsultataion()
        {
            Faculty faculty = LoginViewModel.Faculty;
            var facultyConsultation = await _faculty.FacultyConsultation(faculty.FacultyID);

            var sortStatus = facultyConsultation.Where(fc =>
             fc.Status == Consultation.Domain.Enum.Status.Pending).ToList();

            if (faculty == null) return;
            
            if (sortStatus.Count == 0)
            {
                MvvmHelper.Helper.DisplayMessage("No Consultation Request");
                return;
            }

            foreach (var i in sortStatus)
            {
                PendingRequests.Add(new RequestList(
                    i.ConsultationID,
                    i.SubjectCode,
                    i.Student.StudentName,
                    i.DateSchedule.ToString("MM/dd/yyyy"),
                    $"{i.StartedTime} - {i.EndedTime}"
                    ));
            }
        }

        [RelayCommand]
        private async Task ApproveRequest(Consultations selectedConsultation)
        {
            //Change the status of the request to approved
            int id = int.Parse(selectedConsultation.Id); 
            
            bool option = await MvvmHelper.Helper.DisplayOption(
                $"Are you sure you want to approve this request?",
                "Yes",
                "No");
            if (option == true)
            {
                await _faculty.ChangeConsultationByID(id,Consultation.Domain.Enum.Status.Approved,"");
                PendingRequests.RemoveAt(id);
                return;
            }
            if (option == false)
            {
                return;
            }
        }

        [RelayCommand]
        private async Task DisApproveRequest(Consultations selectedConsultation)
        {
            //Change the status of the request to disapproved

            int id = int.Parse(selectedConsultation.Id);

            bool option = await MvvmHelper.Helper.DisplayOption(
                $"Are you sure you want to approve this request?",
                "Yes",
                "No");
            if (option == true)
            {
                string reason = await App.Current.MainPage.DisplayPromptAsync(
                    "Reason",
                    "Please state the reason why you disapprove this request",
                    "Ok",
                    "Cancel",
                    "Type reason here");
                await _faculty.ChangeConsultationByID(id, Consultation.Domain.Enum.Status.Approved, reason);
                PendingRequests.RemoveAt(id);
                return;
            }
            if (option == false)
            {
                return;
            }
        }
    }


    public partial class RequestList : ObservableObject
    {
        public int Id { get; set; }
        public string CourseCode { get; set; }
        public string StudentName { get; set; }
        public string Date { get; set; }
        public string Time { get; set; }

        public RequestList(int id, string courseCode, string studentName,string date, string time)
        {
            Id = id;
            CourseCode = courseCode;
            StudentName = studentName;
            Date = date;
            Time = time;

        }
    }
}
