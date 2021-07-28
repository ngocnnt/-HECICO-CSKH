using HECICO_CSKH.Models.Search;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace HECICO_CSKH.Views.Search
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class BieuDoDienNangTieuThu : ContentPage
    {
        public BieuDoDienNangTieuThu(ObservableCollection<TraCuuDienNangTieuThuModel> _list)
        {
            InitializeComponent();
            chart.ItemsSource = _list ;
        }
    }
}