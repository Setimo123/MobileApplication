using CommunityToolkit.Mvvm.ComponentModel;
using Consultation.Domain;
using Consultation.Repository.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UM_Consultation_App_MAUI.ViewModels
{
    public partial class FacultyCLPViewModel : ObservableObject
    {
        public ObservableCollection<Consultations> ConsultationsList = new ObservableCollection<Consultations>();
        private readonly IFacultyServices _faculty;

        public FacultyCLPViewModel(IFacultyServices faculty)
        {
            _faculty = faculty;
            DisplayConsultation();

            MvvmHelper.Helper.DisplayMessage("No Consultation List");
        }


        private async void DisplayConsultation()
        {
            Faculty faculty = LoginViewModel.Faculty;
            var facultyConsultation = await _faculty.FacultyConsultation(faculty.FacultyID);
            if (facultyConsultation == null)
            {
                MvvmHelper.Helper.DisplayMessage("No Consultation List");
                return;
            }
            foreach (var x in facultyConsultation)
            {
                ConsultationsList.Add(new Consultations
                    (
                    x.ConsultationID.ToString(),
                    x.SubjectCode,
                    x.DateSchedule.ToString("MM/dd/yyyy"),
                    x.StartedTime.ToString("hh:mm tt") + " - " + x.EndedTime.ToString("hh:mm tt"),
                    x.Status.ToString())
                    );
            }
        }
    }

    public partial class Consultations : ObservableObject
    {
        [ObservableProperty]
        private string id;

        [ObservableProperty]
        private string coursecode;

        [ObservableProperty]
        private string date;

        [ObservableProperty]
        private string time;

        [ObservableProperty]
        private Color statusColor;

        [ObservableProperty]
        private string status;

        public Consultations(string id, string coursecode, string date, string time, string status)
        {
            this.id = id;
            this.coursecode = coursecode;
            this.date = date;
            this.time = time;
            this.status = status;
        
        }
    }


}
