using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SCTV
{
    class TimerInfo_old
    {
        public DateTime StartTime;
        public TimeSpan Duration;
        public string MethodToCall = "";
        public string UrlToGoTo = "";
        public System.Windows.Forms.TextBox TxtDisplay = null;
        public ExtendedWebBrowser Browser = null;
    }
}
