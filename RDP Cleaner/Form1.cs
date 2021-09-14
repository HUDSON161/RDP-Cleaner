using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;

namespace RDP_Cleaner
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        Thread tn;
        Thread tp;
        public static int NumS=0;
        public static bool binterrupted = false;
        public static bool bDestroy = false;
        public static bool bSMTP = false;

        private void button1_Click(object sender, EventArgs e)
        {
            NumS = 0;
            if ( binterrupted == false )
            {
                if (tn != null)
                    tn.Abort();
                if (tp != null)
                    tp.Abort();

                tp = new Thread(delegate ()
                {
                    while (progressBar1.Value < 100 && textBox1.Lines.Length > 0)
                    {
                        if (textBox1.Lines.Length > 0)
                        {
                            progressBar1.Value = (int)(((double)(NumS + 1) / (double)(textBox1.Lines.Length)) * 100.0);
                        }
                        Thread.Sleep(100);
                    }
                    //................................
                });
                tp.Start();

                tn = new Thread(delegate ()
                {
                    if ( bDestroy == false )
                    {
                        if (bSMTP == false)
                        {
                            Mesilshik.Mesi(ref textBox1, ref textBox2);
                        }
                        else
                        {
                            Mesilshik.MesiSMTP(ref textBox1, ref textBox2,ref textBox5,checkBox4.Checked);
                        }
                    }
                    else
                    {
                        Mesilshik.DestroyTheSame(ref textBox1, ref textBox2);
                    }
                    //................................
                });
                tn.Start();
            }
            else
            {
                if (tn != null)
                    tn.Resume();
                if (tp != null)
                    tp.Resume();
            }
            binterrupted = false;
        }
        //***************Добавляем сочетание клавиш CTRL + A**************
        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == (Keys.Control | Keys.A))
            {
                textBox1.SelectAll();
                //убираем звуковое сопровождение при нажатии клавиш
                e.Handled = e.SuppressKeyPress = true;
            }
        }
        private void textBox2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == (Keys.Control | Keys.A))
            {
                textBox1.SelectAll();
                //убираем звуковое сопровождение при нажатии клавиш
                e.Handled = e.SuppressKeyPress = true;
            }
        }
        //*****************************************************************
        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            textBox3.Text = textBox1.Lines.Length.ToString();
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            textBox4.Text = textBox2.Lines.Length.ToString();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (tn != null)
                tn.Suspend();
            if (tp != null)
                tp.Suspend();
            binterrupted = true;
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (tn != null)
                tn.Abort();
            if (tp != null)
                tp.Abort();
            Application.Exit();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            bDestroy = checkBox1.Checked;
            bSMTP = checkBox3.Checked;
            checkBox3.Enabled = !checkBox1.Checked;
            checkBox2.Enabled = !checkBox1.Checked;
            checkBox4.Enabled = !checkBox1.Checked;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            bDestroy = checkBox1.Checked;
            bSMTP = checkBox3.Checked;
            checkBox3.Enabled = !checkBox1.Checked;
            checkBox2.Enabled = !checkBox1.Checked;
            checkBox4.Enabled = !checkBox1.Checked;
        }

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {
            checkBox2.Checked = !checkBox3.Checked;
            bSMTP = checkBox3.Checked;
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            checkBox3.Checked = !checkBox2.Checked;
            bSMTP = checkBox3.Checked;
        }

        private void Button3_Click(object sender, EventArgs e)
        {
             Form1.ActiveForm.FormBorderStyle = FormBorderStyle.Sizable;
            if (button3.Text == ">>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>")
            {
                Form1.ActiveForm.Width = 760;
                button3.Text = "<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<";
            }
            else
            {
                Form1.ActiveForm.Width = 598;
                button3.Text = ">>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>";
            }
             Form1.ActiveForm.FormBorderStyle = FormBorderStyle.FixedDialog;
        }

        private void CheckBox4_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox4.Checked)
            {
                Form1.ActiveForm.FormBorderStyle = FormBorderStyle.Sizable;
                Form1.ActiveForm.Height = 584;
                button3.Height = 545;
                Form1.ActiveForm.FormBorderStyle = FormBorderStyle.FixedDialog;
            }
            else
            {
                Form1.ActiveForm.FormBorderStyle = FormBorderStyle.Sizable;
                Form1.ActiveForm.Height = 451;
                button3.Height = 411;
                Form1.ActiveForm.FormBorderStyle = FormBorderStyle.FixedDialog;
            }
        }
    }
}
