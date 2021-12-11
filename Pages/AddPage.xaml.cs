using Microsoft.Win32;
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
    /// Логика взаимодействия для AddPage.xaml
    /// </summary>
    public partial class AddPage : Page
    {
        private Users _user;
        string path;
        PetPass PET = new PetPass();
        bool flag;
        List<PetCharacter> PetCharacterList = BaseClass.Base.PetCharacter.ToList();
        List<PetFood> PetFoodList = BaseClass.Base.PetFood.ToList();
        public AddPage(Users User)
        {
            InitializeComponent();
            _user = User;
            LBType.ItemsSource = BaseClass.Base.Animals.ToList();
            LBType.SelectedValuePath = "animalsID";
            LBType.DisplayMemberPath = "name";
            LBTraits.ItemsSource = BaseClass.Base.PetTraits.ToList();
            LBTraits.SelectedValuePath = "charID";
            LBTraits.DisplayMemberPath = "charname";
            LBDiets.ItemsSource = BaseClass.Base.FoodFirm.ToList();
            flag = true;

        }
        public AddPage(PetPass PetUpdate)
        {
            InitializeComponent();
            //обновление
            PET = PetUpdate;  // ассоциируем выше созданный глобавльный объект с объектом в кострукторе для дальнейшего редактирования этих данных
            TBPetName.Text = PET.petname; // вывод имени кота
            TBPetDate.Text = PET.birthdate;
            TBPetPrice.Text = PET.price.ToString();

            LBType.ItemsSource = BaseClass.Base.Animals.ToList();
            LBType.SelectedValuePath = "animalsID";
            LBType.DisplayMemberPath = "name";

            Animals petType = BaseClass.Base.Animals.Where(x => x.animalsID == PET.animalsID).FirstOrDefault();
            if (petType != null)
            {
                LBType.SelectedItem = petType;
            }

            switch (PetUpdate.petGenderID)  // вывод пола
            {
                case 1:
                    RBGenderM.IsChecked = true;
                    break;
                case 2:
                    RBGenderW.IsChecked = true;
                    break;
            }

            LBTraits.ItemsSource = BaseClass.Base.PetTraits.ToList();  // ассоциируем коллекцию списка с чертами характера с соответствующей таблицей БД (Traits)
            LBTraits.SelectedValuePath = "charID";
            LBTraits.DisplayMemberPath = "charname";
            List<PetCharacter> TC = PetCharacterList.Where(x => x.petID == PetUpdate.petID).ToList();  // находим черты характера того кота, которого мы редактируем
            //цикл для выделения черт характера кота в общем списке:
            foreach (PetTraits t in LBTraits.Items)
            {
                if (TC.FirstOrDefault(x => x.charID == t.charID) != null)
                {
                    LBTraits.SelectedItems.Add(t);
                }
            }

            LBDiets.ItemsSource = BaseClass.Base.FoodFirm.ToList();  // ассоциируем коллекцию списка с кормами кота с соответствующей таблицей БД (Diets)
            List<PetFood> D = PetFoodList.Where(x => x.petID == PetUpdate.petID).ToList();  // находим корма для того кота, которого мы редактируем
            // цикл для отображения кормов и их количества для кота:
            foreach (FoodFirm f in LBDiets.Items)
            {
                PetFood obj = D.FirstOrDefault(x => x.firmID == f.firmID);
                if (obj != null)
                {
                    f.kolMonth = obj.kolMonth;
                }
            }
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog OFD = new OpenFileDialog();
            OFD.ShowDialog();
            path = OFD.FileName;
            int n = path.IndexOf("PetPhoto");
            path = path.Substring(n);
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            //try
            //{
            int gender = 0;
            if (RBGenderM.IsChecked == true)
            {
                gender = 1;
            }
            if (RBGenderW.IsChecked == true)
            {
                gender = 2;

            }
            int idAnimal = LBType.SelectedIndex + 1;

            Animals petType = (Animals)LBType.SelectedItem;
            if (petType != null)
            {
                PET.animalsID = petType.animalsID;
            }
            PET.petname = TBPetName.Text;
            PET.price = Convert.ToInt32(TBPetPrice.Text);
            PET.birthdate = TBPetDate.Text;
            PET.petGenderID = gender;
            if (path == "" || path == null)
            {
                path = "Resources\\а.png";
            }
            PET.photo = "\\" + path ;
            
            if (flag == true)
            {
                BaseClass.Base.PetPass.Add(PET);
            }
            BaseClass.Base.SaveChanges();

            

            List<PetCharacter> LTC = PetCharacterList.Where(x => x.petID == PET.petID).ToList();  // находим список черт характера кота
            if (LTC.Count != 0)  // если список не пустой, удаляем из него все черты характера  этого кота
            {
                foreach (PetCharacter tc in LTC)
                {
                    BaseClass.Base.PetCharacter.Remove(tc);
                }
            }

            foreach (PetTraits PT in LBTraits.SelectedItems)
            {
                PetCharacter PC = new PetCharacter();
                PC.petID = PET.petID;
                PC.charID = PT.charID;
                BaseClass.Base.PetCharacter.Add(PC);
            }
            BaseClass.Base.SaveChanges();

            
            List<PetFood> LD = PetFoodList.Where(x => x.petID == PET.petID).ToList();  // находим список с кормами для кота
            if (LD.Count != 0)  // если список не пустой, удаляем из него все корма для  этого кота
            {
                foreach (PetFood d in LD)
                {
                    BaseClass.Base.PetFood.Remove(d);
                }
            }
            foreach (FoodFirm FF in LBDiets.Items)
            {
                PetFood PF = new PetFood();
                PF.firmID = FF.firmID;
                PF.petID = PET.petID;
                PF.kolMonth = FF.kolMonth;
                if (FF.kolMonth != 0)
                {
                    BaseClass.Base.PetFood.Add(PF);
                }
            }
            BaseClass.Base.SaveChanges();

            MessageBox.Show("Данные записаны", "Запись");
            FrameClass.FrameMain.Navigate(new ListAnimalsPage(_user));

            //}
            //catch
            //{
            //    MessageBox.Show("Данные не записаны","Ошибка");
            //}
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            FrameClass.FrameMain.Navigate(new ListAnimalsPage(_user));
        }
    }
}
