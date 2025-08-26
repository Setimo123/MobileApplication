using UM_Consultation_App_MAUI.Views;
using UM_Consultation_App_MAUI.Views.Common;
using UM_Consultation_App_MAUI.Views.FacultyView;
using UM_Consultation_App_MAUI.Views.StudentView;


namespace UM_Consultation_App_MAUI
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            //naka set ni to AppShell, kay nag base ang bottom navigation sa AppShell.xaml
            //change lng ninyo ang structure ani para sa loginpage (if dli mag function ang navigation) kay dili na nko to hilabtan ang login
            MainPage = new AppShell();

            //startup in order for the create account button to work (change the structure if needed)
            //MainPage = new AppShell(); //change lng ni if need ninyo i debug or polish ang uban pages (pasagdi lng ug naa error sa initialize component)
            //Shell.Current.GoToAsync("//Student"); // Set the initial page to FacultyView, change this to StudentView if needed (debug purposes para sa navigation bar)
        }
    }
}
