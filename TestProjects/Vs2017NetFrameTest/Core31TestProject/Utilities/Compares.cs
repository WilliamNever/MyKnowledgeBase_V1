using System;
using System.Collections.Generic;
using System.Text;

namespace Core31TestProject.Utilities
{
    public delegate bool EqualsComparer<T>(T x, T y);
    public class Compares<T> : IEqualityComparer<T>
    {
        private EqualsComparer<T> _equalsComparer;

        public Compares(EqualsComparer<T> equalsComparer)
        {
            _equalsComparer = equalsComparer;
        }
        public bool Equals(T x, T y)
        {
            if (null != _equalsComparer)
                return _equalsComparer(x, y);
            else
                return false;
        }
        public int GetHashCode(T obj)
        {
            return obj.ToString().GetHashCode();
            //return 1;
        }
    }
}
