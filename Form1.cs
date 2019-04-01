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

namespace NKLinkGUI
{
    public partial class Form1 : Form
    {

        Point lastPoint;

        public Form1()
        {
            InitializeComponent();
            //Form1.ActiveForm.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            openFileDialog1.ShowDialog();
        }

        private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
        {
            textBox1.Text = openFileDialog1.FileName.ToString();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != null)
            {
                listBox1.Items.Add(textBox1.Text);
            }
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            textBox3.Text = textBox2.Text + " " + listBox1.SelectedItem;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            /*
            System.Diagnostics.Process process = new System.Diagnostics.Process();
            System.Diagnostics.ProcessStartInfo startInfo = new System.Diagnostics.ProcessStartInfo();
            startInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
            startInfo.FileName = "nxlink";
            startInfo.Arguments = textBox3.Text;
            process.StartInfo = startInfo;
            process.Start();
            */
            string strCmdText;
            strCmdText = "-a" + textBox3.Text;
            System.Diagnostics.Process.Start("nxlink.exe", strCmdText);

        }

        private void button4_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
                textBox2.Text = Properties.Settings.Default.IP;
                foreach(object item in Properties.Settings.Default.ListNRO)
                {
                    listBox1.Items.Add(item);
                }
            textBox3.Text = Properties.Settings.Default.command;
        }

        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                this.Left += e.X - lastPoint.X;
                this.Top += e.Y - lastPoint.Y;
            }
        }

        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            lastPoint = new Point(e.X, e.Y);
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }
        
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            var newList = new ArrayList();
            foreach (object item in listBox1.Items)
            {
                newList.Add(item);
            }

            Properties.Settings.Default.ListNRO = newList;
            Properties.Settings.Default.IP = textBox2.Text;
            Properties.Settings.Default.command = textBox3.Text;
            Properties.Settings.Default.Save();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            listBox1.Items.Remove(listBox1.SelectedItem);
        }
    }
}
