using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;
//namespace Input_Overlay
//{
//    class Keyboard
//    {
//        public IKeyboardMouseEvents m_GlobalHook;
//        private Key[] keys;
//        //privare List<>
//        public Keyboard(IKeyboardMouseEvents km)
//        {

//            m_GlobalHook = km;

//            m_GlobalHook.MouseDownExt += GlobalHookMouseDownExt;
//            m_GlobalHook.KeyDown += GlobalHookKeyDown;
//            m_GlobalHook.KeyUp += GlobalHookKeyUp;
//            keys = new Key[254];
//            // Top Row
//            keys[(int)Keys.D1] = new Key(0.55f, "1", (int)Keys.D1, new Point(100 + 10, 20), Properties.Resources.Key, Properties.Resources.Key_Pressed);
//            keys[(int)Keys.D2] = new Key(0.55f, "2", (int)Keys.D2, new Point(200 + 10, 20), Properties.Resources.Key, Properties.Resources.Key_Pressed);
//            keys[(int)Keys.D3] = new Key(0.55f, "3", (int)Keys.D3, new Point(300 + 10, 20), Properties.Resources.Key, Properties.Resources.Key_Pressed);
//            keys[(int)Keys.D4] = new Key(0.55f, "4", (int)Keys.D4, new Point(400 + 10, 20), Properties.Resources.Key, Properties.Resources.Key_Pressed);
//            keys[(int)Keys.D5] = new Key(0.55f, "5", (int)Keys.D5, new Point(500 + 10, 20), Properties.Resources.Key, Properties.Resources.Key_Pressed);
//            // 2nd Row
//            keys[(int)Keys.Tab] = new Key(0.55f, "Tab", (int)Keys.Tab, new Point(5, 120), Properties.Resources.Tab, Properties.Resources.Tab_Pressed);
//            keys[(int)Keys.Q] = new Key(0.55f, "Q", (int)Keys.Q, new Point(150 + 10, 120), Properties.Resources.Key, Properties.Resources.Key_Pressed);
//            keys[(int)Keys.W] = new Key(0.55f, "W", (int)Keys.W, new Point(250 + 10, 120), Properties.Resources.Key, Properties.Resources.Key_Pressed);
//            keys[(int)Keys.E] = new Key(0.55f, "E", (int)Keys.E, new Point(350 + 10, 120), Properties.Resources.Key, Properties.Resources.Key_Pressed);
//            keys[(int)Keys.R] = new Key(0.55f, "R", (int)Keys.R, new Point(450 + 10, 120), Properties.Resources.Key, Properties.Resources.Key_Pressed);
//            keys[(int)Keys.T] = new Key(0.55f, "T", (int)Keys.T, new Point(550 + 10, 120), Properties.Resources.Key, Properties.Resources.Key_Pressed);
//            // 3rd Row
//            keys[(int)Keys.CapsLock] = new Key(0.55f, "CapsLock", (int)Keys.CapsLock, new Point(5, 220), Properties.Resources.Caps, Properties.Resources.Caps_Pressed);
//            keys[(int)Keys.A] = new Key(0.55f, "A", (int)Keys.A, new Point(200 - 20, 220), Properties.Resources.Key, Properties.Resources.Key_Pressed);
//            keys[(int)Keys.S] = new Key(0.55f, "S", (int)Keys.S, new Point(300 - 20, 220), Properties.Resources.Key, Properties.Resources.Key_Pressed);
//            keys[(int)Keys.D] = new Key(0.55f, "D", (int)Keys.D, new Point(400 - 20, 220), Properties.Resources.Key, Properties.Resources.Key_Pressed);
//            keys[(int)Keys.F] = new Key(0.55f, "F", (int)Keys.F, new Point(500 - 20, 220), Properties.Resources.Key, Properties.Resources.Key_Pressed);
//            keys[(int)Keys.G] = new Key(0.55f, "G", (int)Keys.G, new Point(600 - 20, 220), Properties.Resources.Key, Properties.Resources.Key_Pressed);
//            // 4th row
//            keys[(int)Keys.LShiftKey] = new Key(0.55f, "Shift", (int)Keys.LShiftKey, new Point(5, 320), Properties.Resources.Shift, Properties.Resources.Shift_Pressed);
//            keys[(int)Keys.Z] = new Key(0.55f, "Z", (int)Keys.Z, new Point(250 - 20, 320), Properties.Resources.Key, Properties.Resources.Key_Pressed);
//            keys[(int)Keys.X] = new Key(0.55f, "X", (int)Keys.X, new Point(350 - 20, 320), Properties.Resources.Key, Properties.Resources.Key_Pressed);
//            keys[(int)Keys.C] = new Key(0.55f, "C", (int)Keys.C, new Point(450 - 20, 320), Properties.Resources.Key, Properties.Resources.Key_Pressed);
//            keys[(int)Keys.V] = new Key(0.55f, "V", (int)Keys.V, new Point(550 - 20, 320), Properties.Resources.Key, Properties.Resources.Key_Pressed);
//            //5th Row
//            keys[(int)Keys.LControlKey] = new Key(0.55f, "Ctrl", (int)Keys.LControlKey, new Point(5, 420), Properties.Resources.Tab, Properties.Resources.Tab_Pressed);
//            keys[(int)Keys.LMenu] = new Key(0.55f, "Alt", (int)Keys.LMenu, new Point(200, 420), Properties.Resources.Tab, Properties.Resources.Tab_Pressed);
//            keys[(int)Keys.Space] = new Key(0.65f,0.55f, "Space", (int)Keys.Space, new Point(360, 420), Properties.Resources.Space, Properties.Resources.Space_Pressed);
//        }
//        public void Paint(PaintEventArgs e)
//        {
//            foreach (Key key in keys ?? Enumerable.Empty<Key>())
//            {
//                if (key != null)
//                    key.Paint(e);
//            }
//        }
//        private void PaintKey(Point point,string key, bool pressed, PaintEventArgs e)
//        {

//        }
//        private void GlobalHookKeyDown(object sender, KeyEventArgs e)
//        {
//            Console.WriteLine("KeyDown: \t{0}", e.KeyValue);
//            if (keys[e.KeyValue] != null)
//            {
//                keys[e.KeyValue].Pressed = true;
//            }
//        }
//        private void GlobalHookKeyUp(object sender, KeyEventArgs e)
//        {
//            Console.WriteLine("KeyUp: \t{0}", e.KeyValue);
//            if (keys[e.KeyValue] != null)
//                keys[e.KeyValue].Pressed = false;
//        }

//        private void GlobalHookMouseDownExt(object sender, MouseEventExtArgs e)
//        {
//            Console.WriteLine("MouseDown: \t{0}; \t System Timestamp: \t{1}", e.Button, e.Timestamp);

//            // uncommenting the following line will suppress the middle mouse button click
//            // if (e.Buttons == MouseButtons.Middle) { e.Handled = true; }
//        }
//        public void Stop()
//        {
//            m_GlobalHook.MouseDownExt -= GlobalHookMouseDownExt;
//            m_GlobalHook.KeyDown -= GlobalHookKeyDown;
//            m_GlobalHook.KeyUp -= GlobalHookKeyUp;
//            m_GlobalHook.Dispose();
//        }
//    }
//}
