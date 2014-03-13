/// <summary>
/// <para name = "Module"> Utilities</para>
/// <para name = "Describe"> Some useful functions. If you want to use these function, please add 
/// DecorationSystem namespase in you code.
/// e.g, using DecorationSystem;
/// Then you can use function like follow:
/// e.g, transform.SetXPos(0.1);
/// </para>
/// <para name = "Author"> YS </para>
/// <para name = "Date">  2014-1-1 </para>
/// </summary>
using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

namespace DecorationSystem.CommonUtilities
{
    public static class Utilities
    {

        #region extend some function to Array type 

        /// <summary>
        /// For the each.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="array">The array.</param>
        /// <param name="action">The action.</param>
        /// <returns></returns>
        public static T[] ForEach<T>(this T[] array, Action<T> action)
        {
            Array.ForEach<T>(array, action);
            return array;
        }

        /// <summary>
        /// Prints the specified array.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="array">The array.</param>
        public static void Print<T>(this T[] array)
        {
            array.ForEach(item => Debug.Log(item));
        }

        /// <summary>
        /// Filters the specified array.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="array">The array.</param>
        /// <param name="match">The match.</param>
        /// <returns></returns>
        public static T[] Filter<T>(this T[] array, Predicate<T> match)
        {
            return Array.FindAll<T>(array, match);
        }

        /// <summary>
        /// Ins the specified value.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value">The value.</param>
        /// <param name="array">The array.</param>
        /// <returns></returns>
        public static bool In<T>(this T value, T[] array)
        {
            return Array.Exists<T>(array, item => value.Equals(item));
        }

        /// <summary>
        /// Determines whether [contains] [the specified array].
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="array">The array.</param>
        /// <param name="value">The value.</param>
        /// <returns>
        ///   <c>true</c> if [contains] [value] ; otherwise, <c>false</c>.
        /// </returns>
        public static bool Contains<T>(this T[] array, T value)
        {
            return value.In(array);
        }

        /// <summary>
        /// Finds the specified array.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="array">The array.</param>
        /// <param name="match">The match.</param>
        /// <returns></returns>
        public static int Find<T>(this T[] array, Predicate<T> match)
        {
            return Array.FindIndex<T>(array, match);
        }

        /// <summary>
        /// Indexes the of.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="array">The array.</param>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public static int IndexOf<T>(this T[] array, T value)
        {
            return Array.IndexOf<T>(array, value);
        }

        /// <summary>
        /// Maps the specified array.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="array">The array.</param>
        /// <param name="converter">The converter.</param>
        /// <returns></returns>
        public static T[] Map<T>(this T[] array, Converter<T, T> converter)
        {
            return Array.ConvertAll<T, T>(array, converter);
        }

        /// <summary>
        /// Converts all.
        /// </summary>
        /// <typeparam name="TInput">The type of the input.</typeparam>
        /// <typeparam name="TOutput">The type of the output.</typeparam>
        /// <param name="array">The array.</param>
        /// <param name="converter">The converter.</param>
        /// <returns></returns>
        public static TOutput[] ConvertAll<TInput, TOutput>(this TInput[] array, Converter<TInput, TOutput> converter)
        {
            return Array.ConvertAll<TInput, TOutput>(array, converter);
        }

        /// <summary>
        /// Reduces the specified source.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source">The source.</param>
        /// <param name="reducer">The reducer.</param>
        /// <returns></returns>
        public static T Reduce<T>(this IEnumerable<T> source, Func<T, T, T> reducer)
        {
            T sum = default(T);
            foreach (var item in source)
            {
                sum = reducer(sum, item);
            }
            return sum;
        }

        /// <summary>
        /// Ranges the specified count.
        /// </summary>
        /// <param name="count">The count.</param>
        /// <returns></returns>
        public static int[] Range(int count)
        {
            int[] result = new int[count];
            for (int i = 0; i< count; i += 1)
            {
                result [i] = i;
            }
            return result;
        }

        /// <summary>
        /// Ranges the specified start.
        /// </summary>
        /// <param name="start">The start.</param>
        /// <param name="count">The count.</param>
        /// <param name="step">The step.</param>
        /// <returns></returns>
        public static IEnumerator Range(int start, int count, int step)
        {
            for (int i = 0; i < count; i += 1)
            {
                yield return start + i * step;
            }
        }

        /// <summary>
        /// Ranges the specified array.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="array">The array.</param>
        /// <param name="start">The start.</param>
        /// <param name="count">The count.</param>
        /// <returns></returns>
        public static T[] Range<T>(this T[] array, int start, int count)
        {
            T[] result = new T[count];
            Array.ConstrainedCopy(array, start, result, 0, count);
            return result;
        }

