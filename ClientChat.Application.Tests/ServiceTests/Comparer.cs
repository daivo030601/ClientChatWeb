using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientChat.Application.Tests.ServiceTests
{
    public class Comparer
    {
        public static Comparer<U?> Get<U> (Func<U?, U?, bool> f)
        {
            return new Comparer<U?> (f);
        }
    }

    public class Comparer<T> : Comparer, IEqualityComparer<T>
    {
        private Func<T?, T?, bool> compareFunc;
        public Comparer(Func<T?, T?, bool> compareFunc)
        {
            this.compareFunc = compareFunc;
        }
        public bool Equals(T? x, T? y)
        {
            return compareFunc(x, y);
        }
        public int GetHashCode(T? obj)
        {
            return obj?.GetHashCode() ?? 0;
        }
    }
}
