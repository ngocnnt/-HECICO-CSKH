using System;
using System.Collections.Generic;
using System.Text;

namespace HECICO_CSKH.Models.Search
{
  public  class TraCuuThongTinThanhToanModel
    {
        public string NAM { get; set; }
        public string THANG { get; set; }
        public Decimal?  TONG_TIEN { get; set; }
        public decimal?  TONG_NO { get; set; }

        public string TTHAI_NO { get; set; }
        public string CTIET_NOP { get; set; } 
    }
}
