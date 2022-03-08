namespace TestLibrary
{
    public class Point
    {
        public int Row { get; set; }
        public int Column { get; set; }

        /// <summary>
        /// Contrstucts new Point with specified row and column properties
        /// </summary>
        /// <param name="row"></param>
        /// <param name="column"></param>
        public Point(int row, int column)
        {
            this.Row = row;
            this.Column = column;
        }

        /// <summary>
        /// Returns the row and column in string format
        /// </summary>
        /// <returns>[Row,Column]</returns>
        public override string ToString()
        {
            return $"[{Row}, {Column}]";
        }
    }
}