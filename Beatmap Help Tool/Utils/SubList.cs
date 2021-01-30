using Beatmap_Help_Tool.BeatmapTools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Beatmap_Help_Tool.Utils
{
    public class SubList<T> : List<T>, IList<T>, ICollection<T>, IEnumerable<T>, IReadOnlyList<T>, IReadOnlyCollection<T>
    {
        private readonly List<T> originalList;
        private readonly int startIndex;
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

        new internal T this[int index]
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

        new internal int Count
        {
            get
            {
                return originalList.Count - startIndex;
            }
        }

        new internal void Add(T item)
        {
            originalList.Insert(startIndex, item);
            endIndex++;
        }

        new internal void AddRange(IEnumerable<T> collection)
        {
            originalList.InsertRange(startIndex, collection);
            endIndex += collection.Count();
        }

        new internal void Clear()
        {
            originalList.RemoveRange(startIndex, endIndex - startIndex);
            endIndex = startIndex;
        }

        new internal bool Contains(T item)
        {
            for (int i = startIndex; i <= endIndex; i++)
                if (originalList[i].Equals(item))
                    return true;

            return false;
        }

        new internal bool Exists(Predicate<T> match)
        {
            for (int i = startIndex; i <= endIndex; i++)
                if (match.Invoke(originalList[i]))
                    return true;

            return false;
        }

        new internal T Find(Predicate<T> match)
        {
            for (int i = startIndex; i <= endIndex; i++)
                if (match.Invoke(originalList[i]))
                    return originalList[i];

            return default;
        }

        new internal List<T> FindAll(Predicate<T> match)
        {
            List<T> list = new List<T>();
            for (int i = startIndex; i <= endIndex; i++)
                if (match.Invoke(originalList[i]))
                    list.Add(originalList[i]);
            return list;
        }

        new internal int FindIndex(Predicate<T> match)
        {
            for (int i = startIndex; i <= endIndex; i++)
                if (match.Invoke(originalList[i]))
                    return i - startIndex;
            return -1;
        }
        new internal int FindIndex(int startIndex, Predicate<T> match)
        {
            for (int i = this.startIndex + startIndex; i <= endIndex; i++)
                if (match.Invoke(originalList[i]))
                    return i - startIndex;
            return -1;
        }
        new internal int FindIndex(int startIndex, int count, Predicate<T> match)
        {
            return originalList.GetRange(startIndex, endIndex).FindIndex(startIndex, count, match);
        }
        new internal T FindLast(Predicate<T> match)
        {
            for (int i = endIndex; i >= startIndex; i--)
                if (match.Invoke(originalList[i]))
                    return originalList[i];

            return default;
        }
        new internal int FindLastIndex(Predicate<T> match)
        {
            for (int i = endIndex; i >= startIndex; i--)
                if (match.Invoke(originalList[i]))
                    return i - startIndex;

            return default;
        }
        new internal int FindLastIndex(int startIndex, Predicate<T> match)
        {
            for (int i = endIndex; i >= startIndex + this.startIndex; i--)
                if (match.Invoke(originalList[i]))
                    return i - startIndex;

            return default;
        }
        new internal int FindLastIndex(int startIndex, int count, Predicate<T> match)
        {
            return originalList.GetRange(startIndex, endIndex).FindLastIndex(startIndex, count, match);
        }
        new internal List<T> GetRange(int startIndex, int count)
        {
            return new SubList<T>(this, startIndex, startIndex + count);
        }
        new internal IEnumerator<T> GetEnumerator()
        {
            return originalList.GetRange(startIndex, endIndex).GetEnumerator();
        }
        new internal int IndexOf(T item, int index, int count)
        {
            return originalList.GetRange(startIndex, endIndex).IndexOf(item, index, count);
        }
        new internal int IndexOf(T item, int index)
        {
            for (int i = startIndex + index; i <= endIndex; i++)
                if (item.Equals(originalList[i]))
                    return i - startIndex;
            return -1;
        }
        new internal int IndexOf(T item)
        {
            for (int i = startIndex; i <= endIndex; i++)
                if (item.Equals(originalList[i]))
                    return i - startIndex;
            return -1;
        }
        new internal void Insert(int index, T item)
        {
            originalList.Insert(startIndex + index, item);
        }
        new internal void InsertRange(int index, IEnumerable<T> collection)
        {
            originalList.InsertRange(startIndex + index, collection);
        }
        new internal int LastIndexOf(T item)
        {
            for (int i = endIndex; i >= startIndex; i--)
                if (item.Equals(originalList[i]))
                    return i - startIndex;
            return -1;
        }
        new internal int LastIndexOf(T item, int index)
        {
            for (int i = endIndex; i >= startIndex + index; i--)
                if (item.Equals(originalList[i]))
                    return i - startIndex;
            return -1;
        }
        new internal int LastIndexOf(T item, int index, int count)
        {
            return originalList.GetRange(startIndex, endIndex).LastIndexOf(item, index, count);
        }
        new internal bool Remove(T item)
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
        new internal int RemoveAll(Predicate<T> match)
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
        new internal void RemoveAt(int index)
        {
            originalList.RemoveAt(index + startIndex);
        }
        new internal void RemoveRange(int index, int count)
        {
            originalList.RemoveRange(index + startIndex, count);
        }
        new internal void Reverse(int index, int count)
        {
            originalList.Reverse(startIndex + index, count);
        }
        new internal void Reverse()
        {
            originalList.Reverse(startIndex, endIndex);
        }
        new internal void Sort(int index, int count, IComparer<T> comparer)
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
        new internal void Sort(Comparison<T> comparison)
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
        new internal void Sort()
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
        new internal void Sort(IComparer<T> comparer)
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
        new internal T[] ToArray()
        {
            T[] array = new T[endIndex - startIndex];
            for (int i = startIndex; i <= endIndex; i++)
                array[startIndex - i] = originalList[i];
            return array;
        }
        new internal void TrimExcess()
        {
            throw new InvalidOperationException("trimExcess is unsupported.");
        }
        new internal bool TrueForAll(Predicate<T> match)
        {
            for (int i = startIndex; i <= endIndex; i++)
                if (!match.Invoke(originalList[i]))
                    return false;
            return true;
        }
    }
}
