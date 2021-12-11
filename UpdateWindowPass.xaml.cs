using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
    /// Логика взаимодействия для UpdateWindowPass.xaml
    /// </summary>
    public partial class UpdateWindowPass : Window
    {
        private Users _user;
        public UpdateWindowPass(Users User)
        {
            InitializeComponent();
            _user = User;
        }

        private void UpdateInfo_Click(object sender, RoutedEventArgs e)
        {
            _user.login = TBUpdateLogin.Text;
            if (TBUpdatePassN.Password.ToString() != "")
            {
                if (TBUpdatePassN.Password.ToString() != TBUpdatePassNA.Password.ToString())
                {
                    MessageBox.Show("Пароли не совпадают!", "Ошибка!");
                }
                int c = 0;
                Regex r = new Regex("^.*(?=.*[0-9]{2}).*$");
                if (r.IsMatch(TBUpdatePassN.Password.ToString()) == false)
                {
                    MessageBox.Show("Слабый пароль! Менее 2-ух чисел.", "Регистрация");
                }
                else { c += 1; }
                r = new Regex("^.*(?=.*[A-Z]).*$");
                if (r.IsMatch(TBUpdatePassN.Password.ToString()) == false)
                {
                    MessageBox.Show("Слабый пароль! Менее 1-го заглавного символа латинского алфавита.", "Регистрация");
                }
                else { c += 1; }
                r = new Regex("^.*(?=.*[a-z]{3}).*$");
                if (r.IsMatch(TBUpdatePassN.Password.ToString()) == false)
                {
                    MessageBox.Show("Слабый пароль! Менее 3-ёх символов латинского алфавита", "Регистрация");
                }
                else { c += 1; }
                r = new Regex("^.*(?=.*[!@#$%^&*()?+=]).*$");
                if (r.IsMatch(TBUpdatePassN.Password.ToString()) == false)
                {
                    MessageBox.Show("Слабый пароль! Менее 1-го специального символа.", "Регистрация");
                }
                else { c += 1; }
                r = new Regex("^.*(?=.{8,}).*$");
                if (r.IsMatch(TBUpdatePassN.Password.ToString()) == false)
                {
                    MessageBox.Show("Слабый пароль! Менее 8-ми символов.", "Регистрация");
                }
                else { c += 1; }
                if (c == 5)
                {
                    int pasCode = TBUpdatePassN.Password.GetHashCode();
                    _user.password = pasCode;
                    BaseClass.Base.SaveChanges();
                    MessageBox.Show("Данные обновлены", "Обновление");
                    this.Close();
                }
            }
            else
            {
                _user.password = _user.password;
                BaseClass.Base.SaveChanges();
                MessageBox.Show("Данные обновлены", "Обновление");
                this.Close();
            }
        }
    }
}
