using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NKLinkGUI
{
    public partial class Form2 : Form
    {

        Point lastPoint;

        public Form2()
        {
            InitializeComponent();
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void Form2_Load(object sender, EventArgs e)
        {

        }

        private void richTextBox1_MouseMove(object sender, MouseEventArgs e)
        {
           
                if (e.Button == MouseButtons.Left)
                {
                    this.Left += e.X - lastPoint.X;
                    this.Top += e.Y - lastPoint.Y;
                }

                
        }

        private void richTextBox1_MouseDown(object sender, MouseEventArgs e)
        {
            lastPoint = new Point(e.X, e.Y);
        }
    }
}
