namespace TestLibrary
{
    public class Queue<T>
    {
        public int Size { get; set; }
        public Node<T>? Head { get; set; }
        public Node<T>? Tail { get; set; }

        /// <summary>
        /// Constructs a new empty stack.
        /// </summary>
        public Queue()
        {
            Clear();
        }

        /// <summary>
        /// Adds a new element to the top of the stack.
        /// </summary>
        /// <param name="element"></param>
        public void Push(T element)
        {
            Head = new Node<T>(element, previousNode: Head);
            Size++;
        }

        /// <summary>
        /// Returns true if the stack is empty.
        /// </summary>
        /// <returns>True if the stack is empty</returns>
        public bool IsEmpty()
        {
            return Size == 0;
        }

        /// <summary>
        /// Returns the element from the top node.
        /// </summary>
        /// <returns>Element from the top of the stack</returns>
        public T Top()
        {
            EmptyException();
            return Head.Element;
        }

        /// <summary>
        /// Removes the top node from the stack and returns the element of that node.
        /// </summary>
        /// <returns>Element from the top of the stack</returns>
        public T Pop()
        {
            EmptyException();
            Node<T> node = Head;
            Head = Head.Next;
            Size--;
            return node.Element;
        }

        /// <summary>
        /// Empties the stack.
        /// </summary>
        public void Clear()
        {
            Size = 0;
            Head = null;
        }

        /// <summary>
        /// Throws an exception if the stack has a size of 0 (is empty).
        /// </summary>
        /// <exception cref="ApplicationException">Queue is empty</exception>
        private void EmptyException()
        {
            if (Size == 0)
            {
                throw new ApplicationException("Stack is emppty");
            }
        }
    }
}
