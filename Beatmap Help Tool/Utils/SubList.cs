using Beatmap_Help_Tool.BeatmapTools;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Beatmap_Help_Tool.Utils
{
    public class SubList<T> : List<T>, IList<T>, ICollection<T>, IEnumerable<T>, IReadOnlyList<T>, IReadOnlyCollection<T>
    {
        private readonly List<T> originalList;
        private int startIndex;
        private int endIndex;

        public SubList(List<T> originalList, int startIndex) : this(originalList, startIndex, originalList.Count - 1)
        {
            
        }

        public SubList(List<T> originalList, int startIndex, int endIndex)
        {
            this.originalList = originalList;
            this.startIndex = startIndex;
            this.endIndex = endIndex;

            VerifyUtils.verifyListRangeOrThrow(originalList, startIndex, endIndex);
        }

        public new T this[int index]
        {
            get
            {
                return originalList[startIndex + index];
            }
            set
            {
                originalList[startIndex + index] = value;
            }
        }

        public new int Count
        {
            get
            {
                return endIndex - startIndex + 1;
            }
        }

        public new void Add(T item)
        {
            originalList.Insert(endIndex, item);
            endIndex++;
        }

        public new void AddRange(IEnumerable<T> collection)
        {
            originalList.InsertRange(endIndex, collection);
            endIndex += collection.Count();
        }

        public new void Clear()
        {
            originalList.RemoveRange(startIndex, endIndex - startIndex);
            endIndex = startIndex;
        }

        public new bool Contains(T item)
        {
            for (int i = startIndex; i <= endIndex; i++)
                if (originalList[i].Equals(item))
                    return true;

            return false;
        }

        public void TrimStart(Predicate<T> match)
        {
            int index;
            while ((index = FindIndex(match)) != -1)
                startIndex += index + 1;
        }

        public void TrimEnd(Predicate<T> match)
        {
            int index;
            while ((index = FindLastIndex(match)) != -1)
                endIndex = startIndex + index - 1;
        }

        public new bool Exists(Predicate<T> match)
        {
            for (int i = startIndex; i <= endIndex; i++)
                if (match.Invoke(originalList[i]))
                    return true;

            return false;
        }

        public new T Find(Predicate<T> match)
        {
            for (int i = startIndex; i <= endIndex; i++)
                if (match.Invoke(originalList[i]))
                    return originalList[i];

            return default;
        }

        public new List<T> FindAll(Predicate<T> match)
        {
            List<T> list = new List<T>();
            for (int i = startIndex; i <= endIndex; i++)
                if (match.Invoke(originalList[i]))
                    list.Add(originalList[i]);
            return list;
        }

        public new int FindIndex(Predicate<T> match)
        {
            for (int i = startIndex; i <= endIndex; i++)
                if (match.Invoke(originalList[i]))
                    return i - startIndex;
            return -1;
        }
        public new int FindIndex(int startIndex, Predicate<T> match)
        {
            for (int i = this.startIndex + startIndex; i <= endIndex; i++)
                if (match.Invoke(originalList[i]))
                    return i - startIndex;
            return -1;
        }
        public new int FindIndex(int startIndex, int count, Predicate<T> match)
        {
            return originalList.GetRange(startIndex, endIndex).FindIndex(startIndex, count, match);
        }
        public new T FindLast(Predicate<T> match)
        {
            for (int i = endIndex; i >= startIndex; i--)
                if (match.Invoke(originalList[i]))
                    return originalList[i];

            return default;
        }
        public new int FindLastIndex(Predicate<T> match)
        {
            for (int i = endIndex; i >= startIndex; i--)
                if (match.Invoke(originalList[i]))
                    return i - startIndex;

            return -1;
        }
        public new int FindLastIndex(int startIndex, Predicate<T> match)
        {
            for (int i = endIndex; i >= startIndex + this.startIndex; i--)
                if (match.Invoke(originalList[i]))
                    return i - startIndex;

            return -1;
        }
        public new int FindLastIndex(int startIndex, int count, Predicate<T> match)
        {
            return originalList.GetRange(startIndex, endIndex).FindLastIndex(startIndex, count, match);
        }
        public new List<T> GetRange(int startIndex, int count)
        {
            return new SubList<T>(this, startIndex, startIndex + count);
        }
        public new IEnumerator<T> GetEnumerator()
        {
            return new Enumerator<T>(this);
        }
        public new int IndexOf(T item, int index, int count)
        {
            return originalList.GetRange(startIndex, endIndex).IndexOf(item, index, count);
        }
        public new int IndexOf(T item, int index)
        {
            for (int i = startIndex + index; i <= endIndex; i++)
                if (item.Equals(originalList[i]))
                    return i - startIndex;
            return -1;
        }
        public new int IndexOf(T item)
        {
            for (int i = startIndex; i <= endIndex; i++)
                if (item.Equals(originalList[i]))
                    return i - startIndex;
            return -1;
        }
        public new void Insert(int index, T item)
        {
            originalList.Insert(startIndex + index, item);
            endIndex++;
        }
        public new void InsertRange(int index, IEnumerable<T> collection)
        {
            originalList.InsertRange(startIndex + index, collection);
            endIndex += collection.Count();
        }
        public new int LastIndexOf(T item)
        {
            for (int i = endIndex; i >= startIndex; i--)
                if (item.Equals(originalList[i]))
                    return i - startIndex;
            return -1;
        }
        public new int LastIndexOf(T item, int index)
        {
            for (int i = endIndex; i >= startIndex + index; i--)
                if (item.Equals(originalList[i]))
                    return i - startIndex;
            return -1;
        }
        public new int LastIndexOf(T item, int index, int count)
        {
            return originalList.GetRange(startIndex, endIndex).LastIndexOf(item, index, count);
        }
        public new bool Remove(T item)
        {
            bool found = false;
            for (int i = startIndex; i <= endIndex; i++)
            {
                if (originalList[i].Equals(item))
                {
                    originalList.RemoveAt(i);
                    found = true;
                    break;
                }
            }
            return found;
        }
        public new int RemoveAll(Predicate<T> match)
        {
            int removedCount = 0;
            for (int i = startIndex; i <= endIndex; i++)
            {
                if (match.Invoke(originalList[i]))
                {
                    originalList.RemoveAt(i);
                    i--;
                    removedCount++;
                }
            }
            endIndex -= removedCount;
            return removedCount;
        }
        public new void RemoveAt(int index)
        {
            originalList.RemoveAt(index + startIndex);
        }
        public new void RemoveRange(int index, int count)
        {
            originalList.RemoveRange(index + startIndex, count);
        }
        public new void Reverse(int index, int count)
        {
            originalList.Reverse(startIndex + index, count);
        }
        public new void Reverse()
        {
            originalList.Reverse(startIndex, endIndex);
        }
        public new void Sort(int index, int count, IComparer<T> comparer)
        {
            for (int i = startIndex + index; i <= startIndex + count - 1; i++)
            {
                if (comparer.Compare(originalList[i], originalList[i + 1]) > 0)
                {
                    T temp = originalList[i];
                    originalList[i] = originalList[i + 1];
                    originalList[i + 1] = temp;
                }
            }
        }
        public new void Sort(Comparison<T> comparison)
        {
            for (int i = startIndex; i < endIndex; i++)
            {
                if (comparison.Invoke(originalList[i], originalList[i + 1]) > 0)
                {
                    T temp = originalList[i];
                    originalList[i] = originalList[i + 1];
                    originalList[i + 1] = temp;
                }
            }
        }
        public new void Sort()
        {
            if (originalList[startIndex] is IComparable)
            {
                for (int i = startIndex; i < endIndex; i++)
                {
                    if (((IComparable) originalList[i]).CompareTo(originalList[i + 1]) > 0)
                    {
                        T temp = originalList[i];
                        originalList[i] = originalList[i + 1];
                        originalList[i + 1] = temp;
                    }
                }
            }
            else if (originalList[startIndex] is IComparable<T>)
            {
                for (int i = startIndex; i < endIndex; i++)
                {
                    if (((IComparable<T>)originalList[i]).CompareTo(originalList[i + 1]) > 0)
                    {
                        T temp = originalList[i];
                        originalList[i] = originalList[i + 1];
                        originalList[i + 1] = temp;
                    }
                }
            }
        }
        public new void Sort(IComparer<T> comparer)
        {
            for (int i = startIndex; i < endIndex; i++)
            {
                if (comparer.Compare(originalList[i], originalList[i + 1]) > 0)
                {
                    T temp = originalList[i];
                    originalList[i] = originalList[i + 1];
                    originalList[i + 1] = temp;
                }
            }
        }
        public new T[] ToArray()
        {
            T[] array = new T[endIndex - startIndex];
            for (int i = startIndex; i <= endIndex; i++)
                array[startIndex - i] = originalList[i];
            return array;
        }
        public new void TrimExcess()
        {
            throw new InvalidOperationException("trimExcess is unsupported.");
        }
        public new bool TrueForAll(Predicate<T> match)
        {
            for (int i = startIndex; i <= endIndex; i++)
                if (!match.Invoke(originalList[i]))
                    return false;
            return true;
        }

        public class Enumerator<V> : IEnumerator<V>
        {
            private readonly SubList<V> items;
            private int index = -1;
            private V _current;

            public Enumerator(SubList<V> items)
            {
                this.items = items;
            }

            public V Current => _current;

            object IEnumerator.Current => _current;

            public void Dispose()
            {
                
            }

            public bool MoveNext()
            {
                index++;
                if (index >= 0 && index < items.Count)
                {
                    _current = items[index];
                    return true;
                }
                _current = default(V);
                return false;
            }

            public void Reset()
            {
                index = -1;
                _current = default(V);
            }
        }
    }
}
