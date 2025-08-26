using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UM_Consultation_App_MAUI.ViewModels
{
    public partial class MenuViewModel : ObservableObject
    {
        [ObservableProperty]
        private string username;


        [ObservableProperty]
        private string useremail;

        public MenuViewModel()
        {
            DisplayAccount();

        }
        //If the true = student, if false = faculty
        private async Task DisplayAccount()
        {
            if (LoginViewModel.AccountVerification == true)
            {
                username = LoginViewModel.studentUsers.UserName;
                useremail = LoginViewModel.studentUsers.Email;
                return;
            }

            if (LoginViewModel.AccountVerification == false)
            {
                username = LoginViewModel.facultyUsers.UserName;
                useremail = LoginViewModel.facultyUsers.Email;
                return;
            }
        }
    }
}
