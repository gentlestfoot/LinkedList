namespace TestLibrary
{
    /// <summary>
    /// Contains methods to solve 2D mazes
    /// </summary>
    internal class Maze
    {
        public Point StartingPoint { get; set; }
        public int RowLength { get; internal set; }
        public int ColumnLength { get; internal set; }
        public char[][] CharMaze { get; internal set; }

        private Stack<Point> path;

        /// <summary>
        /// Constructs an instance of the maze class using a prebult maze
        /// </summary>
        /// <param name="startingRow">The row of the starting point</param>
        /// <param name="startingColumn">The column of the starting point</param>
        /// <param name="maze">The char array array of the maze</param>
        /// <exception cref="ApplicationException">Starting point is the exit</exception>
        /// <exception cref="ApplicationException">Starting point is in a wall</exception>
        public Maze(int startingRow, int startingColumn, char[][] maze)
        {
            this.StartingPoint = new Point(startingRow, startingColumn);
            this.ColumnLength = maze[0].Length;
            this.RowLength = maze.Length;
            this.CharMaze = maze;

            if (GetCharAtPoint(StartingPoint) == 'E')
            {
                throw new ApplicationException("Starting point is the exit.");
            }

            if (GetCharAtPoint(StartingPoint) == 'W')
            {
                throw new ApplicationException("Starting point is in a wall.");
            }
        }

        /// <summary>
        /// Constructs an instance of maze class by importing a maze file
        /// </summary>
        /// <param name="mazeName">Name of the maze file to import</param>
        public Maze(string mazeName)
        {
            string[] lines = File.ReadAllLines(mazeName);

            string[] dimensions = lines[0].Split(' ');
            RowLength = int.Parse(dimensions[0]);
            ColumnLength = int.Parse(dimensions[1]);

            string[] startingCoordinates = lines[1].Split(' ');
            StartingPoint = new Point(int.Parse(startingCoordinates[0]), int.Parse(startingCoordinates[1]));

            CharMaze = new char[0][];
            foreach (string line in lines)
            {
                if (line[0] == 'W')
                {
                    CharMaze.Append(line.ToCharArray());
                }
            }
        }

        /// <summary>
        /// Alias for CharMaze get
        /// </summary>
        /// <returns>The value of CharMaze</returns>
        public char[][] GetMaze() => CharMaze;

        /// <summary>
        /// Converts the char array array to a string
        /// </summary>
        /// <returns>CharMaze as a formatted string</returns>
        public string PrintMaze()
        {
            string output = "";

            foreach (char[] row in CharMaze)
            {
                output += new string(row) + "\n";
            }

            return output.Trim();
        }

        /// <summary>
        /// Does a depth first search to find the path from the starting to point to the exit
        /// </summary>
        /// <returns>The exit path or no exit found</returns>
        public string DepthFirstSearch()
        {
            path = new Stack<Point>();
            path.Push(StartingPoint);
            string message;

            do
            {
                SetCharAtPoint(path.Top(), 'V');

                Point southPoint = new Point(path.Top().Row +1, path.Top().Column);
                Point eastPoint = new Point(path.Top().Row, path.Top().Column +1);
                Point northPoint = new Point(path.Top().Row - 1, path.Top().Column);
                Point westPoint = new Point(path.Top().Row, path.Top().Column -1);

                if (SpaceOrExit(southPoint))
                {
                    path.Push(southPoint);
                }
                else if (SpaceOrExit(eastPoint))
                {
                    path.Push(eastPoint);
                }
                else if (SpaceOrExit(westPoint))
                {
                    path.Push(westPoint);
                }
                else if (SpaceOrExit(northPoint))
                {
                    path.Push(northPoint);
                }
                else
                {
                    path.Pop();
                }

                if (path.Size == 0)
                {
                    return "No exit found in maze!\n\n" + PrintMaze();
                }

            } while (GetCharAtPoint(path.Top()) != 'E' || path.Size == 0);

            Point exitPoint = path.Top();
            string steps = "";
            Stack<Point> backup = new();


            while (!path.IsEmpty())
            {
                steps = $"{path.Top()}\n{steps}";
                SetCharAtPoint(path.Top(), '.');
                backup.Push(path.Pop());
            }

            path = backup;
            SetCharAtPoint(exitPoint, 'E');

            return $"Path to follow from Start {StartingPoint} to Exit {exitPoint} - {path.Size} steps:\n{steps}{PrintMaze()}";
        }

        /// <summary>
        /// The path as a point stack with the starting point at the top
        /// </summary>
        /// <returns>Point stack of the path</returns>
        /// <exception cref="ApplicationException">Maze has not been searched</exception>
        public Stack<Point> GetPathToFollow()
        {
            if (path == null)
            {
                throw new ApplicationException("Maze has not been searched");
            }

            Stack<Point> invertedPath = new();
            Stack<Point> copy = new();

            while (!path.IsEmpty())
            {
                invertedPath.Push(path.Pop());
            }

            while (!invertedPath.IsEmpty())
            {
                copy.Push(invertedPath.Top());
                path.Push(invertedPath.Pop());
            }

            return copy;
        }

        /// <summary>
        /// Gets the character at a specified point in the maze
        /// </summary>
        /// <param name="point">Point to get from</param>
        /// <returns>Char from point</returns>
        private char GetCharAtPoint(Point point)
        {
            return CharMaze[point.Row][point.Column];
        }

        /// <summary>
        /// Sets the character at a specified point in the maze
        /// </summary>
        /// <param name="point">Point to set</param>
        /// <param name="character">Char to set point to</param>
        private void SetCharAtPoint(Point point, char character)
        {
            CharMaze[point.Row][point.Column] = character;
        }

        /// <summary>
        /// Checks if a point in the maze is empty or the exit
        /// </summary>
        /// <param name="point">Point to check</param>
        /// <returns>True if the point contains a space or the exit</returns>
        private bool SpaceOrExit(Point point)
        {
            return new char[]{' ', 'E'}.Contains(GetCharAtPoint(point));
        }

        internal string BreadthFirstSearch()
        {
            throw new NotImplementedException();
        }
    }
}