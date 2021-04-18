﻿using CalcClass;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Calculator
{
    public partial class Form1 : Form
    {
        double mr;
        Calc Calc;
        public Form1()
        {
            InitializeComponent();
            mr = 0;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            textBoxException.Text += (sender as Button).Text;
        }

        private void buttonClear_Click(object sender, EventArgs e)
        {
            textBoxException.Text = "";
        }

        private void buttonBackspace_Click(object sender, EventArgs e)
        {
            if (textBoxException.Text.Length > 0)
            {
                textBoxException.Text = textBoxException.Text.Substring(0, textBoxException.Text.Length - 1);
            }
        }

        private void buttonMR_Click(object sender, EventArgs e)
        {
            textBoxException.Text += mr.ToString();
        }

        private void buttonMPlus_Click(object sender, EventArgs e)
        {
            try
            {
                mr = Calc.Add(mr, Convert.ToDouble(textBoxResult.Text));
            }catch(Exception ex)
            {
                textBoxException.Text = ex.Message;
            }
        }

        private void buttonMClear_Click(object sender, EventArgs e)
        {
            mr = 0;
        }
    }
}
