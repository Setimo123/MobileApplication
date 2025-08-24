using Consultation.Domain.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace UM_Consultation_App_MAUI.Helper
{
    public class MvvmHelper
    {
        public static string GetDisplayName(Semester value)
        {
            return value.GetType()
                        .GetMember(value.ToString())
                        .First()
                        .GetCustomAttribute<DisplayAttribute>()?
                        .GetName() ?? value.ToString();
        }

        //Call this if you want to use the App.Current to display something to avoid redunduncy 
        public static void DisplayMessage(string messageTitle, string message, string conditions)
        {
            App.Current.MainPage.DisplayAlert(messageTitle, message, conditions);
        }


        public static List<string> stringSplitter(char splitter,string word)
        {
            string[] split = word.Split(splitter);    
            List<string> wordSplit = new List<string>();
            foreach (var i in split)
            {
                wordSplit.Add(i);
            }

            return wordSplit;
        }
    }
}
