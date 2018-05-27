using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace WindowsFormsApp
{
    public partial class Form1 : Form
    {

        public static void DrawLine(Graphics graphics, ref Line newLine)
        {

            graphics.DrawLine(new Pen(Color.Black, 3), newLine.A.X, newLine.A.Y, newLine.B.X, newLine.B.X);

            ColorDialog colorDialog1 = new ColorDialog();
            if (colorDialog1.ShowDialog() == DialogResult.OK)
            {
                newLine.LineColor = colorDialog1.Color;
                graphics.DrawLine(new Pen(colorDialog1.Color, 3), newLine.A.X, newLine.A.Y, newLine.B.X, newLine.B.X);
            }
            newLine.Name = ShowDialog("Please enter name", "Name");

        }
        public static string ShowDialog(string text, string caption)
        {
            Form prompt = new Form()
            {
                Width = 500,
                Height = 150,
                FormBorderStyle = FormBorderStyle.FixedDialog,
                Text = caption,
                StartPosition = FormStartPosition.CenterScreen
            };
            Label textLabel = new Label() { Left = 50, Top = 20, Text = text };
            TextBox textBox = new TextBox() { Left = 50, Top = 50, Width = 400 };
            Button confirmation = new Button() { Text = "Ok", Left = 350, Width = 100, Top = 70, DialogResult = DialogResult.OK };
            confirmation.Click += (sender, e) => { prompt.Close(); };
            prompt.Controls.Add(textBox);
            prompt.Controls.Add(confirmation);
            prompt.Controls.Add(textLabel);
            prompt.AcceptButton = confirmation;

            return prompt.ShowDialog() == DialogResult.OK ? textBox.Text : "";
        }
        public static Point DrawPoint(Panel panel, Graphics graphics, EventArgs e)
        {
            Brush brush = Brushes.Black;
            MouseEventArgs m = e as MouseEventArgs;
            Point point = new Point(m.X, m.Y);
            graphics.FillRectangle(brush, point.X, point.Y, 1, 1);
            return point;
        }
        public void DrawLines(List<Line> lines)
        {
            pictureBox1.Refresh();

            shapesToolStripMenuItem.DropDownItems.Clear();

            foreach (var a in lines)
            {
                graphics.DrawLine(new Pen(a.LineColor, 3), a.A.X, a.A.Y, a.B.X, a.B.X);
                ToolStripItem item = new ToolStripMenuItem(a.Name);
                shapesToolStripMenuItem.DropDownItems.Add(item);
            }
            if (activeLine != null)
            {
                graphics.DrawLine(new Pen(activeLine.LineColor, 3), activeLine.A.X, activeLine.A.Y, activeLine.B.X, activeLine.B.X);
                activeLine = null;
            }
        }
    }
}
