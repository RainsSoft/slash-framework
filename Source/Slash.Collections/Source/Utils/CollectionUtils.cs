﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CollectionUtils.cs" company="Slash Games">
//   Copyright (c) Slash Games. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Slash.Collections.Utils
{
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    /// <summary>
    ///   Utility methods for enumerables and collections.
    /// </summary>
    public static class CollectionUtils
    {
        #region Public Methods and Operators

        /// <summary>
        ///   Compares two dictionary for equality.
        /// </summary>
        /// <typeparam name="TKey">Type of dictionary keys.</typeparam>
        /// <typeparam name="TValue">Type of dictionary values.</typeparam>
        /// <param name="first">First dictionary.</param>
        /// <param name="second">Second dictionary.</param>
        /// <returns>True if the two dictionaries are equal; otherwise, false.</returns>
        public static bool DictionaryEqual<TKey, TValue>(
            IDictionary<TKey, TValue> first,
            IDictionary<TKey, TValue> second)
        {
            if (first == second)
            {
                return true;
            }
            if ((first == null) || (second == null))
            {
                return false;
            }
            if (first.Count != second.Count)
            {
                return false;
            }

            EqualityComparer<TValue> comparer = EqualityComparer<TValue>.Default;

            foreach (KeyValuePair<TKey, TValue> kvp in first)
            {
                TValue secondValue;
                if (!second.TryGetValue(kvp.Key, out secondValue))
                {
                    return false;
                }

                if (kvp.Value is IEnumerable && secondValue is IEnumerable)
                {
                    IEnumerable enumerable1 = (IEnumerable)kvp.Value;
                    IEnumerable enumerable2 = (IEnumerable)secondValue;
                    if (!SequenceEqual(enumerable1.Cast<object>(), enumerable2.Cast<object>()))
                    {
                        return false;
                    }
                }
                else
                {
                    if (!comparer.Equals(kvp.Value, secondValue))
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        /// <summary>
        ///   Creates a hash code for a sequence.
        /// </summary>
        /// <typeparam name="T">Type of items in sequence.</typeparam>
        /// <param name="sequence">Sequence to get hash code for.</param>
        /// <returns>Hash code for the specified sequence.</returns>
        public static int GetSequenceHashCode<T>(this IEnumerable<T> sequence)
        {
            const int seed = 487;
            const int modifier = 31;

            unchecked
            {
                return sequence.Aggregate(seed, (current, item) =>
                    (current*modifier) + item.GetHashCode());
            }            
        }

        /// <summary>
        ///   Creates a separated string from the items of a collection.
        ///   Mostly used for debugging issues.
        /// </summary>
        /// <param name="collection">Collection of items to create a string from.</param>
        /// <param name="separator">String to use to separate the item strings.</param>
        /// <typeparam name="T">Type of items in collection.</typeparam>
        /// <returns></returns>
        public static string Implode<T>(this IEnumerable<T> collection, string separator)
        {
            return collection != null
                ? string.Join(separator, collection.Select(item => item != null ? item.ToString() : "<null>").ToArray())
                : string.Empty;
        }

        /// <summary>
        ///   Creates a separated string from the items of a collection.
        ///   Mostly used for debugging issues.
        /// </summary>
        /// <param name="collection">Collection of items to create a string from.</param>
        /// <param name="separator">String to use to separate the item strings.</param>
        /// <returns>String created from the items which are separated by the provided separator.</returns>
        public static string Implode(this IEnumerable collection, string separator)
        {
            return collection != null
                ? collection.Cast<object>().Implode(separator)
                : string.Empty;
        }

        /// <summary>
        ///   Compares the two passed lists for equality. Two lists are considered equal, if they contain equal elements in the
        ///   same order.
        /// </summary>
        /// <param name="list1">First list to compare.</param>
        /// <param name="list2">Second list to compare.</param>
        /// <returns>
        ///   <c>true</c>, if either both or no list is <c>null</c>, both lists contain the same number of elements, and all
        ///   elements are equal and in the same order.
        /// </returns>
        public static bool ListEqual(IList list1, IList list2)
        {
            if (list1 == list2)
            {
                return true;
            }

            if (list1 != null && list2 != null)
            {
                if (list1.Count != list2.Count)
                {
                    return false;
                }

                for (var i = 0; i < list1.Count; i++)
                {
                    if (!Equals(list1[i], list2[i]))
                    {
                        return false;
                    }
                }

                return true;
            }

            // One list is null.
            return false;
        }

        /// <summary>
        ///   Compares two sequences by comparing their items instead of their references. Also checks the case that
        ///   both sequences are null. In this case the method returns true.
        /// </summary>
        /// <typeparam name="T">Type of items inside sequences.</typeparam>
        /// <param name="sequence1">First sequence.</param>
        /// <param name="sequence2">Second sequence.</param>
        /// <returns>
        ///   True if the sequences are equal (i.e. the sequences contain equal items in the same order), else false.
        /// </returns>
        public static bool SequenceEqual<T>(IEnumerable<T> sequence1, IEnumerable<T> sequence2)
        {
            return sequence1 == sequence2
                   || sequence1 != null && sequence2 != null && sequence1.SequenceEqual(sequence2);
        }

        /// <summary>
        ///   Compares two sequences by comparing their items instead of their references. Also checks the case that
        ///   both sequences are null. In this case the method returns true.
        /// </summary>
        /// <typeparam name="T">Type of items inside sequences.</typeparam>
        /// <param name="sequence1">First sequence.</param>
        /// <param name="sequence2">Second sequence.</param>
        /// <param name="comparer">Comparer to check equality between two objects.</param>
        /// <returns>
        ///   True if the sequences are equal (i.e. the sequences contain equal items in the same order), else false.
        /// </returns>
        public static bool SequenceEqual<T>(
            IEnumerable<T> sequence1,
            IEnumerable<T> sequence2,
            IEqualityComparer<T> comparer)
        {
            return sequence1 == sequence2
                   || sequence1 != null && sequence2 != null && sequence1.SequenceEqual(sequence2, comparer);
        }

        /// <summary>
        ///   Swaps the elements with the specified indices in the passed list.
        /// </summary>
        /// <typeparam name="T">Type of the list to swap the items of.</typeparam>
        /// <param name="list">List to swap the items of.</param>
        /// <param name="first">Index of the first item to swap.</param>
        /// <param name="second">Index of the second item to swap.</param>
        public static void Swap<T>(IList<T> list, int first, int second)
        {
            T temp = list[first];
            list[first] = list[second];
            list[second] = temp;
        }

        /// <summary>
        ///   Returns a comma-separated list of the elements of the passed sequence.
        /// </summary>
        /// <param name="sequence">Sequence to get a comma-separated list of.</param>
        /// <returns>Comma-separated list of the elements of the passed sequence.</returns>
        public static string ToString(IEnumerable sequence)
        {
            if (sequence == null)
            {
                return "null";
            }

            StringBuilder stringBuilder = new StringBuilder();

            stringBuilder.Append("[");

            var dictionary = sequence as IDictionary;
            if (dictionary != null)
            {
                foreach (var key in dictionary.Keys)
                {
                    var keyString = ObjectToString(key);
                    var valueString = ObjectToString(dictionary[key]);
                    stringBuilder.AppendFormat("[{0} = {1}], ", keyString, valueString);
                }
            }
            else
            {
                foreach (var element in sequence)
                {
                    var elementString = ObjectToString(element);
                    stringBuilder.AppendFormat("{0}, ", elementString);
                }
            }

            if (stringBuilder.Length > 1)
            {
                stringBuilder[stringBuilder.Length - 2] = ']';
                return stringBuilder.ToString().Substring(0, stringBuilder.Length - 1);
            }

            return "[]";
        }

        #endregion

        #region Methods

        private static string ObjectToString(object obj)
        {
            if (obj == null)
            {
                return "null";
            }

            var objString = obj as string;
            if (objString != null)
            {
                return objString;
            }

            var elementEnumerable = obj as IEnumerable;
            objString = elementEnumerable != null ? ToString(elementEnumerable) : obj.ToString();

            return objString;
        }

        #endregion
    }
}