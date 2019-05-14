using System;
using System.Drawing;
using System.Windows.Forms;

namespace DragonServer
{
    [Serializable]
    class Painter
    {
        public int x1 { get; set; }
        public int y1 { get; set; }
        public int x2 { get; set; }
        public int y2 { get; set; }
        public PictureBox pictureBox { get; set; }
        public int complexity { get; set; }

        public Painter()
        {
            x1 = 150;
            y1 = 100;
            x2 = 250;
            y2 = 200;
        }

        public Painter(PictureBox pictureBox, int complexity) : this()
        {
            this.pictureBox = pictureBox;
            this.complexity = complexity;
        }

        public void Draw()
        {
            DragonFunction(x1, y1, x2, y2, complexity, pictureBox);
        }

        void DragonFunction(int x1, int y1, int x2, int y2, int n, PictureBox pictureBox)
        {
            int xn, yn;
            Graphics g = Graphics.FromHwnd(pictureBox.Handle);
            var drawingPen = new Pen(Brushes.Navy, 1);

            if (n > 0)
            {
                xn = (x1 + x2) / 2 + (y2 - y1) / 2;
                yn = (y1 + y2) / 2 - (x2 - x1) / 2;

                DragonFunction(x2, y2, xn, yn, n - 1, pictureBox);
                DragonFunction(x1, y1, xn, yn, n - 1, pictureBox);
            }

            var point1 = new Point(x1, y1);
            var point2 = new Point(x2, y2);
            g.DrawLine(drawingPen, point1, point2);
        }
    }
}
