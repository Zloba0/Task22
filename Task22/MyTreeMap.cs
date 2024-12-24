
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using Task22;

namespace Task22
{
    internal class MyTreeMap<K, T>
    {
        public class TreeMapComparer : IComparer<K>
        {
            public int Compare(K x, K y)
            {
                return Comparer<K>.Default.Compare(x, y);
            }

        }

        Tree<K, T> root;
        int size;
        TreeMapComparer comparator;
        public class Tree<K, T>
        {
            public Entry<K, T> info;
            public Tree<K, T> r;
            public Tree<K, T> l;
            public Tree(Entry<K, T> x)
            {
                info = x;
                r = null;
                l = null;
            }
            public Tree(Tree<K, T> x)
            {
                info = x.info;
                r = x.r;
                l = x.l;
            }

        }
        public void AddTree(Tree<K, T> x, Tree<K, T> root1)
        {
            if (root1 == null)
            {
                root = new Tree<K, T>(x);
            }
            if (Comparer<K>.Default.Compare(x.info.key, root1.info.key) == 1)
            {
                if (root1.r == null)
                {
                    root1.r = new Tree<K, T>(x);
                }
                else
                {
                    AddTree(x, root1.r);
                }
            }
            else
            {
                if (root1.l == null)
                {
                    root1.l = new Tree<K, T>(x);
                }
                else
                {
                    AddTree(x, root1.l);
                }
            }
        }
        public void AddTree(Entry<K, T> x, Tree<K, T> root1)
        {
            if (root1 == null)
            {
                root = new Tree<K, T>(x);
                size++;
            }
            else if (Comparer<K>.Default.Compare(x.key, root1.info.key) == 1)
            {
                if (root1.r == null)
                {
                    root1.r = new Tree<K, T>(x);
                    size++;
                }
                else
                {
                    AddTree(x, root1.r);
                }
            }
            else
            {
                if (root1.l == null)
                {
                    root1.l = new Tree<K, T>(x);
                    size++;
                }
                else
                {
                    AddTree(x, root1.l);
                }
            }
        }
        public MyTreeMap()
        {
            size = 0;
            root = null;
            comparator = new TreeMapComparer();
        }
        public MyTreeMap(Entry<K, T>[] x)
        {
            size = 0;
            for (int i = 0; i < x.Length; i++)
            {
                Put(x[i]);
            }
            comparator = new TreeMapComparer();
        }
        public MyTreeMap(TreeMapComparer comp)
        {
            size = 0;
            root = null;
            comparator = comp;
        }
        public void Clear()
        {
            size = 0;
            root = null;
        }
        public bool ContainsKey(K Key)
        {
            return ContainsKey(Key, root);
        }
        public bool ContainsKey(K Key, Tree<K, T> root1)
        {
            if (root1 == null)
            {
                return false;
            }
            if (comparator.Compare(Key, root1.info.key) == 0)
            {
                return true;
            }
            else if (comparator.Compare(Key, root1.info.key) == 1)
            {
                return ContainsKey(Key, root1.r);
            }
            else
            {
                return ContainsKey(Key, root1.l);
            }
        }
        public bool ContainsValue(T value)
        {
            return ContainsValue(value, root);
        }
        public bool ContainsValue(T value, Tree<K, T> root1)
        {
            if (root1 == null)
            {
                return false;
            }
            if (Comparer<T>.Default.Compare(root1.info.value, value) == 0)
            {
                return true;
            }
            return ContainsValue(value, root1.l) || ContainsValue(value, root1.r);
        }
        public Entry<K, T>[] EntrySet()
        {
            MyVector<Entry<K, T>> set = new MyVector<Entry<K, T>>();
            if (root == null)
            {
                throw new InvalidOperationException();
            }
            set = EntrySet(set, root);
            return set.ToArray();
        }
        public MyVector<Entry<K, T>> EntrySet(MyVector<Entry<K, T>> set, Tree<K, T> root1)
        {
            if (root1 == null)
            {
                return set;
            }
            if (!set.Contains(root1.info))
            {
                set.Add(root1.info);
            }
            EntrySet(set, root1.l);
            EntrySet(set, root1.r);
            return set;
        }
        public object Get(K key)
        {
            return Get(key, root);
        }
        public object Get(K key, Tree<K, T> root1)
        {
            if (root1 == null)
            {
                return null;
            }
            if (comparator.Compare(key, root1.info.key) == 0)
            {
                return root1.info.value;
            }
            else if (comparator.Compare(key, root1.info.key) == 1)
            {
                return Get(key, root1.r);
            }
            else
            {
                return Get(key, root1.l);
            }
        }
        public T GetT(K key)
        {
            return GetT(key, root);
        }
        public T GetT(K key, Tree<K, T> root1)
        {
            if (root1 == null)
            {
                throw new InvalidOperationException();
            }
            if (comparator.Compare(key, root1.info.key) == 0)
            {
                return root1.info.value;
            }
            else if (comparator.Compare(key, root1.info.key) == 1)
            {
                return GetT(key, root1.r);
            }
            else
            {
                return GetT(key, root1.l);
            }
        }
        public bool IsEmpty()
        {
            if (size == 0 || root == null)
            {
                return true;
            }
            return false;
        }
        public K[] KeySet()
        {
            MyVector<K> set = new MyVector<K>();
            if (root == null)
            {
                throw new InvalidOperationException();
            }
            set = KeySet(set, root);
            return set.ToArray();
        }
        public MyVector<K> KeySet(MyVector<K> set, Tree<K, T> root1)
        {
            if (root1 == null)
            {
                return set;
            }
            if (!set.Contains(root1.info))
            {
                set.Add(root1.info.key);
            }
            KeySet(set, root1.l);
            KeySet(set, root1.r);
            return set;
        }
        public void Put(Entry<K, T> x)
        {
            AddTree(x, root);
        }
        public void Put(K key, T value)
        {
            Entry<K, T> x = new Entry<K, T>(key, value);
            AddTree(x, root);
        }
        public void Remove(K key)
        {
            Remove(key, root, null, false);
        }
        public void Remove(K Key, Tree<K, T> root1, Tree<K, T> rootMuter, bool left)
        {
            if (root1 == null)
            {
                return;
            }
            if (comparator.Compare(Key, root1.info.key) == 0)
            {
                if (rootMuter == null && root1.l == null && root1.r == null)
                {
                    size = 0;
                    root = null;
                }
                else if (rootMuter == null && root1.r == null)
                {
                    root = root1.l;
                    size--;
                }
                else if (rootMuter == null && root1.l == null)
                {
                    root = root1.r;
                    size--;
                }
                else if (rootMuter == null)
                {
                    root = root1.r;
                    Bunching(root1.r, root1.l);
                    size--;
                }
                else if (root1.l == null && root1.r == null)
                {
                    rootMuter = (left ? rootMuter.l = null : rootMuter.r = null);
                    size--;
                }
                else if (root1.l == null)
                {
                    rootMuter = (left ? rootMuter.l = root1.r : rootMuter.r = root1.r);
                    size--;
                }
                else if (root1.r == null)
                {
                    rootMuter = (left ? rootMuter.l = root1.l : rootMuter.r = root1.l);
                    size--;
                }
                else
                {
                    rootMuter = (left ? rootMuter.l = root1.r : rootMuter.r = root1.r);
                    Bunching(rootMuter.l, root1.l);
                    size--;
                }
            }
            else if (comparator.Compare(Key, root1.info.key) == 1)
            {
                Remove(Key, root1.r, root1, false);
            }
            else
            {
                Remove(Key, root1.l, root1, true);
            }
        }
        public void Bunching(Tree<K, T> rootFrom, Tree<K, T> rootAdd)
        {
            AddTree(rootAdd, rootFrom);
        }
        public int Size()
        {
            return size;
        }
        public K FirstKey()
        {
            if (root == null)
            {
                throw new InvalidOperationException();
            }
            return root.info.key;
        }
        public K LastKey()
        {
            return LastKey(root);
        }
        public K LastKey(Tree<K, T> root1)
        {
            if (root1 == null)
            {
                throw new Exception();
            }
            if (root1.r != null)
            {
                return LastKey(root1.r);
            }
            else if (root1.l != null)
            {
                return LastKey(root1.l);
            }
            else
            {
                return root1.info.key;
            }
        }
        public MyTreeMap<K, T> HeadMap(K end)
        {
            MyVector<Entry<K, T>> set = new MyVector<Entry<K, T>>();
            if (root == null)
            {
                throw new InvalidOperationException();
            }
            set = HeadMap(end, set, root);
            return new MyTreeMap<K, T>(set.ToArray());
        }
        public MyVector<Entry<K, T>> HeadMap(K end, MyVector<Entry<K, T>> set, Tree<K, T> root1)
        {
            if (root1 == null)
            {
                return set;
            }
            if (comparator.Compare(end, root1.info.key) == 1)
            {
                if (!set.Contains(root1.info))
                {
                    set.Add(root1.info);
                }
            }
            HeadMap(end, set, root1.l);
            HeadMap(end, set, root1.r);
            return set;
        }

