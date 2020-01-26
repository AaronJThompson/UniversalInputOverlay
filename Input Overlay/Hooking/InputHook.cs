using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using System.Diagnostics;
using Linearstar.Windows.RawInput;

namespace Input_Overlay.Hooking
{
    public class InputHook
    {
        private RawInputDevice[] devices;

        public InputHook()
        {
            devices = RawInputDevice.GetDevices();
        }
    }
}
