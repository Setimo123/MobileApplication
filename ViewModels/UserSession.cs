namespace UM_Consultation_App_MAUI.ViewModels
{
    //kaning usersession para ni sa menupage.xaml
    //menupage.xaml is utilized on both student and faculty
    //since used by both, kani mag decide if unsa na bottomnav iya gamiton
    //goto menupage.xaml.cs para sa logic
    public class UserSession
    {
        public static bool IsFaculty { get; set; } = false;
    }
}
