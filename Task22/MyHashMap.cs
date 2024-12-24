
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task22
{
    internal class MyHashMap<T, K>
    {
        MyLinkedList<Entry<T, K>>[] table;
        int size;
        float loadFactor;
        int numOfFullBuckets;
        public void Put(T key, K value)
        {
            int hash = Math.Abs(key.GetHashCode());
            int numBucket = hash % table.Length;
            Entry<T, K> x = new Entry<T, K>(key, value);
            bool flag = false;
            if (table[numBucket] == null || table[numBucket].Size() == 0)
            {
                table[numBucket] = new MyLinkedList<Entry<T, K>>(x);
                size++;
                if (numOfFullBuckets >= (float)table.Length * loadFactor)
                {
                    table = TableUp();
                }
            }
            else
            {
                MyLinkedList<Entry<T, K>>.List<Entry<T, K>> p = table[numBucket].first;
                for (int i = 0; i < table[numBucket].Size(); i++)
                {
                    if (key.Equals(p.info.key))
                    {
                        if (value.Equals(p.info.value))
                        {
                            p.info.value = value;
                            flag = true;
                            break;
                        }

                    }
                    p = p.next;
                }
                if (!flag)
                {
                    table[numBucket].Add(x);
                    size++;
                    if (numOfFullBuckets >= (float)table.Length * loadFactor)
                    {
                        table = TableUp();
                    }
                }
            }
        }
        private MyLinkedList<Entry<T, K>>[] Put(T key, K value, MyLinkedList<Entry<T, K>>[] table)
        {
            int hash = Math.Abs(key.GetHashCode());
            int numBucket = hash % table.Length;
            Entry<T, K> x = new Entry<T, K>(key, value);
            bool flag = false;
            if (table[numBucket].Size() == 0)
            {
                table[numBucket] = new MyLinkedList<Entry<T, K>>(x);
                size++;
            }
            else
            {
                MyLinkedList<Entry<T, K>>.List<Entry<T, K>> p = table[numBucket].first;
                for (int i = 0; i < table[numBucket].Size(); i++)
                {
                    if (key.Equals(p.info.key))
                    {
                        if (value.Equals(p.info.value))
                        {
                            p.info.value = value;
                            return table;
                        }

                    }
                    p = p.next;
                }
                if (!flag)
                {
                    table[numBucket].Add(x);
                    size++;
                }
            }
            return table;
        }
        private MyLinkedList<Entry<T, K>>[] TableUp()
        {
            MyLinkedList<Entry<T, K>>[] newTable = new MyLinkedList<Entry<T, K>>[table.Length * 2];
            numOfFullBuckets = 0;
            for (int numBucket = 0; numBucket < table.Length; numBucket++)
            {
                MyLinkedList<Entry<T, K>>.List<Entry<T, K>> p = table[numBucket].first;
                for (int i = 0; i < table[numBucket].Size(); i++)
                {
                    T key = p.info.key;
                    K value = p.info.value;
                    newTable = Put(key, value, newTable);
                    p = p.next;
                }
            }
            return newTable;
        }
        public MyHashMap()
        {
            table = new MyLinkedList<Entry<T, K>>[16];
            loadFactor = 0.75f;
            size = 0;
            numOfFullBuckets = 0;
        }
        public MyHashMap(int initialCapacity)
        {
            table = new MyLinkedList<Entry<T, K>>[initialCapacity];
            loadFactor = 0.75f;
            size = 0;
            numOfFullBuckets = 0;
        }
        public MyHashMap(int initialCapacity, float loadFactor)
        {
            table = new MyLinkedList<Entry<T, K>>[initialCapacity];
            this.loadFactor = loadFactor;
            size = 0;
            numOfFullBuckets = 0;
        }
        public void Clear()
        {
            for (int i = 0; i < size; i++)
            {
                table[i] = new MyLinkedList<Entry<T, K>>();
            }
            size = 0;
        }
        public bool ContainsKey(object key)
        {
            int hash = Math.Abs(key.GetHashCode());
            int numBucket = hash % table.Length;
            if (table[numBucket] == null || table[numBucket].Size() == 0)
            {
                return false;
            }
            else
            {
                MyLinkedList<Entry<T, K>>.List<Entry<T, K>> p = table[numBucket].first;
                for (int i = 0; i < table[numBucket].Size(); i++)
                {
                    if (key.Equals(p.info.key))
                    {
                        return true;
                    }
                    p = p.next;
                }
            }
            return false;
        }
        public bool ContainsValue(object value)
        {
            for (int numBucket = 0; numBucket < table.Length; numBucket++)
            {
                MyLinkedList<Entry<T, K>>.List<Entry<T, K>> p = table[numBucket].first;
                for (int i = 0; i < table[numBucket].Size(); i++)
                {
                    if (value.Equals(p.info.value))
                    {
                        return true;
                    }
                    p = p.next;
                }
            }
            return false;
        }
        public Entry<T, K>[] EntrySet()
        {
            Entry<T, K>[] result = new Entry<T, K>[size];
            int index = 0;
            for (int i = 0; i < table.Length; i++)
            {
                if (table[i] == null || table[i].Size() == 0)
                {

                }
                else
                {
                    MyLinkedList<Entry<T, K>>.List<Entry<T, K>> p = table[i].first;
                    for (int j = 0; j < table[i].Size(); j++)
                    {
                        result[index] = p.info;
                        index++;
                        p = p.next;
                    }
                }
            }
            return result;
        }
        public object Get(object key)
        {
            int hash = Math.Abs(key.GetHashCode());
            int numBucket = hash % table.Length;
            if (table[numBucket] == null || table[numBucket].Size() == 0)
            {
                return null;
            }
            else
            {
                MyLinkedList<Entry<T, K>>.List<Entry<T, K>> p = table[numBucket].first;
                for (int i = 0; i < table[numBucket].Size(); i++)
                {
                    if (key.Equals(p.info.key))
                    {
                        return p.info.value;

                    }
                    p = p.next;
                }
            }
            return null;
        }
        public bool IsEmpty()
        {
            return size == 0 ? true : false;
        }
        public T[] KeySet()
        {
            T[] result = new T[size];
            int index = 0;
            for (int i = 0; i < table.Length; i++)
            {
                if (table[i] == null || table[i].Size() == 0)
                {

                }
                else
                {
                    MyLinkedList<Entry<T, K>>.List<Entry<T, K>> p = table[i].first;
                    for (int j = 0; i < table[i].Size(); j++)
                    {
                        result[index] = p.info.key;
                        index++;
                        p = p.next;
                    }
                }
            }
            return result;
        }
        public void Remove(object key)
        {
            int hash = Math.Abs(key.GetHashCode());
            int numBucket = hash % table.Length;
            if (table[numBucket].Size() != 0)
            {
                MyLinkedList<Entry<T, K>>.List<Entry<T, K>> p = table[numBucket].first;
                if (table[numBucket].Size() == 1)
                {
                    if (key.Equals(p.info.key))
                    {
                        table[numBucket].Remove(p.info);
                        size--;
                        numOfFullBuckets--;
                        return;
                    }
                }
                for (int i = 0; i < table[numBucket].Size(); i++)
                {
                    if (key.Equals(p.info.key))
                    {
                        table[numBucket].Remove(p.info);
                        size--;
                        return;
                    }
                    p = p.next;
                }
            }
        }
        public int Size()
        {
            return size;
        }
    }
}
