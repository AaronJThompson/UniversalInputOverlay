using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;
using System.Diagnostics;
using System.Collections.Concurrent;

namespace Input_Overlay
{
    using Hooking;
    using System.Runtime.InteropServices;
    class Mouse
    {
        private InputHook inputHook;
        private float deltaX = 0;
        private float deltaY = 0;
        private float smoothing = 2;
        private float smoothingMult;
        private Stopwatch timer;
        private RectangleF leftRectangle;
        private RectangleF rightRectangle;
        private long lastFrame = 0;
        private Point pos;
        private Image leftImage;
        private Image leftPressedImage;
        private Image rightImage;
        private PointF leftOrigin;
        private PointF rightOrigin;
        private Image rightPressedImage;
        private float scaleY = .55f;
        private float scaleX = .55f;
        private GraphicsUnit pixels = GraphicsUnit.Point;

        public Mouse(Point pos, InputHook ih)
        {
            timer = Stopwatch.StartNew();
            this.pos = pos;
            inputHook = ih;
            inputHook.onMouseMove += onMove;
            smoothingMult = 1 / smoothing;
            leftImage = Properties.Resources.Mouse_Left;
            rightImage = Properties.Resources.Mouse_Right;
            var leftPoint = new PointF(pos.X - 50, pos.Y - 380);
            var rightPoint = new PointF(pos.X + 84, pos.Y - 380);
            leftRectangle = calculateRectangle(leftPoint, leftImage, scaleX, scaleY);
            rightRectangle = calculateRectangle(rightPoint, rightImage, scaleX, scaleY);
            leftOrigin = leftRectangle.Location;
            rightOrigin = rightRectangle.Location;
        }

        private RectangleF calculateRectangle(PointF pos, Image img, float scaleX, float scaleY)
        {
            RectangleF imgRc = img.GetBounds(ref pixels);
            imgRc.Size = new SizeF(imgRc.Size.Width * scaleX, imgRc.Size.Height * scaleY);
            imgRc.Offset(pos);
            return imgRc;
        }

        private void onMove(object sender, MouseEventArgs e)
        {
            deltaX += (e.Data.DeltaX * smoothingMult);
            deltaY += (e.Data.DeltaY * smoothingMult);
        }

        public void Stop()
        {
            inputHook.onMouseMove -= onMove;
        }
        public void Paint(PaintEventArgs e)
        {
            var tDelta = timer.ElapsedMilliseconds - lastFrame;
            lastFrame = timer.ElapsedMilliseconds;
            float xSpeed = deltaX / tDelta;
            float ySpeed = deltaY / tDelta;
            leftRectangle.Location = leftOrigin;
            rightRectangle.Location = rightOrigin;
            leftRectangle.Offset(new PointF(xSpeed * 10, ySpeed * 10));
            rightRectangle.Offset(new PointF(xSpeed * 10, ySpeed * 10));
            e.Graphics.DrawImage(leftImage, leftRectangle);
            e.Graphics.DrawImage(rightImage, rightRectangle);
            deltaX *= smoothingMult * 1.5f;
            deltaY *= smoothingMult * 1.5f;
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
