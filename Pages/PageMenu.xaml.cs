using System;
using System.Collections.Generic;
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
    /// Логика взаимодействия для PageMenu.xaml
    /// </summary>
    public partial class PageMenu : Page
    {
        public PageMenu()
        {
            InitializeComponent();
            TBTime.Text = DateTime.Now.ToString("HH:mm");
        }

        private void ButQuery_Click(object sender, RoutedEventArgs e)
        {
            FrameClass.MainFrame.Navigate(new PageQuery());
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            FrameClass.MainFrame.Navigate(new PageTest());
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            FrameClass.MainFrame.Navigate(new PageConnect());
        }
    }
}
