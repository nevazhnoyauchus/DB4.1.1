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
    /// Логика взаимодействия для ListAnimalsPage.xaml
    /// </summary>
    public partial class ListAnimalsPage : Page
    {
        private Users _user;
        List<PetPass> PetsStart = BaseClass.Base.PetPass.ToList();
        PageChange pc = new PageChange();
        public ListAnimalsPage(Users User)
        {
            InitializeComponent();
            LVAnimals.ItemsSource = PetsStart;
            LVAnimals.ItemsSource = BaseClass.Base.PetPass.ToList();
            _user = User;

            //сортировка
            List<PetGender> GB = BaseClass.Base.PetGender.ToList();
            CBFilter.Items.Add("Все");
            for (int i = 0; i < GB.Count(); i++)
            {
                CBFilter.Items.Add(GB[i].petGender1);
            }
            CBFilter.SelectedIndex = 0;
            DataContext = pc;

        }

        private void TextBlock_Loaded(object sender, RoutedEventArgs e)
        {
            TextBlock tb = (TextBlock)sender;
            int index = Convert.ToInt32(tb.Uid);
            List<PetCharacter> TC = BaseClass.Base.PetCharacter.Where(x => x.petID == index).ToList();
            string str = "";
            foreach (PetCharacter item in TC)
            {
                str += item.PetTraits.charname + ", ";
            }
            tb.Text = str.Substring(0, str.Length - 2);
        }

        private void TextBlock_Loaded_1(object sender, RoutedEventArgs e)
        {
            TextBlock tb = (TextBlock)sender;
            int index = Convert.ToInt32(tb.Uid);
            List<PetFood> TC = BaseClass.Base.PetFood.Where(x => x.petID == index).ToList();
            int sum = 0;
            foreach (PetFood item in TC)
            {
                sum += item.kolMonth * item.FoodFirm.priceFood;
            }
            tb.Text = sum + " руб. уйдёт на пропитание в среднем в месяц";
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            FrameClass.FrameMain.Navigate(new AdminNavigatePage(_user));
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            Button B = (Button)sender;
            int id = Convert.ToInt32(B.Uid);
            PetPass PetPassDelete = BaseClass.Base.PetPass.FirstOrDefault(x => x.petID == id);
            BaseClass.Base.PetPass.Remove(PetPassDelete);
            BaseClass.Base.SaveChanges();
            FrameClass.FrameMain.Navigate(new ListAnimalsPage(_user));
            MessageBox.Show("Запись удалена!", "Удаление");

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            FrameClass.FrameMain.Navigate(new AddPage(_user));
        }

        private void Update_Click(object sender, RoutedEventArgs e)
        {
            Button B = (Button)sender;  // задаем кнопке имя
            int ind = Convert.ToInt32(B.Uid);  // считываем индекс кнопки, который соответсвует id кота
            PetPass PetUpdate = BaseClass.Base.PetPass.FirstOrDefault(y => y.petID == ind);  // находим кота с соответствующим индексом
            FrameClass.FrameMain.Navigate(new AddPage(PetUpdate));  // переходим на страницу с формой добавления, которую будем использовать и для редактирования
            //Обратите внимание, что конструктор в этом случае не пустой. Он содержит того кота, который соотвествует нужному индексу
        }
        //сортировка

        List<PetPass> PetsFilter;
        private void Filter()
        {
            int index = CBFilter.SelectedIndex;
            if (index != 0)
            {
                PetsFilter = PetsStart.Where(x => x.petGenderID == index).ToList();
            }
            else
            {
                PetsFilter = PetsStart;
            }
            if (!string.IsNullOrWhiteSpace(TBFilter.Text))
            {
                PetsFilter = PetsFilter.Where(x => x.Animals.name.ToString().Contains(TBFilter.Text)).ToList();
            }
            if (ChBFilter.IsChecked == true)
            {
                PetsFilter = PetsFilter.Where(x => x.photo != null).ToList();
            }
            LVAnimals.ItemsSource = PetsFilter;
        }
        private void CBFilter_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Filter();
        }

        private void BtnSortUp_Click(object sender, RoutedEventArgs e)
        {
            PetsFilter.Sort((x, y) => x.petname.CompareTo(y.petname));
            LVAnimals.Items.Refresh();
        }

        private void BtnSortDown_Click(object sender, RoutedEventArgs e)
        {
            PetsFilter.Sort((x, y) => x.petname.CompareTo(y.petname));
            PetsFilter.Reverse();
            LVAnimals.Items.Refresh();
        }

        private void ChBFilter_Checked(object sender, RoutedEventArgs e)
        {
            Filter();
        }

        private void ChBFilter_Unchecked(object sender, RoutedEventArgs e)
        {
            Filter();
        }

        private void TBFilter_TextChanged(object sender, TextChangedEventArgs e)
        {
            Filter();
        }

        private void txtPageCount_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                pc.CountPage = Convert.ToInt32(txtPageCount.Text);
            }
            catch
            {
                pc.CountPage = PetsFilter.Count; 
            }
            pc.Countlist = PetsFilter.Count; 
            LVAnimals.ItemsSource = PetsFilter.Skip(0).Take(pc.CountPage).ToList();  
        }
        private void GoPage_MouseDown(object sender, MouseButtonEventArgs e)  // обработка нажатия на один из Textblock в меню с номерами страниц
        {
            TextBlock tb = (TextBlock)sender;
            switch (tb.Uid)  // определяем, куда конкретно было сделано нажатие
            {
                case "prev":
                    pc.CurrentPage--;
                    break;
                case "next":
                    pc.CurrentPage++;
                    break;
                default:
                    pc.CurrentPage = Convert.ToInt32(tb.Text);
                    break;
            }
            LVAnimals.ItemsSource = PetsFilter.Skip(pc.CurrentPage * pc.CountPage - pc.CountPage).Take(pc.CountPage).ToList();  // оображение записей постранично с определенным количеством на каждой странице
        }
    }
}
