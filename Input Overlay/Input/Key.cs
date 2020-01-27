using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Input_Overlay
{
    class Key
    {
        //private static Font font = new Font("Source Sans Pro", 16);
        //private Image image;
        //private Image pressedImage;
        private String name;
        private Point pos;
        private int keyCode;
        public bool Pressed;
        private float scaleX;
        private float scaleY;
        private RectangleF imgRectangle;
        //private GraphicsUnit pixels = GraphicsUnit.Point;
        //public Key(float scale, string name,int code, Point pos, Image baseImg, Image pressed)
        //{
        //    this.name = name;
        //    this.pos = pos;
        //    this.image = baseImg;
        //    this.pressedImage = pressed;
        //    this.scaleX = scale;
        //    this.scaleY = scale;
        //    this.imgRectangle = calculateRectangle(pos, baseImg, this.scaleX, this.scaleY);
        //    this.keyCode = code;
        //}
        //public Key(float scaleX, float scaleY, string name, int code, Point pos, Image baseImg, Image pressed)
        //{
        //    this.name = name;
        //    this.pos = pos;
        //    this.image = baseImg;
        //    this.pressedImage = pressed;
        //    this.scaleX = scaleX;
        //    this.scaleY = scaleY;
        //    this.imgRectangle = calculateRectangle(pos, baseImg, scaleX, scaleY);
        //    this.keyCode = code;
        //}
        //public void Paint(PaintEventArgs e)
        //{
        //    Image img = Pressed ? pressedImage : image;
        //    e.Graphics.DrawImage(img, imgRectangle);
        //    StringFormat drawFormat = new StringFormat();
        //    drawFormat.Alignment = StringAlignment.Center;
        //    e.Graphics.DrawString(name,font,Brushes.White, imgRectangle, drawFormat);
        //}
        //private RectangleF calculateRectangle(Point pos,Image img, float scaleX, float scaleY)
        //{
        //    RectangleF imgRc = img.GetBounds(ref pixels);
        //    imgRc.Size = new SizeF(imgRc.Size.Width * scaleX, imgRc.Size.Height * scaleY);
        //    imgRc.Offset(pos);
        //    return imgRc;
        //}
    }
}


