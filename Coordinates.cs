namespace Laba
{
    public class Coordinates
    {
        // Публичные свойства для хранения координат и цвета.
        public int Color { get; set; }
        public int Column { get; set; }
        public int Row { get; set; }

        // Конструктор класса, инициализирующий координаты без указания цвета.
        public Coordinates(int Row, int Column)
        {
            this.Column = Column;
            this.Row = Row;
        }

        // Конструктор класса, инициализирующий координаты с указанием цвета.
        public Coordinates(int Row, int Column, int Color)
        {
            this.Column = Column;
            this.Row = Row;
            this.Color = Color;
        }
    }
}
