using UM_Consultation_App_MAUI.ViewModels;

namespace UM_Consultation_App_MAUI.Views.FacultyView;

public partial class RequestListPage : ContentPage
{
	public RequestListPage(FacultyRequestViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;
    }
}