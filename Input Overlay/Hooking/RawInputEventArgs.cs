using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Linearstar.Windows.RawInput;

namespace Input_Overlay.Hooking
{
    class RawInputEventArgs : EventArgs
    {
        public RawInputEventArgs(RawInputData data)
        {
            Data = data;
        }

        public RawInputData Data { get; }
    }
}
