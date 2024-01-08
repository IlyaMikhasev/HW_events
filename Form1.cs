using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;

namespace HW_events
{
    public partial class Form1 : Form
    {
        string _path = "randomNumber.txt";
        Thread thread1;
        Thread thread2;
        Thread thread3;
        public Form1()
        {
            InitializeComponent();
        }

        private void b_start_Click(object sender, EventArgs e)
        {
            thread1 = new Thread(() => GenPairNumber(_path));
            thread1.Start();
            thread2 = new Thread(() => SumPair(_path));
            thread2.Start();
            thread3 = new Thread(() => MultiPair(_path));
            thread3.Start();
        }
        private void GenPairNumber(string path) { 
            Random random = new Random();
            string res = "";
            for (int i = 0; i < 10; i++)
            {
                res += $"{random.Next(1000, 9999)}:{random.Next(1000, 9999)}\n\r";
            }
            SaveFile(res,path);
        }
        private void SaveFile(string file,string path) {
            using (FileStream fs = new FileStream(path, FileMode.Append))
            {
                byte[] buffer = Encoding.Default.GetBytes(file);
                fs.Write(buffer, 0, file.Length);
            }
        }
        private void SumPair(string path) {
            thread1.Join();
            string textFromFile = string.Empty;
            foreach (string line in File.ReadLines(path))
            {
                if (line == "")
                    continue;
                var pair = line.Split(':');
                textFromFile += line + " сумма пары = "+(Convert.ToInt32(pair[0]) + Convert.ToInt32(pair[1])).ToString()+"\n\r"; 
            }
            SaveFile(textFromFile, "sumPair.txt");
        }
        private void MultiPair(string path) {
            thread1.Join();
            string textFromFile = string.Empty;
            foreach (string line in File.ReadLines(path))
            {
                if (line == "")
                    continue;
                var pair = line.Split(':');
                textFromFile += line + " произведение пары = " + (Convert.ToInt32(pair[0]) * Convert.ToInt32(pair[1])).ToString() + "\n\r";
            }
            SaveFile(textFromFile, "multiPair.txt");
        }
    }
}
