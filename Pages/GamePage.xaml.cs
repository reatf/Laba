using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;

namespace Laba
{
    public partial class GamePage : Page
    {

        public GamePage()
        {
            InitializeComponent();
            FoundMoves();
        }
        private readonly GameLogic Logic = new();
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

            DoubleAnimation WidthAnimation = new(Positions[Column, Row].Width, Positions[Column, Row].Width-2*Change, TimeSpan.FromSeconds(0.2));
            DoubleAnimation LeftAnimation = new(0, Change, TimeSpan.FromSeconds(0.2));
            TranslateTransform Transform = new();
            WidthAnimation.AutoReverse = true;
            LeftAnimation.AutoReverse = true;
            Positions[Column, Row].RenderTransform = Transform;
            Positions[Column, Row].BeginAnimation(WidthProperty, WidthAnimation);
            Transform.BeginAnimation(TranslateTransform.XProperty, LeftAnimation);
            await Task.Delay(200);
            if (Positions[Column, Row].Source.ToString() == WhiteChip.Source.ToString())
            {

                Positions[Column, Row].Source = BlackChip.Source;
            }
            else
            {
                Positions[Column, Row].Source = WhiteChip.Source;
            }
        }
        public void ChangeChipsCountVisual (int WhiteTens, int WhiteUnits, int BlackTens, int BlackUnits)
        {
            int ChangeOne = 1690;
            int ChangeAnother = 1670;
            switch(WhiteTens)
            {
                case 0:
                    {
                        this.WhiteTens.Visibility = Visibility.Hidden;
                        break;
                    }
                case 1:
                    {
                        this.WhiteTens.Source = new BitmapImage(new Uri("/Images/Numbers/1.png", UriKind.Relative));
                        this.WhiteTens.Visibility = Visibility.Visible;
                        break;
                    }
                case 2:
                    {
                        this.WhiteTens.Source = new BitmapImage(new Uri("/Images/Numbers/2.png", UriKind.Relative));
                        this.WhiteTens.Visibility = Visibility.Visible;
                        break;
                    }
                case 3:
                    {
                        this.WhiteTens.Source = new BitmapImage(new Uri("/Images/Numbers/3.png", UriKind.Relative));
                        this.WhiteTens.Visibility = Visibility.Visible;
                        break;
                    }
                case 4:
                    {
                        this.WhiteTens.Source = new BitmapImage(new Uri("/Images/Numbers/4.png", UriKind.Relative));
                        this.WhiteTens.Visibility = Visibility.Visible;
                        break;
                    }
                case 5:
                    {
                        this.WhiteTens.Source = new BitmapImage(new Uri("/Images/Numbers/5.png", UriKind.Relative));
                        this.WhiteTens.Visibility = Visibility.Visible;
                        break;
                    }
                case 6:
                    {
                        this.WhiteTens.Source = new BitmapImage(new Uri("/Images/Numbers/6.png", UriKind.Relative));
                        this.WhiteTens.Visibility = Visibility.Visible;
                        break;
                    }
            }
            switch(WhiteUnits)
            {
                case 0:
                    {
                        this.WhiteUnits.Source = new BitmapImage(new Uri("/Images/Numbers/0.png", UriKind.Relative));
                        break;
                    }
                case 1:
                    {
                        this.WhiteUnits.Source = new BitmapImage(new Uri("/Images/Numbers/1.png", UriKind.Relative));
                        break;
                    }
                case 2:
                    {
                        this.WhiteUnits.Source = new BitmapImage(new Uri("/Images/Numbers/2.png", UriKind.Relative));
                        break;
                    }
                case 3:
                    {
                        this.WhiteUnits.Source = new BitmapImage(new Uri("/Images/Numbers/3.png", UriKind.Relative));
                        break;
                    }
                case 4:
                    {
                        this.WhiteUnits.Source = new BitmapImage(new Uri("/Images/Numbers/4.png", UriKind.Relative));
                        break;
                    }
                case 5:
                    {
                        this.WhiteUnits.Source = new BitmapImage(new Uri("/Images/Numbers/5.png", UriKind.Relative));
                        break;
                    }
                case 6:
                    {
                        this.WhiteUnits.Source = new BitmapImage(new Uri("/Images/Numbers/6.png", UriKind.Relative));
                        break;
                    }
                case 7:
                    {
                        this.WhiteUnits.Source = new BitmapImage(new Uri("/Images/Numbers/7.png", UriKind.Relative));
                        break;
                    }
                case 8:
                    {
                        this.WhiteUnits.Source = new BitmapImage(new Uri("/Images/Numbers/8.png", UriKind.Relative));
                        break;
                    }
                case 9:
                    {
                        this.WhiteUnits.Source = new BitmapImage(new Uri("/Images/Numbers/9.png", UriKind.Relative));
                        break;
                    }
            }
            switch(BlackTens)
            {
                case 0:
                    {
                        this.BlackTens.Visibility = Visibility.Hidden;
                        break;
                    }
                case 1:
                    {
                        this.BlackTens.Source = new BitmapImage(new Uri("/Images/Numbers/1.png", UriKind.Relative));
                        this.BlackTens.Visibility = Visibility.Visible;
                        break;
                    }
                case 2:
                    {
                        this.BlackTens.Source = new BitmapImage(new Uri("/Images/Numbers/2.png", UriKind.Relative));
                        this.BlackTens.Visibility = Visibility.Visible;
                        break;
                    }
                case 3:
                    {
                        this.BlackTens.Source = new BitmapImage(new Uri("/Images/Numbers/3.png", UriKind.Relative));
                        this.BlackTens.Visibility = Visibility.Visible;
                        break;
                    }
                case 4:
                    {
                        this.BlackTens.Source = new BitmapImage(new Uri("/Images/Numbers/4.png", UriKind.Relative));
                        this.BlackTens.Visibility = Visibility.Visible;
                        break;
                    }
                case 5:
                    {
                        this.BlackTens.Source = new BitmapImage(new Uri("/Images/Numbers/5.png", UriKind.Relative));
                        this.BlackTens.Visibility = Visibility.Visible;
                        break;
                    }
                case 6:
                    { 
                        this.BlackTens.Source = new BitmapImage(new Uri("/Images/Numbers/6.png", UriKind.Relative));
                        this.BlackTens.Visibility = Visibility.Visible;
                        break;
                    }
            }
            switch(BlackUnits)
            {
                case 0:
                    {
                        this.BlackUnits.Source = new BitmapImage(new Uri("/Images/Numbers/0.png", UriKind.Relative));
                        break;
                    }
                case 1:
                    {
                        this.BlackUnits.Source = new BitmapImage(new Uri("/Images/Numbers/1.png", UriKind.Relative));
                        break;
                    }
                case 2:
                    {
                        this.BlackUnits.Source = new BitmapImage(new Uri("/Images/Numbers/2.png", UriKind.Relative));
                        break;
                    }
                case 3:
                    {
                        this.BlackUnits.Source = new BitmapImage(new Uri("/Images/Numbers/3.png", UriKind.Relative));
                        break;
                    }
                case 4:
                    {
                        this.BlackUnits.Source = new BitmapImage(new Uri("/Images/Numbers/4.png", UriKind.Relative));
                        break;
                    }
                case 5:
                    {
                        this.BlackUnits.Source = new BitmapImage(new Uri("/Images/Numbers/5.png", UriKind.Relative));
                        break;
                    }
                case 6:
                    {
                        this.BlackUnits.Source = new BitmapImage(new Uri("/Images/Numbers/6.png", UriKind.Relative));
                        break;
                    }
                case 7:
                    {
                        this.BlackUnits.Source = new BitmapImage(new Uri("/Images/Numbers/7.png", UriKind.Relative));
                        break;
                    }
                case 8:
                    {
                        this.BlackUnits.Source = new BitmapImage(new Uri("/Images/Numbers/8.png", UriKind.Relative));
                        break;
                    }
                case 9:
                    {
                        this.BlackUnits.Source = new BitmapImage(new Uri("/Images/Numbers/9.png", UriKind.Relative));
                        break;
                    }
            }
            if(WhiteTens == 1)
            {
                Canvas.SetLeft(this.WhiteTens, ChangeOne);
            }
            else if(WhiteTens/10 > 1)
            {
                Canvas.SetLeft(this.WhiteTens, ChangeAnother);
            }
            if (BlackTens == 1)
            {
                Canvas.SetLeft(this.BlackTens, ChangeOne);
            }
            else if (BlackTens > 1)
            {
                Canvas.SetLeft(this.BlackTens, ChangeAnother);
            }
        }
        public void ChangeMoveVisual()
        {
            if(MoveChip.Source.ToString() == BlackChip.Source.ToString())
            {
                Canvas.SetLeft(MoveOfPlayer, Canvas.GetLeft(MoveOfPlayer) + 36);
                MoveOfPlayer.Source = new BitmapImage(new Uri("/Images/WhiteMove.png", UriKind.Relative));
                MoveChip.Source = new BitmapImage(new Uri("/Images/WhiteChip.png", UriKind.Relative));
            }
            else
            {
                Canvas.SetLeft(MoveOfPlayer, Canvas.GetLeft(MoveOfPlayer) - 36);
                MoveOfPlayer.Source = new BitmapImage(new Uri("/Images/BlackMove.png", UriKind.Relative));
                MoveChip.Source = new BitmapImage(new Uri("/Images/BlackChip.png", UriKind.Relative));
            }
        }
        public void FoundMoves()
        {
            Image[,] Positions = { { A1, B1, C1, D1, E1, F1, G1, H1 },
                                   { A2, B2, C2, D2, E2, F2, G2, H2 },
                                   { A3, B3, C3, D3, E3, F3, G3, H3 },
                                   { A4, B4, C4, D4, E4, F4, G4, H4 },
                                   { A5, B5, C5, D5, E5, F5, G5, H5 },
                                   { A6, B6, C6, D6, E6, F6, G6, H6 },
                                   { A7, B7, C7, D7, E7, F7, G7, H7 },
                                   { A8, B8, C8, D8, E8, F8, G8, H8 } };

            Logic.AvailableMoves(out List<Coordinates>? AvailableMoves);
            if (AvailableMoves.Count > 0)
            {
                foreach (var Coordinates in AvailableMoves)
                {
                    Positions[Coordinates.Row, Coordinates.Column].Visibility = Visibility.Visible;
                }
            }
            else
            {
                NextTurn();
            }
        }
        private async void ChipPlace(object sender, MouseButtonEventArgs e)
        {
            Image GetImage = (Image) sender;
            if(GetImage.Source.ToString() == HintMove.Source.ToString())
            {
            Image[,] Positions = { { A1, B1, C1, D1, E1, F1, G1, H1 },
                                   { A2, B2, C2, D2, E2, F2, G2, H2 },
                                   { A3, B3, C3, D3, E3, F3, G3, H3 },
                                   { A4, B4, C4, D4, E4, F4, G4, H4 },
                                   { A5, B5, C5, D5, E5, F5, G5, H5 },
                                   { A6, B6, C6, D6, E6, F6, G6, H6 },
                                   { A7, B7, C7, D7, E7, F7, G7, H7 },
                                   { A8, B8, C8, D8, E8, F8, G8, H8 } };
                for (int Row = 0; Row < Positions.GetLength(0); Row++)
                {
                    for (int Column = 0; Column < Positions.GetLength(1); Column++)
                    {
                        if (Positions[Row, Column].Name == GetImage.Name)
                        {
                            List<Coordinates> Reversi = Logic.GetReversiChips(Row, Column, out int Turn);
                            if (Turn == 1)
                            {
                                Positions[Row, Column].Source = new BitmapImage(new Uri("/Images/WhiteChip.png", UriKind.Relative));
                                foreach (var Position in Reversi)
                                {
                                    MainAnimation(Position.Row, Position.Column);
                                }
                            }
                            else
                            {
                                Positions[Row, Column].Source = new BitmapImage(new Uri("/Images/BlackChip.png", UriKind.Relative));
                                foreach (var Position in Reversi)
                                {
                                    MainAnimation(Position.Row, Position.Column);
                                }
                            }
                        }
                    }
                }
                for (int Row = 0; Row < Positions.GetLength(0); Row++)
                {
                    for (int Column = 0; Column < Positions.GetLength(1); Column++)
                    {
                        if (Positions[Row,Column].Source.ToString() == HintMove.Source.ToString())
                        {
                            Positions[Row, Column].Visibility = Visibility.Hidden;
                        }
                    }
                }
                await Task.Delay(400);
                NextTurn();
            }
        }
        private void NextTurn()
        {
            ChangeMoveVisual();
            Logic.ChangeTurn();
            Logic.ChipsCount(out int WhiteTens, out int WhiteUnits, out int BlackTens, out int BlackUnits);
            ChangeChipsCountVisual(WhiteTens, WhiteUnits, BlackTens, BlackUnits);
            FoundMoves();
        }
        public void End()
        {
            NavigationService.RemoveBackEntry();
            NavigationService.Navigate(new Uri("/Pages/EndPage.xaml", UriKind.Relative));
        }
    }
}
