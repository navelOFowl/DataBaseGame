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
    /// Логика взаимодействия для PageConnect.xaml
    /// </summary>
    public partial class PageConnect : Page
    {
        //string ConnPath = "connections.csv";
        string ConnPathBin = "";
        int TaskNum = 0;
        List<ConnFill> fill = new List<ConnFill>();
        public PageConnect()
        {
            InitializeComponent();
            TBTime.Text = DateTime.Now.ToString("HH:mm");

            OpenFileDialog dlg = new OpenFileDialog();
            dlg.InitialDirectory = Environment.CurrentDirectory;
            bool IsChose = (bool)dlg.ShowDialog();
            if (IsChose)
            {
                ConnPathBin = dlg.FileName;
            }
            else
            {
                MessageBox.Show("Файл не выбран", "Ошибка");
                FrameClass.MainFrame.Navigate(new PageMenu());
                return;
            }
            try
            {

                using (BinaryReader sr = new BinaryReader(File.Open(ConnPathBin, FileMode.Open)))
                {
                    int i = 0;
                    while (sr.PeekChar() > -1)
                    {
                        fill.Add(new ConnFill());
                        fill[i].Task = sr.ReadString();
                        fill[i].First1 = sr.ReadString();
                        fill[i].First2 = sr.ReadString();
                        fill[i].First3 = sr.ReadString();
                        fill[i].Second1 = sr.ReadString();
                        fill[i].Second2 = sr.ReadString();
                        fill[i].Second3 = sr.ReadString();
                        fill[i].FirstCorrect = sr.ReadString();
                        fill[i].SecondCorrect = sr.ReadString();
                        i++;
                    }
                }
            }
            catch
            {
                MessageBox.Show("Файл может быть поврежден", "Ошибка");
            }

            //using (StreamReader sr = new StreamReader(ConnPath))
            //{
            //    while (sr.EndOfStream != true)
            //    {
            //        string[] arr = sr.ReadLine().Split(';');
            //        fill.Add(new ConnFill { Task = arr[0], First1 = arr[1], First2 = arr[2], First3 = arr[3], Second1 = arr[4], Second2 = arr[5], Second3 = arr[6], FirstCorrect = arr[7], SecondCorrect = arr[8] });
            //    }
            //}
            Fill();
        }

        private void Fill()
        {
            LBFirst.Items.Clear();
            LBSecond.Items.Clear();
            TBTask.Text = fill[TaskNum].Task;
            LBFirst.Items.Add(fill[TaskNum].First1);
            LBFirst.Items.Add(fill[TaskNum].First2);
            LBFirst.Items.Add(fill[TaskNum].First3);
            LBSecond.Items.Add(fill[TaskNum].Second1);
            LBSecond.Items.Add(fill[TaskNum].Second2);
            LBSecond.Items.Add(fill[TaskNum].Second3);
        }

        private void LB_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Drawing();
        }

        private void Drawing()
        {
            switch (LBFirst.SelectedIndex)
            {
                case 0:
                    Connection.X1 = 260;
                    Connection.Y1 = 45;
                    break;
                case 1:
                    Connection.X1 = 260;
                    Connection.Y1 = 80;
                    break;
                case 2:
                    Connection.X1 = 260;
                    Connection.Y1 = 115;
                    break;
                default:
                    Connection.X1 = 650;
                    Connection.Y1 = 110;
                    break;
            }

            switch (LBSecond.SelectedIndex)
            {
                case 0:
                    Connection.X2 = 650;
                    Connection.Y2 = 110;                 
                    Connection.StrokeThickness = 4;
                    Connection.Stroke = Brushes.Black;
                    break;
                case 1:
                    Connection.X2 = 650;
                    Connection.Y2 = 140;
                    Connection.StrokeThickness = 4;
                    Connection.Stroke = Brushes.Black;
                    break;
                case 2:
                    Connection.X2 = 650;
                    Connection.Y2 = 175;
                    Connection.StrokeThickness = 4;
                    Connection.Stroke = Brushes.Black;
                    break;
                default:
                    Connection.X2 = Connection.X1;
                    Connection.Y2 = Connection.Y1;
                    break;
            }
        }

        int taskCount = 0;
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if(LBFirst.SelectedItem == null || LBSecond.SelectedItem == null)
            {
                MessageBox.Show("Выберете поля", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (LBFirst.SelectedItem.ToString() == fill[TaskNum].FirstCorrect && LBSecond.SelectedItem.ToString() == fill[TaskNum].SecondCorrect)
            {
                MessageBox.Show("Готово!", "Связи", MessageBoxButton.OK, MessageBoxImage.Information);
                TaskNum++;
                Connection.X1 = 0;
                Connection.Y1 = 0;
                Connection.X2 = 0;
                Connection.Y2 = 0;
            }
            else
            {
                if (MessageBox.Show("Неверно. Продемонстрировать правильный ответ?", "Связи", MessageBoxButton.OKCancel, MessageBoxImage.Information) == MessageBoxResult.OK)
                {
                    DrawingCorrect();
                    return;
                }
            }
            taskCount++;
            if (taskCount == 10)
            {
                MessageBox.Show("Все задания успешно пройдены!", "Финиш!", MessageBoxButton.OK, MessageBoxImage.Asterisk);
                FrameClass.MainFrame.Navigate(new PageMenu());
                return;
            }
            Fill();
        }

        private void DrawingCorrect()
        {
            int i = 0;
            foreach (var item in LBFirst.Items)
            {
                if (item.ToString() == fill[TaskNum].FirstCorrect)
                {
                    LBFirst.SelectedIndex = i;
                }
                i++;
            }
            i = 0;
            foreach (var item in LBSecond.Items)
            {
                if (item.ToString() == fill[TaskNum].SecondCorrect)
                {
                    LBSecond.SelectedIndex = i;
                }
                i++;
            }
            switch (LBFirst.SelectedIndex)
            {
                case 0:
                    Connection.X1 = 260;
                    Connection.Y1 = 45;
                    break;
                case 1:
                    Connection.X1 = 260;
                    Connection.Y1 = 80;
                    break;
                case 2:
                    Connection.X1 = 260;
                    Connection.Y1 = 115;
                    break;
            }

            switch (LBSecond.SelectedIndex)
            {
                case 0:
                    Connection.X2 = 650;
                    Connection.Y2 = 110;
                    Connection.StrokeThickness = 4;
                    Connection.Stroke = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#070770"));
                    break;
                case 1:
                    Connection.X2 = 650;
                    Connection.Y2 = 140;
                    Connection.StrokeThickness = 4;
                    Connection.Stroke = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#070770"));
                    break;
                case 2:
                    Connection.X2 = 650;
                    Connection.Y2 = 175;
                    Connection.StrokeThickness = 4;
                    Connection.Stroke = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#070770"));
                    break;
            }
        }

        private void ButBackMenu_Click(object sender, RoutedEventArgs e)
        {
            FrameClass.MainFrame.Navigate(new PageMenu());
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}
