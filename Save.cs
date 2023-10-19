namespace Laba
{
    internal class Save
    {

        private static string Seed;
        private static string Nick1;
        private static string Nick2;

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
    }
}
