using System;
using System.IO;


namespace Поле_чудес
{
    public class Функции
    {

        public static string path_answer = "Resources\\Вопросы-ответы.txt";
        public static string path_score = "\\Поле чудес\\рекорды\\рекорды.txt";
        public static string question;
        public static string answer;
        public static string help;
        
        static int count = System.IO.File.ReadAllLines(path_answer).Length;
        public static Random rnd = new Random();
        public static int up = count / 4;
        public static int p = rnd.Next(0, up);
        public static void read_file()
        {
            StreamReader quest = new StreamReader(path_answer);
            if (p != 0)
            {
                
                for (int i = 0; i < p * 4; i++)
                {
                    quest.ReadLine();
                }
            }
            while (!quest.EndOfStream)
            {
                answer = quest.ReadLine();
                question = quest.ReadLine();
                help=quest.ReadLine();
                if(quest.ReadLine()=="!")
                {
                    quest.ReadToEnd();
                }
            }
            quest.Close();
           
        }
        public static void save_score(string nicknm,int score)
        {
            
            
            
            StreamWriter scorelist = new StreamWriter(path_score, true);
            scorelist.WriteLine(nicknm);
            scorelist.WriteLine(score);
            scorelist.Close();

        }
        public static void log_save(string temp)
        {
            StreamWriter log = new StreamWriter(Form1.file_name, true);
            log.WriteLine(temp);
            log.Close();
            
        }
    }
}
