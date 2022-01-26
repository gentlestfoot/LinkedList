namespace TestLibrary
{
    public class LinkedList<T> where T : IComparable<T>
    {
        public int Size { get; set; }
        public Node<T>? Head { get; set; }
        public Node<T>? Tail { get; set; }

        /// <summary>
        /// Constructs an empty LinkedList
        /// </summary>
        public LinkedList()
        {
            Clear();
        }

        /// <summary>
        /// Adds a new node to the beginning of the list
        /// </summary>
        /// <param name="element">Element to be added to the new first node</param>
        public void AddFirst(T element)
        {
            Node<T> newNode = new Node<T>(element, previousNode: null, nextNode: null);

            if(IsEmpty())
            {
                Tail = newNode;
            }
            else
            {
                newNode.Next = Head;
                Head.Previous = newNode;
            }

            Head = newNode;
            Size++;
        }

        /// <summary>
        /// Empties the list
        /// </summary>
        public void Clear()
        {
            Size = 0;
            Head = null;
            Tail = null;
        }

        /// <summary>
        /// Checks if the list is empty
        /// </summary>
        /// <returns>True if the list is empty</returns>
        public bool IsEmpty() => Size == 0;

        /// <summary>
        /// Returns the element of the first node in the list
        /// </summary>
        /// <returns>Element of the first list node</returns>
        /// <exception cref="ApplicationException">List is empty</exception>
        public T GetFirst()
        {
            EmptyListException();

            return Head.Element;
        }

        /// <summary>
        /// Returns the element of the last node in the list
        /// </summary>
        /// <returns>Element of the last list node</returns>
        /// <exception cref="ApplicationException">List is empty</exception>
        public T GetLast()
        {
            EmptyListException();
            return Tail.Element;

            //return Tail == null ? default(T) : (T)Tail.Element;
        }

        /// <summary>
        /// Sets the element of the first list node
        /// </summary>
        /// <param name="element">New element for first node</param>
        /// <returns>Existing element from first node</returns>
        /// <exception cref="ApplicationException">List is empty</exception>
        public T SetFirst(T element)
        {
            ///mptyListException();

            T oldHead = GetFirst(); //ead.Element;

            Head.Element = element;

            return oldHead;
        }

        /// <summary>
        /// Sets the element of the last list node
        /// </summary>
        /// <param name="element">New element for the last node</param>
        /// <returns>Existing element from the last node</returns>
        /// <exception cref="ApplicationException">List is empty</exception>
        public T SetLast(T element)
        {
            EmptyListException();

            T oldTail = Tail.Element;

            Tail.Element = element;

            return oldTail;
        }

        /// <summary>
        /// Adds a node to the end of the list
        /// </summary>
        /// <param name="element">Element to add to the new end node</param>
        public void AddLast(T element)
        {
            Node<T> newNode = new Node<T>(element, previousNode: null, nextNode: null);

            if (IsEmpty())
            {
                Head = newNode;
            }
            else
            {
                newNode.Previous = Tail;
                Tail.Next = newNode;
            }

            Tail = newNode;
            Size++;
        }

        #region Milestone 2

        /// <summary>
        /// Removes the first node from the list
        /// </summary>
        /// <returns>The element from the removed node</returns>
        /// <exception cref="ApplicationException">List is empty</exception>
        public T? RemoveFirst()
        {
            EmptyListException();
            T? firstElement = Head.Element;

            if (Size > 1)
            {
                Head = Head.Next;
                Head.Previous = null;
                Size--;
            } 
            else
            {
                this.Clear();
            }

            return firstElement;
        }

        /// <summary>
        /// Removes the last node from the list
        /// </summary>
        /// <returns>The element from the removed node</returns>
        /// <exception cref="ApplicationException">List is empty</exception>
        public T? RemoveLast()
        {
            EmptyListException();
            T? lastElement = Tail.Element;

            if(Size > 1)
            {
                Tail = Tail.Previous;
                Tail.Next = null;
                Size--;
            }
            else
            {
                this.Clear();
            }

            return lastElement;
        }

        /// <summary>
        /// Gets the element of the node at the specified position. First element is position 1.
        /// </summary>
        /// <param name="position">1 indexed position to get element</param>
        /// <returns>Element of node at specified position</returns>
        /// <exception cref="ApplicationException">List is empty</exception>
        /// <exception cref="ApplicationException">Position cannot be less than 1</exception>
        /// <exception cref="ApplicationException">Position beyond end of list</exception>
        public T Get(int position)
        {
            Node<T> nodeAtPosition = GetNode(position);

            return nodeAtPosition.Element;
        }

        /// <summary>
        /// Removes the node at the specified position and returns the element from the removed node.
        /// </summary>
        /// <param name="position">Position of element to remove</param>
        /// <returns>Element from removed node</returns>
        /// <exception cref="ApplicationException">List is empty</exception>
        /// <exception cref="ApplicationException">Position cannot be less than 1</exception>
        /// <exception cref="ApplicationException">Position beyond end of list</exception>
        public T Remove(int position)
        {
            Node<T> nodeAtPosition = GetNode(position);

            if (position == 1)
            {
                RemoveFirst();
            }
            else if (position == Size)
            {
                RemoveLast();
            }
            else
            {
                nodeAtPosition.Previous.Next = nodeAtPosition.Next;
                nodeAtPosition.Next.Previous = nodeAtPosition.Previous;
                Size--;
            }

            return nodeAtPosition.Element;
        }

        /// <summary>
        /// Sets the element of a node and returns the existing element
        /// </summary>
        /// <param name="element">New element</param>
        /// <param name="position">Node to set</param>
        /// <returns>Existing element from specified node</returns>
        /// <exception cref="ApplicationException">List is empty</exception>
        /// <exception cref="ApplicationException">Position cannot be less than 1</exception>
        /// <exception cref="ApplicationException">Position beyond end of list</exception>
        public T Set(T element, int position)
        {
            Node<T> nodeAtPosition = GetNode(position);
            T? oldElement = nodeAtPosition.Element;

            nodeAtPosition.Element = element;

            return oldElement;
        }

        /// <summary>
        /// Adds a new node after the node after the specified position
        /// </summary>
        /// <param name="element">Element to put in the new node</param>
        /// <param name="position">Position of node previous to the new node</param>
        /// <exception cref="ApplicationException">List is empty</exception>
        /// <exception cref="ApplicationException">Position cannot be less than 1</exception>
        /// <exception cref="ApplicationException">Position beyond end of list</exception>
        public void AddAfter(T element, int position)
        {
            AddAfterNode(element, GetNode(position));
        }

        /// <summary>
        /// Adds a new node after the node before the specified position
        /// </summary>
        /// <param name="element">Element to put in the new node</param>
        /// <param name="position">Position of the node after the new node</param>
        /// <exception cref="ApplicationException">List is empty</exception>
        /// <exception cref="ApplicationException">Position cannot be less than 1</exception>
        /// <exception cref="ApplicationException">Position beyond end of list</exception>
        public void AddBefore(T element, int position)
        {
            AddBeforeNode(element, GetNode(position));
        }

        #endregion

        #region Milestone 3

        /// <summary>
        /// 
        /// </summary>
        /// <param name="element"></param>
        /// <returns></returns>
        public T Get(T element) => GetNode(element).Element;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="element"></param>
        /// <param name="oldElement"></param>
        public void AddAfter(T element, T oldElement)
        {
            AddAfterNode(element, GetNode(oldElement));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="element"></param>
        /// <param name="oldElement"></param>
        public void AddBefore(T element, T oldElement)
        {
            AddBeforeNode(element, GetNode(oldElement));
        }

        public T Remove(T element)
        {
            throw new NotImplementedException();
        }

        public T Set(T element, T oldElement)
        {
            throw new NotImplementedException();
        }

        public void Insert(T element)
        {
            throw new NotImplementedException();
        }

        public void SortAscending()
        {
            throw new NotImplementedException();
        }

        #endregion

        /// <summary>
        /// Throws an exception if the list has a size of 0 (is empty).
        /// </summary>
        /// <exception cref="ApplicationException">List is empty</exception>
        private void EmptyListException()
        {
            if (Size == 0)
            {
                throw new ApplicationException("List is emppty");
            }
        }

        /// <summary>
        /// Gets a node from the list by position
        /// </summary>
        /// <param name="position"></param>
        /// <returns>Node at specified position</returns>
        /// <exception cref="ApplicationException">List is empty</exception>
        /// <exception cref="ApplicationException">Position cannot be less than 1</exception>
        /// <exception cref="ApplicationException">Position beyond end of list</exception>
        private Node<T> GetNode(int position)
        {
            EmptyListException();

            if (position < 1)
            {
                throw new ApplicationException("Position cannot be less than 1");
            }

            if (position > Size)
            {
                throw new ApplicationException("Position beyond end of list");
            }

            int currentPosition = 1;
            Node<T> currentNode = Head;

            while (currentPosition < position)
            {
                currentNode = currentNode.Next;
                currentPosition++;
            }

            return currentNode;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="element"></param>
        /// <returns></returns>
        /// <exception cref="ApplicationException">List is empty</exception>
        /// <exception cref="ApplicationException">Element not found in list</exception>
        private Node<T> GetNode(T element)
        {
            EmptyListException();

            Node<T> currentNode = Head;

            while (!currentNode.Element.Equals(element))
            {
                currentNode = currentNode.Next ?? throw new ApplicationException("Element not found in list");
            }

            return currentNode;
        }

        private void AddAfterNode(T element, Node<T> existingNode)
        {
            if(existingNode == Tail)
            {
                AddLast(element);
            }
            else
            {
                Node<T> newNode = new(element, previousNode: existingNode, nextNode: existingNode.Next);
                existingNode.Next.Previous = newNode;
                existingNode.Next = newNode;
                Size++;
            }
        }

        private void AddBeforeNode(T element, Node<T> existingNode)
        {
            if (existingNode == Head)
            {
                AddFirst(element);
            }
            else
            {
                Node<T> newNode = new(element, previousNode: existingNode.Previous, nextNode: existingNode);
                existingNode.Previous.Next = newNode;
                existingNode.Previous = newNode;
                Size++;
            }
        }
    }
}
