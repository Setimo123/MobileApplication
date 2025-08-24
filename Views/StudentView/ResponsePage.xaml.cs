using UM_Consultation_App_MAUI.ViewModels;

namespace UM_Consultation_App_MAUI.Views.StudentView;

public partial class ResponsePage : ContentPage
{
	public ResponsePage()
	{
		InitializeComponent();
		BindingContext = new ResponseViewModel();
	}
}