        public MyTreeMap<K, T> SubdMap(K start, K end)
        {
            MyVector<Entry<K, T>> set = new MyVector<Entry<K, T>>();
            if (root == null)
            {
                throw new InvalidOperationException();
            }
            set = SubMap(start, end, set, root);
            return new MyTreeMap<K, T>(set.ToArray());
        }
        public MyVector<Entry<K, T>> SubMap(K start, K end, MyVector<Entry<K, T>> set, Tree<K, T> root1)
        {
            if (root1 == null)
            {
                return set;
            }
            if (comparator.Compare(end, root1.info.key) == 1 && comparator.Compare(start, root1.info.key) != 1)
            {
                if (!set.Contains(root1.info))
                {
                    set.Add(root1.info);
                }
            }
            SubMap(start, end, set, root1.l);
            SubMap(start, end, set, root1.r);
            return set;
        }

        public MyTreeMap<K, T> TailMap(K start)
        {
            MyVector<Entry<K, T>> set = new MyVector<Entry<K, T>>();
            if (root == null)
            {
                throw new InvalidOperationException();
            }
            set = TailMap(start, set, root);
            return new MyTreeMap<K, T>(set.ToArray());
        }
        public MyVector<Entry<K, T>> TailMap(K start, MyVector<Entry<K, T>> set, Tree<K, T> root1)
        {
            if (root1 == null)
            {
                return set;
            }
            if (comparator.Compare(start, root1.info.key) == -1)
            {
                if (!set.Contains(root1.info))
                {
                    set.Add(root1.info);
                }
            }
            TailMap(start, set, root1.l);
            TailMap(start, set, root1.r);
            return set;
        }

