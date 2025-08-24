
using UM_Consultation_App_MAUI.ViewModels;

namespace UM_Consultation_App_MAUI;

public partial class LoginPage : ContentPage
{
    bool isPasswordHidden = true;
    bool isConfirmPasswordHidden = true;
    public LoginPage(LoginViewModel vm)
	{
		InitializeComponent();
         BindingContext = vm;
    }
    //private void TogglePasswordVisibility(object sender, EventArgs e)
    //{
    //    isPasswordHidden = !isPasswordHidden;
    //    txtboxPassword.IsPassword = isPasswordHidden;
    //    btnPasswordEye.Source = isPasswordHidden ? "eyeclosed.png" : "eyeopen.png";
    //}
}