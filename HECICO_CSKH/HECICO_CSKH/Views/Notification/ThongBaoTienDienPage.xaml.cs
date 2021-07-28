using HECICO_CSKH.ViewModels.Notitication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace HECICO_CSKH.Views.Notification
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ThongBaoTienDienPage : ContentPage
    {
        ThongBaoTienDienViewModel viewModel;
        public ThongBaoTienDienPage()
        {
            InitializeComponent();
            viewModel = new ThongBaoTienDienViewModel();
            viewModel.Navigation = Navigation;
            BindingContext = viewModel;

        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            if (viewModel.ListTraCuu.Count == 0)
            {
                viewModel.LoadCommand.Execute(null);
            }
        }
    }
}