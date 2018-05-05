using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace maze
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            Bitmap start = new Bitmap(550, 550);
            for (int i = 0; i < start.Size.Width; i++)
            {
                for (int j = 0; j < start.Size.Height; j++)
                {
                    start.SetPixel(i, j, Color.Black);
                }
            }
            pictureBox1.Image = start;
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }
    }
}
