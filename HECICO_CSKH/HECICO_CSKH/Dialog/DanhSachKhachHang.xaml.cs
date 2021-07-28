using HECICO_CSKH.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace HECICO_CSKH.Dialog
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DanhSachKhachHang : ContentPage
    {
       
        ObservableCollection<CustomerByTel> ListData;       
        public DanhSachKhachHang(ObservableCollection<CustomerByTel> _list)
        {
            InitializeComponent();
            ListData = _list;           
            lstView.ItemsSource = ListData;
        }

        private void ListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            try
            {
                CustomerByTel item = lstView.SelectedItem as CustomerByTel;
                if (item != null)
                {
                    MessagingCenter.Send<DanhSachKhachHang, CustomerByTel>(this, "chonkhachhang", item);
                    Navigation.PopModalAsync();

                }
            }
            catch (Exception)
            {


            }
        }
    }
}
