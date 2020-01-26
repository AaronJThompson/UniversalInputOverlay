using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using System.Diagnostics;
using Linearstar.Windows.RawInput;
using System.Windows.Forms;

namespace Input_Overlay.Hooking
{
    public class InputHook
    {
        private RawInputDevice[] devices;
        public event EventHandler<RawInputEventArgs> onInput;
        public event EventHandler<RawInputEventArgs> onMouse;
        public event EventHandler<RawInputEventArgs> onKeyboard;
        public InputHook(IntPtr handle)
        {
            devices = RawInputDevice.GetDevices();
            RawInputDevice.RegisterDevice(HidUsageAndPage.Keyboard, RawInputDeviceFlags.InputSink | RawInputDeviceFlags.NoLegacy, handle);
            RawInputDevice.RegisterDevice(HidUsageAndPage.Mouse, RawInputDeviceFlags.InputSink | RawInputDeviceFlags.NoLegacy, handle);
            onInput += _onInput;
            onMouse += _onMouse;
        }

        public void UnHook()
        {
            RawInputDevice.UnregisterDevice(HidUsageAndPage.Keyboard);
            RawInputDevice.UnregisterDevice(HidUsageAndPage.Mouse);
        }

        private void _onInput(object sender, RawInputEventArgs e)
        {
            RawInputData data = e.Data;
            switch(data.Header.Type)
            {
                case RawInputDeviceType.Mouse:
                    onMouse?.Invoke(this, e);
                    break;
                case RawInputDeviceType.Keyboard:
                    onKeyboard?.Invoke(this, e);
                    break;
                default:
                    break;
            }
        }

        private void _onMouse(object sender, RawInputEventArgs e)
        {
            RawInputMouseData data = (RawInputMouseData)e.Data;
            Console.WriteLine("dX: {0} dY: {1}", data.Mouse.LastX, data.Mouse.LastY);
            //switch()
        }

        public void OnWndProc(ref Message m)
        {
            const int WM_INPUT = 0x00FF;

            if (m.Msg == WM_INPUT)
            {
                var data = RawInputData.FromHandle(m.LParam);

                onInput?.Invoke(this, new RawInputEventArgs(data));
            }
        }
    }
}