        /// <summary>
        /// Time the specified source.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source">The source.</param>
        /// <param name="action">The action.</param>
        public static void Times<T>(this T[] source, Action action)
        {
            source.ForEach((item) => action());
        }

        /// <summary>
        /// Adds the specified array.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="array">The array.</param>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public static T[] Add<T>(this T[] array, T value)
        {
            T[] result = new T[array.Length + 1];
            for (int i = 0; i < array.Length; i += 1)
            {
                result [i] = array [i];
            }
            result [array.Length] = value;
            return result;
        }

        /// <summary>
        /// Adds the specified value.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value">The value.</param>
        /// <param name="array">The array.</param>
        /// <returns></returns>
        public static T[] Adds<T>(this T value, T[] array)
        {
            T[] result = new T[array.Length + 1];
            for (int i = 0; i < array.Length; i += 1)
            {
                result [i + 1] = array [i];
            }
            result [0] = value;
            return result;
        }

        /// <summary>
        /// Resizes the specified array.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="array">The array.</param>
        /// <param name="newSize">The new size.</param>
        /// <returns></returns>
        public static T[] Resize<T>(this T[] array, int newSize)
        {
            T[] result = array.Clone() as T[];
            Array.Resize<T>(ref result, newSize);
            return result;
        }

        /// <summary>
        /// Merges the specified array.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="array">The array.</param>
        /// <param name="other">The other.</param>
        /// <returns></returns>
        public static T[] Merge<T>(this T[] array, T[] other)
        {
            T[] result = array.Resize(array.Length + other.Length);
            for (int i = 0; i < other.Length; i += 1)
            {
                result [i + array.Length] = other [i];
            }
            return result;
        }

        /// <summary>
        /// To the list.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="array">The array.</param>
        /// <returns></returns>
        public static List<T> ToList<T>(this T[] array)
        {
            List<T> result = new List<T>();
            array.ForEach(item => result.Add(item));
            return result;
        }

        /// <summary>
        /// Insteads the of.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="array">The array.</param>
        /// <param name="index">The index.</param>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public static T[] InsteadOf<T>(this T[] array, int index, T value)
        {
            T[] result = array.Clone() as T[];
            result [index] = value;
            return result;
        }

        /// <summary>
        /// Heads the specified array.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="array">The array.</param>
        /// <returns></returns>
        public static T Head<T>(this T[] array)
        {
            return array [0];
        }

        /// <summary>
        /// Tails the specified array.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="array">The array.</param>
        /// <returns></returns>
        public static T[] Tail<T>(this T[] array)
        {
            T[] result = array.Range(1, array.Length - 1);
            return result;
        }

        /// <summary>
        /// Lasts the specified array.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="array">The array.</param>
        /// <returns></returns>
        public static T Last<T>(this T[] array)
        {
            return array [array.Length - 1];
        }

        /// <summary>
        /// Inits the specified array.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="array">The array.</param>
        /// <returns></returns>
        public static T[] Init<T>(this T[] array)
        {
            T[] result = array.Range(0, array.Length - 1);
            return result;
        }

        /// <summary>
        /// Takes the specified array.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="array">The array.</param>
        /// <param name="count">The count.</param>
        /// <returns></returns>
        public static T[] Take<T>(this T[] array, int count)
        {
            T[] result = array.Range(0, count);
            return result;
        }

        /// <summary>
        /// Drops the specified array.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="array">The array.</param>
        /// <param name="count">The count.</param>
        /// <returns></returns>
        public static T[] Drop<T>(this T[] array, int count)
        {
            T[] result = array.Range(count, array.Length - count);
            return result;
        }

        /// <summary>
        /// Pops the specified array.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="array">The array.</param>
        /// <returns></returns>
        public static IEnumerator<T> Pop<T>(this T[] array)
        {
            for (int i = array.Length - 1; i >= 0; i -= 1)
            {
                yield return array [i];
            }
        }

        /// <summary>
        /// Reverses the specified array.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="array">The array.</param>
        /// <returns></returns>
        public static T[] Reverse<T>(this T[] array)
        {
            T[] result = array.Clone() as T[];
            Array.Copy(array, result, array.Length);
            Array.Reverse(array);
            return result;
        }

        /// <summary>
        /// Removes the specified array.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="array">The array.</param>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public static T[] Remove<T>(this T[] array, T value)
        {
            List<T> result = array.ToList();
            result.Remove(value);
            return result.ToArray();
        }

        /// <summary>
        /// Removes at.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="array">The array.</param>
        /// <param name="index">The index.</param>
        /// <returns></returns>
        public static T[] RemoveAt<T>(this T[] array, int index)
        {
            T value = array [index];
            T[] result = array.Remove(value);
            return result;
        }

