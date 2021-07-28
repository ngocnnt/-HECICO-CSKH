using System;
using System.Collections.Generic;
using System.Text;

namespace HECICO_CSKH.Models
{
  public  class LoginModel
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string MA_KHANG { get; set; }
        public string LoaiThietBi { get; set; }
        public string TOKEN { get; set; } 
    }
}
