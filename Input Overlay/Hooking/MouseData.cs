using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Linearstar.Windows.RawInput;

namespace Input_Overlay.Hooking
{
    class MouseData
    {
        public int DeltaX;
        public int DeltaY;

        public MouseData() { }

        public MouseData(RawInputMouseData data)
        {
            DeltaX = data.Mouse.LastX;
            DeltaY = data.Mouse.LastY;
        } 
    }
}
