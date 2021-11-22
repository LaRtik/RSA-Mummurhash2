
using System.Numerics;

namespace RSAsharp
{
    public class Pair<T, U>
    {
        public Pair()
        {
        }

        public Pair(T first, U second)
        {
            this.First = first;
            this.Second = second;
        }

        public T First { get; set; }
        public U Second { get; set; }
    };
    public class Keys
    {
        public Pair<BigInteger, BigInteger> _public = new Pair<BigInteger, BigInteger>();
        public Pair<BigInteger, BigInteger> _private = new Pair<BigInteger, BigInteger>();
        public Keys(BigInteger a, BigInteger b, BigInteger c, BigInteger d)
        {
            _public.First = a;
            _public.Second = b;
            _private.First = c;
            _private.Second = d;
        }

        public Keys()
        {

        }
    }
}
