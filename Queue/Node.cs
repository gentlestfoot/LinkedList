namespace TestLibrary
{
    public class Node<T>
    {
        public T? Element { get; set;  }
        public Node<T>? Next { get; set; }

        /// <summary>
        /// Constructs a Node object with given parameters or null
        /// </summary>
        /// <param name="element">The element contained in the node</param>
        /// <param name="previousNode">The previous node</param>
        /// <param name="nextNode">The next node</param>
        public Node(T element = default(T), Node<T>? nextNode = null)
        {
            Element = element;
            Next = nextNode;
        }
    }
}