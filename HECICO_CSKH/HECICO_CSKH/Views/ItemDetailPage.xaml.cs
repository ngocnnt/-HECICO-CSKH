using System.ComponentModel;
using Xamarin.Forms;
using HECICO_CSKH.ViewModels;

namespace HECICO_CSKH.Views
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