using CommunityToolkit.Maui.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using Consultation.Domain;
using Consultation.Service;
using Consultation.Service.IService;
using Consultation.Services.Service.IService;
using UM_Consultation_App_MAUI.ViewModels;


namespace UM_Consultation_App_MAUI.Views.Common;

public partial class ChangePassword : Popup
{
    private readonly IAuthService _authService;
    public ChangePassword(IAuthService authService)
	{
        _authService = authService;
        InitializeComponent();
	}
	
	private void OnCancelClicked(object sender, EventArgs e)
	{
        Close();
    }

 

    private async void OnUpdateClicked(object sender, EventArgs e)
	{
        if (
            string.IsNullOrWhiteSpace(NewPasswordEntry.Text) ||
            string.IsNullOrWhiteSpace(ConfirmPasswordEntry.Text))
        {
            await Application.Current.MainPage.DisplayAlert("Error", "Please fill in all fields.", "OK");
            return;
        }
        if (ConfirmPasswordEntry.Text != NewPasswordEntry.Text)
        {
            MvvmHelper.Helper.DisplayMessage("Password does not match");
            return;
        }

        Student student = LoginViewModel.Student;
        Faculty faculty = LoginViewModel.Faculty;
        if (student == null || faculty == null)
        {
            MvvmHelper.Helper.DisplayMessage($"Hello world");
            return;
        }
         

        if (LoginViewModel.AccountVerification == true)
        {
            MvvmHelper.Helper.DisplayMessage($"{student.Email} and {NewPasswordEntry.Text}");
            await _authService.ChangePassword(NewPasswordEntry.Text, student.Email);
            return;
        }
        if (LoginViewModel.AccountVerification == false)
        {
            MvvmHelper.Helper.DisplayMessage($"{faculty.FacultyEmail}");
            await _authService.ChangePassword(NewPasswordEntry.Text, faculty.FacultyEmail);
            return;
        }
        Close(true);
    }
}  