        /// <summary>
        /// Maximums the specified array.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="array">The array.</param>
        /// <returns></returns>
        public static T Maximum<T>(this T[] array)
        {
            var result = array.Sort();
            return result.Last();
        }

        /// <summary>
        /// Minimums the specified array.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="array">The array.</param>
        /// <returns></returns>
        public static T Minimum<T>(this T[] array)
        {
            var result = array.Sort();
            return result.Head();
        }

        /// <summary>
        /// Sorts the specified array.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="array">The array.</param>
        /// <param name="comparer">The comparer.</param>
        /// <returns></returns>
        public static T[] Sort<T>(this T[] array, IComparer<T> comparer)
        {
            var result = array.Clone() as T[];
            Array.Sort(result, comparer);
            return result;
        }

        /// <summary>
        /// Sorts the specified array.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="array">The array.</param>
        /// <returns></returns>
        public static T[] Sort<T>(this T[] array)
        {
            var result = array.Clone() as T[];
            Array.Sort(result);
            return result;
        }

        /// <summary>
        /// To the array.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public static int[] ToArray(this int value)
        {
            int[] result = new int[value];
            for (int i = 0; i < value; i++)
            {
                result [i] = i;
            }
            return result;
        }

        /// <summary>
        /// Timeses the specified value.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="action">The action.</param>
        public static void Times(this int value, Action<int> action)
        {
            for (int i = 0; i < value; i++)
            {
                action(i);
            }
        }

        /// <summary>
        /// Timeses the specified value.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="action">The action.</param>
        public static void Times(this int value, Action action)
        {
            for (int i = 0; i < value; i++)
            {
                action();
            }
        }

        /// <summary>
        /// Trues for one.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="array">The array.</param>
        /// <param name="match">The match.</param>
        /// <param name="action">The action.</param>
        public static void TrueForOne<T>(this T[] array, Predicate<T> match, Action<T> action)
        {
            for (int i = 0; i < array.Length; i += 1)
            {
                if (match(array [i]))
                {
                    action(array [i]);
                    break;
                }
            }
        }

        /// <summary>
        /// Trues for all.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="array">The array.</param>
        /// <param name="match">The match.</param>
        /// <returns></returns>
        public static bool TrueForAll<T>(this T[] array, Predicate<T> match)
        {
            return Array.TrueForAll(array, match);
        }

        /// <summary>
        /// Uniques the specified array.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="array">The array.</param>
        /// <returns></returns>
        public static T[] Unique<T>(this T[] array)
        {
            Dictionary<T, int> dictionary = new Dictionary<T, int>();
            T[] result = array.Filter((item) => {
                if (dictionary.ContainsKey(item))
                {
                    return false;
                } else
                {
                    dictionary [item] = 1;
                    return true;
                }
            });
            return result;
        }

        /// <summary>
        /// To the dictionary.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="array">The array.</param>
        /// <returns></returns>
        public static Dictionary<int,T> ToDictionary<T>(this T[] array)
        {
            Dictionary<int, T> result = new Dictionary<int, T>();
            for (int i = 0; i < array.Length; i += 1)
            {
                result [i] = array [i];
            }
            return result;
        }

        /// <summary>
        /// If the specified func.
        /// </summary>
        /// <param name="func">The func.</param>
        /// <param name="match">The match.</param>
        /// <returns></returns>
        public static bool ActionIf(this Action func, Func<bool> match)
        {
            if (match())
            {
                func();
                return true;
            } else
            {
                return false;
            }
        }

        /// <summary>
        /// Unless the specified func.
        /// </summary>
        /// <param name="func">The func.</param>
        /// <param name="match">The match.</param>
        /// <returns></returns>
        public static bool Unless(this Action func, Func<bool> match)
        {
            if (match())
            {
                return false;
            } else
            {
                func();
                return true;
            }
        }


        /// <summary>
        /// If the specified func.
        /// </summary>
        /// <typeparam name="TInput">The type of the input.</typeparam>
        /// <typeparam name="TOutput">The type of the output.</typeparam>
        /// <param name="func">The func.</param>
        /// <param name="value">The value.</param>
        /// <param name="match">The match.</param>
        /// <returns></returns>
        public static TOutput ActionIf<TInput,TOutput>(this Func<TInput,TOutput> func, TInput value, Func<bool> match)
        {
            if (match())
            {
                return func(value);
            } else
            {
                return default(TOutput);
            }
        }

