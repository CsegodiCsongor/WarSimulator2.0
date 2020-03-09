using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WarSim2._0
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            //Engine.UnitChargeTest();

            //Engine.TestChargeArmy();

            //Engine.TestChargeGeneral();

            Engine.TestChargeSides();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //Engine.UnitTestInit(pictureBox1);

            //Engine.TestInitArmy(pictureBox1);

            //Engine.TestInitGeneral(pictureBox1);

            Engine.TestInitSides(pictureBox1);

            //Engine.InitMap(pictureBox1);
            //Engine.CreateRandomNoiseMap();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Engine.GuassianBlurMap(int.Parse(richTextBox1.Text));
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Engine.MeanBlurMap(int.Parse(richTextBox1.Text));
        }

        private void CalcHeight()
        {
            double height = 0;
            for (int i = 0; i < Engine.map.GetLength(0); i++)
            {
                for (int j = 0; j < Engine.map.GetLength(1); j++)
                {
                    height += Engine.map[i, j];
                }
            }
            label1.Text = height.ToString();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            CalcHeight();
        }
    }
}
