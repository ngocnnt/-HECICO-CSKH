using System;
using System.Collections.Generic;
using System.Text;

namespace HECICO_CSKH.Models.Search
{
   public class TraCuuHoaDonDienTuModel
    {
        public string MA_KHANG { get; set; }
        public string TEN_KHANG { get; set; }
        public string DCHI_KHANG { get; set; }     
        public string MA_CTO { get; set; }
        public string HOADON_ID { get; set; }
        public string SOGCS { get; set; }
        public string SO_HDON { get; set; }

        public string MATRACUU { get; set; }
        public string LOAI_BCS { get; set; }
        public string BAC_THANG { get; set; }

        public decimal? DIEN_TTHU { get; set; }
        public decimal? TIEN_DIEN { get; set; }
        public decimal? TIEN_THUE { get; set; }
        public decimal? TONG_TIEN { get; set; }
        public decimal? DIEN_TTHU_CTIET { get; set; }
        public decimal? DON_GIA { get; set; }
        public decimal? TIEN_DIEN_CTIET { get; set; }
        public decimal? TIEN_THUE_CTIET { get; set; }      

    }
}
