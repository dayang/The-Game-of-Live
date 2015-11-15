using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CellularAutomaton
{
    public partial class Form1 : Form
    {
        bool[,] live;
        bool[,] temp;

        int[,] dir = new int[8,2]{{1,1},{1,0},{1,-1},{0,1},{0,-1},{-1,-1},{-1,0},{-1,1}};
        int width;
        int height;
        public Form1()
        {
            InitializeComponent();
            this.DoubleBuffered = true;
        }
        Random random = new Random();

        private void timer1_Tick(object sender, EventArgs e)
        {
            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    int num = 0;
                    for (int k = 0; k < 8; k++)
                    {
                        if (live[(i + width + dir[k, 0]) % width, (j + height + dir[k, 1]) % height])
                        {
                            num++;
                        }
                    }
                    if (live[i, j])
                    {
                        if (num != 2 && num != 3)
                            temp[i, j] = false;
                        else
                            temp[i, j] = true;
                    }
                    else
                    {
                        if (num == 3)
                            temp[i, j] = true;
                        else
                            temp[i, j] = false;
                    }
                }
            }
            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    live[i, j] = temp[i, j];
                }
            }
            this.Invalidate();
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            Pen p = new Pen(Color.Black, 1);
            Brush b = new SolidBrush(Color.Black);
            g.FillRectangle(Brushes.White, 0, 0, this.Width, this.Height);
            for (int i = 0; i < this.Height; i += 20)
            {
                g.DrawLine(p, 0, i, this.Width-119, i);
            }
            for (int i = 0; i < this.Width-119; i += 20)
            {
                g.DrawLine(p, i, 0, i, this.Height);
            }

            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    if (live[i, j])
                    {
                        g.FillRectangle(b, i * 20, j * 20, 20, 20);

                    }
                }
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.timer1.Enabled = !this.timer1.Enabled;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (timer1.Interval > 100)
                timer1.Interval -= 100;
            else if (timer1.Interval > 10)
            {
                timer1.Interval -= 10;
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            timer1.Interval += 100;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            width = (this.Width - 119) / 20;
            height = (this.Height) / 20;
            live = new bool[width,height];
            temp = new bool[width,height];
        }

        private void button1_Click(object sender, EventArgs e)
        {
            live[2, 3] = live[3, 4] = live[4, 2] = live[4, 3] = live[4, 4] = true;
            temp[2, 3] = temp[3, 4] = temp[4, 2] = temp[4, 3] = temp[4, 4] = true;
        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {

        }

        private void button7_Click(object sender, EventArgs e)
        {
            Random r = new Random();
            for (int i = 0; i < 30; i++)
            {
                int x = r.Next(30);
                int y = r.Next(25);
                temp[x,y] = live[x,y] = true;
            }
        }

    }
}
