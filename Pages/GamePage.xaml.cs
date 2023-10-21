using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography.Xml;
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

namespace Laba
{
    public partial class GamePage : Page
    {
        public GamePage()
        {
            InitializeComponent();
        }
        private async void MainAnimation(int Column, int Row)
        {
            const int Change = 42;
            Image[,] Positions = { { A1, B1, C1, D1, E1, F1, G1, H1 },
                                   { A2, B2, C2, D2, E2, F2, G2, H2 },
                                   { A3, B3, C3, D3, E3, F3, G3, H3 },
                                   { A4, B4, C4, D4, E4, F4, G4, H4 },
                                   { A5, B5, C5, D5, E5, F5, G5, H5 },
                                   { A6, B6, C6, D6, E6, F6, G6, H6 },
                                   { A7, B7, C7, D7, E7, F7, G7, H7 },
                                   { A8, B8, C8, D8, E8, F8, G8, H8 } };

            DoubleAnimation WidthAnimation = new DoubleAnimation(Positions[Column, Row].Width, Positions[Column, Row].Width-2*Change, TimeSpan.FromSeconds(0.2));
            DoubleAnimation LeftAnimation = new DoubleAnimation(0, Change, TimeSpan.FromSeconds(0.2));
            TranslateTransform Transform = new();
            WidthAnimation.AutoReverse = true;
            LeftAnimation.AutoReverse = true;
            Positions[Column, Row].RenderTransform = Transform;
            Positions[Column, Row].BeginAnimation(WidthProperty, WidthAnimation);
            Transform.BeginAnimation(TranslateTransform.XProperty, LeftAnimation);
            await Task.Delay(200);
            if (Positions[Column, Row].Source == WhiteChip.Source)
            {

                Positions[Column, Row].Source = BlackChip.Source;
            }
            else
            {
                Positions[Column, Row].Source = WhiteChip.Source;
            }
        }
    }
}