        /// <summary>
        /// Unless the specified func. return null;
        /// </summary>
        /// <typeparam name="TInput">The type of the input.</typeparam>
        /// <typeparam name="TOutput">The type of the output.</typeparam>
        /// <param name="func">The func.</param>
        /// <param name="value">The value.</param>
        /// <param name="match">The match.</param>
        /// <returns></returns>
        public static TOutput Unless<TInput, TOutput>(this Func<TInput, TOutput> func, TInput value, Func<bool> match)
        {
            if (match())
            {
                return default(TOutput);
            } else
            {
                return func(value);
            }
        }

        /// <summary>
        /// Copies the specified source array.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sourceArray">The source array.</param>
        /// <param name="sourceStart">The source start.</param>
        /// <param name="destinationArray">The destination array.</param>
        /// <param name="destionationStart">The destionation start.</param>
        /// <param name="length">The length.</param>
        /// <returns></returns>
        public static T[] Copy<T>(this T[] sourceArray, int sourceStart, T[] destinationArray, int destionationStart, int length)
        {
            Array.Copy(sourceArray, sourceStart, destinationArray, destionationStart, length);
            return destinationArray;
        }

        #endregion

        public static bool IsEmpty<T>(this List<T> list)
        {
            return list.Count > 0 ? false : true;
        }

        public static Func<T1, Func<T2, Func<T3, T4>>> CurryMe<T1, T2, T3, T4>(this Func<T1, T2, T3, T4> func)
        {
            return (T1 a) => (T2 b) => (T3 c) => func(a, b, c);
        }

        public static Func<T1, Func<T2, T3>> CurryMe<T1, T2, T3>(this Func<T1, T2, T3> func)
        {
            return (T1 a) => (T2 b) => func(a, b);
        }

    }

}

namespace DecorationSystem
{

    /// <summary>
    /// Do not allow to set null to fields.
    /// </summary>
    [AttributeUsage(AttributeTargets.Field)]
    public class NotAllowNullAttribute : Attribute
    {
    }

    /// <summary>
    /// Set default value to field.
    /// </summary>
    [AttributeUsage(AttributeTargets.Field)]
    public class DefaultValueAttribute : Attribute
    {
        public string GameObjectName;
        public System.Type type;

        public DefaultValueAttribute(string name, Type type)
        {
            this.GameObjectName = name;
            this.type = type;
        }
    }

    /// <summary>
    /// MonoBehaviourBase
    /// </summary>
    public class MonoBehaviourBase : MonoBehaviour
    {

        public T AddSafeComponent<T>() where T:Component
        {
            T cpt = gameObject.GetComponent<T>();
            if (cpt == null)
            {
                cpt = gameObject.AddComponent<T>();
            }
            return cpt;
        }
    }

    public static class GameObjectUtilities
    {

        #region Set x,y,z of position 

        /// <summary>
        /// Sets the X pos.
        /// </summary>
        /// <param name="tfm">Transform type</param>
        /// <param name="x">The value of transform's position x.</param>
        public static void SetXPos(this Transform tfm, float x)
        {
            Vector3 newPosition = new Vector3(x, tfm.position.y, tfm.position.z);
            tfm.position = newPosition;
        }

        /// <summary>
        /// Sets the Y pos.
        /// </summary>
        /// <param name="tfm">Transform type</param>
        /// <param name="y">The value of transform's position y</param>
        public static void SetYPos(this Transform tfm, float y)
        {
            Vector3 newPosition = new Vector3(tfm.position.x, y, tfm.position.z);
            tfm.position = newPosition;
        }

        /// <summary>
        /// Sets the Z pos.
        /// </summary>
        /// <param name="tfm">Transform type</param>
        /// <param name="z">The value of transform's position z.</param>
        public static void SetZPos(this Transform tfm, float z)
        {
            Vector3 newPosition = new Vector3(tfm.position.x, tfm.position.y, z);
            tfm.position = newPosition;
        }

        #endregion

        #region Get component without null value

        /// <summary>
        /// Gets the safe component.
        /// </summary>
        /// <typeparam name="T"> T type is monobehaviour.</typeparam>
        /// <param name="go">GameObject Tpye </param>
        public static T GetSafeComponent<T>(this GameObject go) where T : MonoBehaviour
        {
            T cpt = go.GetComponent<T>();

            if (cpt == null)
            {
                Debug.LogError("Expected to find component of type"
                    + typeof(T) + "but found none ", go);
            }

            return cpt;
        }

        public static T GetCommponent<T>(GameObject go) where T:MonoBehaviour
        {
            return go.GetSafeComponent<T>();
        }

        public static bool IsNull(this object go)
        {
            return go == null ? true : false;
        }

        public static bool InNotNull(this object go)
        {
            return go == null ? false : true; 
        }

        #endregion
    }
}