using System;
using System.Numerics;

namespace RSAsharp
{
    public static class Utils
    {

        public static BigInteger gcd(BigInteger a, BigInteger b)
        {
            while ((a > 0) && (b > 0))
            {
                if (a > b) a = a % b;
                else b = b % a;
            }

            return a + b;
        }

        public static BigInteger Sqrt(this BigInteger n)
        {
            if (n == 0) return 0;
            if (n > 0)
            {
                int bitLength = Convert.ToInt32(Math.Ceiling(BigInteger.Log(n, 2)));
                BigInteger root = BigInteger.One << (bitLength / 2);

                while (!isSqrt(n, root))
                {
                    root += n / root;
                    root /= 2;
                }

                return root;
            }

            throw new ArithmeticException("NaN");
        }

        private static Boolean isSqrt(BigInteger n, BigInteger root)
        {
            BigInteger lowerBound = root * root;
            BigInteger upperBound = (root + 1) * (root + 1);

            return (n >= lowerBound && n < upperBound);
        }

        public static bool prime(BigInteger n)
        {
            BigInteger end = Sqrt(n);
            for (BigInteger i = 2; i <= end; i = i + 1)
                if (n % i == 0)
                    return false;
            return true;
        }

        public static BigInteger ToBigInteger(string value)
        {
            BigInteger result = 0;
            for (int i = 0; i < value.Length; i++)
            {
                if (value[i] - '0' > 9 || value[i] - '0' < 0)
                {
                    Exception ex = new Exception("Incorrect data.");
                    throw ex;
                }
                result = result * 10 + (value[i] - '0');
            }
            return result;
        }
    }
}
