using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Consultation.Domain;
using Consultation.Service;
using Consultation.Service.IService;
using System.Collections.ObjectModel;
using System.Windows.Input;
using UM_Consultation_App_MAUI.Helper;
using UM_Consultation_App_MAUI.Views.StudentView;

namespace UM_Consultation_App_MAUI.ViewModels
{

    public partial class RequestViewModel : ObservableObject
    {
        [ObservableProperty]
        private string semestername;

        [ObservableProperty]
        private ObservableCollection<string> enrolledsemseter = new ObservableCollection<string>();

        [ObservableProperty]
        private string? selectedsemester;


        private readonly IAuthService _authService;

        private string? _pickerChoiceName;
         
        public RequestViewModel(IAuthService authService)
        {
            _authService = authService;
            _ = StudentInfoAsync();
        }

        private async Task StudentInfoAsync()
        {
            Student studentInfo = await _authService.GetStudentInformation(new LoginViewModel().userUMIDNumber);

            _pickerChoiceName = $"{studentInfo.SchoolYear.Year1} - {studentInfo.SchoolYear.Year2}"
                + $" {MvvmHelper.GetDisplayName(studentInfo.SchoolYear.Semester)}";

            enrolledsemseter.Clear();
            enrolledsemseter.Add(_pickerChoiceName);
        }

        [RelayCommand]
        private async Task ConfirmRequest()
        {
            if (Selectedsemester == _pickerChoiceName)
            {
                await Shell.Current.GoToAsync("///RequestConsultationPage");
                return;
            }
            App.Current.MainPage.DisplayAlert("Message", "Select course please", "Ok");

        }


        //logic for handling the request consultation command MvvmHelper.GetDisplayName(item.Semester)
        private async void OnRequestConsultation()
        {
            await Shell.Current.GoToAsync("///RequestConsultationPage");
        }
    }
}
