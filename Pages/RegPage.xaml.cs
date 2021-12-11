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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace database2.Pages
{
    /// <summary>
    /// Логика взаимодействия для RegPage.xaml
    /// </summary>
    public partial class RegPage : Page
    {
        
        public RegPage()
        {
            
            InitializeComponent();
            CbGenderReg.ItemsSource = BaseClass.Base.Gender.ToList();
            CbGenderReg.SelectedValuePath = "idGender";
            CbGenderReg.DisplayMemberPath = "gender1";
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string passCheck = TbPasReg.Password.ToString();
            if (passCheck.Length < 8) { MessageBox.Show("Длина пароля должна быть не менее 8-ми символов. Повторите ввод.", "Внимание!");return; }
           //загл буква
            Regex LAT = new Regex("(?=[A-Z])");
            bool LATmatch = LAT.IsMatch(passCheck);
            if (LATmatch != true) { MessageBox.Show("В пароле должна быть как минимум одна латинская заглавная буква. Повторите ввод.", "Внимание!"); return; }
            //3 строчн 
            Regex LATkol = new Regex("(?=[a-z])");
            MatchCollection LATkolmatch = LATkol.Matches(passCheck);
            if(LATkolmatch.Count < 3 ) { MessageBox.Show("В пароле должно быть как минимум 3 строчных латинскаих букв. Повторите ввод.", "Внимание!"); return; }
            //цифры
            Regex num = new Regex("(?=[0-9])");
            MatchCollection numMatch = num.Matches(passCheck);
            if (numMatch.Count < 2) {MessageBox.Show("В пароле должно быть как минимум 2 цифры. Повторите ввод.", "Внимание!"); return;}
            //спец символы
            Regex spec = new Regex("(?=[#?!@$%^&*`~_-])");
            bool specMatch = spec.IsMatch(passCheck);
            if (specMatch != true) { MessageBox.Show("В пароле должен быть как минимум один специальный символ. Повторите ввод.", "Внимание!"); return; }
            int pasGegCode = TbPasReg.Password.GetHashCode();
            Users UserRez = new Users()
            {   name = TbNameReg.Text,
                surname = TbSurnameReg.Text, 
                login = TbLoginReg.Text, 
                idGender = CbGenderReg.SelectedIndex + 1,
                idRole = 2,
                password = pasGegCode
            };
            BaseClass.Base.Users.Add(UserRez);
            BaseClass.Base.SaveChanges();
            MessageBox.Show("Регистрация прошла успешно!");
            FrameClass.FrameMain.Navigate(new AutoPage());
        }
    }
}
