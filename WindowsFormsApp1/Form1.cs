using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        Core NewCore;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            NewCore = new Core(Convert.ToInt32(numericUpDown4.Value));
            numericUpDown4_ValueChanged(sender, e);
        }

        private void numericUpDown4_ValueChanged(object sender, EventArgs e)
        {
            int nudValue = Convert.ToInt32(numericUpDown4.Value),
                nudTag = Convert.ToInt32(numericUpDown4.Tag);
            NewCore.CoreChange(nudValue);
            if(nudTag > nudValue)
            {
                int difference = nudTag - nudValue;
                for (int i = 0; i < difference; i++)
                {
                    comboBox1.Items.RemoveAt(nudValue);
                }
            }
            else
            {
                for (int i = nudTag; i < nudValue; i++)
                {
                    comboBox1.Items.Add("i = " + (i + 1));
                }
            }
            numericUpDown4.Tag = numericUpDown4.Value;
        }
        
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboBox3.SelectedIndex = Array.IndexOf(Algoritms.ReturnFunc, NewCore.Funcs[0, comboBox1.SelectedIndex]);
            comboBox4.SelectedIndex = Array.IndexOf(Algoritms.ReturnFunc, NewCore.Funcs[1, comboBox1.SelectedIndex]);
            numericUpDown5.Value = NewCore.coefs[0, comboBox1.SelectedIndex];
            numericUpDown6.Value = NewCore.coefs[1, comboBox1.SelectedIndex];
        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex >= 0 && comboBox3.SelectedIndex >= 0)
                NewCore.Funcs[0, comboBox1.SelectedIndex] = Algoritms.ReturnFunc[comboBox3.SelectedIndex];
        }

        private void comboBox4_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex >= 0 && comboBox4.SelectedIndex >= 0)
                NewCore.Funcs[1, comboBox1.SelectedIndex] = Algoritms.ReturnFunc[comboBox4.SelectedIndex];
        }

        private void CalcBtn_Click(object sender, EventArgs e)
        {
            if (!NewCore.IsFull() || comboBox2.SelectedIndex < 0)
            {
                MessageBox.Show("Не все элементы заполнены!");
                return;
            }
            double[] res = NewCore.Solve(Algoritms.ReturnFunc[comboBox2.SelectedIndex], (double)numericUpDown7.Value, 
                (double)numericUpDown3.Value, (double)numericUpDown2.Value, (double)numericUpDown1.Value);
            label11.Text = "φ(x) = ";
            label11.Text += FuncStr(comboBox2.SelectedIndex, (double)numericUpDown7.Value);
            int n = (int)numericUpDown4.Value;
            for (int i = 0; i < n; i++)
            {
                double temp = (double)numericUpDown1.Value * res[i];
                if (temp != 0)
                {
                    if (temp > 0)
                        label11.Text += "+";
                    label11.Text += Math.Round(temp, 4) + "*" + FuncStr(Array.IndexOf(Algoritms.ReturnFunc, 
                        NewCore.Funcs[0, i]), (double)NewCore.coefs[0, i]);
                }
            }
        }

        string FuncStr(int ind, double n)
        {
            switch (ind)
            {
                case 0:
                    return "e^(" + n + "x)";
                case 1:
                    return "ln(" + n + "x)";
                case 2:
                    return "x^" + n;
                case 3:
                    return "sin(" + n + "x)";
                case 4:
                    return "cos(" + n + "x)";
                case 5:
                    return n.ToString();
                default:
                    break;
            }
            return "";
        }

        private void numericUpDown5_ValueChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex >= 0)
                NewCore.coefs[0, comboBox1.SelectedIndex] = numericUpDown5.Value;
        }

        private void numericUpDown6_ValueChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex >= 0)
                NewCore.coefs[1, comboBox1.SelectedIndex] = numericUpDown6.Value;
        }
    }
}
