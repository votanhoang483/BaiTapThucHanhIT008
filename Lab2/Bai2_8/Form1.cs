using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Bai2_8
{
    public partial class Form1 : Form
    {
        private List<Point> pointsList;
        Pen pen = new Pen(Color.Black, 1);
        Color color = Color.Black;
        private bool drawing;
        private bool moving;
        private string option;
        private Bitmap drawingBitmap;
        private Rectangle rectangle;
        private bool resizing;
        private Point startPoint;
        private Point previousLocation;
        private Rectangle circleBounds;
        public Form1()
        {
            InitializeComponent();
            pointsList = new List<Point>();
            drawing = false;
            resizing = false;
            moving = false;
            option = "FreeDraw";
            rectangle = new Rectangle();
            circleBounds = new Rectangle();
            this.BackColor = SystemColors.GradientActiveCaption;
            this.Width = 750;
            this.Height = 600;
            pictureBox1.Width = 750;
            pictureBox1.Height = 500;
            pictureBox1.Location = new Point(0, 0);
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            drawingBitmap = new Bitmap(this.Width, this.Height);
            pictureBox1.BackColor = Color.White;
            pictureBox1.Image = drawingBitmap;
            pictureBox1.Paint += pictureBox1_Paint;
            pictureBox1.MouseDown += Form1_MouseDown;
            pictureBox1.MouseUp += Form1_MouseUp;
            pictureBox1.MouseMove += Form1_MouseMove;
            comboBox1.Text = "Brush Size 1";
        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            using (Graphics g = Graphics.FromImage(drawingBitmap))
            {
                pen.Color = color;
                if (option == "FreeDraw")
                {
                    if (pointsList.Count > 1)
                        g.DrawLines(pen, pointsList.ToArray());
                }
                else if (option == "Rectangle")
                {
                    e.Graphics.DrawRectangle(pen, rectangle);
                }
                else if (option == "Circle")
                {
                    e.Graphics.DrawEllipse(pen, circleBounds);
                }
            }
        }

        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            toolStripStatusLabel1.Text = "MouseDown";
            if (option == "FreeDraw")
            {
                pointsList = new List<Point>();
                Point point = new Point(e.X, e.Y);
                pointsList.Add(point);
                drawing = true;
            }
            else if (option == "Rectangle")
            {
                if (e.Button == MouseButtons.Left)
                {
                    drawing = true;
                    rectangle.Location = e.Location;
                    startPoint = e.Location;
                }
                if (e.Button == MouseButtons.Right)
                {
                    using (Graphics g = Graphics.FromImage(drawingBitmap))
                    {
                        g.DrawRectangle(pen, rectangle);
                    }
                }
            }
            else if (option == "Circle")
            {
                if (e.Button == MouseButtons.Left)
                {
                    drawing = true;
                    circleBounds.Location = e.Location;
                    startPoint = e.Location;
                }
                if (e.Button == MouseButtons.Right)
                {
                    using (Graphics g = Graphics.FromImage(drawingBitmap))
                    {
                        g.DrawEllipse(pen, circleBounds);
                    }
                }
            }
        }

        private void Form1_MouseUp(object sender, MouseEventArgs e)
        {
            toolStripStatusLabel1.Text = "MouseUP";
            if (option == "FreeDraw")
                drawing = false;
            else if (option == "Rectangle")
            {
                if (e.Button == MouseButtons.Left)
                {
                    if (drawing)
                    {
                        drawing = false;
                        resizing = true;
                    }
                    else if (resizing)
                    {
                        resizing = false;
                    }
                }
                else if (e.Button == MouseButtons.Right)
                {
                    rectangle = new Rectangle(0, 0, 0, 0);
                }
            }
            else if (option == "Circle")
            {
                if (e.Button == MouseButtons.Left)
                {
                    if (drawing)
                    {
                        drawing = false;
                        resizing = true;
                    }
                    else if (resizing)
                    {
                        resizing = false;
                    }
                }
                else if (e.Button == MouseButtons.Right)
                {
                    circleBounds = new Rectangle(0, 0, 0, 0);
                }
            }
            pictureBox1.Image = drawingBitmap;
        }

        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            toolStripStatusLabel1.Text = "MouseMove";
            if (drawing && option == "FreeDraw")
            {
                Point point2 = new Point(e.X, e.Y);
                pointsList.Add(point2);
                pictureBox1.Invalidate();
            }
            else if (drawing && option == "Rectangle")
            {
                if (drawing)
                {
                    rectangle.Width = e.X - rectangle.X;
                    rectangle.Height = e.Y - rectangle.Y;
                    pictureBox1.Invalidate();
                }
                else if (resizing)
                {
                    int deltaX = e.X - startPoint.X;
                    int deltaY = e.Y - startPoint.Y;
                    rectangle.Width = Math.Max(0, rectangle.Width + deltaX);
                    rectangle.Height = Math.Max(0, rectangle.Height + deltaY);
                    rectangle.Location = new Point(previousLocation.X + deltaX, previousLocation.Y + deltaY);
                    pictureBox1.Invalidate();
                }
            }
            else if (drawing && option == "Circle")
            {
                if (drawing)
                {
                    circleBounds.Width = e.X - circleBounds.X;
                    circleBounds.Height = e.Y - circleBounds.Y;
                    pictureBox1.Invalidate();
                }
                else if (resizing)
                {
                    int deltaX = e.X - startPoint.X;
                    int deltaY = e.Y - startPoint.Y;
                    circleBounds.Width = Math.Max(0, circleBounds.Width + deltaX);
                    circleBounds.Height = Math.Max(0, circleBounds.Height + deltaY);
                    circleBounds.Location = new Point(previousLocation.X + deltaX, previousLocation.Y + deltaY);
                    pictureBox1.Invalidate();
                }
            }
        }
        private void button1_Click(object sender, EventArgs e) 
        {
            using (Bitmap bitmap = new Bitmap(pictureBox1.Width, pictureBox1.Height))
            {
                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.Filter = "Image Files (*.png;*.jpg;*.jpeg;*.gif)|*.png;*.jpg;*.jpeg;*.gif|All Files (*.*)|*.*";
                saveFileDialog.Title = "Save Image";

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string filePath = saveFileDialog.FileName;
                    pictureBox1.DrawToBitmap(bitmap, new Rectangle(0, 0, pictureBox1.Width, pictureBox1.Height));
                    bitmap.Save(filePath);
                }
            }
        }

        private void button2_Click(object sender, EventArgs e) 
        {
            option = "FreeDraw";
        }

        private void button3_Click(object sender, EventArgs e) 
        {
            option = "Rectangle";
        }

        private void button4_Click(object sender, EventArgs e) 
        {
            option = "Circle";
        }

        private void button5_Click(object sender, EventArgs e) 
        {
            MessageBox.Show("Khi vẽ hình chữ nhật hoặc hình tròn, ấn, giữ và di chuyển chuột trái để vẽ và chỉnh độ lớn, click chuột trái sau khi vẽ để đổi vị trí, click chuột phải để cố định hình đã vẽ.", "Hướng dẫn");
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
        }

        private void button7_Click(object sender, EventArgs e) 
        {
            using (Graphics g = Graphics.FromImage(drawingBitmap))
            {
                g.Clear(Color.White);
            }
            pictureBox1.Image = drawingBitmap;
            pointsList = new List<Point>();
            drawing = false;
            resizing = false;
            moving = false;
            option = "FreeDraw";
            rectangle = new Rectangle();
            circleBounds = new Rectangle();
        }

        private void button6_Click(object sender, EventArgs e) 
        {
            ColorDialog colorDialog = new ColorDialog();

            if (colorDialog.ShowDialog() == DialogResult.OK)
            {
                color = colorDialog.Color;
                button6.BackColor = color;
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            color = Color.Red;
            button6.BackColor = color;
        }

        private void button9_Click(object sender, EventArgs e)
        {
            color = Color.Orange;
            button6.BackColor = color;
        }

        private void button11_Click(object sender, EventArgs e)
        {
            color = Color.Gold;
            button6.BackColor = color;
        }

        private void button10_Click(object sender, EventArgs e)
        {
            color = Color.LimeGreen;
            button6.BackColor = color;
        }

        private void button12_Click(object sender, EventArgs e)
        {
            color = Color.DeepSkyBlue;
            button6.BackColor = color;
        }

        private void button13_Click(object sender, EventArgs e)
        {
            color = Color.DarkOrchid;
            button6.BackColor = color;
        }

        private void button14_Click(object sender, EventArgs e)
        {
            color = Color.HotPink;
            button6.BackColor = color;
        }

        private void button15_Click(object sender, EventArgs e)
        {
            color = Color.Blue;
            button6.BackColor = color;
        }

        private void button16_Click(object sender, EventArgs e)
        {
            color = Color.Black;
            button6.BackColor = color;
        }

        private void button17_Click(object sender, EventArgs e)
        {
            color = Color.White;
            button6.BackColor = color;
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.Text == "Brush Size 1") 
            {
                Pen pen = new Pen(color, 1);
            }
            else if (comboBox1.Text == "Brush Size 2")
            {
                pen = new Pen(color, 2);
            }
            else if (comboBox1.Text == "Brush Size 3")
            {
                pen = new Pen(color, 3);
            }
            else if (comboBox1.Text == "Brush Size 5")
            {
                pen = new Pen(color, 5);
            }
            else if (comboBox1.Text == "Brush Size 10")
            {
                pen = new Pen(color, 10);
            }
            else if (comboBox1.Text == "Brush Size 20")
            {
                pen = new Pen(color, 20);
            }
            else
            {
                pen = new Pen(color, 100);
            }
        }
    }
}
