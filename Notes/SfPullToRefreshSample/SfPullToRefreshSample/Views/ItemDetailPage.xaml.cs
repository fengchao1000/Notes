using SfPullToRefreshSample.ViewModels;
using System.ComponentModel;
using Xamarin.Forms;

namespace SfPullToRefreshSample.Views
{
    public partial class ItemDetailPage : ContentPage
    {
        public ItemDetailPage()
        {
            InitializeComponent();
            BindingContext = new ItemDetailViewModel();
        }
    }
}