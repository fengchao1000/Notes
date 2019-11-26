using Notes.Models.Categorys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Notes.Controls
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CategoryContentView : ContentView
    {  
        public CategoryContentView(Category category)
        {
            InitializeComponent();

            this.lblCategoryName.Text = category.Name;
            this.lblUsageCount.Text = category.UsageCount.ToString(); 
        }



    }
}