        public Entry<K, T> FlorEntry(K end)
        {
            if (root == null)
            {
                throw new InvalidOperationException();
            }
            return FlorEntry(end, root);
        }
        public Entry<K, T> FlorEntry(K end, Tree<K, T> root1)
        {
            if (comparator.Compare(end, root1.info.key) != -1)
            {
                return root1.info;
            }
            else if (root1.l != null)
            {
                return FlorEntry(end, root1.l);
            }
            else
            {
                throw new Exception();
            }
        }

        public Entry<K, T> LowerEntry(K end)
        {
            if (root == null)
            {
                throw new InvalidOperationException();
            }
            return LowerEntry(end, root);
        }
        public Entry<K, T> LowerEntry(K end, Tree<K, T> root1)
        {
            if (comparator.Compare(end, root1.info.key) == 1)
            {
                return root1.info;
            }
            else if (root1.l != null)
            {
                return LowerEntry(end, root1.l);
            }
            else
            {
                throw new Exception();
            }
        }
        public Entry<K, T> HierEntry(K end)
        {

            if (root == null)
            {
                throw new InvalidOperationException();
            }
            return HierEntry(end, root);
        }
        public Entry<K, T> HierEntry(K end, Tree<K, T> root1)
        {
            if (comparator.Compare(end, root1.info.key) == -1)
            {
                return root1.info;
            }
            else if (root1.r != null)
            {
                return HierEntry(end, root1.r);
            }
            else
            {
                throw new Exception();
            }
        }

        public Entry<K, T> CeilingEntry(K end)
        {
            if (root == null)
            {
                throw new InvalidOperationException();
            }
            return CeilingEntry(end, root);
        }
        public Entry<K, T> CeilingEntry(K end, Tree<K, T> root1)
        {
            if (comparator.Compare(end, root1.info.key) != 1)
            {
                return root1.info;
            }
            else if (root1.r != null)
            {
                return CeilingEntry(end, root1.r);
            }
            else if (root1.l != null)
            {
                return CeilingEntry(end, root1.l);
            }
            else
            {
                throw new Exception();
            }
        }

