using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace database2
{
    public partial class PetPass
    {
        public string PetGenger
        {
            get
            {
                switch (petGenderID)
                {
                    case 1:
                        return "Мальчик - " + petname;
                    case 2:
                        return "Девочка - " + petname;
                    default:
                        return "Неизвестно" + petname;
                }

            }
        }

        public SolidColorBrush GenColor
        {
            get
            {
                switch (petGenderID)
                {
                    case 1:
                        return Brushes.LightBlue;
                    case 2:
                        return Brushes.LightPink;
                    default:
                        return Brushes.White;
                }
            }
        }
    }
}
