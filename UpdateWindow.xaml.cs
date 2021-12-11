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
using System.Windows.Shapes;

namespace database2
{
    /// <summary>
    /// Логика взаимодействия для UpdateWindow.xaml
    /// </summary>
    public partial class UpdateWindow : Window
    {
        private Users _user;
        public UpdateWindow(Users User)
        {
            InitializeComponent();
            _user = User;
            TBUpName.Text = _user.name;
            TBUpSurname.Text = _user.surname;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            _user.name = TBUpName.Text;
            _user.surname = TBUpSurname.Text;
            BaseClass.Base.SaveChanges();
            MessageBox.Show("Изменения сохранены");
            this.Close();
        }
    }
}
