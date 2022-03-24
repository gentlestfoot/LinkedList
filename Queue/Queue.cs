namespace TestLibrary
{
    /// <summary>
    /// Encapsulates a generic queue structure
    /// </summary>
    /// <typeparam name="T">The data type to be queued</typeparam>
    public class Queue<T>
    {
        public int Size { get; set; }
        public Node<T>? Head { get; set; }
        public Node<T>? Tail { get; set; }

        /// <summary>
        /// Constructs a new empty queue.
        /// </summary>
        public Queue()
        {
            Clear();
        }

        /// <summary>
        /// Adds a new element to the tail of the queue.
        /// </summary>
        /// <param name="element">Element to add</param>
        public void Enqueue(T element)
        {
            Node<T> newNode = new Node<T>(element);

            if (IsEmpty())
            {
                Head = newNode;
            }
            else
            {
                Tail.Next = newNode;
            }
            Tail = newNode;
            Size++;
        }

        /// <summary>
        /// Returns true if the queue is empty.
        /// </summary>
        /// <returns>True if the queue is empty</returns>
        public bool IsEmpty()
        {
            return Size == 0;
        }

        /// <summary>
        /// Returns the element from the head node.
        /// </summary>
        /// <returns>Element from the head of the queue</returns>
        public T Front()
        {
            EmptyException();
            return Head.Element;
        }

        /// <summary>
        /// Removes the head node from the queue and returns the element of that node.
        /// </summary>
        /// <returns>Element from the head of the queue</returns>
        public T Dequeue()
        {
            EmptyException();
            Node<T> old = Head;
            Head = Head.Next;
            Size--;
            if (IsEmpty())
            {
                Tail = null;
            }
            return old.Element;
        }

        /// <summary>
        /// Empties the queue.
        /// </summary>
        public void Clear()
        {
            Size = 0;
            Head = null;
            Tail = null;
        }

        /// <summary>
        /// Throws an exception if the queue has a size of 0 (is empty).
        /// </summary>
        /// <exception cref="ApplicationException">Queue is empty</exception>
        private void EmptyException()
        {
            if (IsEmpty())
            {
                throw new ApplicationException("Queue is emppty");
            }
        }
    }
}
