
using System;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Windows.Forms;
using Поле_чудес.Properties;
using Image = System.Drawing.Image;

namespace Поле_чудес
{
    public partial class Игровая : Form
    {
        public string nickname;
        float angle = 0;
        float temp = 0;
        float spin = 0;
        int rtime = 0, ntime = 0;
        int[] nums = new int[16] { 1000, 1500, 2000, 500, 300, 200, 1300, 700, 1200, 100, 900, 1100, 600, 800, 50, 400 };
        Random rnd = new Random();
        string answer;
        int score;
        int player_score = 0;
        char[] arr = Enumerable.Range(0, 32).Select((x, i) => (char)('а' + i)).ToArray();
        public Игровая()
        {
            InitializeComponent();
            Функции.read_file();
            answer = Функции.answer;
            label3.Text += Функции.question;
            dataGridView1.RowCount = 1;
            dataGridView1.ColumnCount = answer.Length;
            dataGridView1.ClearSelection();
            Функции.log_save("Загаданное слово:"+answer);
        }

        void timer1_Tick(object sender, EventArgs e)
        {
            ntime += 30;

            angle = 5;
            temp += angle;
            if (temp >= 360)
            {
                pictureBox2.Image = Resources.baraban_finel;
                temp = 0;
            }
            pictureBox2.Invalidate();
            pictureBox2.Image = RotateImage(pictureBox2.Image, angle);
            if (rtime <= ntime)
            {
                timer1.Enabled = false;
                bool score_flag = true;
                double up_t = 22.5;
                int numb_t = 0;
                bool shk = false;
                while (score_flag)
                {
                    if (temp > up_t)
                    {
                        up_t += 22.5;
                        numb_t++;
                    }
                    else
                        score_flag = false;
                }
                score = nums[numb_t];
                if (score == nums[2])
                {
                    Шкатулки f3 = new Шкатулки();
                    f3.ShowDialog();
                    //f3.Close();
                    shk = f3.Truechoice;
                    button2.Enabled = true;
                    if (shk)
                    {
                        label1.Text = "Вы выиграли бонусные очки из шкатулки";
                        player_score += score;
                        label2.Text = "Очки:" + player_score;
                    }
                    else
                    {
                        label1.Text = "Вы не угадали шкатулку, бонусных очков нет";
                    }
                }
                else
                {
                    textBox2.Enabled = true;
                    textBox1.Enabled = true;
                }


            }
        }
        public static Image RotateImage(Image img, float rotationAngle)
        {
            //create an empty Bitmap image
            Bitmap bmp = new Bitmap(img.Width, img.Height);
            bmp.SetResolution(img.HorizontalResolution, img.VerticalResolution);
            //turn the Bitmap into a Graphics object
            Graphics gfx = Graphics.FromImage(bmp);

            //now we set the rotation point to the center of our image
            gfx.TranslateTransform((float)bmp.Width / 2, (float)bmp.Height / 2);

            //now rotate the image
            gfx.RotateTransform(rotationAngle);

            gfx.TranslateTransform(-(float)bmp.Width / 2, -(float)bmp.Height / 2);

            //set the InterpolationMode to HighQualityBicubic so to ensure a high
            //quality image once it is transformed to the specified size
            gfx.InterpolationMode = InterpolationMode.HighQualityBicubic;

            //now draw our new image onto the graphics object
            gfx.DrawImage(img, new Point(0, 0));

            //dispose of our Graphics object
            gfx.Dispose();

            //return the image
            return bmp;
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            label1.Text = "Подсказка:" + Функции.help;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            
            bool flag = false;
            bool main = false;
            int check = 0;
            if (!main)
            {

                char letter = Convert.ToChar(textBox2.Text);
                Функции.log_save(textBox2.Text);
                int temp1 = 0;


                for (int i = 0; i < arr.Length; i++)
                {
                    if (arr[i] == letter)
                        temp1 = i;
                }
                if (arr[temp1] == ' ')
                {
                    label1.Text = "Такую букву уже называли, очки не засчитаны";
                }
                else
                {

                    for (int i = 0; i < answer.Length; i++)
                    {

                        if (answer[i] == letter)
                        {
                            dataGridView1.Rows[0].Cells[i].Value = letter;
                            flag = true;
                            arr[temp1] = ' ';

                        }

                    }
                }

                if (flag)
                {
                    player_score += score;
                    label2.Text = "Очки:" + player_score;
                }
                else if (arr[temp1] != ' ')
                {
                    label1.Text = "Такой буквы нет в слове, очки отнимаются";
                    player_score -= score;
                    label2.Text = "Очки:" + player_score;
                }
                button3.Enabled = false;
                button2.Enabled = true;
                textBox2.Enabled = false;
                for (int i = 0; i < dataGridView1.ColumnCount; i++)
                {
                    if (Convert.ToChar(dataGridView1.Rows[0].Cells[i].Value) == answer[i])
                        check++;
                    if (check == answer.Length)
                        main = true;

                }
            }
            if (main)
            {
                pictureBox2.Visible = false;
                pictureBox3.Visible = false;
                label2.Visible = false;
                label6.Visible = true;
                button1.Enabled = false;
                button1.Visible = false;
                button2.Enabled = false;
                button2.Visible = false;
                button3.Enabled = false;
                button3.Visible = false;
                textBox2.Visible = false;
                textBox1.Visible = false;
                toolStripButton2.Enabled = false;
                label6.Text = "Поздравляю вы выиграли!!!\nВаш счёт игры:" + player_score;
                label6.ForeColor = Color.SpringGreen;
                Функции.save_score(nickname, player_score);
                Функции.log_save("Количество круток:"+Convert.ToString(spin));
                Функции.log_save("Счёт игрока:"+Convert.ToString(player_score));
                Функции.log_save("Отгадано");
            }
            if (player_score <= -2500)
            {
                toolStripButton2.Enabled = false;
                button1.Enabled = false;
                button1.Visible = false;
                button2.Enabled = false;
                button2.Visible = false;
                button3.Enabled = false;
                button3.Visible = false;
                dataGridView1.Visible = false;
                textBox2.Visible = false;
                pictureBox1.Visible = false;
                pictureBox2.Visible = false;
                pictureBox3.Visible = false;
                textBox1.Visible = false;
                label1.Visible = false;
                label2.Visible = false;
                label3.Visible = false;
                label5.Visible = true;
                label5.Text += "\nВаш счёт:" + player_score;
                Функции.log_save(Convert.ToString(spin));
                Функции.log_save(Convert.ToString(player_score));
                Функции.log_save("Неотгадано");
            }
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            if (textBox2.Text != "")
                button3.Enabled = true;
            else
                button3.Enabled = false;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (textBox1.Text != "" && textBox1.TextLength == answer.Length)
                button1.Enabled = true;
            else
                button1.Enabled = false;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            int count = 0;
            for (int i = 0; i < dataGridView1.ColumnCount; i++)
            {
                if (dataGridView1.Rows[0].Cells[i].Value == null)
                    count++;
            }
            if (textBox1.Text == answer)
            {
                for (int i = 0; i < dataGridView1.ColumnCount; i++)
                {
                    dataGridView1.Rows[0].Cells[i].Value = answer[i];
                }
                score = 100 * count;
                player_score += score;
                pictureBox2.Visible = false;
                pictureBox3.Visible = false;
                label2.Visible = false;
                label6.Visible = true;
                button1.Enabled = false;
                button1.Visible = false;
                button2.Enabled = false;
                button2.Visible = false;
                button3.Enabled = false;
                button3.Visible = false;
                textBox2.Visible = false;
                textBox1.Visible = false;
                toolStripButton2.Enabled = false;
                label6.Text = "Поздравляю вы выиграли!!!\nВаш счёт игры:" + player_score;
                label6.ForeColor = Color.SpringGreen;
                Функции.save_score(nickname, player_score);
                Функции.log_save("Количество круток:" + Convert.ToString(spin));
                Функции.log_save("Счёт игрока:" + Convert.ToString(player_score));
                Функции.log_save("Отгадано");
            }
            else
            {
                toolStripButton2.Enabled = false;
                button1.Enabled = false;
                button1.Visible = false;
                button2.Enabled = false;
                button2.Visible = false;
                button3.Enabled = false;
                button3.Visible = false;
                dataGridView1.Visible = false;
                textBox2.Visible = false;
                pictureBox1.Visible = false;
                pictureBox2.Visible = false;
                pictureBox3.Visible = false;
                textBox1.Visible = false;
                label1.Visible = false;
                label2.Visible = false;
                label3.Visible = false;
                label5.Visible = true;
                Функции.log_save("Количество круток:" + Convert.ToString(spin));
                Функции.log_save("Счёт игрока:" + Convert.ToString(player_score));
                Функции.log_save("Неотгадано");
            }
            button3.Enabled = false;
            button2.Enabled = true;

        }
        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            textBox1.Text = "";
            e.KeyChar = char.ToLower(e.KeyChar);
            char l = e.KeyChar;
            if ((l < 'А' || l > 'я') && l != '\b' && l != '.')
            {
                e.Handled = true;
            }
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            textBox2.Text = "";
            e.KeyChar = char.ToLower(e.KeyChar);
            char l = e.KeyChar;
            if ((l < 'А' || l > 'я') && l != '\b' && l != '.')
            {
                e.Handled = true;
            }
        }



        private void button2_Click(object sender, EventArgs e)
        {
            spin++;
            button2.Enabled = false;
            rtime = rnd.Next(600, 900);
            timer1.Enabled = true;
            ntime = 0;
            textBox2.Enabled = false;
            textBox2.Clear();
            textBox1.Enabled = false;
            label1.Text = "";
        }
    }
}
