using System;
using System.Collections.Generic;
using HECICO_CSKH.ViewModels;
using HECICO_CSKH.Views;
using Xamarin.Forms;
using HECICO_CSKH.Views.Search;
using HECICO_CSKH.Views.Notification;
namespace HECICO_CSKH
{
    public partial class AppShell : Xamarin.Forms.Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(ItemDetailPage), typeof(ItemDetailPage));
            Routing.RegisterRoute(nameof(NewItemPage), typeof(NewItemPage));
            Routing.RegisterRoute(nameof(TraCuuLichGhiChiSoPage), typeof(TraCuuLichGhiChiSoPage));
            Routing.RegisterRoute(nameof(ThongBaoKhacPage), typeof(ThongBaoKhacPage));
            Routing.RegisterRoute(nameof(LoginPage), typeof(LoginPage));
        }

    }
}
