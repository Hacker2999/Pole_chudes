using System;
using System.ComponentModel;
using System.IO;
using System.Windows.Forms;

namespace Поле_чудес
{
    public partial class Рекорды : Form
    {
        public static string path_score = "\\Поле чудес\\рекорды\\рекорды.txt";
        int file_length = System.IO.File.ReadAllLines(path_score).Length;
        StreamReader scorelist = new StreamReader(path_score);
        public Рекорды()
        {
            InitializeComponent();
        }

        private void Рекорды_Load(object sender, EventArgs e)
        {
            string temp;
            dataGridView1.RowCount = file_length/2;

            for (int i = 0; i < file_length; i++)
            {
                temp = scorelist.ReadLine();
                if (i % 2 == 0 && !comboBox1.Items.Contains(temp))
                {
                    comboBox1.Items.Add(temp);
                }
                scorelist.ReadLine();
                i++;
            }
            scorelist.DiscardBufferedData();
            scorelist.BaseStream.Seek(0, System.IO.SeekOrigin.Begin);
            for (int i = 0; i < file_length / 2; i++)
            {
                dataGridView1.Rows[i].Cells[0].Value = scorelist.ReadLine();
                dataGridView1.Rows[i].Cells[1].Value = Convert.ToInt32(scorelist.ReadLine());
                dataGridView1.Rows[i].HeaderCell.Value = i;
            }
            dataGridView1.Sort(dataGridView1.Columns[1], ListSortDirection.Descending);
        }

        private void comboBox1_TextChanged(object sender, EventArgs e)
        {
            if (comboBox1.Text != "")
            {
                button1.Enabled = true;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            dataGridView1.Rows.Clear();
            dataGridView1.Refresh();
            int count = 0;
            string temp;
            scorelist.DiscardBufferedData();
            scorelist.BaseStream.Seek(0, System.IO.SeekOrigin.Begin);
            for (int i = 0; i < file_length; i++)
            {
                temp = scorelist.ReadLine();
                if (temp == comboBox1.Text)
                {
                    count++;
                }
            }
            scorelist.DiscardBufferedData();
            scorelist.BaseStream.Seek(0, System.IO.SeekOrigin.Begin);
            int numb=0;
            for (int i = 0; i < file_length; i++)
            {
                temp = scorelist.ReadLine();
                if (temp==comboBox1.Text )
                {
                    
                    dataGridView1.RowCount++;
                    dataGridView1.Rows[numb].Cells[0].Value = temp;
                    dataGridView1.Rows[numb].Cells[1].Value = Convert.ToInt32(scorelist.ReadLine());
                    numb++;
                }
            }
            dataGridView1.Sort(dataGridView1.Columns[1], ListSortDirection.Descending);
            button1.Enabled = false;
            button2.Enabled = true;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            scorelist.DiscardBufferedData();
            scorelist.BaseStream.Seek(0, System.IO.SeekOrigin.Begin);
            dataGridView1.RowCount = file_length / 2;
            for (int i = 0; i < file_length / 2; i++)
            {
                dataGridView1.Rows[i].Cells[0].Value = scorelist.ReadLine();
                dataGridView1.Rows[i].Cells[1].Value = Convert.ToInt32(scorelist.ReadLine());
            }
            button1.Enabled = false;
            button2.Enabled = false;
            dataGridView1.Sort(dataGridView1.Columns[1], ListSortDirection.Descending);
        }
    }
}