        public K LowerKey(K end)
        {
            if (root == null)
            {
                throw new InvalidOperationException();
            }
            return LowerKey(end, root);
        }
        public K LowerKey(K end, Tree<K, T> root1)
        {
            if (comparator.Compare(end, root1.info.key) == 1)
            {
                return root1.info.key;
            }
            else if (root1.l != null)
            {
                return LowerKey(end, root1.l);
            }
            else
            {
                throw new Exception();
            }
        }
        public K FlorKey(K end)
        {
            if (root == null)
            {
                throw new InvalidOperationException();
            }
            return FlorKey(end, root);
        }
        public K FlorKey(K end, Tree<K, T> root1)
        {
            if (comparator.Compare(end, root1.info.key) != -1)
            {
                return root1.info.key;
            }
            else if (root1.l != null)
            {
                return FlorKey(end, root1.l);
            }
            else
            {
                throw new Exception();
            }
        }
        public K HierKey(K end)
        {

            if (root == null)
            {
                throw new InvalidOperationException();
            }
            return HierKey(end, root);
        }
        public K HierKey(K end, Tree<K, T> root1)
        {
            if (comparator.Compare(end, root1.info.key) == -1)
            {
                return root1.info.key;
            }
            else if (root1.r != null)
            {
                return HierKey(end, root1.r);
            }
            else
            {
                throw new Exception();
            }
        }
        public K CeilingKey(K end)
        {
            if (root == null)
            {
                throw new InvalidOperationException();
            }
            return CeilingKey(end, root);
        }
        public K CeilingKey(K end, Tree<K, T> root1)
        {
            if (comparator.Compare(end, root1.info.key) != 1)
            {
                return root1.info.key;
            }
            else if (root1.r != null)
            {
                return CeilingKey(end, root1.r);
            }
            else if (root1.l != null)
            {
                return CeilingKey(end, root1.l);
            }
            else
            {
                throw new Exception();
            }
        }
        public Entry<K, T> PullFirstEntry()
        {
            if (root == null)
            {
                throw new InvalidOperationException();
            }
            Entry<K, T> tmp = root.info;
            if (root.l == null && root.r == null)
            {
                size = 0;
                root = null;
            }
            else if (root.r == null)
            {
                root = root.l;
                size--;
            }
            else if (root.l == null)
            {
                root = root.r;
                size--;
            }
            else
            {
                root = root.r;
                Bunching(root.r, root.l);
                size--;
            }
            return tmp;
        }
        public Entry<K, T> PullLastEntry()
        {
            if (root == null)
            {
                throw new InvalidOperationException();
            }
            return RemuveLastKey(root, null, false);
        }

        public Entry<K, T> RemuveLastKey(Tree<K, T> root1, Tree<K, T> rootMuter, bool left)
        {
            if (rootMuter == null && root1.r == null && root1.l == null)
            {
                Entry<K, T> tmp = root1.info;
                size = 0;
                root = null;
                return tmp;
            }
            else if (root1.r != null)
            {
                return RemuveLastKey(root1.r, root1, false);
            }
            else if (root1.l != null)
            {
                return RemuveLastKey(root1.l, root1, true);
            }
            else
            {
                Entry<K, T> tmp = root1.info;
                rootMuter = (left ? rootMuter.l = null : rootMuter.r = null);
                size--;
                return tmp;
            }
        }
        public Entry<K, T> FirstEntry()
        {
            if (root == null)
            {
                throw new InvalidOperationException();
            }
            return root.info;
        }
        public Entry<K, T> LastEntry()
        {
            if (root == null)
            {
                throw new InvalidOperationException();
            }
            return LastEntry(root);
        }
        public Entry<K, T> LastEntry(Tree<K, T> root1)
        {
            if (root1 == null)
            {
                throw new Exception();
            }
            if (root1.r != null)
            {
                return LastEntry(root1.r);
            }
            else if (root1.l != null)
            {
                return LastEntry(root1.l);
            }
            else
            {
                return root1.info;
            }
        }
        public void Output(Tree<K, T> root1)
        {
            if (root == null)
            {
                return;
            }
            Output(root1.l);
            Console.WriteLine($"({root1.info.key},{root1.info.value})\n");
            Output(root1.r);
        }
    }
}