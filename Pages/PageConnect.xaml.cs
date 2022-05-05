using DataBaseGame.Classes;
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
        string ConnPath = "connections.csv";
        int TaskNum = 0;
        List<ConnFill> fill = new List<ConnFill>();
        public PageConnect()
        {
            InitializeComponent();
            using (StreamReader sr = new StreamReader(ConnPath))
            {
                while (sr.EndOfStream != true)
                {
                    string[] arr = sr.ReadLine().Split(';');
                    fill.Add(new ConnFill { Task = arr[0], First1 = arr[1], First2 = arr[2], First3 = arr[3], Second1 = arr[4], Second2 = arr[5], Second3 = arr[6], FirstCorrect = arr[7], SecondCorrect = arr[8] });
                }
            }
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
                    Connection.Y1 = 155;
                    break;
                case 1:
                    Connection.X1 = 260;
                    Connection.Y1 = 190;
                    break;
                case 2:
                    Connection.X1 = 260;
                    Connection.Y1 = 225;
                    break;
                default:
                    Connection.X1 = 540;
                    Connection.Y1 = 150;
                    break;
            }

            switch (LBSecond.SelectedIndex)
            {
                case 0:
                    Connection.X2 = 540;
                    Connection.Y2 = 155;                 
                    Connection.StrokeThickness = 4;
                    Connection.Stroke = new SolidColorBrush((Color) ColorConverter.ConvertFromString("#A7CBE9"));
                    break;
                case 1:
                    Connection.X2 = 540;
                    Connection.Y2 = 190;
                    Connection.StrokeThickness = 4;
                    Connection.Stroke = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#A7CBE9"));
                    break;
                case 2:
                    Connection.X2 = 540;
                    Connection.Y2 = 225;
                    Connection.StrokeThickness = 4;
                    Connection.Stroke = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#A7CBE9"));
                    break;
                default:
                    Connection.X2 = Connection.X1;
                    Connection.Y2 = Connection.Y1;
                    break;
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if(LBFirst.SelectedItem.ToString() == fill[TaskNum].FirstCorrect && LBSecond.SelectedItem.ToString() == fill[TaskNum].SecondCorrect)
            {
                MessageBox.Show("Все ок");
                TaskNum++;
                //Connection.X1 = 560;
                //Connection.Y1 = 155;
                Connection.X1 = 0;
                Connection.Y1 = 0;
                Connection.X2 = 0;
                Connection.Y2 = 0;
            }
            else
            {
                if(MessageBox.Show("Не ок. Показать правильно?", "Ошибка", MessageBoxButton.OKCancel) == MessageBoxResult.OK)
                {
                    DrawingCorrect();
                    return;
                }
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
                    Connection.Y1 = 155;
                    break;
                case 1:
                    Connection.X1 = 260;
                    Connection.Y1 = 190;
                    break;
                case 2:
                    Connection.X1 = 260;
                    Connection.Y1 = 225;
                    break;
            }

            switch (LBSecond.SelectedIndex)
            {
                case 0:
                    Connection.X2 = 540;
                    Connection.Y2 = 155;
                    Connection.StrokeThickness = 4;
                    Connection.Stroke = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFD395"));
                    break;
                case 1:
                    Connection.X2 = 540;
                    Connection.Y2 = 190;
                    Connection.StrokeThickness = 4;
                    Connection.Stroke = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFD395"));
                    break;
                case 2:
                    Connection.X2 = 540;
                    Connection.Y2 = 225;
                    Connection.StrokeThickness = 4;
                    Connection.Stroke = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFD395"));
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
