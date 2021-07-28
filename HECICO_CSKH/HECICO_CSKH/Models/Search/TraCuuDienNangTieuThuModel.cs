using System;
using System.Collections.Generic;
using System.Text;

namespace HECICO_CSKH.Models.Search
{
  public   class TraCuuDienNangTieuThuModel
    {
        public string MA_KHANG { get; set; }
        public string TEN_KHANG { get; set; }
        public string NAM { get; set; }
        public string THANG { get; set; }
        public string MA_CTO { get; set; }
        public decimal? CHISO_CU { get; set; }
        public decimal? HSNHAN { get; set; }
        public decimal? CHISO_MOI { get; set; }
        public decimal? SLUONG_THAO { get; set; }
        public decimal? DIEN_TTHU { get; set; }
        public DateTime? NGAY_DKY { get; set; }
        public DateTime? NGAY_CKY { get; set; } 
    }
}
