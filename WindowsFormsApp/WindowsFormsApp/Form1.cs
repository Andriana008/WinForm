using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;


namespace WindowsFormsApp
{
    public partial class Form1 : Form
    {
        bool moving=false;
        private static int points;
        private Graphics graphics;
        Point previousPoint = Point.Empty;
        Point firstPoint = Point.Empty;
        private Line activeLine;
        private Line nextLine;
        private List<Line> Lines;

        public Form1()
        {
            InitializeComponent();
            activeLine = new Line();
            nextLine = new Line();
            Lines = new List<Line>();
            graphics = pictureBox1.CreateGraphics();

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            MouseEventArgs m = e as MouseEventArgs;
            if (m.Button == MouseButtons.Right)
            {
                if (activeLine == null)
                {
                    MessageBox.Show("You haven't chosen the active figure.", "No active figure");
                }
                else
                {
                    int A1 = m.X;
                    int A2 = m.Y;
                    double dist = activeLine.Distance();
                    int B1 = A1 - (int)dist;
                    int B2 = A2 - (int)dist;

                    activeLine.A = new Point(A1, A2);
                    activeLine.B = new Point(B1, B2);
                    Line active = activeLine;

                    DrawLines(Lines);
                    activeLine = active;
                }

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
                Lines.Clear();
                Lines = LineBL.DeserializeList(openFileDialog1.FileName);
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
                LineBL.SerializeList(Lines, saveFileDialog1.FileName);
            }
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            if (moving)
            {
                var d = new Point(e.X - previousPoint.X, e.Y - previousPoint.Y);
                activeLine.Move(d);
                previousPoint = e.Location;
                this.Invalidate();
            }
            base.OnMouseMove(e);
        }

        private void pictureBox1_DoubleClick(object sender, EventArgs e)
        {
            points++;
            var p = sender as Panel;
            Point point = DrawPoint(p, graphics, e);

            if (points % 2 == 1)
            {
                nextLine.A = point;
            }
            else
            {
                nextLine.B = point;
                DrawLine(graphics, ref nextLine);

                Lines.Add(new Line(nextLine));
                ToolStripItem item = new ToolStripMenuItem(nextLine.Name);
                shapesToolStripMenuItem.DropDownItems.Add(item);
            }
        }


        private void shapesToolStripMenuItem_DropDownItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            string clickedname = e.ClickedItem.Text;
            activeLine = Lines.Find(x => x.Name == clickedname);
        }
    }
}
