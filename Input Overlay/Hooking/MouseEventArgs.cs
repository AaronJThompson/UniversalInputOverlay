using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Input_Overlay.Hooking
{
    public class MouseEventArgs : EventArgs
    {
        public MouseEventArgs(MouseData data)
        {
            Data = data;
        }

        public MouseData Data { get; }
    }
}
