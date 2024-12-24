
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Task22
{
    internal class MyLinkedList<T>
    {
        public class List<T>
        {
            public T info;
            public List<T> next;
            public List<T> pred;
            public List(T x)
            {
                info = x;
                next = null;
                pred = null;
            }
            public List()
            {
                info = default;
                next = null;
                pred = null;
            }
            public List<T> AddList(T x)
            {
                List<T> r = new List<T>(x);
                r.pred = this;
                this.next = r;
                return r;
            }
        }
        public List<T> first;
        public List<T> last;
        int size;
        public MyLinkedList()
        {
            size = 0;
            first = new List<T>();
            last = new List<T>();
        }
        public MyLinkedList(T[] mas)
        {
            size = (mas.Length == 0 ? throw new Exception() : mas.Length);
            first = new List<T>(mas[0]);
            last = first;
            for (int i = 1; i < size; i++)
            {
                last = last.AddList(mas[i]);
            }
        }
        public MyLinkedList(T x)
        {
            size = 1;
            first = new List<T>(x);
            last = first;
        }
        public void Add(T x)
        {
            if (size == 0)
            {
                first = new List<T>(x);
                last = first;
            }
            else
            {
                last = last.AddList(x);
            }
            size++;
        }
        public void AddAll(T[] mas)
        {
            for (int i = 0; i < size; i++)
            {
                this.Add(mas[i]);
            }
        }
        public void Clear()
        {
            size = 0;
            first = new List<T>();
            last = new List<T>();
        }
        public bool Contains(object o)
        {
            List<T> p = first;
            for (int i = 0; i < size; ++i)
            {
                if (p.info.Equals(o))
                {
                    return true;
                }
                p = p.next;
            }
            return false;
        }
        public bool ContainsAll(T[] mas)
        {
            for (int i = 0; i < mas.Length; ++i)
            {
                if (!Contains(mas[i]))
                {
                    return false;
                }
            }
            return true;
        }
        public bool IsEmpty()
        {
            if (size == 0)
            {
                return true;
            }
            return false;
        }
        public void Remove(object o)
        {
            if (size == 0)
            {
                return;
            }
            List<T> p = first;
            for (int i = 0; i < size; ++i)
            {
                if (p.info.Equals(o))
                {
                    if (p.pred == null && p.next == null)
                    {
                        this.Clear();
                        return;
                    }
                    else if (p.pred == null)
                    {
                        p.next.pred = null;
                        first = p.next;
                        size--;
                        return;
                    }
                    else if (p.next == null)
                    {
                        p.pred.next = null;
                        last = p.pred;
                        size--;
                        return;
                    }
                    else
                    {
                        p.pred.next = p.next;
                        p.next.pred = p.pred;
                        size--;
                        return;
                    }
                }
                p = p.next;
            }
            return;
        }
        public void RemoveAll(T[] mas)
        {
            for (int i = 0; i < mas.Length; ++i)
            {
                this.Remove(mas[i]);
            }
        }
        public void RetainAll(T[] mas)
        {
            List<T> p = first;
            for (int i = 0; i < size; ++i)
            {
                bool needToDel = true;
                for (int j = 0; j < mas.Length; ++j)
                {

                    if (p.info.Equals(mas[i]))
                    {
                        needToDel = false;
                        break;
                    }
                }
                if (needToDel)
                {
                    this.Remove(p.info);
                }
                p = p.next;
            }
        }
        public int Size()
        {
            return size;
        }
        public T[] ToArray()
        {
            List<T> p = first;
            T[] array = new T[size];
            for (int i = 0; i < size; ++i)
            {
                array[i] = p.info;
                p = p.next;
            }
            return array;
        }
        public T[] ToArray(T[] mas)
        {
            if (mas == null)
            {
                return ToArray();
            }
            List<T> p = first;
            T[] array = new T[size + mas.Length];
            mas.CopyTo(array, 0);
            ToArray().CopyTo(array, mas.Length);
            return array;
        }
        public void Add(int index, T x)
        {
            if (index < 0)
            {
                throw new ArgumentOutOfRangeException("Wrong index");
            }
            if (index == 0)
            {
                List<T> r = new List<T>(x);
                r.next = first;
                first.pred = r;
                first = r;
                size++;
            }
            else if (index < size)
            {
                List<T> p = first;
                for (int i = 0; i < size; ++i)
                {
                    if (i == index - 1)
                    {
                        List<T> r = new List<T>(x);
                        r.next = p.next;
                        r.pred = p;
                        p.next.pred = r;
                        p.next = r;
                        size++;
                        return;
                    }
                    p = p.next;
                }
            }
            else
            {
                int j = size;
                for (; j < index; j++)
                {
                    Add(default);
                }
                Add(x);
            }
        }
        public void AddAll(int index, T[] mas)
        {
            if (index < 0)
            {
                throw new ArgumentOutOfRangeException("Wrong index");
            }
            if (index == 0)
            {
                MyLinkedList<T> r = new MyLinkedList<T>(mas);
                r.last = first;
                first = r.last;
                first = r.first;
                size += r.size;
            }
            else if (index < size)
            {
                List<T> p = first;
                for (int i = 0; i < size; ++i)
                {
                    if (i == index - 1)
                    {
                        MyLinkedList<T> r = new MyLinkedList<T>(mas);
                        r.last = p.next;
                        r.first = p;
                        p.next.pred = r.last;
                        p.next = r.first;
                        size += r.size;
                        return;
                    }
                    p = p.next;
                }
            }
            else
            {
                int j = size;
                for (; j < index; j++)
                {
                    Add(default);
                }
                AddAll(mas);
            }
        }
        public T Get(int index)
        {
            if (index < 0)
            {
                throw new ArgumentOutOfRangeException();
            }
            List<T> p = first;
            for (int i = 0; i < size; ++i)
            {
                if (i == index)
                {
                    return p.info;
                }
                p = p.next;
            }
            throw new IndexOutOfRangeException();
        }
        public int IndexOf(object o)
        {
            List<T> p = first;
            for (int i = 0; i < size; ++i)
            {
                if (o.Equals(p.info))
                {
                    return i;
                }
                p = p.next;
            }
            return -1;
        }
        public int LastIndexOf(object o)
        {
            List<T> p = last;
            for (int i = size - 1; i >= 0; --i)
            {
                if (o.Equals(p.info))
                {
                    return i;
                }
                p = p.pred;
            }
            return -1;
        }
        public T Remove(int index)
        {
            if (index < 0 || index >= size)
            {
                throw new ArgumentOutOfRangeException();
            }
            else if (size == 1)
            {
                size = 0;
                T tmp = first.info;
                first = null;
                last = null;
                return tmp;
            }
            else if (index == 0)
            {
                T tmp = first.info;
                first = first.next;
                first.pred = null;
                return tmp;
            }
            else if (index == size - 1)
            {
                T tmp = last.info;
                last = last.pred;
                last.next = null;
                return tmp;
            }
            else
            {
                List<T> p = first;
                for (int i = 0; i < index; ++i)
                {
                    p = p.next;
                }
                T tmp = last.info;
                p.pred.next = p.next;
                p.next.pred = p.pred;
                return tmp;
            }
        }
        public void Set(int index, T x)
        {
            if (index < 0 || index >= size)
            {
                throw new ArgumentOutOfRangeException();
            }
            List<T> p = first;
            for (int i = 0; i < size; ++i)
            {
                if (index == i)
                {
                    p.info = x;
                    return;
                }
                p = p.next;
            }
        }
        public MyLinkedList<T> SubList(int fromIndex, int toIndex)
        {
            if (fromIndex < 0 || toIndex < 0 || fromIndex >= size || toIndex >= size || fromIndex > toIndex)
            {
                throw new ArgumentOutOfRangeException();
            }
            List<T> p = first;
            for (int i = 0; i < fromIndex; ++i)
            {
                p = p.next;
            }
            MyLinkedList<T> subList = new MyLinkedList<T>();
            for (int i = fromIndex; i < toIndex; ++i)
            {
                subList.Add(p.info);
                p = p.next;
            }
            return subList;
        }
        public T Element()
        {
            if (size == 0)
            {
                throw new Exception();
            }
            return last.info;
        }
        public bool Offer(T obj)
        {
            try
            {
                Add(obj);
                return true;
            }
            catch
            {
                return false;
            }
        }
        public object Peek()
        {
            if (size == 0)
            {
                return null;
            }
            return last.info;
        }
        public object Poll()
        {
            if (size == 0)
            {
                return null;
            }
            object tmp = last.info;
            last = last.pred;
            last.next = null;
            size--;
            return tmp;
        }
        public void AddFirst(T obj)
        {
            Add(obj);
        }
        public void AddLast(T obj)
        {
            if (size == 0)
            {
                first = new List<T>(obj);
                last = first;
            }
            else
            {
                List<T> r = new List<T>(obj);
                r.next = first;
                first.pred = r;
                first = r;
            }
            size++;
        }
        public T GetFirst()
        {
            return first.info;
        }
        public T GetLast()
        {
            return last.info;
        }
        public bool OfferFirst(T obj)
        {
            try
            {
                Add(obj);
                return true;
            }
            catch
            {
                return false;
            }
        }
        public bool OfferLast(T obj)
        {
            try
            {
                AddLast(obj);
                return true;
            }
            catch
            {
                return false;
            }
        }
        public T Pор()
        {
            if (size == 0)
            {
                throw new Exception();
            }
            T tmp = last.info;
            last = last.pred;
            last.next = null;
            size--;
            return tmp;
        }
        public void Push(T obj)
        {
            Add(obj);
        }
        public object PeekFirst()
        {
            if (size == 0)
            {
                return null;
            }
            return last.info;
        }
        public object PeekLast()
        {
            if (size == 0)
            {
                return null;
            }
            return first.info;
        }
        public object PollFirst()
        {
            if (size == 0)
            {
                return null;
            }
            object tmp = last.info;
            last = last.pred;
            last.next = null;
            size--;
            return tmp;
        }
        public object PollLast()
        {
            if (size == 0)
            {
                return null;
            }
            object tmp = first.info;
            first = first.next;
            first.pred = null;
            size--;
            return tmp;
        }
        public T RemoveLast()
        {
            if (size == 0)
            {
                throw new Exception();
            }
            T tmp = last.info;
            last = last.pred;
            last.next = null;
            size--;
            return tmp;
        }
        public T RemoveFirst()
        {
            if (size == 0)
            {
                throw new Exception();
            }
            T tmp = first.info;
            first = first.next;
            first.pred = null;
            size--;
            return tmp;
        }
        public bool RemoveLastOccurrence(object obj)
        {
            List<T> p = last;
            for (int i = size - 1; i >= 0; --i)
            {
                if (p.info.Equals(obj))
                {
                    Remove(i);
                    return true;
                }
                p = p.pred;
            }
            return false;
        }
        public bool removeFirstOccurrence(object obj)
        {
            List<T> p = first;
            for (int i = 0; i < size; ++i)
            {
                if (p.info.Equals(obj))
                {
                    Remove(i);
                    return true;
                }
                p = p.next;
            }
            return false;
        }
    }
}

