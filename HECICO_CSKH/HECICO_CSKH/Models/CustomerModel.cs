using HECICO_CSKH.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace HECICO_CSKH.Models
{
   public class CustomerModel
    {
        public string NAM { get; set; }
        string _makh;
       
        public string TEN_KHANG { get; set; }
        public string DCHI_KHANG { get; set; }        
        public string DTHOAI_KHANG { get; set; }
        public decimal TIEN_DIEN { get; set; }
        public decimal TIEN_THUE { get; set; }
        public decimal TONG_TIEN { get; set; }
        string _danop;
        public string DA_NOP
        {
            get => _danop; set
            {
                _danop = value;
                if (_danop.ToLower() == "true") _danop = "Đã thanh toán";
                else _danop = "Chưa thanh toán";
            }
        }
        public decimal  CHISO_MOI { get; set; }
        public decimal DIEN_TTHU { get; set; }
        public string _ma_cto;
        public string  MA_CTO { get => _ma_cto; set  { _ma_cto = value; _ma_cto = "Số công tơ :" + MA_CTO; } }
        public string KY_THANH_TOAN  { get; set; }
        int _thang;
        public int THANG
        {
            get { return _thang; }
            set
            {
                _thang = value;
                KY_THANH_TOAN = "Tháng " + _thang + ", " + NAM.ToString();
            }
        }
        public string MA_KHANG
        {
            get => _makh;
            set
            {
                _makh = value;
                _makh = "Mã KH: " + _makh;
            }
        }
    }
}
