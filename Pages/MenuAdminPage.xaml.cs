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
    /// Логика взаимодействия для MenuAdminPage.xaml
    /// </summary>
    public partial class MenuAdminPage : Page
    {
        private Users _user;
        public MenuAdminPage(Users User)
        {
            InitializeComponent();
            DgUsers.ItemsSource = BaseClass.Base.Users.ToList();
            _user = User;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            FrameClass.FrameMain.Navigate(new AdminNavigatePage(_user));
        }
    }
}
