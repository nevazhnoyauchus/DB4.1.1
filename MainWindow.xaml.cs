using database2.Pages;
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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace database2
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            BaseClass.Base = new Entities();
            FrameClass.FrameMain = MainFrame;
            
            FrameClass.FrameMain.Navigate(new AutoPage());

            DoubleAnimation BHA = new DoubleAnimation();
            BHA.From = 15;
            BHA.To = 30;
            BHA.Duration = TimeSpan.FromSeconds(1.5);
            BHA.RepeatBehavior = RepeatBehavior.Forever;
            BHA.AutoReverse = true;
            BtnAdv.BeginAnimation(HeightProperty, BHA);

        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            FrameClass.FrameMain.Navigate(new RegPage());
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
           
            FrameClass.FrameMain.Navigate(new AdvPage());
        }
    }
}
