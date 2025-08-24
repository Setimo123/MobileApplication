namespace UM_Consultation_App_MAUI.Views;


public partial class CreateAccountPage : ContentPage
{
    // and kani
    bool isPasswordHidden = true;
    bool isConfirmPasswordHidden = true;
    public CreateAccountPage()
    {
        InitializeComponent();
    }
    // everything below kay Arobo
    // backend team feel free to modify
    private async void BackToPreviousPage(object? sender, EventArgs e)
    {
        bool confirmExit = await DisplayAlert("Warning!", "You haven’t completed the form. Exit anyway?", "Yes", "No");

        if (confirmExit)
        {
            txtboxEmail.Text = string.Empty;
            txtboxPassword.Text = string.Empty;
            txtboxConfirmPassword.Text = string.Empty;
            btnPasswordEye.Source = "eyeclosed.png";
            txtboxPassword.IsPassword = true;
            btnConfirmPasswordEye.Source = "eyeclosed.png";
            txtboxConfirmPassword.IsPassword = true;
            lblPasswordMatch.IsVisible = false;
            iconStatus.IsVisible = false;
            lblPasswordMatch.IsVisible = false;
            btnSignUp.BackgroundColor = Colors.LightPink;
            btnSignUp.TextColor = Colors.White;
            btnSignUp.IsEnabled = false;
            //Pa-add sa show Login Page code...
        }
    }
    private async void SignUpButton(object? sender, EventArgs e)
    {
        if (string.IsNullOrWhiteSpace(txtboxEmail.Text) || string.IsNullOrWhiteSpace(txtboxPassword.Text) || string.IsNullOrWhiteSpace(txtboxConfirmPassword.Text))
        {
            await DisplayAlert("Error", "Please fill in all fields.", "OK");
            return;
        }
        else
        {
            await DisplayAlert("Sign Up", "Account Created Successfully.", "OK");
            //Pa-add sa code for login page...
        }
    }

    private void TogglePasswordVisibility(object sender, EventArgs e)
    {
        isPasswordHidden = !isPasswordHidden;
        txtboxPassword.IsPassword = isPasswordHidden;
        btnPasswordEye.Source = isPasswordHidden ? "eyeclosed.png" : "eyeopen.png";
    }

    private void ToggleConfirmPasswordVisibility(object sender, EventArgs e)
    {
        isConfirmPasswordHidden = !isConfirmPasswordHidden;
        txtboxConfirmPassword.IsPassword = isConfirmPasswordHidden;
        btnConfirmPasswordEye.Source = isConfirmPasswordHidden ? "eyeclosed.png" : "eyeopen.png";
    }

    private void OnPasswordTextChanged(object sender, TextChangedEventArgs e)
    {
        string password = txtboxPassword.Text;
        string confirmPassword = txtboxConfirmPassword.Text;
        lblPasswordMatch.IsVisible = false;
        iconStatus.IsVisible = false;

        if (!string.IsNullOrEmpty(password) || !string.IsNullOrEmpty(confirmPassword))
        {
            lblPasswordMatch.IsVisible = true;
            iconStatus.IsVisible = true;

            if (password == confirmPassword)
            {
                lblPasswordMatch.Text = "Passwords match!";
                lblPasswordMatch.TextColor = Colors.Green;
                iconStatus.Source = "checkicon.png";
                btnSignUp.BackgroundColor = Colors.Maroon;
                btnSignUp.TextColor = Colors.White;
                btnSignUp.IsEnabled = true;
            }
            else
            {
                lblPasswordMatch.Text = "Passwords do not match";
                lblPasswordMatch.TextColor = Colors.OrangeRed;
                iconStatus.Source = "dissaproveicon.png";
                btnSignUp.BackgroundColor = Colors.LightPink;
                btnSignUp.TextColor = Colors.White;
                btnSignUp.IsEnabled = false;
            }
        }
        else
        {
            lblPasswordMatch.IsVisible = false;
        }
    }
}
