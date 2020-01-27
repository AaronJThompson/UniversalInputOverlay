using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using XInput.Wrapper;

namespace Input_Overlay.Input
{
    class Controller
    {
        public X.Gamepad gamepad = null;
        private Point leftStickPos = new Point(180, 116);
        private Point rightStickPos = new Point(435, 214);
        private Point XPos = new Point(492, 136);
        private Point YPos = new Point(538, 92);
        private Point APos = new Point(538, 180);
        private Point BPos = new Point(582, 136);
        private Point LeftBumperPos = new Point(164, 35);
        private Point RightBumperPos = new Point(480, 35);
        private Point LeftTriggerPos = new Point(132, 45);
        private Point RightTriggerPos = new Point(578, 45);
        private Point UpDpadPos = new Point(288, 220);
        private Point DownDpadPos = new Point(288, 283);
        private Point RightDpadPos = new Point(320, 250);
        private Point LeftDpadPos = new Point(256, 250);
        private float stickRadius = 30f;
        public Controller(int id)
        {
            if (X.IsAvailable)
            {
                gamepad = GetGamepad(id);
                gamepad.LStick_DeadZone = 2000;
                X.StartPolling(gamepad);
            }
        }
        public Controller(X.Gamepad gp)
        {
            if (X.IsAvailable)
            {
                gamepad = gp;
                gamepad.LStick_DeadZone = 2000;
                X.StartPolling(gamepad);
            }
        }
        private X.Gamepad GetGamepad(int id)
        {
            X.Gamepad gp = null;
            switch (id)
            {
                case 1:
                    gp = X.Gamepad_1;
                    break;
                case 2:
                    gp = X.Gamepad_2;
                    break;
                case 3:
                    gp = X.Gamepad_3;
                    break;
                case 4:
                    gp = X.Gamepad_4;
                    break;
                default:
                    gp = X.Gamepad_1;
                    break;
            }
            return gp;
        }
        //public void Paint(float X, float Y,PaintEventArgs e)
        //{
        //    Image leftTriggerImage = gamepad.LTrigger_N > 0.1f ? Properties.Resources.LeftTrigger_Pressed : Properties.Resources.LeftTrigger;
        //    Image rightTriggerImage = gamepad.RTrigger_N > 0.1f ? Properties.Resources.RightTrigger_Pressed : Properties.Resources.RightTrigger;
        //    e.Graphics.DrawImageUnscaled(leftTriggerImage, LeftTriggerPos);
        //    e.Graphics.DrawImageUnscaled(rightTriggerImage, RightTriggerPos);

        //    Image leftBumperImage = gamepad.LBumper_down ? Properties.Resources.LeftBumper_Pressed : Properties.Resources.LeftBumper;
        //    Image rightBumperImage = gamepad.RBumper_down ? Properties.Resources.RightBumper_Pressed : Properties.Resources.RightBumper;
        //    e.Graphics.DrawImageUnscaled(leftBumperImage, LeftBumperPos);
        //    e.Graphics.DrawImageUnscaled(rightBumperImage, RightBumperPos);

        //    e.Graphics.DrawImageUnscaled(Properties.Resources.Controller, 0, 0);

        //    X.PointF LStick = gamepad.LStick_N;
        //    X.PointF RStick = gamepad.RStick_N;
        //    PaintStick(leftStickPos, LStick, e);
        //    PaintStick(rightStickPos, RStick, e);
        //    PaintButton(XPos, gamepad.X_down, e);
        //    PaintButton(YPos, gamepad.Y_down, e);
        //    PaintButton(BPos, gamepad.B_down, e);
        //    PaintButton(APos, gamepad.A_down, e);
        //    PaintDPADButton(UpDpadPos, RotateFlipType.RotateNoneFlipNone, gamepad.Dpad_Up_down, e);
        //    PaintDPADButton(DownDpadPos, RotateFlipType.Rotate180FlipNone, gamepad.Dpad_Down_down, e);
        //    PaintDPADButton(RightDpadPos, RotateFlipType.Rotate90FlipNone, gamepad.Dpad_Right_down, e);
        //    PaintDPADButton(LeftDpadPos, RotateFlipType.Rotate270FlipNone, gamepad.Dpad_Left_down, e);
        //}
        //private void PaintStick(Point point, X.PointF stick, PaintEventArgs e)
        //{
        //    bool stickChanged = (bool)(stick.X + stick.Y != 0);
        //    Image stickImage = stickChanged ? Properties.Resources.Stick_Pressed : Properties.Resources.Stick;
        //    Point stickFinal = new Point(point.X, point.Y);
        //    stickFinal.Offset((int)(stick.X * stickRadius), (int)(stick.Y * -stickRadius));
        //    e.Graphics.DrawImageUnscaled(stickImage, stickFinal);

        //}
        //private void PaintButton(Point point, bool pressed, PaintEventArgs e) {
        //    Image img = pressed ? Properties.Resources.Button_Pressed : Properties.Resources.Button;
        //    e.Graphics.DrawImageUnscaled(img, point);
        //}
        //private void PaintDPADButton(Point point, RotateFlipType rotate, bool pressed, PaintEventArgs e)
        //{
        //    Image img = pressed ? Properties.Resources.DPad_Pressed : Properties.Resources.DPad;
        //    img.RotateFlip(rotate);
        //    e.Graphics.DrawImageUnscaled(img, point);
        //}
        public void Stop()
        {
            X.StopPolling();
        }
    }
}
