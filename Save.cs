namespace Laba
{
    internal class Save
    {

        private static string Seed;
        private static string Nick1;
        private static string Nick2;
        private static int Player2Chips;
        private static int Player1Chips;
        private static string SaveName;
        private static string PreviousSaveName;
        public static string Information
        {
            get { return Seed; }
            set { Seed = value; }
        }
        public static string Player1
        {
            get { return Nick1; }
            set { Nick1 = value; }
        }
        public static string Player2
        {
            get { return Nick2; }
            set { Nick2 = value; }
        }
        public static int WhiteChips
        {
            get { return Player1Chips; }
            set {  Player1Chips = value; }
        }
        public static int BlackChips
        {
            get { return Player2Chips; }
            set { Player2Chips = value; }
        }
        public static string SavingName
        {
            get { return SaveName; }
            set { SaveName = value; }
        }
        public static string PreviousSavingName
        {
            get { return PreviousSaveName; }
            set { PreviousSaveName = value; }
        }
    }
}
