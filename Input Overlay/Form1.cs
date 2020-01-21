﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using Gma.System.MouseKeyHook;
using XInput.Wrapper;

namespace Input_Overlay
{
    public partial class Form1 : Form
    {
        Controller controller = null;
        Keyboard kb = null;
        Mouse mouse = null;
        private bool controllerMode = true;
        private Rectangle background;
        private IKeyboardMouseEvents KeyboardMouseHook;
        private IMouseEvents MouseHook;
        private static int lastTick;
        private static int lastFrameRate;
        private static int frameRate;

        private Font font = new Font("Time New Roman", 10f);

        public Form1()
        {
            this.Load += Form1_Load;
            InitializeComponent();
            this.FormClosing += Form1_FormClosing;
            this.ClientSize = new Size(780, 520);
            KeyboardMouseHook = Hook.GlobalEvents();
            MouseHook = Hook.GlobalEvents();
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
                kb.Paint(e);
                mouse.Paint(e);
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
            kb = new Keyboard(KeyboardMouseHook);
            controller.gamepad.KeyDown += Gamepad_KeyDown;
            kb.m_GlobalHook.KeyDown += M_GlobalHook_KeyDown;
            mouse = new Mouse(new Point(800, 400),MouseHook);
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

        private void Gamepad_KeyDown(object sender, EventArgs e)
        {
            controllerMode = true;
            this.ClientSize = new Size(780, 520);
            background = new Rectangle(0, 0, this.Size.Width, this.Size.Height);
        }

        public void Unsubscribe()
        {
            controller?.Stop();
            kb?.Stop();
            controller = null;
            kb = null;
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