using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Drawing2D;

namespace Lab9
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        int len = 484;
        int xCenter, yCenter;
        int slen, mlen, hlen;
        Timer t = new Timer();

        private void Form1_Load(object sender, EventArgs e)
        {
            t.Interval = 1000;
            t.Tick += new EventHandler(this.Form1_Paint);
            t.Start();
        }

        private void Form1_Paint(object sender, EventArgs e)
        {
            Graphics g = Graphics.FromHwnd(Handle);
            g.Clear(Color.White);

            xCenter = ClientSize.Width / 2; yCenter = ClientSize.Height / 2;

            int WIDTH, HEIGHT;
            WIDTH = HEIGHT = Math.Min(ClientSize.Height, ClientSize.Width) - 100;
            g.DrawEllipse(new Pen(Color.Black, 6f), (ClientSize.Width - WIDTH) / 2, (ClientSize.Height - HEIGHT) / 2, WIDTH, HEIGHT);

            slen = WIDTH / 2 - 30;
            mlen = WIDTH / 2 - 60;
            hlen = WIDTH / 2 - 80;

            drawArrows(g);

            g.Dispose();
        }

        private void Form1_MouseUp(object sender, MouseEventArgs e)
        {
            if (t.Enabled) t.Stop();
            else t.Start();
        }

        private void drawArrows(Graphics g)
        {
            int hour = DateTime.Now.Hour;
            int minute = DateTime.Now.Minute;
            int second = DateTime.Now.Second;

            this.Text = $"{DateTime.Now}";


            g.DrawLine(new Pen(Color.Red, 1f), xCenter, yCenter, msCoord(second, slen)[0], msCoord(second, slen)[1]);
            g.DrawLine(new Pen(Color.Gray, 2f), xCenter, yCenter, msCoord(minute, mlen)[0], msCoord(minute, mlen)[1]);
            g.DrawLine(new Pen(Color.Black, 3f), xCenter, yCenter, hCoord(hour % 12, minute)[0], hCoord(hour % 12, minute)[1]);
        }

        private int[] msCoord(int second, int len)
        {
            int[] coord = new int[2];
            second *= 6;
            if (second >= 0 && second <= 100)
            {
                coord[0] = xCenter + (int)(len * Math.Sin(Math.PI * second/ 180));
                coord[1] = yCenter - (int)(len * Math.Cos(Math.PI * second / 180));
            }
            else
            {
                coord[0] = xCenter - (int)(len * -Math.Sin(Math.PI * second/ 180));
                coord[1] = yCenter - (int)(len * Math.Cos(Math.PI * second / 180));
            }
            return coord;
        }

        private int[] hCoord(int hour, int minute)
        {
            int[] coord = new int[2];
            hour = (int)((hour * 30) + (minute * 0.5));
            if (hour >= 0 && hour <= 180)
            {
                coord[0] = xCenter + (int)(hlen * Math.Sin(Math.PI * hour / 180));
                coord[1] = yCenter - (int)(hlen * Math.Cos(Math.PI * hour / 180));
            }
            else
            {
                coord[0] = xCenter - (int)(hlen * -Math.Sin(Math.PI * hour / 180));
                coord[1] = yCenter - (int)(hlen * Math.Cos(Math.PI * hour / 180));
            }
            return coord;
        }
    }
}
