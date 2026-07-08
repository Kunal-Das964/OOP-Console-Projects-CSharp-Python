using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStructures
{
    public class CustomQueue<T>
    {
        private List<T> _items = new List<T>();

        public void Enqueue(T item)
        {
            _items.Add(item);
        }

        public T Dequeue()
        {
            if (IsEmpty())
            {
                Console.WriteLine("Queue is empty. Nothing to dequeue.");
                return default(T);
            }
            T item = _items[0];
            _items.RemoveAt(0);
            return item;
        }

        public T Peek()
        {
            if (IsEmpty())
            {
                Console.WriteLine("Queue is empty. Nothing to peek.");
                return default(T);
            }
            return _items[0];
        }

        public bool IsEmpty()
        {
            return _items.Count == 0;
        }
    }
}
