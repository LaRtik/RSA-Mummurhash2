using System;
using System.Numerics;

namespace RSAsharp
{
    class RSA
    {
        public Keys calculateRSAKeys()
        {
            Random rnd = new Random();
            Prime pr = new Prime();
            int numb = rnd.Next(0, 22);
          
            BigInteger p = Utils.ToBigInteger(pr.prime[numb]);
            BigInteger q = Utils.ToBigInteger(pr.prime[numb+1]);

            BigInteger n = p * q;

            BigInteger functionE = (p - 1) * (q - 1);

            BigInteger e = 1;
            for (BigInteger i = functionE - 1; i > 0; i = i - 1)
            {
                if (Utils.gcd(i, functionE) == 1)
                {
                    e = i;
                    break;
                }
            }

            BigInteger d;
            for (BigInteger i = functionE; ; i = i - 1)
            {
                if ((BigInteger)i * (BigInteger)e % (BigInteger)functionE == 1)
                {
                    d = i;
                    break;
                }
            }

            Keys keys = new Keys(d,n,e,n);          
            return keys;
        }

        public BigInteger cryptData(BigInteger data, Pair<BigInteger, BigInteger> _privateKey)
        {
            if (data > _privateKey.Second)
            {
                var exception = new Exception("Invalid data");
                throw exception;
            }

            BigInteger cryptedData = (data ^ _privateKey.First) % _privateKey.Second;
            return cryptedData;
        }

        public BigInteger encryptData(BigInteger data, Pair<BigInteger, BigInteger> _publickey)
        {
            BigInteger encryptedData = (data ^ _publickey.First) % _publickey.Second;
            return encryptedData;
        }
    }
}
