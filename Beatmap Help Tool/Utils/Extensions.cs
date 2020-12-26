using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Beatmap_Help_Tool.Utils
{
    public static class Extensions
    {
        public static void Invoke(this Control control, Action action)
        {
            control.Invoke(action);
        }

        public static void ForEach<TKey, TValue>(this Dictionary<TKey, TValue> dict, Action<TKey, TValue> action)
        {
            foreach (KeyValuePair<TKey, TValue> pair in dict)
            {
                action.Invoke(pair.Key, pair.Value);
            }
        }

        public static string AddLines(this string text, int lines)
        {
            for (int i = 0; i < lines; i++)
                text += Environment.NewLine;
            return text;
        }

        public static string AddLines(this string text, int lines, string append)
        {
            for (int i = 0; i < lines; i++)
                text += Environment.NewLine;
            return text + append;
        }

        public static T Last<T>(this List<T> list)
        {
            return list[list.Count - 1];
        }

        public static T First<T>(this List<T> list)
        {
            return list[0];
        }

        public static int BinarySearchClosest(this IReadOnlyList<int> list, int searched)
        {
            int first = 0;
            int last = list.Count - 1;
            int mid = 0;
            double diff1, diff2, diff3;
            do
            {
                mid = first + (last - first) / 2;
                if (searched > list[mid])
                    first = mid + 1;
                else
                    last = mid - 1;
                if (list[mid] == searched)
                    return list[mid];
            } while (first <= last);

            if (mid > first + 1 && mid < last - 1)
            {
                diff1 = Math.Abs(searched - list[mid - 1]);
                diff2 = Math.Abs(searched - list[mid]);
                diff3 = Math.Abs(searched - list[mid + 1]);

                if (diff1 < diff2 && diff1 < diff3)
                    return list[mid - 1];
                else if (diff3 < diff1 && diff3 < diff2)
                    return list[mid + 1];
                else
                    return mid;
            }
            else
            {
                if (mid > first + 1)
                {
                    diff1 = Math.Abs(searched - list[mid - 1]);
                    diff2 = Math.Abs(searched - list[mid]);

                    if (diff1 < diff2)
                        return list[mid - 1];
                    else
                        return list[mid];
                }
                else
                {
                    diff1 = Math.Abs(searched - list[mid + 1]);
                    diff2 = Math.Abs(searched - list[mid]);

                    if (diff1 < diff2)
                        return list[mid + 1];
                    else
                        return list[mid];
                }
            }
        }

        public static float BinarySearchClosest(this IReadOnlyList<float> list, float searched)
        {
            int first = 0;
            int last = list.Count - 1;
            int mid = 0;
            double diff1, diff2, diff3;
            do
            {
                mid = first + (last - first) / 2;
                if (searched > list[mid])
                    first = mid + 1;
                else
                    last = mid - 1;
                if (list[mid] == searched)
                    return list[mid];
            } while (first <= last);

            if (mid > first + 1 && mid < last - 1)
            {
                diff1 = Math.Abs(searched - list[mid - 1]);
                diff2 = Math.Abs(searched - list[mid]);
                diff3 = Math.Abs(searched - list[mid + 1]);

                if (diff1 < diff2 && diff1 < diff3)
                    return list[mid - 1];
                else if (diff3 < diff1 && diff3 < diff2)
                    return list[mid + 1];
                else
                    return mid;
            }
            else
            {
                if (mid > first + 1)
                {
                    diff1 = Math.Abs(searched - list[mid - 1]);
                    diff2 = Math.Abs(searched - list[mid]);

                    if (diff1 < diff2)
                        return list[mid - 1];
                    else
                        return list[mid];
                }
                else
                {
                    diff1 = Math.Abs(searched - list[mid + 1]);
                    diff2 = Math.Abs(searched - list[mid]);

                    if (diff1 < diff2)
                        return list[mid + 1];
                    else
                        return list[mid];
                }
            }
        }

        public static long BinarySearchClosest(this IReadOnlyList<long> list, long searched)
        {
            int first = 0;
            int last = list.Count - 1;
            int mid = 0;
            double diff1, diff2, diff3;
            do
            {
                mid = first + (last - first) / 2;
                if (searched > list[mid])
                    first = mid + 1;
                else
                    last = mid - 1;
                if (list[mid] == searched)
                    return list[mid];
            } while (first <= last);

            if (mid > first + 1 && mid < last - 1)
            {
                diff1 = Math.Abs(searched - list[mid - 1]);
                diff2 = Math.Abs(searched - list[mid]);
                diff3 = Math.Abs(searched - list[mid + 1]);

                if (diff1 < diff2 && diff1 < diff3)
                    return list[mid - 1];
                else if (diff3 < diff1 && diff3 < diff2)
                    return list[mid + 1];
                else
                    return mid;
            }
            else
            {
                if (mid > first + 1)
                {
                    diff1 = Math.Abs(searched - list[mid - 1]);
                    diff2 = Math.Abs(searched - list[mid]);

                    if (diff1 < diff2)
                        return list[mid - 1];
                    else
                        return list[mid];
                }
                else
                {
                    diff1 = Math.Abs(searched - list[mid + 1]);
                    diff2 = Math.Abs(searched - list[mid]);

                    if (diff1 < diff2)
                        return list[mid + 1];
                    else
                        return list[mid];
                }
            }
        }

        public static double BinarySearchClosest(this IReadOnlyList<double> list, double searched)
        {
            int first = 0;
            int last = list.Count - 1;
            int mid = 0;
            double diff1, diff2, diff3;
            do
            {
                mid = first + (last - first) / 2;
                if (searched > list[mid])
                    first = mid + 1;
                else
                    last = mid - 1;
                if (list[mid] == searched)
                    return list[mid];
            } while (first <= last);

            if (mid > first + 1 && mid < last - 1)
            {
                diff1 = Math.Abs(searched - list[mid - 1]);
                diff2 = Math.Abs(searched - list[mid]);
                diff3 = Math.Abs(searched - list[mid + 1]);

                if (diff1 < diff2 && diff1 < diff3)
                    return list[mid - 1];
                else if (diff3 < diff1 && diff3 < diff2)
                    return list[mid + 1];
                else
                    return mid;
            }
            else
            {
                if (mid > first + 1)
                {
                    diff1 = Math.Abs(searched - list[mid - 1]);
                    diff2 = Math.Abs(searched - list[mid]);

                    if (diff1 < diff2)
                        return list[mid - 1];
                    else
                        return list[mid];
                }
                else
                {
                    diff1 = Math.Abs(searched - list[mid + 1]);
                    diff2 = Math.Abs(searched - list[mid]);

                    if (diff1 < diff2)
                        return list[mid + 1];
                    else
                        return list[mid];
                }
            }
        }

        public static decimal BinarySearchClosest(this IReadOnlyList<decimal> list, decimal searched)
        {
            int first = 0;
            int last = list.Count - 1;
            int mid = 0;
            decimal diff1, diff2, diff3;
            do
            {
                mid = first + (last - first) / 2;
                if (searched > list[mid])
                    first = mid + 1;
                else
                    last = mid - 1;
                if (list[mid] == searched)
                    return list[mid];
            } while (first <= last);

            if (mid > first + 1 && mid < last - 1)
            {
                diff1 = Math.Abs(searched - list[mid - 1]);
                diff2 = Math.Abs(searched - list[mid]);
                diff3 = Math.Abs(searched - list[mid + 1]);

                if (diff1 < diff2 && diff1 < diff3)
                    return list[mid - 1];
                else if (diff3 < diff1 && diff3 < diff2)
                    return list[mid + 1];
                else
                    return mid;
            }
            else
            {
                if (mid > first + 1)
                {
                    diff1 = Math.Abs(searched - list[mid - 1]);
                    diff2 = Math.Abs(searched - list[mid]);

                    if (diff1 < diff2)
                        return list[mid - 1];
                    else
                        return list[mid];
                }
                else
                {
                    diff1 = Math.Abs(searched - list[mid + 1]);
                    diff2 = Math.Abs(searched - list[mid]);

                    if (diff1 < diff2)
                        return list[mid + 1];
                    else
                        return list[mid];
                }
            }
        }
    }
}
