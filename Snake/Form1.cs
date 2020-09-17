using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Snake
{
    public partial class Form1 : Form
    {
        public int[,] map = new int[15, 15];
        //{
        //    {0,0,0,0,0,0,0,0,0,0 },
        //    {0,0,0,0,0,0,0,0,0,0 },
        //    {0,0,0,0,0,0,0,0,0,0 },
        //    {0,0,0,0,0,0,0,0,0,0 },
        //    {0,0,0,0,0,0,0,0,0,0 },
        //    {0,0,0,0,0,0,0,0,0,0 },
        //    {0,0,0,0,0,0,0,0,0,0 },
        //    {0,0,0,0,0,0,0,0,0,0 },
        //    {0,0,0,0,0,0,0,0,0,0 },
        //    {0,0,0,0,0,0,0,0,0,0 },
        //};
        Snake s = new Snake();
        Point egg;
        Point p_h;
        int width = 20, height = 20;
        int sorce;
        int length = 2;
        public Form1()
        {
            InitializeComponent();
        }

        public void Pro_Egg()
        {
            Random r = new Random();
            do
            {
                egg.X = r.Next(0, 15);
                egg.Y = r.Next(0, 15);
            } while (map[egg.X, egg.Y] == 1);
            map[egg.X, egg.Y] = 2;
        }

        public void Game_Over()
        {
            timer1.Dispose();
            timer2.Dispose();
            MessageBox.Show("游戏结束！！！", "GameOver");
        }

        public void Paints()
        {
            Graphics g = this.CreateGraphics();
            g.DrawRectangle(Pens.Black, 9, 9, map.GetLength(1) * width + 1, map.GetLength(0) * height + 1);
            for (int i = 0; i < map.GetLength(0); i++)
            {
                for (int j = 0; j < map.GetLength(1); j++)
                {
                    if (map[i, j] == 0)
                        g.FillRectangle(new SolidBrush(Color.White), j * width + 10, i * height + 10, width, height);
                    if (map[i, j] == 1)
                    {
                        if (s.tail.X == i && s.tail.Y == j)
                        {
                            g.FillRectangle(new SolidBrush(Color.White), j * width + 10, i * height + 10, width, height);
                            g.FillPie(new SolidBrush(Color.Black), j * width + 10, i * height + 10, width, height, -50, 100);
                        }
                        else if (s.head.X == i && s.head.Y == j)
                        {
                            g.FillEllipse(new SolidBrush(Color.Black), j * width + 10, i * height + 10, width, height);
                            g.FillEllipse(new SolidBrush(Color.White), j * width + 10 + width / 3, i * height + 10 + height / 3, width / 3, height / 3);
                        }
                        else
                            g.FillEllipse(new SolidBrush(Color.Gray), j * width + 10, i * height + 10, width, height);
                    }
                    if (map[i, j] == 2)
                        g.FillEllipse(new SolidBrush(Color.Red), j * width + 10, i * height + 10, width, height);
                }
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            if(button1.Text=="重新游戏")
            {
                sorce = 0;
                map = new int[15, 15];
                s = new Snake();
                button2.Enabled = false;
                button3.Enabled = false;
                button4.Enabled = false;
                button5.Enabled = false;
                button1.Text = "暂停游戏";
                timer1.Start();
                timer2.Start();
                s.head = new Point(0, 1);
                s.tail = new Point(0, 0);
                s.p_s.Add(new Point(0, 0));
                s.p_s.Add(new Point(0, 1));
                s.direction = 'd';
                map[0, 1] = 1;
                map[0, 0] = 1;
                Pro_Egg();
                Paints();
            }
            else if (button1.Text == "开始游戏")
            {
                button2.Enabled = false;
                button3.Enabled = false;
                button4.Enabled = false;
                button5.Enabled = false;
                button1.Text = "暂停游戏";
                timer1.Start();
                timer2.Start();
                s.head = new Point(0, 1);
                s.tail = new Point(0, 0);
                s.p_s.Add(new Point(0, 0));
                s.p_s.Add(new Point(0, 1));
                s.direction = 'd';
                map[0, 1] = 1;
                map[0, 0] = 1;
                Pro_Egg();
                Paints();
            }
            else if (button1.Text == "暂停游戏")
            {
                button2.Enabled = true;
                button3.Enabled = true;
                button4.Enabled = true;
                button5.Enabled = true;
                button1.Text = "继续游戏";
                timer1.Stop();
                timer2.Stop();
            }
            else
            {
                button2.Enabled = false;
                button3.Enabled = false;
                button4.Enabled = false;
                button5.Enabled = false;
                button1.Text = "暂停游戏";
                timer1.Start();
                timer2.Start();
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            sorce += (1000 - timer1.Interval) / 100;
            switch (s.direction)
            {
                case 'w':
                    if (s.head.X - 1 == -1 || map[s.head.X - 1, s.head.Y] == 1)
                    {
                        Game_Over();
                        button1.Text = "重新游戏";
                    }
                    else if (map[s.head.X - 1, s.head.Y] == 0)
                    {
                        p_h = new Point(s.head.X - 1, s.head.Y);
                        s.p_s.Add(p_h);
                        s.head = p_h;
                        map[s.head.X, s.head.Y] = 1;
                        map[s.p_s[0].X, s.p_s[0].Y] = 0;
                        s.p_s.RemoveAt(0);
                        s.tail = s.p_s[0];
                        Paints();
                    }
                    else if (map[s.head.X - 1, s.head.Y] == 2)
                    {
                        length++;
                        textBox3.Text = length.ToString();
                        p_h = new Point(s.head.X - 1, s.head.Y);
                        s.p_s.Add(p_h);
                        s.head = p_h;
                        map[s.head.X, s.head.Y] = 1;
                        Paints();
                        Pro_Egg();
                        sorce += 100;
                    }
                    break;
                case 's':
                    if (s.head.X + 1 == map.GetLength(1) || map[s.head.X + 1, s.head.Y] == 1)
                    {
                        Game_Over();
                        button1.Text = "重新游戏";
                    }
                    else if (map[s.head.X + 1, s.head.Y] == 0)
                    {
                        p_h = new Point(s.head.X + 1, s.head.Y);
                        s.p_s.Add(p_h);
                        s.head = p_h;
                        map[s.head.X, s.head.Y] = 1;
                        map[s.p_s[0].X, s.p_s[0].Y] = 0;
                        s.p_s.RemoveAt(0);
                        s.tail = s.p_s[0];
                        Paints();
                    }
                    else if (map[s.head.X + 1, s.head.Y] == 2)
                    {
                        length++;
                        textBox3.Text = length.ToString();
                        p_h = new Point(s.head.X + 1, s.head.Y);
                        s.p_s.Add(p_h);
                        s.head = p_h;
                        map[s.head.X, s.head.Y] = 1;
                        Paints();
                        Pro_Egg();
                        sorce += 100;
                    }
                    break;
                case 'a':
                    if (s.head.Y - 1 == -1 || map[s.head.X, s.head.Y - 1] == 1)
                    {
                        Game_Over();
                        button1.Text = "重新游戏";
                    }
                    else if (map[s.head.X, s.head.Y - 1] == 0)
                    {
                        p_h = new Point(s.head.X, s.head.Y - 1);
                        s.p_s.Add(p_h);
                        s.head = p_h;
                        map[s.head.X, s.head.Y] = 1;
                        map[s.p_s[0].X, s.p_s[0].Y] = 0;
                        s.p_s.RemoveAt(0);
                        s.tail = s.p_s[0];
                        Paints();
                    }
                    else if (map[s.head.X, s.head.Y - 1] == 2)
                    {
                        length++;
                        textBox3.Text = length.ToString();
                        p_h = new Point(s.head.X, s.head.Y - 1);
                        s.p_s.Add(p_h);
                        s.head = p_h;
                        map[s.head.X, s.head.Y] = 1;
                        Paints();
                        Pro_Egg();
                        sorce += 100;
                    }
                    break;
                case 'd':
                    if (s.head.Y + 1 == map.GetLength(0) || map[s.head.X, s.head.Y + 1] == 1)
                    {
                        Game_Over();
                        button1.Text = "重新游戏";
                    }
                    else if (map[s.head.X, s.head.Y + 1] == 0)
                    {
                        p_h = new Point(s.head.X, s.head.Y + 1);
                        s.p_s.Add(p_h);
                        s.head = p_h;
                        map[s.head.X, s.head.Y] = 1;
                        map[s.p_s[0].X, s.p_s[0].Y] = 0;
                        s.p_s.RemoveAt(0);
                        s.tail = s.p_s[0];
                        Paints();
                    }
                    else if (map[s.head.X, s.head.Y + 1] == 2)
                    {
                        length++;
                        textBox3.Text = length.ToString();
                        p_h = new Point(s.head.X, s.head.Y + 1);
                        s.p_s.Add(p_h);
                        s.head = p_h;
                        map[s.head.X, s.head.Y] = 1;
                        Paints();
                        Pro_Egg();
                        sorce += 100;
                    }
                    break;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (timer1.Interval < 1000)
            {
                textBox2.Text = (int.Parse(textBox2.Text) - 10).ToString();
                timer1.Interval += 10;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (timer1.Interval > 10)
            {
                textBox2.Text = (int.Parse(textBox2.Text) + 10).ToString();
                timer1.Interval -= 10;
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (timer1.Interval < 1000)
            {
                textBox2.Text = (int.Parse(textBox2.Text) - 100).ToString();
                timer1.Interval += 100;
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (timer1.Interval > 100)
            {
                textBox2.Text = (int.Parse(textBox2.Text) + 100).ToString();
                timer1.Interval -= 100;
            }
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            sorce += 1;
            textBox1.Text = sorce.ToString();
        }

        private void button1_KeyPress(object sender, KeyPressEventArgs e)
        {
            switch (e.KeyChar)
            {
                case 'w':
                    if (s.direction != 's')
                        s.direction = 'w';
                    break;
                case 's':
                    if (s.direction != 'w')
                        s.direction = 's';
                    break;
                case 'a':
                    if (s.direction != 'd')
                        s.direction = 'a';
                    break;
                case 'd':
                    if (s.direction != 'a')
                        s.direction = 'd';
                    break;
            }
        }
    }
}
