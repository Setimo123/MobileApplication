using UM_Consultation_App_MAUI.ViewModels;

namespace UM_Consultation_App_MAUI.Views.FacultyView;

public partial class ConsultationListPage : ContentPage
{
	public ConsultationListPage(FacultyCLPViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;
    }
}