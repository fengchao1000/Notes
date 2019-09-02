using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Notes.ViewModels
{ 
    public class TestPageViewModel : BaseViewModel
    {
        private Command _changeLangCommand;

        public Command ChangeLanguageCommand => _changeLangCommand ?? (_changeLangCommand = new Command(async () => await ChangeLanguage()));

#warning Do not do this
        public Page CurrentPage { get; set; }


        private bool ShowCN = false;

        private async Task ChangeLanguage()
        { 
            if (ShowCN)
            { 
                LoadLocale("zh-cn");
                ShowCN = false;
            }
            else{ 
                ShowCN = true; 
                LoadLocale("en-us");
            } 

            
        }
    }
}
