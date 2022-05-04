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

        string TaskPath = "tasks.csv";
        int TaskNumber = 0;
        string QueryPath = "queries.csv";
        int QueryNumber = 0;
        string GridPath = "gridfill.csv";
        int GridNumber = 1;

        string[] Tasks;
        string[] Queries;
        List<GridFill> fill = new List<GridFill>();

        public PageQuery()
        {  
            InitializeComponent();
            using (StreamReader sr = new StreamReader(TaskPath))
            {
                while (sr.EndOfStream != true)
                {
                    Tasks = sr.ReadLine().Split(';');
                }
            }
            using (StreamReader sr = new StreamReader(QueryPath))
            {
                while (sr.EndOfStream != true)
                {
                    Queries = sr.ReadLine().Split(';');
                }
            }
            TbTask.Text = Tasks[0];
            cFn.Header = "Имя";
            cSn.Header = "Фамилия";
            cBd.Header = "Дата рождения";
            cFn.Binding = new Binding("Fn");
            cSn.Binding = new Binding("Sn");
            cBd.Binding = new Binding("Bd");
            DgResult.Columns.Add(cFn);
            DgResult.Columns.Add(cSn);
            DgResult.Columns.Add(cBd);
            GridReader();
            DgResult.ItemsSource = fill;
            //TbQuery.Text = "SELECT * FROM Customers \nWHERE ...";
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
                        fill.Add(new GridFill { Fn = arr[1], Sn = arr[2], Bd = arr[3] });
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

        }
        private void ButExecute_Click(object sender, RoutedEventArgs e)
        {
            string queryError = "";
            if(TaskNumber == 10)
            {
                MessageBox.Show("Заданий больше нет" + queryError, "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            if (TbQuery.Text != Queries[QueryNumber])
            {
                if (TbQuery.Text.ToLower().Contains("where") != true)
                {
                    queryError = "отсутствует оператор WHERE";
                }
                if (TbQuery.Text.ToLower().Contains("from") != true)
                {
                    queryError = "отсутствует оператор FROM";
                }
                if (TbQuery.Text.ToLower().Contains("select") != true)
                {
                    queryError = "отсутствует оператор SELECT";
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
            QueryNumber++;
            TaskNumber++;
            if(TaskNumber == 10)
            {
                MessageBox.Show("Позравляем! Все задания успешно пройдены", "Финиш!", MessageBoxButton.OK, MessageBoxImage.Information);
                TbTask.Text = "";
                return;
            }
            TbTask.Text = Tasks[TaskNumber];
        }
    }
}
