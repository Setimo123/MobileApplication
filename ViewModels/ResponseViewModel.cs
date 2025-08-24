using System.Collections.ObjectModel;

namespace UM_Consultation_App_MAUI.ViewModels
{
    public class Response
    {
        public int Id { get; set; }
        public string CourseCode { get; set; }
        public string Name { get; set; }
        public string Date { get; set; }
        public string Time { get; set; }
        public string Status { get; set; }
        public Color StatusColor { get; set; }

    }
    public class ResponseViewModel
    {
        public ObservableCollection<Response> Responses { get; set; }

        public ResponseViewModel()
        {
            Responses = new ObservableCollection<Response>
        {
            new Response { Id = 7599, CourseCode = "CPE 223", Name = "Jay Al Gallenero", Date = "Mon, April 14, 2025", Time = "4:30 PM - 5:30 PM", Status = "Pending", StatusColor = Colors.Yellow },
            new Response { Id = 7600, CourseCode = "CPE 224", Name = "Maria Cruz", Date = "Tue, April 15, 2025", Time = "2:00 PM - 3:00 PM", Status = "Approved", StatusColor = Colors.Green },
            new Response { Id = 7601, CourseCode = "CPE 225", Name = "Juan Dela Cruz", Date = "Wed, April 16, 2025", Time = "1:00 PM - 2:00 PM", Status = "Disapproved", StatusColor = Colors.Red },
            new Response { Id = 7602, CourseCode = "CPE 226", Name = "Ana Santos", Date = "Thu, April 17, 2025", Time = "10:00 AM - 11:00 AM", Status = "Done", StatusColor = Colors.Gray }
        };

        }

    }
}
