using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using System.Diagnostics;
using System.Windows.Forms;
using System.IO;
using System.Timers;
using System.Security.Cryptography;
using System.Security.Cryptography.Xml;
using System.Configuration;

namespace Timerr
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsDigit(e.KeyChar) && !Char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {

            if (!Char.IsDigit(e.KeyChar) && !Char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void textBox3_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsDigit(e.KeyChar) && !Char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private int sec, min, hou;
        private void button1_Click(object sender, EventArgs e)
        {
            if (comboBox1.Text != "Shut Down"&& comboBox1.Text != "Log Out"&& comboBox1.Text != "Restart")
            {
                MessageBox.Show("You have not chosen the option");
            }
            if (comboBox1.Text == "Shut Down" || comboBox1.Text == "Log Out" || comboBox1.Text == "Restart")

            {
                hou = int.Parse(textBox1.Text);
                min = int.Parse(textBox2.Text);
                sec = int.Parse(textBox3.Text);
                timer1.Start();
                if (sec >= 0)
                {
                    if (sec >= 10)
                    {
                        label5.Text = sec.ToString();
                    }
                    else
                    {
                        label5.Text = "0" + sec.ToString();
                    }
                }
                if (min >= 0)
                {
                    if (min >= 10)
                    {
                        label8.Text = min.ToString() + " :";
                    }
                    else
                    {
                        label8.Text = "0" + min.ToString() + " :";
                    }
                }
                if (hou >= 0)
                {
                    if (hou >= 10)
                    {
                        label9.Text = hou.ToString() + " :";
                    }
                    else
                    {
                        label9.Text = "0" + hou.ToString() + " :";
                    }
                }
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (sec > 0)
            {
                sec--;
            }
            if (sec == 0 && min > 0)
            {
                sec = 59;
                min--;
            }
            if (sec == 0 && min == 0 && hou > 0)
            {
                hou--;
                min = 59;
                sec = 59;
            }
            if (sec == 0 && min == 0 && hou == 0)
            {
                timer1.Stop();
                if (comboBox1.Text == "Shut Down")
                {
                    System.Diagnostics.Process.Start("shutdown", "-s -f -t 0");
                }
                if (comboBox1.Text == "Log Out")
                {
                    System.Diagnostics.Process.Start("shutdown", "-l -f -t 0");

                }
                if (comboBox1.Text == "Restart")
                {
                    System.Diagnostics.Process.Start("shutdown", "-r -f -t 0");

                }
            }
            if (sec >= 0)
            {
                if (sec >= 10)
                {
                    label5.Text = sec.ToString();
                }
                else
                {
                    label5.Text = "0" + sec.ToString();
                }
            }
            if (min >= 0)
            {
                if (min >= 10)
                {
                    label8.Text = min.ToString() + " :";
                }
                else
                {
                    label8.Text = "0" + min.ToString() + " :";
                }
            }
            if (hou >= 0)
            {
                if (hou >= 10)
                {
                    label9.Text = hou.ToString() + " :";
                }
                else
                {
                    label9.Text = "0" + hou.ToString() + " :";
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            timer1.Stop();
            MessageBox.Show("You have canceled your option, please set up the time again");
        }
    }
}