using System;
using System.Collections.Generic;
using System.Text;

namespace HECICO_CSKH.Models
{
  public  class CustomerByTel
    {
      
        public string MA_KHANG { get; set; }
        
        string _tenKhachhang;
        public string TEN_KHANG { get => _tenKhachhang; set {
                _tenKhachhang = value;
                _tenKhachhang =_tenKhachhang;
            } }
        public string DCHI_KHANG { get; set; } 
    }
}
