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
    public partial class Salono : Form
    {
        private AnaMenu anaMenu;
        public Salono(AnaMenu anaMenu)
        {
            InitializeComponent();
            this.anaMenu = anaMenu;
        }

        private void Salono_Load(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {

        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {

        }
        private void numericUpDown2_ValueChanged(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }
        private void button2_Click(object sender, EventArgs e)
        {
            anaMenu.Show();
            this.Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
            decimal satir = numericUpDown1.Value;
            decimal sutun = numericUpDown2.Value;
            int salonNo = Convert.ToInt32((textBox1.Text));
            MessageBox.Show(string.Format("Satır: {0}, Sütun: {1} Salon No: {2}", satir, sutun, salonNo));

        }
    }
}
