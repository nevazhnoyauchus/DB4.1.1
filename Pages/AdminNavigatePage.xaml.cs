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

namespace database2.Pages
{
    /// <summary>
    /// Логика взаимодействия для AdminNavigatePage.xaml
    /// </summary>
    public partial class AdminNavigatePage : Page
    {
        private Users _user;
        public AdminNavigatePage(Users User)
        {
            InitializeComponent();
            _user = User;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            FrameClass.FrameMain.Navigate(new MenuAdminPage(_user));
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            FrameClass.FrameMain.Navigate(new ListAnimalsPage(_user));
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            FrameClass.FrameMain.Navigate(new AutoPage());
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            FrameClass.FrameMain.Navigate(new AddPage(_user));
        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            FrameClass.FrameMain.Navigate(new UserPage(_user));
        }
    }
}
