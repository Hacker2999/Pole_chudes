using System;
using System.IO;
using System.Windows.Forms;
using System.Diagnostics;

namespace Поле_чудес
{
    public partial class Form1 : Form
    {
        string default_nickname = "Player";
        public static string file_name;
        public Form1()
        {
            InitializeComponent();
            string data=DateTime.Now.ToString("yy.MM.dd HH.mm.ss");
            Directory.CreateDirectory(Path.GetDirectoryName("\\Поле чудес\\"));
            Directory.CreateDirectory(Path.GetDirectoryName("\\Поле чудес\\логи\\"));
            Directory.CreateDirectory(Path.GetDirectoryName("\\Поле чудес\\рекорды\\"));
            file_name = "\\Поле чудес\\логи\\" + Convert.ToString(data)+".txt";
            var myFile =File.Create(file_name);
            StreamWriter w = File.AppendText("\\Поле чудес\\рекорды\\рекорды.txt");
            w.Close();
            myFile.Close();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == String.Empty)
                textBox1.Text = default_nickname;
            Hide();
            Игровая f = new Игровая();
            f.nickname = textBox1.Text;
            Функции.log_save("Никнейм:" + f.nickname);
            f.ShowDialog();
            this.Close();
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((char)e.KeyChar == (Char)Keys.Back) return;
            if (char.IsDigit(e.KeyChar) || char.IsLetter(e.KeyChar)) return;
            e.Handled = true;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Process.Start("Resources\\Инструкция.docx");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            
            Рекорды f4 = new Рекорды();
            f4.ShowDialog();
            
        }


    }
}
