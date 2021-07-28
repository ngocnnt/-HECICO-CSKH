using System;
using System.Collections.Generic;
using System.Text;

namespace HECICO_CSKH.Interface
{
    public interface IProcessLoader
    {
        void Hide();
        void Show(string title = "Loading");
    }
}
