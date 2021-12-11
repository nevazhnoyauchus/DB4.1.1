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
    /// Логика взаимодействия для AutoPage.xaml
    /// </summary>
    public partial class AutoPage : Page
    {
        public AutoPage()
        {
            InitializeComponent();
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {

            int paswordCode = TbPasAvto.Password.GetHashCode();
            Users User = BaseClass.Base.Users.FirstOrDefault(z => z.login == TbLoginAvto.Text && z.password == paswordCode);
            if (User == null)
            {
                MessageBox.Show("Несуществующий логин или пароль.");
            }
            else
            {
                switch (User.idRole)
                {
                    case 1:
                        MessageBox.Show("Здравствуйте, администратор " + User.name);
                        FrameClass.FrameMain.Navigate(new AdminNavigatePage(User));
                        break;
                    case 2:
                        MessageBox.Show("Здравствуйте, пользователь " + User.name);
                        FrameClass.FrameMain.Navigate(new UserPage(User));
                        break;
                }
            }
        }
    }
}
