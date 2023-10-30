namespace Laba.ViewModels
{
    internal class EndPageVM
    {
        EndPage Visual;
        public EndPageVM(EndPage endPage) => Visual = endPage;

        // Метод определения итога игры.
        public void CheckEnd()
        {
            if (Save.BlackChips > Save.WhiteChips)
            {
                Visual.Winner1();
            }
            else if (Save.WhiteChips > Save.BlackChips)
            {
                Visual.Winner2();
            }
            else
            {
                Visual.Draw();
            }
        }

        // Метод завершения работы с текущей страницей.
        public void Ending() => Visual = default!;
    }
}
