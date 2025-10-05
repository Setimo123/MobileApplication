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
    public class RequestList
    {
        public int Id { get; set; }
        public string CourseCode { get; set; }
        public string StudentName { get; set; }
        public string Date { get; set; }
        public string Time { get; set; }
        public string Status { get; set; }
        public Color StatusColor { get; set; }
    }
    public class FacultyRequestViewModel
    {
        public ObservableCollection<RequestList> PendingRequests { get; set; }
        public ObservableCollection<RequestList> ConsultationsList { get; set; }

        public ICommand ApproveCommand { get; }
        public ICommand DisapproveCommand { get; }

        public FacultyRequestViewModel()
        {
            PendingRequests = new ObservableCollection<RequestList>
            {
                new RequestList { Id = 7599, CourseCode = "CPE 223", StudentName = "Mike Oxmaul", Date = "Mon, April 14", Time = "4:30 PM - 5:30 PM", Status = "Approved", StatusColor = Colors.Green },
                new RequestList { Id = 7600, CourseCode = "CPE 224", StudentName = "Maria Cruz", Date = "Tue, April 15", Time = "2:00 PM - 3:00 PM", Status = "Disapproved", StatusColor = Colors.Red }
            };

            ConsultationsList = new ObservableCollection<RequestList>();

            ApproveCommand = new Command<RequestList>(ApproveRequest);
            DisapproveCommand = new Command<RequestList>(DisapproveRequest);
        }

        private void ApproveRequest(RequestList request)
        {
            if (request != null && PendingRequests.Contains(request))
            {
                PendingRequests.Remove(request);
                request.Status = "Approved";
                request.StatusColor = Colors.Green;
                ConsultationsList.Add(request);
            }
        }

        private void DisapproveRequest(RequestList request)
        {
            if (request != null && PendingRequests.Contains(request))
            {
                PendingRequests.Remove(request);
                request.Status = "Disapproved";
                request.StatusColor = Colors.Red;
                ConsultationsList.Add(request);
            }
        }
    }
}
