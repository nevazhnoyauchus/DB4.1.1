using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Drawing;
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

namespace database2.Pages
{
    /// <summary>
    /// Логика взаимодействия для UserPage.xaml
    /// </summary>
    
    public partial class UserPage : Page
    {
        private Users _user;
        private string _path;
        private UsersPhoto UP;  // пустой объект для добавления изображения в таблицу UsersPhoto

        public UserPage(Users User)
        {
            InitializeComponent();
            _user = User;
            TBUserName.Text = _user.name;
            TBUserSurname.Text = _user.surname;

            if (User.UsersPhoto != null && User.UsersPhoto.PhotoBinary != null)
            {
                byte[] BArr = User.UsersPhoto.PhotoBinary;  // считываем изображение из базы (считываем байтовый массив двоичных данных)
                BitmapImage BI = new BitmapImage();  // создаем объект для загрузки изображения
                using (MemoryStream MS = new MemoryStream(BArr))  // для считывания байтового потока
                {
                    BI.BeginInit();  // начинаем считывание
                    BI.StreamSource = MS;  // задаем источник потока
                    BI.CacheOption = BitmapCacheOption.OnLoad;  // переводим изображение
                    BI.EndInit();  // заканчиваем считывание
                }
                UserPhotoImage.Source = BI;  // показываем картинку на экране
            }
        }



        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            UpdateWindow upWin = new UpdateWindow(_user);  // создаем окно для редактирования данных
            upWin.ShowDialog();
            FrameClass.FrameMain.Navigate(new UserPage(_user));  // перезагружаем страницу
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            UpdateWindowPass upWinPass = new UpdateWindowPass(_user);  // создаем окно для редактирования данных
            upWinPass.ShowDialog();
            FrameClass.FrameMain.Navigate(new AutoPage());  // перезагружаем страницу
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            try
            {
                UsersPhoto U = BaseClass.Base.UsersPhoto.FirstOrDefault(x => x.IDUser == _user.idUser);
                if (U == null)
                {
                    UP = new UsersPhoto();
                    UP.IDUser = _user.idUser;
                    OpenFileDialog OFD = new OpenFileDialog();
                    OFD.ShowDialog();
                    _path = OFD.FileName;
                    System.Drawing.Image SDI = System.Drawing.Image.FromFile(_path);
                    ImageConverter IC = new ImageConverter();
                    byte[] Barray = (byte[])IC.ConvertTo(SDI, typeof(byte[]));
                    UP.PhotoBinary = Barray;
                    BaseClass.Base.UsersPhoto.Add(UP);
                    BaseClass.Base.SaveChanges();
                    MessageBox.Show("Картинка добавлена");
                }
                else  // если у пользователя уже было изображение 
                {

                    OpenFileDialog OFD = new OpenFileDialog();
                    OFD.ShowDialog();
                    _path = OFD.FileName;
                    System.Drawing.Image SDI = System.Drawing.Image.FromFile(_path);
                    ImageConverter IC = new ImageConverter();
                    byte[] Barray = (byte[])IC.ConvertTo(SDI, typeof(byte[]));
                    U.PhotoBinary = Barray;
                    BaseClass.Base.SaveChanges();
                    MessageBox.Show("Картинка изменена");
                }

                FrameClass.FrameMain.Navigate(new UserPage(_user));  // перезагружаем страницу
            }
            catch { MessageBox.Show("Изображение не выбрано.", "Внимание!"); }
            }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            FrameClass.FrameMain.Navigate(new AutoPage());
        }
    }
}
