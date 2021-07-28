using System;
using System.Collections.Generic;
using System.Text;

namespace HECICO_CSKH.Interface
{
    public interface IMessage
    {
        void LongAlert(string message);
        void ShortAlert(string message);
    }
}
