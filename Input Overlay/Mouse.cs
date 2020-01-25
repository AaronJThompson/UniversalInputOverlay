using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gma.System.MouseKeyHook;
using System.Drawing;
using System.Windows.Forms;
using System.Diagnostics;
using System.Collections.Concurrent;
using Input = System.Windows.Input;
namespace Input_Overlay
{
    using Hooking;
    using System.Runtime.InteropServices;
    using static Hooking.InputHook;
    class Mouse
    {
        private IMouseEvents m_GlobalHook;
        private FixedSizedQueue<MouseFrame> mouseBuffer;
        private Stopwatch timer;
        private MouseFrame lastMouseFrame;
        private MouseSpeed currentSpeed;
        private Point pos;
        private static HookProc mouseDelegate;
        private IntPtr _hookID;
        public Mouse(Point pos)
        {
            mouseBuffer = new FixedSizedQueue<MouseFrame>(10);
            timer = Stopwatch.StartNew();
            this.pos = pos;
            mouseDelegate = ProcessMouse;
            _hookID = HookMouse(mouseDelegate);
        }

        public void Stop()
        {
            UnHookMouse();
        }

        private IntPtr ProcessMouse(
            int nCode, IntPtr wParam, IntPtr lParam)
        {
            if (nCode >= 0 &&
                MouseMessages.WM_MOUSEMOVE == (MouseMessages)wParam)
            {
                MSLLHOOKSTRUCT hookStruct = (MSLLHOOKSTRUCT)Marshal.PtrToStructure(lParam, typeof(MSLLHOOKSTRUCT));
                AddSpeedFrame(hookStruct.pt.x, hookStruct.pt.y);
            }
            return CallNextHookEx(_hookID, nCode, wParam, lParam);
        }
        private void AddSpeedFrame(int x, int y)
        {
            float speedX = 0;
            float speedY = 0;
            MouseFrame currentFrame = new MouseFrame(x, y, timer.ElapsedMilliseconds);
            if (lastMouseFrame == null)
            {
                lastMouseFrame = currentFrame;
                return;
            }
            long dT = currentFrame.TimeStamp - lastMouseFrame.TimeStamp;
            if (dT > 0) {
                speedX = (currentFrame.X - lastMouseFrame.X) / dT;
                speedY = (currentFrame.Y - lastMouseFrame.Y) / dT;
                Console.WriteLine("dX: \t{0} dY: \t{1}", speedX, speedY);
            }
            currentSpeed = new MouseSpeed(speedX, speedY);
            lastMouseFrame = currentFrame;
        }
        private void OutputFlick()
        {
            MouseFrame firstFrame = mouseBuffer.First<MouseFrame>();
            MouseFrame lastFrame = mouseBuffer.Last<MouseFrame>();
            if (firstFrame == null || lastFrame == null || firstFrame == lastFrame)
                return;
            int dX = lastFrame.X - firstFrame.X;
            int dY = lastFrame.Y - firstFrame.Y;
            long dT = lastFrame.TimeStamp - firstFrame.TimeStamp;
            float totalMovement;
            if (dT > 0)
            {
                totalMovement = (Math.Abs(dX) + Math.Abs(dY)) / dT;
            } else
            {
                totalMovement = 5;
            }
            Console.WriteLine("Movement: \t{0}", totalMovement);
        }
        public void Paint(PaintEventArgs e)
        {
            e.Graphics.DrawLine(Pens.White, this.pos, new PointF(pos.X + (currentSpeed.SpeedX * 10), pos.Y + (currentSpeed.SpeedY * 10)));
        }
    }

    class MouseFrame
    {
        public int X;
        public int Y;
        public long TimeStamp;
        public MouseFrame(int x, int y, long time)
        {
            this.X = x;
            this.Y = y;
            this.TimeStamp = time;
        }
    }
    class MouseSpeed
    {
        public float SpeedX;
        public float SpeedY;
        public MouseSpeed(float x, float y)
        {
            this.SpeedX = x;
            this.SpeedY = y;
        }
    }

    public class FixedSizedQueue<T> : ConcurrentQueue<T>
    {
        private readonly object syncObject = new object();

        public int Size { get; private set; }

        public FixedSizedQueue(int size)
        {
            Size = size;
        }

        public new void Enqueue(T obj)
        {
            base.Enqueue(obj);
            lock (syncObject)
            {
                while (base.Count > Size)
                {
                    T outObj;
                    base.TryDequeue(out outObj);
                }
            }
        }
    }
}
