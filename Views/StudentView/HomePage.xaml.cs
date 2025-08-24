using UM_Consultation_App_MAUI.ViewModels;

namespace UM_Consultation_App_MAUI.Views.StudentView;

public partial class HomePage : ContentPage
{
	public HomePage(HomeViewModel shpvm)
	{
		InitializeComponent();
        BindingContext = shpvm;
    }
}