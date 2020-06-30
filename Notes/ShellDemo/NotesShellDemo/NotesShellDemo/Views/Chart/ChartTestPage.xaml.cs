using NotesShellDemo.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace NotesShellDemo.Views.Chart
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ChartTestPage : ContentPage
    {
        private VisitCountryViewModel viewModel;

        public ChartTestPage()
        {
            InitializeComponent();

            BindingContext = viewModel = new VisitCountryViewModel();
        }
    }


}