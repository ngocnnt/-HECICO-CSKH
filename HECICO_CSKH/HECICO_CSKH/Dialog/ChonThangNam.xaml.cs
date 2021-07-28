using HECICO_CSKH.Global;
using Rg.Plugins.Popup.Extensions;
using Rg.Plugins.Popup.Pages;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace HECICO_CSKH.Dialog
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ChonThangNam : PopupPage, INotifyPropertyChanged
    {
        TaskCompletionSource<DateTime> _tsk = null;
        private ObservableCollection<String> _nam;
        public ObservableCollection<String> Nam
        {
            get { return _nam; }
            set { _nam = value; OnPropertyChanged("NAM"); }
        }
        
        public ChonThangNam(DateTime date) 
        {
            InitializeComponent();
            List<string> arr = new List<string> { "1", "2","3","4","5","6","7","8","9","10","11","12" };
            comboBox.DataSource = arr;
            for (int i =0; i < arr.Count() ;i++)
            {
                if (arr[i] == date.Month.ToString())
                {
                    comboBox.SelectedIndex = i;
                }    
            }    
            Nam = new ObservableCollection<string>();
           for (int i =0;i<5; i++)
            {
                Nam.Add((DateTime.Now.Year - i).ToString());
            }
            cbNam.DataSource = Nam;
            for (int i = 0; i < Nam.Count(); i++)
            {
                if (Nam  [i] == date.Year.ToString())
                {
                    cbNam.SelectedIndex = i;
                }
            }
          
           
        }
        private async void btnOK_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopPopupAsync(true);
            _tsk.SetResult(new DateTime(Convert.ToInt32(cbNam.SelectedValue),Convert.ToInt32( comboBox.SelectedValue),1 ) );
        }
        public async Task<DateTime> Show()
        {
            _tsk = new TaskCompletionSource<DateTime>();
            await Navigation.PushPopupAsync(this);
            return await _tsk.Task;
        }
    }
}