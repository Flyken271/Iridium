using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Threading;
using System.IO;

namespace NKLinkGUI
{
    public partial class Form1 : Form
    {

        Point lastPoint;

        Size windSize = new Size(980, 355);

        [DllImport("kernel32.dll",
            EntryPoint = "GetStdHandle",
            SetLastError = true,
            CharSet = CharSet.Auto,
            CallingConvention = CallingConvention.StdCall)]
        private static extern IntPtr GetStdHandle(int nStdHandle);
        [DllImport("kernel32.dll",
            EntryPoint = "AllocConsole",
            SetLastError = true,
            CharSet = CharSet.Auto,
            CallingConvention = CallingConvention.StdCall)]
        private static extern int AllocConsole();
        private const int STD_OUTPUT_HANDLE = -11;
        private const int MY_CODE_PAGE = 437;


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

            var process = new Process
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = "nxlink.exe",
                    Arguments = "-s -a" + textBox3.Text,
                    CreateNoWindow = true,
                    UseShellExecute = false,
                    RedirectStandardOutput = true,
                    RedirectStandardError = true
                }
            };
            process.Start();
            //process.BeginOutputReadLine();
            //process.BeginErrorReadLine();

            StreamReader reader = process.StandardOutput;
            string output = reader.ReadToEnd();
            StreamReader readerERR = process.StandardError;
            string outputERR = readerERR.ReadToEnd();
            richTextBox1.SelectionColor = Color.Green;
            
            //richTextBox1.AppendText(Environment.NewLine + "[STANDARD] - " + output);
            richTextBox1.SelectedText = Environment.NewLine + "[STANDARD] - " + output;
            richTextBox1.SelectionColor = Color.Red;
            
            //richTextBox1.AppendText(Environment.NewLine + "[ERROR] - " + outputERR);
            richTextBox1.SelectedText = Environment.NewLine + "[ERROR] - " + outputERR;

            process.WaitForExit();
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
            if(Properties.Settings.Default.command != null && Properties.Settings.Default.IP != null && Properties.Settings.Default.ListNRO != null)
            {
                textBox2.Text = Properties.Settings.Default.IP;
                foreach (object item in Properties.Settings.Default.ListNRO)
                {
                    listBox1.Items.Add(item);
                }
                textBox3.Text = Properties.Settings.Default.command;
            }

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


        private void progressBar1_Click(object sender, EventArgs e)
        {

        }
    }
}
