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
            if (GetCharAtPoint(point) != 'E')
            {
                CharMaze[point.Row][point.Column] = character;
            }
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

        /// <summary>
        /// Does a breadth first search of the maze to find a path to the exit
        /// </summary>
        /// <returns>A string result of the search</returns>
        internal string BreadthFirstSearch()
        {
            path = new Stack<Point>();
            Queue<Point> explore = new Queue<Point>();
            explore.Enqueue(StartingPoint);
            Point currentLocation;

            do
            {
                currentLocation = explore.Dequeue();
                SetCharAtPoint(currentLocation, 'V');

                Point southPoint = new Point(currentLocation.Row + 1, currentLocation.Column);
                Point eastPoint = new Point(currentLocation.Row, currentLocation.Column + 1);
                Point northPoint = new Point(currentLocation.Row - 1, currentLocation.Column);
                Point westPoint = new Point(currentLocation.Row, currentLocation.Column - 1);
                Point[] directions = { southPoint, eastPoint, westPoint, northPoint };

                foreach (Point direction in directions)
                {
                    if (SpaceOrExit(direction))
                    {
                        direction.Parent = currentLocation;
                        explore.Enqueue(direction);
                    }
                }

                if (explore.IsEmpty())
                {
                    return "No exit found in maze!\n\n" + PrintMaze();
                }
            } while (GetCharAtPoint(currentLocation) != 'E');

            string steps = "";
            Point exitPoint = currentLocation;

            while (currentLocation != null)
            {
                path.Push(currentLocation);
                steps = $"{path.Top()}\n{steps}";
                SetCharAtPoint(path.Top(), '.');
                currentLocation = currentLocation.Parent;
            }

            return $"Path to follow from Start {StartingPoint} to Exit {exitPoint} - {path.Size} steps:\n{steps}{PrintMaze()}";
        }
    }
}