namespace Laba.ViewModels
{
    internal class PlayersNickVM
    {
        private PlayersNicksPage Visual;
        public PlayersNickVM(PlayersNicksPage playersNicksPage) => Visual = playersNicksPage;

        // Метод проверки имён игроков.
        public void SetNames(string Player1, string Player2)
        {
            Save.Player1 = Player1;
            Save.Player2 = Player2;

            // Проверка имен игроков. 
            if (GameLogic.CheckNickNames())
            {
                Visual.StartGame();
            }
            else
            {
                Visual.Error();
            }
        }

        // Метод завершения работы с текущей страницей.
        public void End() => Visual = default!;
    }
}
