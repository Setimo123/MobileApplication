using UM_Consultation_App_MAUI.ViewModels;
using UM_Consultation_App_MAUI.Views.FacultyView;
using UM_Consultation_App_MAUI.Views.StudentView;

namespace UM_Consultation_App_MAUI.Views.Common;

public partial class MenuPage : ContentPage
{
	public MenuPage()
	{
		InitializeComponent();
    }
    // Migration from Benny
    // feel free to modify this backend team
    // dli ni final na pag backend kay para rani ma meet ang desired output sa UI
    private void OnAccountTapped(object sender, EventArgs e)
    {
        bool isExpanded = !AccountDetails.IsVisible;
        AccountDetails.IsVisible = isExpanded;


        ArrowIcon.RotateTo(isExpanded ? 180 : 0, 200, Easing.CubicInOut);
    }

    private async void OnLogoutClicked(object sender, EventArgs e)
    {

        //UserSession.Clear();

        // Navigate to login (assuming ang route is naka fixed or registered)
        // mag crash pani siya if i remove ang comment
        // await Shell.Current.GoToAsync("//LoginPage");
    }

    private async void OnChangePasswordClicked(object sender, EventArgs e)
    {
        /*
        string newPassword = string.Empty;
        string confirmPassword = string.Empty;

        string result = await DisplayPromptAsync("Change Password", "Enter new password:", "OK", "Cancel", "New password", maxLength: 20, keyboard: Keyboard.Text);
        if (!string.IsNullOrWhiteSpace(result))
        {
            newPassword = result;
            confirmPassword = await DisplayPromptAsync("Confirm Password", "Re-enter new password:", "OK", "Cancel", "Confirm password", maxLength: 20, keyboard: Keyboard.Text);

            if (newPassword != confirmPassword)
            {
                await DisplayAlert("Error", "Passwords do not match.", "OK");
                return;
            }

            //Update password
            if (BindingContext is MenuViewModel vm)
            {
                vm.Password = newPassword;
                await DisplayAlert("Success", "Password updated.", "OK");
            }
        }
        */
    }
    // till dri
}