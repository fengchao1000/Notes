using Notes.Models.Articles;
using Notes.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Notes.Views.Article
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ArticlesDetailsPage : ContentPage
    {
        ArticlesDetailViewModel viewModel;

        public ArticlesDetailsPage(ArticlesDto message)
        {
            InitializeComponent();

            BindingContext = viewModel = new ArticlesDetailViewModel(message);
             
        }
        protected override async void OnAppearing()
        {
            base.OnAppearing();

            Navigation.InsertPageBefore(new ArticlesMenuPage(), this);
        }
        
          
    }
}