using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;


namespace WindowsFormsApp
{
    public partial class Form1 : Form
    {
        bool moving;
        Point previousPoint = Point.Empty;
        Point firstPoint = Point.Empty;
        private Line currentLine;
        private List<Line> drawenLine;

        public Form1()
        {
            InitializeComponent();
            currentLine = new Line();
            drawenLine = new List<Line>();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            MouseEventArgs m = e as MouseEventArgs;
            var p = sender as Panel;
           // Graphics g = p.CreateGraphics();
            Pen pen = new Pen(Color.Black, 3);
            Brush pointBrush = (Brush)Brushes.Black;
            int pointX = ((MouseEventArgs)e).X;
            int pointY = ((MouseEventArgs)e).Y;
            PaintEventArgs a = e as PaintEventArgs;
            a.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            //g.FillRectangle(pointBrush, pointX, pointY, 2, 2);
            if (m.Button == MouseButtons.Right)
            {
                if (firstPoint == Point.Empty)
                {
                    firstPoint = new Point(m.X, m.Y);
                }
                else
                {
                    Line curr = new Line(firstPoint, new Point(m.X, m.Y));
                    drawenLine.Add(new Line(curr));
                    firstPoint = Point.Empty;
                    curr.Draw(a.Graphics);
                    this.Refresh();

                }

            }
            
        }
        private void pictureBox1_DoubleClick(object sender, MouseEventArgs e)
        {
            if (firstPoint == Point.Empty)
            {
                firstPoint = new Point(e.X, e.Y);
            }
            else
            {
                drawenLine.Add(new Line(firstPoint, new Point(e.X, e.Y)));
                firstPoint = Point.Empty;
                this.Refresh();
            }
        }


        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            openFileDialog1 = new OpenFileDialog
            {
                Filter = "(*.xml)|*.xml",
                RestoreDirectory = true,
                CheckFileExists = true,
                CheckPathExists = true,
                Title = "Choose file"
            };
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                drawenLine.Clear();
                drawenLine = LineBL.DeserializeList(openFileDialog1.FileName);
                this.Refresh();
            }
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            saveFileDialog1 = new SaveFileDialog
            {
                RestoreDirectory = true,
                DefaultExt = "xml",
                CheckPathExists = true,
                Title = "Save your work",
                ValidateNames = true
            };

            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                LineBL.SerializeList(drawenLine, saveFileDialog1.FileName);
            }
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            if (moving)
            {
                var d = new Point(e.X - previousPoint.X, e.Y - previousPoint.Y);
                currentLine.Move(d);
                previousPoint = e.Location;
                this.Invalidate();
            }
            base.OnMouseMove(e);
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
