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
using DataBaseGame.Classes;

namespace DataBaseGame.Pages
{
    /// <summary>
    /// Логика взаимодействия для PageQuery.xaml
    /// </summary>
    public partial class PageQuery : Page
    {

        DataGridTextColumn cFn = new DataGridTextColumn();
        DataGridTextColumn cSn = new DataGridTextColumn();
        DataGridTextColumn cBd = new DataGridTextColumn();
        DataGridTextColumn cCl = new DataGridTextColumn();
        DataGridTextColumn cGr = new DataGridTextColumn();

        string TaskQueryPath = "taskqueries.csv";
        int TaskQueryNumber = 0;
        string GridPath = "gridfill.csv";
        int GridNumber = 1;

        List<TaskQuery> taskQueryFill = new List<TaskQuery>();
        List<GridFill> fill = new List<GridFill>();

        public PageQuery()
        {  
            InitializeComponent();
            TBTime.Text = DateTime.Now.ToString("HH:mm");
            using (StreamReader sr = new StreamReader(TaskQueryPath))
            {
                while (sr.EndOfStream != true)
                {
                    string[] arr = sr.ReadLine().Split(';');
                    taskQueryFill.Add(new TaskQuery { Task = arr[0], Query = arr[1]});
                }
            }
            TbTask.Text = taskQueryFill[0].Task;
            cFn.Header = "Имя";
            cSn.Header = "Фамилия";
            cBd.Header = "Дата рождения";
            cFn.Header = "Класс";
            cSn.Header = "Успеваемость";
            cFn.Binding = new Binding("Fn");
            cSn.Binding = new Binding("Sn");
            cBd.Binding = new Binding("Bd");
            cCl.Binding = new Binding("Cl");
            cGr.Binding = new Binding("Gr");
            DgResult.Columns.Add(cFn);
            DgResult.Columns.Add(cSn);
            DgResult.Columns.Add(cBd);
            DgResult.Columns.Add(cCl);
            DgResult.Columns.Add(cGr);
            GridReader();
            DgResult.ItemsSource = fill;
        }
        
        private void GridReader()
        {
            using (StreamReader sr = new StreamReader(GridPath))
            {
                while(sr.EndOfStream != true)
                {
                    string[] arr = sr.ReadLine().Split(';');
                    if (arr[0] == GridNumber.ToString())
                    {
                        fill.Add(new GridFill { Fn = arr[1], Sn = arr[2], Bd = arr[3], Cl = arr[4], Gr = arr[5]});
                    }
                    if (arr[0] == "end" + GridNumber.ToString())
                    {
                        break;
                    }
                }
            }
            DgResult.Items.Refresh(); 
            GridNumber++;
        }

        private void ButBackMenu_Click(object sender, RoutedEventArgs e)
        {
            FrameClass.MainFrame.Navigate(new PageMenu());
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
        private void ButExecute_Click(object sender, RoutedEventArgs e)
        {
            string queryError = "";
            if (TbQuery.Text.ToLower() != taskQueryFill[TaskQueryNumber].Query.ToLower())
            {
                if (taskQueryFill[TaskQueryNumber].Query.ToLower().Contains("where") && TbQuery.Text.ToLower().Contains("where") != true)
                {
                    queryError = "отсутствует оператор WHERE";
                }
                if (taskQueryFill[TaskQueryNumber].Query.ToLower().Contains("from") && TbQuery.Text.ToLower().Contains("from") != true)
                {
                    queryError = "отсутствует оператор FROM";
                }
                if (taskQueryFill[TaskQueryNumber].Query.ToLower().Contains("select") && TbQuery.Text.ToLower().Contains("select") != true)
                {
                    queryError = "отсутствует оператор SELECT";
                }
                if(queryError == "")
                {
                    queryError = "неверные аргументы";
                }
                
                MessageBox.Show("Запрос введен неправильно: " + queryError, "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                DgResult.Visibility = Visibility.Visible;
                ButNext.Visibility = Visibility.Visible;
            }
        }

        private void ButNext_Click(object sender, RoutedEventArgs e)
        {
            fill.Clear();
            DgResult.Visibility = Visibility.Hidden;
            ButNext.Visibility = Visibility.Hidden;
            GridReader();
            TaskQueryNumber++;
            if(TaskQueryNumber == 10)
            {
                MessageBox.Show("Позравляем! Все задания успешно пройдены", "Финиш!", MessageBoxButton.OK, MessageBoxImage.Information);
                FrameClass.MainFrame.Navigate(new PageMenu());
                return;
            }
            TbTask.Text = taskQueryFill[TaskQueryNumber].Task;
        }
    }
}
