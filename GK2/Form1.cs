using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GK2
{
    public partial class Form1 : Form
    {
        public Grid Grid { get; set; }
        public int W { get; set; }
        public int H { get; set; }
        public Form1()
        {
            InitializeComponent();
            buttonPickColor.BackColor = Color.Crimson;
            W = Width;
            H = Height;
            Grid = new Grid(100, 100, pictureBox1, Color.Crimson);

        }

        private void CheckBoxNet_CheckedChanged(object sender, EventArgs e)
        {
            Grid.EditingNet = !checkBoxNet.Checked;
            Grid.UpdateAllGrid();
        }

        double Time;
        private void TimerAnimation_Tick(object sender, EventArgs e)
        {
            Time += 0.2;
            Grid.LightVersor.X = Math.Cos(Time);
            Grid.LightVersor.Y = Math.Sin(Time);
            Grid.LightVersor.Z = 1;
            Grid.LightVersor = Grid.LightVersor / Grid.LightVersor.Length;
            Grid.UpdateAllGrid();
        }
    }
}
