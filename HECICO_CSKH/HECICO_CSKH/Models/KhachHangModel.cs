using System;
using System.Collections.Generic;
using System.Text;

namespace HECICO_CSKH.Models
{
 public    class KhachHangModel
    {
        public string MaKhang { get; set; }
        public string Sdt { get; set; } 
        public string Token { get; set; }
        public string HoaDonId { get; set; } 
        public string  NoticeType { get; set; }
        public string TuNgay { get; set; }
        public string DenNgay { get; set; }
        public string ThangBatDau { get; set; }
        public string ThangKetThuc { get; set; }

        public int Thang { get; set; }
        public int Nam { get; set; }
    }
}
