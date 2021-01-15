using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace EntityProj
{
    /// <summary>
    /// Логика взаимодействия для Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        string dot;
        bool b = false;
        public Window1()
        {
            InitializeComponent();
            try
            {
                Process process = Process.Start(new ProcessStartInfo
                {
                    FileName = "cmd",
                    Arguments = "/c chcp 65001 & curl \"https://www.anekdot.ru/random/anekdot/\" -o C:/Users/ASUS/Desktop/newtext.txt",
                    UseShellExecute = false,
                    CreateNoWindow = true,
                    RedirectStandardOutput = true
                });
                process.WaitForExit();
                FileStream file = new FileStream("C:/Users/ASUS/Desktop/newtext.txt", FileMode.Open);
                StreamReader readFile = new StreamReader(file);

                string[] words = readFile.ReadToEnd().ToString().Split();

                foreach (string word in words)
                {
                    if (word.Contains("class=\"text\""))
                        b = true;
                    if (b)
                    {
                        dot += " " + word;
                        if (word.Contains("<div"))
                        {
                            b = false;
                            break;
                        }
                    }
                }
                dot = dot.Remove(0, 14);
                dot = dot.Remove(dot.Length - 10, 10);
                dot = dot.Replace("<br>", "\n");
                anek.Text = dot;
            }
            catch(Exception)
            { MessageBox.Show("Что-то пошло не так..."); }
        }
    }
}
