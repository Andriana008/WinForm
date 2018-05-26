using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;


using System.Drawing.Drawing2D;

namespace WindowsFormsApp
{
    [Serializable]
    public class Line
    {
        public Point A { get; set; }
        public Point B { get; set; }
        public string Name { get; set; }
        public Color LineColor { get; set; }

       
        public Line(Line c)
        {
            A = c.A;
            B = c.B;
            Name = c.Name;
            LineColor = c.LineColor;
        }
        public Line() { }

        public Line(Point a, Point b)
        {
            this.A = a;
            this.B = b;
            LineColor = Color.Black;
        }

        public double Distance()
        {
            return Math.Sqrt(Math.Pow(B.X - A.X, 2) + Math.Pow(B.Y - A.Y, 2));
        }
        public void Draw(Graphics g)
        {
            using (var path = GetPath())
            using (var brush = new SolidBrush(LineColor))
                g.FillPath(brush, path);
        }
        public GraphicsPath GetPath()
        {
            var path = new GraphicsPath();
            var p =A;
            path.AddLine(A,B);
            return path;
        }
        public void Move(Point d)
        {
            A = new Point(A.X + d.X, A.Y + d.Y);
        }
    }
}
