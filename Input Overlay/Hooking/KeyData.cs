using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Linearstar.Windows.RawInput;

namespace Input_Overlay.Hooking
{
    public class KeyData
    {
        public int keyValue;
        public bool isDown;

        public KeyData() { }

        public KeyData(int key, bool down)
        {
            keyValue = key;
            isDown = down;
        }
    }
}
