
using System;
using System.Reflection;

namespace Task22
{
    internal class MyVector<tipe>
    {
        private int size;
        private tipe[] elementData;
        private int capacityInccrement;
        public MyVector(int initialCapacity, int capacityInccrement)
        {
            this.capacityInccrement = capacityInccrement;
            this.elementData = new tipe[initialCapacity];
            this.size = initialCapacity;
        }
        public MyVector(int initialCapacity)
        {
            this.capacityInccrement = 0;
            this.elementData = new tipe[initialCapacity];
            this.size = initialCapacity;
        }
        public MyVector()
        {
            this.capacityInccrement = 0;
            this.elementData = new tipe[10];
            this.size = 10;
        }
        public MyVector(tipe[] mas)
        {
            this.capacityInccrement = 0;
            this.elementData = new tipe[mas.Length];
            this.size = mas.Length;
        }
        public void Add(tipe x)
        {
            if (size == 0)
            {
                tipe[] newMas;
                if (capacityInccrement == 0)
                {
                    newMas = new tipe[1 + Convert.ToInt32(Math.Ceiling((float)size * 2))];
                }
                else
                {
                    newMas = new tipe[1 + Convert.ToInt32(Math.Ceiling((float)size + capacityInccrement))];
                }
                newMas[0] = x;
                elementData = newMas;
                size++;
            }
            else if (size < elementData.Length)
            {
                elementData[size] = x;
                size++;
            }
            else
            {
                tipe[] newMas;
                if (capacityInccrement == 0)
                {
                    newMas = new tipe[1 + Convert.ToInt32(Math.Ceiling((float)size * 2))];
                }
                else
                {
                    newMas = new tipe[1 + Convert.ToInt32(Math.Ceiling((float)size + capacityInccrement))];
                }
                for (int i = 0; i < size; i++)
                {
                    newMas[i] = elementData[i];
                }
                newMas[size] = x;
                elementData = newMas;
                size++;
            }
            return;
        }



        public void AddAll(tipe[] x)
        {
            for (int i = 0; i < x.Length; i++)
            {
                Add(x[i]);
            }
            return;
        }
        public void Clear()
        {
            size = 0;
            elementData = new tipe[0];
        }
        public bool Contains(object x)
        {

            for (int i = 0; i < size; i++)
            {
                if (x.Equals(elementData[i]))
                {
                    return true;
                }
            }
            return false;
        }
        public bool ContainsAll(tipe[] x)
        {
            if (size < x.Length)
            {
                return false;
            }
            bool flag = true;
            for (int i = 0; i < size; i++)
            {
                if (x[i].Equals(this.Get(i)))
                {
                    for (int j = i; j < x.Length; j++)
                    {
                        if (!this.Get(j).Equals(x[i]))
                        {
                            flag = false;
                            break;
                        }
                    }
                    if (flag)
                    {
                        return flag;
                    }
                }
                flag = true;
            }
            return false;
        }
        public tipe Get(int i)
        {
            if (i >= size)
            {
                throw new IndexOutOfRangeException();
            }
            return elementData[i];
        }
        public bool IsEmpty()
        {
            if (size == 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        public void ShiftLeft(int i)
        {
            for (int j = i; j < size; j++)
            {
                elementData[j] = elementData[j + 1];
            }
        }
        public void Remove(object x)
        {
            for (int i = 0; i < size; i++)
            {
                if (x.Equals(elementData[i]))
                {
                    ShiftLeft(i);
                    size--;
                    i--;
                }
            }
        }
        public void RemoveAll(tipe[] mas)
        {
            for (int j = 0; j < mas.Length; j++)
            {
                for (int i = 0; i < size; i++)
                {
                    if (mas[j].Equals(elementData[i]))
                    {
                        ShiftLeft(i);
                        size--;
                        i--;
                    }
                }
            }
        }
        public void RetainAll(tipe[] mas)
        {
            for (int j = 0; j < mas.Length; j++)
            {
                for (int i = 0; i < size; i++)
                {
                    if (!mas[j].Equals(elementData[i]))
                    {
                        ShiftLeft(i);
                        size--;
                        i--;
                    }
                }
            }
        }
        public int Size()
        {
            return size;
        }
        public tipe[] ToArray()
        {
            tipe[] newMas = new tipe[size];
            for (int i = 0; i < size; i++)
            {
                newMas[i] = elementData[i];
            }
            return newMas;
        }
        public tipe[] ToArray(tipe[] mas)
        {
            if (mas == null)
            {
                return ToArray();
            }
            else
            {
                if (mas.Length >= size)
                {
                    for (int i = 0; i < size; i++)
                    {
                        mas[i] = elementData[i];
                    }
                    return mas;
                }
                else
                {
                    throw new Exception("Wrong function call");
                }
            }
        }
        public void ShifRight(int i)
        {
            for (int j = size; j > i; j--)
            {
                elementData[j] = elementData[j - 1];
            }
        }
        public void Add(int index, tipe x)
        {
            if (elementData.Length > size)
            {
                ShifRight(index);
                elementData[index] = x;
                size++;
            }
            else
            {
                Add(x);
                ShifRight(index);
            }
        }
        public void AddAll(int index, tipe[] mas)
        {
            while (elementData.Length - size < mas.Length)
            {
                AddAll(mas);
            }
            for (int i = 0; i < mas.Length; i++, index++)
            {
                Add(index, mas[i]);
            }
            size += mas.Length;
        }
        public int IndexOf(object o)
        {
            for (int i = 0; i < size; i++)
            {
                if (o.Equals(elementData[i]))
                {
                    return i;
                }
            }
            return -1;
        }
        public int LastIndexOf(object o)
        {
            for (int i = size - 1; i >= 0; i--)
            {
                if (o.Equals(elementData[i]))
                {
                    return i;
                }
            }
            return -1;
        }
        public tipe Remove(int index)
        {
            tipe result = elementData[index];
            ShiftLeft(index);
            size--;
            return result;
        }
        public void Set(int index, tipe x)
        {
            elementData[index] = x;
        }
        public MyVector<tipe> SubList(int fromIndex, int toIndex)
        {
            if (fromIndex < 0 || toIndex > size)
            {
                throw new Exception("Wrong function call");
            }
            else if (fromIndex > toIndex)
            {
                throw new Exception("Wrong function call");
            }
            else
            {
                tipe[] newMas = new tipe[toIndex - fromIndex + 1];
                for (int i = fromIndex; i < toIndex + 1; i++)
                {
                    newMas[i] = elementData[i];
                }
                MyVector<tipe> newList = new MyVector<tipe>(newMas);
                return newList;
            }
        }
        public tipe FirstElement()
        {
            return elementData[0];
        }
        public tipe LastElement()
        {
            return elementData[size - 1];
        }
        public void RemoveElementAt(int index)
        {
            if (index < 0 || index >= size)
            {
                throw new Exception("Wrong function call");
            }
            ShiftLeft(index);
            size--;
        }
        public void removeRange(int begin, int end)
        {
            if (begin < 0 || end >= size || end < begin)
            {
                throw new Exception("Wrong function call");
            }
            for (int i = end; i >= begin; i++)
            {
                ShiftLeft(i);
                size--;
            }
        }
    }
}