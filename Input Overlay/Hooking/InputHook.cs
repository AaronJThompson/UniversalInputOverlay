using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using System.Diagnostics;
using Linearstar.Windows.RawInput;
using Linearstar.Windows.RawInput.Native;
using System.Windows.Forms;

namespace Input_Overlay.Hooking
{
    public class InputHook
    {
        private RawInputDevice[] devices;
        public event EventHandler<RawInputEventArgs> onInput;
        public event EventHandler<RawInputEventArgs> onMouse;
        public event EventHandler<MouseEventArgs> onMouseMove;
        public event EventHandler<RawInputEventArgs> onKeyboard;
        public event EventHandler<KeyEventArgs> onKeyDown;
        public event EventHandler<KeyEventArgs> onKeyUp;
        public InputHook(IntPtr handle)
        {
            devices = RawInputDevice.GetDevices();
            RawInputDevice.RegisterDevice(HidUsageAndPage.Keyboard, RawInputDeviceFlags.InputSink | RawInputDeviceFlags.NoLegacy, handle);
            RawInputDevice.RegisterDevice(HidUsageAndPage.Mouse, RawInputDeviceFlags.InputSink | RawInputDeviceFlags.NoLegacy, handle);
            onInput += _onInput;
            onMouse += _onMouse;
            onKeyboard += _onKeyboard;
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
            
            switch(data.Mouse.Flags)
            {
                case RawMouseFlags.MoveRelative:
                    var moveData = new MouseData(data);
                    onMouseMove?.Invoke(this, new MouseEventArgs(moveData));
                    break;
                default:
                    break;
            }
        }

        private void _onKeyboard(object sender, RawInputEventArgs e)
        {
            RawInputKeyboardData data = (RawInputKeyboardData)e.Data;
            KeyData keyData = null;
            switch (data.Keyboard.Flags)
            {
                case RawKeyboardFlags.Down:
                    keyData = new KeyData(data.Keyboard.VirutalKey, true);
                    onKeyDown?.Invoke(this, new KeyEventArgs(keyData));
                    break;
                case RawKeyboardFlags.Up:
                    keyData = new KeyData(data.Keyboard.VirutalKey, false);
                    onKeyUp?.Invoke(this, new KeyEventArgs(keyData));
                    break;
            }
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
