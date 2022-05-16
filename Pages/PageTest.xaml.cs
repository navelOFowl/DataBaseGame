using DataBaseGame.Classes;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace DataBaseGame.Pages
{
    /// <summary>
    /// Логика взаимодействия для PageTest.xaml
    /// </summary>
    public partial class PageTest : Page
    {
        int CorrectCount = 0;
        List<QuestFill> fill = new List<QuestFill>();
        //string QuestPath = "quests.csv";
        string QuestPathBin = "";
        string CorrectAns = "";
        public PageTest()
        {
            InitializeComponent();
            TBTime.Text = DateTime.Now.ToString("HH:mm");

            OpenFileDialog dlg = new OpenFileDialog();
            dlg.InitialDirectory = Environment.CurrentDirectory;
            bool IsChose = (bool)dlg.ShowDialog();
            if (IsChose)
            {
                QuestPathBin = dlg.FileName;
            }
            else
            {
                MessageBox.Show("Файл не выбран", "Ошибка");
                FrameClass.MainFrame.Navigate(new PageMenu());
                return;
            }
            try
            {

                using (BinaryReader sr = new BinaryReader(File.Open(QuestPathBin, FileMode.Open)))
                {
                    int i = 0;
                    while (sr.PeekChar() > -1)
                    {
                        fill.Add(new QuestFill());
                        fill[i].Quest = sr.ReadString();
                        fill[i].Answer1 = sr.ReadString();
                        fill[i].Answer2 = sr.ReadString();
                        fill[i].Answer3 = sr.ReadString();
                        fill[i].Answer4 = sr.ReadString();
                        i++;
                    }
                }
            }
            catch
            {
                MessageBox.Show("Файл может быть поврежден", "Ошибка");
            }

            //using (StreamReader sr = new StreamReader(QuestPath))
            //{
            //    while (sr.EndOfStream != true)
            //    {
            //        string[] arr = sr.ReadLine().Split(';');
            //        fill.Add(new QuestFill { Quest = arr[0], Answer1 = arr[1], Answer2 = arr[2], Answer3 = arr[3], Answer4 = arr[4] });
            //    }
            //}
            Fill();
        }

        int QuestNum = 0;
        private void Fill()
        {
            Random rndFill = new Random();
            switch (rndFill.Next(1, 4))
            {
                case 1:
                    RbAns1.Content = fill[QuestNum].Answer1;
                    RbAns2.Content = fill[QuestNum].Answer2;
                    RbAns3.Content = fill[QuestNum].Answer3;
                    RbAns4.Content = fill[QuestNum].Answer4;
                    break;
                case 2:
                    RbAns1.Content = fill[QuestNum].Answer2;
                    RbAns2.Content = fill[QuestNum].Answer1;
                    RbAns3.Content = fill[QuestNum].Answer4;
                    RbAns4.Content = fill[QuestNum].Answer3;
                    break;
                case 3:
                    RbAns1.Content = fill[QuestNum].Answer4;
                    RbAns2.Content = fill[QuestNum].Answer2;
                    RbAns3.Content = fill[QuestNum].Answer1;
                    RbAns4.Content = fill[QuestNum].Answer3;
                    break;
                case 4:
                    RbAns1.Content = fill[QuestNum].Answer3;
                    RbAns2.Content = fill[QuestNum].Answer1;
                    RbAns3.Content = fill[QuestNum].Answer4;
                    RbAns4.Content = fill[QuestNum].Answer2;
                    break;
            }
            TbQuest.Text = fill[QuestNum].Quest;
            CorrectAns = fill[QuestNum].Answer4;
        }
        private void ButBackMenu_Click(object sender, RoutedEventArgs e)
        {
            FrameClass.MainFrame.Navigate(new PageMenu());
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void ButAnswer_Click(object sender, RoutedEventArgs e)
        {
            if (RbAns1.IsChecked == true)
            {
                if (RbAns1.Content.ToString() == CorrectAns)
                {
                    CorrectCount++;
                }
                QuestNum++;
            }
            else if (RbAns2.IsChecked == true)
            {
                if (RbAns2.Content.ToString() == CorrectAns)
                {
                    CorrectCount++;
                }
                QuestNum++;
            }
            else if (RbAns3.IsChecked == true)
            {
                if (RbAns3.Content.ToString() == CorrectAns)
                {
                    CorrectCount++;
                }
                QuestNum++;
            }
            else if (RbAns4.IsChecked == true)
            {
                if (RbAns4.Content.ToString() == CorrectAns)
                {
                    CorrectCount++;
                }
                QuestNum++;
            }
            else
            {
                QuestNum++;
            }
            if (QuestNum == 10)
            {
                MessageBox.Show("Правильных ответов: " + CorrectCount + " из 10!", "Тест завершен!", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                FrameClass.MainFrame.Navigate(new PageMenu());
                return;
            }
            Fill();
        }

            
    }
}
