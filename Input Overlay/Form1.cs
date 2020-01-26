using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using XInput.Wrapper;
using Linearstar.Windows.RawInput;


namespace Input_Overlay
{
    using Hooking;
    public partial class Form1 : Form
    {
        private InputHook inputHook;
        Controller controller = null;
        private bool controllerMode = true;
        private Rectangle background;
        //private Mouse mouse;
        private static int lastTick;
        private static int lastFrameRate;
        private static int frameRate;

        private Font font = new Font("Time New Roman", 10f);

        public Form1()
        {
            inputHook = new InputHook(Handle);
            this.Load += Form1_Load;
            InitializeComponent();
            this.FormClosing += Form1_FormClosing;
            this.ClientSize = new Size(780, 520);

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Subscribe();
            this.Paint += Form1_Paint;
            background = new Rectangle(0,0,this.Size.Width, this.Size.Height);
        }
        public static int CalculateFrameRate()
        {
            if (System.Environment.TickCount - lastTick >= 1000)
            {
                lastFrameRate = frameRate;
                frameRate = 0;
                lastTick = System.Environment.TickCount;
            }
            frameRate++;
            return lastFrameRate;
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            float X = this.Size.Width;
            float Y = this.Size.Height;
            e.Graphics.FillRectangle(Brushes.Magenta, background);
            if (controllerMode)
            {
                controller.Paint(X, Y, e);
            } else
            {
                //kb.Paint(e);
                //mouse.Paint(e);
            }
            e.Graphics.DrawString(CalculateFrameRate().ToString(), font, Brushes.Black, new Point(0, 0));
        }

        public void Subscribe()
        {
            // Note: for the application hook, use the Hook.AppEvents() instead
            controller = new Controller(1);
            X.Gamepad_2.KeyDown += ChooseController;
            X.Gamepad_3.KeyDown += ChooseController;
            X.Gamepad_4.KeyDown += ChooseController;
            X.StartPolling(X.Gamepad_2);
            X.StartPolling(X.Gamepad_3);
            X.StartPolling(X.Gamepad_4);
            controller.gamepad.KeyDown += Gamepad_KeyDown;
            inputHook.onInput += this.onInput;
            //kb.m_GlobalHook.KeyDown += M_GlobalHook_KeyDown;
            //mouse = new Mouse(new Point(800, 400));
        }
        private void ChooseController(object sender, EventArgs e)
        {
            controller.gamepad.KeyDown -= Gamepad_KeyDown;
            controller.Stop();
            controller = null;
            X.StopPolling();
            X.Gamepad_2.KeyDown -= ChooseController;
            X.Gamepad_3.KeyDown -= ChooseController;
            X.Gamepad_4.KeyDown -= ChooseController;
            controller = new Controller((X.Gamepad)sender);
            controller.gamepad.KeyDown += Gamepad_KeyDown;

        }
        private void M_GlobalHook_KeyDown(object sender, KeyEventArgs e)
        {
            controllerMode = false;
            this.ClientSize = new Size(1100, 520);
            background = new Rectangle(0, 0, this.Size.Width, this.Size.Height);
        }

        protected override void WndProc(ref Message m)
        {
            inputHook?.OnWndProc(ref m);

            base.WndProc(ref m);
        }


        private void onInput(object sender, Hooking.RawInputEventArgs e)
        {
            var data = e.Data;

        }

        private void Gamepad_KeyDown(object sender, EventArgs e)
        {
            controllerMode = true;
            this.ClientSize = new Size(780, 520);
            background = new Rectangle(0, 0, this.Size.Width, this.Size.Height);
        }

        public void Unsubscribe()
        {
            controller?.Stop();
            //kb?.Stop();
            controller = null;
            //kb = null;
            //mouse?.Stop();
            inputHook.UnHook();
        }

        private void Form1_FormClosing(Object sender, EventArgs e)
        {
            Unsubscribe();
            Application.Exit();
            Application.ExitThread();
            Environment.Exit(0);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            this.Refresh();
        }
    }
}
