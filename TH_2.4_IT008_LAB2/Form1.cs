using System;
using System.CodeDom;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TH_2._4_IT008_LAB2
{
    public partial class Form1 : Form
    {
        int a, b, c, d;
        private int type;
        public Form1()
        {
            InitializeComponent();
            this.MaximizeBox = false;
        }
        private void random1(ref int a,ref int b, ref int c,ref int d)
        {
            Random rd=new Random();
            a= rd.Next(0, this.Width);
            b= rd.Next(30, this.Height);
            c = rd.Next(0, this.Width);
            d = rd.Next(30, this.Height);
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            MessageBox.Show("Chon loai va an chuot de ve hinh");
        }

        private void Form1_MouseUp(object sender, MouseEventArgs e)
        {
            random1(ref a, ref b, ref c, ref d);
            Point p1 = new Point(a, b);
            Point p2 = new Point(c, d);
            Pen pen = new Pen(Color.Black);
            Graphics g = this.CreateGraphics();
            Rectangle rect = new Rectangle(p1, new Size(Math.Abs(p2.X - p1.X), Math.Abs(p2.Y - p1.Y)));
            switch (type)
            {
                case 3:
                    g.DrawLine(pen, p1, p2);
                    break;
                case 2:
                    g.DrawEllipse(pen, rect);
                    break;
                case 1:
                    g.DrawRectangle(pen, rect);
                    break;

            }

        }

        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            this.Invalidate();
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            type = 1;
            toolStripButton1.Checked = true;
            toolStripButton2.Checked = false;
            toolStripButton3.Checked = false;
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            type=2;
            toolStripButton2.Checked = true;
            toolStripButton1.Checked = false;
            toolStripButton3.Checked = false;
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            type = 3;
            toolStripButton3.Checked = true;
            toolStripButton2.Checked = false;
            toolStripButton1.Checked = false;
        }
    }
}
