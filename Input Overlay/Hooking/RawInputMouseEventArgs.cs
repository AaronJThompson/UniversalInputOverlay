using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Input_Overlay.Hooking
{
    class RawInputMouseEventArgs : EventArgs
    {
        public RawInputMouseEventArgs(MouseData data)
        {
            Data = data;
        }

        public MouseData Data { get; }
    }
}
