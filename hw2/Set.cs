using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace hw2
{

    /*
        Базовая часть
        + все
        Дополнительная часть
        + Поддержка порядка
        + Поддержка параметра конструктора IEqualityComparer<T> (для произольного сравнения) (+
        + IComparer<T> для SortedSet)
        + Перегрузка операторов * (пересечение множеств), - (вычитание множеств)
     */

    public class SortedSet<T> : IEnumerable
    {
        private SortedDictionary<T, int> _data;
        private readonly IComparer<T> _comp;

        #region Constructors

            public SortedSet() : this(null)
        {
            _data = new SortedDictionary<T, int>();
            _comp = _data.Comparer;
        }

        public SortedSet(IComparer<T> comp) : this(null, comp)
        {
            _comp = comp;
            _data = new SortedDictionary<T, int>(comp);
        }

        public SortedSet(IEnumerable<T> e, IComparer<T> comp)
        {
            _comp = comp;
            _data = new SortedDictionary<T, int>(comp);
            foreach (var item in e)
            {
                _data.Add(item, 0);
            }
        }

        #endregion

        
        #region Methods

        public bool Add(T item)
        {
            if (_data.ContainsKey(item)) return false;
            _data.Add(item, 0);
            return true;
        }

        public bool Remove(T item)
        {
            if (!_data.ContainsKey(item)) return false;
            _data.Remove(item);
            return true;
        }

        public bool Contains(T item)
        {
            return _data.ContainsKey(item);
        }

        public override string ToString()
        {
            var ans = _data.Keys.Aggregate("Set: [ ", (current, item) => current + (item.ToString() + " "));
            ans += "]";
            return ans;
        }

        public SortedSet<T> Where(Predicate<T> filter)
        {
            return new SortedSet<T>(_data.Keys.Where(new Func<T, bool>(filter)), _comp);
        }

        public IEnumerator GetEnumerator()
        {
            return ((IEnumerable) _data).GetEnumerator();
        }

        #endregion




        #region Properties

        public int Count => _data.Count;

        public bool IsEmpty => _data.Count == 0;

        #endregion
        #region Operators

        public static SortedSet<T> operator +(SortedSet<T> left, SortedSet<T> right)
        {
            var ans = new SortedSet<T>();
            foreach (T item in right)
            {
                right.Add(item);
            }
            return ans;
        }

        public static SortedSet<T> operator *(SortedSet<T> left, SortedSet<T> right)
        {
            var ans = new SortedSet<T>();
            foreach (T item in left)
            {
                foreach (T item1 in right)
                {
                    if (left.Contains(item1))
                    {
                        ans.Add(item);
                    }
                }
            }
            return ans;
        }

        public static SortedSet<T> operator -(SortedSet<T> left, SortedSet<T> right)
        {
            foreach (T item in right)
            {
                if (left.Contains(item))
                {
                    right.Remove(item);
                }
            }
            return right;
        }

        #endregion
        
    }
}