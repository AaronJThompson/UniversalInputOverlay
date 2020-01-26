using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Input_Overlay.Hooking
{
    public class KeyEventArgs : EventArgs
    {
        public KeyEventArgs(KeyData data)
        {
            Data = data;
        }

        public KeyData Data { get; }
    }
}
