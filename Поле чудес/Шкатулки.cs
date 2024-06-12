using System;
using System.Windows.Forms;
using Поле_чудес.Properties;

namespace Поле_чудес
{
    public partial class Шкатулки : Form
    {
        Random rnd1 = new Random();
        public bool truechoice = false;
        public bool Truechoice
        {
            get { return truechoice; }
            set { truechoice = value; }
        }
        int truly;
        
        public Шкатулки()
        {
            InitializeComponent();
        }

        private void Шкатулки_Load(object sender, EventArgs e)
        {
            truly = rnd1.Next(1, 3);
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            if(truly==1)
            {
                label1.Text = "Вы угадали, получите 2000 очков";
                truechoice = true;
                pictureBox1.Image = Resources.шкатулка_открыта;
                pictureBox2.Visible = false;
                pictureBox2.Enabled = false;
                button1.Visible = true;
                button1.Enabled = true;
                pictureBox1.Enabled = false;
            }
            else
            {
                label1.Text = "Вы не угадали, повезёт в следующий раз";
                truechoice = false;
                pictureBox1.Image = Resources.шкатулка_открыта;
                pictureBox2.Visible = false;
                pictureBox2.Enabled = false;
                button1.Visible = true;
                button1.Enabled = true;
                pictureBox1.Enabled = false;
            }
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            if (truly == 1)
            {
                label1.Text = "Вы не угадали, повезёт в следующий раз";
                truechoice = false;
                pictureBox2.Image = Resources.шкатулка_открыта;
                pictureBox1.Visible = false;
                pictureBox1.Enabled = false;
                button1.Visible = true;
                button1.Enabled = true;
                pictureBox2.Enabled = false;
            }
            else
            {
                label1.Text = "Вы угадали, получите 2000 очков";
                truechoice = true;
                pictureBox2.Image = Resources.шкатулка_открыта;
                pictureBox1.Visible = false;
                pictureBox1.Enabled = false;
                button1.Visible = true;
                button1.Enabled = true;
                pictureBox2.Enabled = false;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
