
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task22
{
    internal class Entry<K, T>
    {
        public K key;
        public T value;
        public Entry(K key1, T value1)
        {
            key = key1;
            value = value1;
        }
    }
}
