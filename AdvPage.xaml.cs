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
    /// Логика взаимодействия для AdvPage.xaml
    /// </summary>
    public partial class AdvPage : Page
    {
        public AdvPage()
        {
            InitializeComponent();
            //котик
            DoubleAnimation WA = new DoubleAnimation();
            WA.From = 200;
            WA.To = 195;
            WA.Duration = TimeSpan.FromSeconds(0.5);
            WA.RepeatBehavior = RepeatBehavior.Forever;
            WA.AutoReverse = true;
            CatImg.BeginAnimation(WidthProperty, WA);

            DoubleAnimation HA = new DoubleAnimation();
            HA.From = 150;
            HA.To = 145;
            HA.Duration = TimeSpan.FromSeconds(0.5);
            HA.RepeatBehavior = RepeatBehavior.Forever;
            HA.AutoReverse = true;
            CatImg.BeginAnimation(HeightProperty, HA);

            //кнопка 
            ColorAnimation BBA = new ColorAnimation();
            Color Bstart = Color.FromRgb(255, 255, 255);
            BtnList.Background = new SolidColorBrush(Bstart);
            BBA.From = Bstart;
            BBA.To = Color.FromRgb(255, 192, 203);
            BBA.Duration = TimeSpan.FromSeconds(0.5);
            BBA.RepeatBehavior = RepeatBehavior.Forever;
            BBA.AutoReverse = true;
            BtnList.Background.BeginAnimation(SolidColorBrush.ColorProperty, BBA);

            DoubleAnimation BHA = new DoubleAnimation();
            BHA.From = 50;
            BHA.To = 70;
            BHA.Duration = TimeSpan.FromSeconds(0.5);
            BHA.RepeatBehavior = RepeatBehavior.Forever;
            BHA.AutoReverse = true;
            BtnList.BeginAnimation(HeightProperty, BHA);

            DoubleAnimation BWA = new DoubleAnimation();
            BWA.From = 438;
            BWA.To = 430;
            BWA.Duration = TimeSpan.FromSeconds(0.5);
            BWA.RepeatBehavior = RepeatBehavior.Forever;
            BWA.AutoReverse = true;
            BtnList.BeginAnimation(WidthProperty, BWA);

            //надпись
            DoubleAnimation TFA = new DoubleAnimation();
            TFA.From = 32;
            TFA.To = 40;
            TFA.Duration = TimeSpan.FromSeconds(0.5);
            TFA.RepeatBehavior = RepeatBehavior.Forever;
            TFA.AutoReverse = true;
            Text2.BeginAnimation(TextBlock.FontSizeProperty, TFA);

            //сердечко
            ColorAnimation TCA = new ColorAnimation();
            Color Cstart = Color.FromRgb(255, 255, 255);
            Text3.Foreground = new SolidColorBrush(Cstart);
            TCA.From = Cstart;
            TCA.To = Color.FromRgb(255, 192, 203);
            TCA.Duration = TimeSpan.FromSeconds(0.5);
            TCA.RepeatBehavior = RepeatBehavior.Forever;
            TCA.AutoReverse = true;
            Text3.Foreground.BeginAnimation(SolidColorBrush.ColorProperty, TCA);
        }

        private void BtnList_Click(object sender, RoutedEventArgs e)
        {
            FrameClass.FrameMain.Navigate(new Pages.AutoPage());
        }
    }
}
