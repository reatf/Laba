using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows;

namespace Laba.ViewModels
{
    internal class StartPageVM
    {
        private StartPage Visual;

        public StartPageVM(StartPage startPage)
        {
            Visual = startPage;
        }

        // Флаг, указывающий, что мы находимся на странице StartPage.
        public bool OnStartPage = true;

        // Метод закрытия меню загрузки игры.
        public void StartEsc(object sender, KeyEventArgs e)
        {
            if (OnStartPage)
            {
                if (e.Key == Key.Escape && Visual.LoadMenu.Visibility == Visibility.Visible)
                {
                    Visual.EscLoad();
                }
            }
        }

        // Метод сохранения предыдущего имени сохранения.
        public static void PreviousName(string Name) => Save.PreviousSavingName = Name;

        // Метод загрузки информации о сохранениях и их отображения на странице.
        public void LoadSaveBack()
        {
            GameLogic.GetSavesNumber(out int SaveNumber, out List<string>? SavesName);

            for (var Index = 0; Index < SaveNumber; Index++)
            {
                Visual.VisualLoadBack(Index, SavesName![Index]);
            }
        }

        // Метод завершения работы с текущей страницей.
        public void End()
        {
            OnStartPage = false;
            Visual = default!;
        }

        // Метод загрузки информации о Топе лидеров.
        public void Leaders()
        {

            for (var Index = 0; Index < 10; Index++)
            {
                Save.LeaderIndex = Index;
                GameLogic.StartLeaderboard(out string? Nick, out int Wins);

                if (Wins == 0)
                {
                    break;
                }
                else
                {
                    Visual.Leaderboard(Index, Nick!, Wins);
                }
            }

        }
    }
}
