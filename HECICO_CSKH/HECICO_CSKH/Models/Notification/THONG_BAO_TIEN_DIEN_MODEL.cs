using System;
using System.Collections.Generic;
using System.Text;

namespace HECICO_CSKH.Models.Notification
{
 public  class THONG_BAO_TIEN_DIEN_MODEL
    {
        public string HOADON_ID { get; set; }
        public string MA_KHANG { get; set; }
        public string TEN_KHANG { get; set; }
        public string TIEU_DE { get; set; }
        public string GHI_CHU { get; set; } 
        public string NOI_DUNG { get; set; }
        public string KIHIEU_HDON { get; set; } 
        public string DCHI_KHANG { get; set; }
        public string LOAI_THONG_BAO { get; set; }
        public string   THANG { get; set; }
        public string  NAM { get; set; }
        public string TIEN_BANG_CHU { get; set; }
        public DateTime? NGAY_TBAO { get; set; }
        public DateTime? NGAY_NOP { get; set; } 
        int _trangthaixem;
        public int TRANG_THAI_XEM { get=> _trangthaixem ; set { _trangthaixem = value;
                if (_trangthaixem == 0)
                    DA_XEM = "chưa xem";
                else
                    DA_XEM = "đã xem";
            } }
     
        public string DA_XEM { get; set; }

        public DateTime? NGAY_DKY { get; set; }
        public DateTime? NGAY_CKY { get; set; }
        public Decimal ? TONG_TIEN { get; set; }
        public Decimal? TIEN_THUE { get; set; }
        public Decimal? TIEN_DIEN { get; set; }
        public Decimal? DIEN_TTHU { get; set; }
    }
}
