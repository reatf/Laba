namespace Laba
{
    internal class Save
    {
        // Статические поля для хранения информации о сохраненной игре
        private static string Seed;
        private static string Nick1;
        private static string Nick2;
        private static int Player2Chips;
        private static int Player1Chips;
        private static string SaveName;
        private static string PreviousSaveName;
        private static int Index;

        // Свойство для доступа к информации о игре
        public static string Information
        {
            get { return Seed; }
            set { Seed = value; }
        }

        // Свойство для доступа к имени первого игрока
        public static string Player1
        {
            get { return Nick1; }
            set { Nick1 = value; }
        }

        // Свойство для доступа к имени второго игрока
        public static string Player2
        {
            get { return Nick2; }
            set { Nick2 = value; }
        }

        // Свойство для доступа к количеству белых фишек
        public static int WhiteChips
        {
            get { return Player1Chips; }
            set {  Player1Chips = value; }
        }

        // Свойство для доступа к количеству черных фишек
        public static int BlackChips
        {
            get { return Player2Chips; }
            set { Player2Chips = value; }
        }

        // Свойство для доступа к имени текущего сохранения
        public static string SavingName
        {
            get { return SaveName; }
            set { SaveName = value; }
        }

        // Свойство для доступа к имени предыдущего сохранения
        public static string PreviousSavingName
        {
            get { return PreviousSaveName; }
            set { PreviousSaveName = value; }
        }

        // Свойство для доступа к индексу лидера игры
        public static int LeaderIndex
        {
            get { return Index; }
            set { Index = value; }
        }
    }
}
