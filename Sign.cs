using System;
using System.IO;
using System.Numerics;
using RSAsharp;

namespace Program
{
    class Program
    {
        public static void cryptMessage()
        {
            string messagePath = Directory.GetCurrentDirectory() + "\\message.txt";
            string originalMessage = "";
     
            try
            {
                StreamReader inputText = new StreamReader(messagePath);
                originalMessage = inputText.ReadToEnd();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Environment.Exit(1);
            }

    
            uint originalMessageHash = MurmurHash2.Hash(originalMessage);
           

            Keys key = new Keys();
            RSA rsaobj = new RSA();
            key = rsaobj.calculateRSAKeys();
            BigInteger signOfMessage = rsaobj.cryptData(originalMessageHash, key._private);


            string SignInfoPath = Directory.GetCurrentDirectory();
          
            using (StreamWriter sw = new StreamWriter(SignInfoPath + "\\sign.txt", false, System.Text.Encoding.Default))
            {
                sw.Write(signOfMessage);
            }

            using (StreamWriter sw = new StreamWriter(SignInfoPath + "\\publicKey.txt", false, System.Text.Encoding.Default))
            {
                sw.Write(key._public.First + "\n" + key._public.Second);
            }

            using (StreamWriter sw = new StreamWriter(SignInfoPath + "\\originalMessage.txt", false, System.Text.Encoding.Default))
            {
                sw.Write(originalMessage);
            }
        }

        public static void decryptMessage()
        {
            string newMessage = "";
            string messagePath = Directory.GetCurrentDirectory() + "\\message.txt";
      
            try
            {
                StreamReader inputText = new StreamReader(messagePath);
                newMessage = inputText.ReadToEnd();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Environment.Exit(1);
            }

            string sign = "";
            try
            {
                StreamReader inputText = new StreamReader(Directory.GetCurrentDirectory() + "\\sign.txt");
                sign = inputText.ReadToEnd();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Environment.Exit(1);
            }

            BigInteger newMessageHash = MurmurHash2.Hash(newMessage);
           

            Pair<BigInteger, BigInteger> publicKey = new Pair<BigInteger, BigInteger>();
            try
            {
                StreamReader inputText = new StreamReader(Directory.GetCurrentDirectory() + "\\publicKey.txt");
                publicKey.First = Utils.ToBigInteger(inputText.ReadLine());
                publicKey.Second = Utils.ToBigInteger(inputText.ReadLine());
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Environment.Exit(1);
            }
            RSA rsaobj = new RSA();
            BigInteger decryptedMessageHash = rsaobj.encryptData(Utils.ToBigInteger(sign), publicKey);

            if (newMessageHash != decryptedMessageHash) Console.WriteLine("Sign is not correct.");
            else Console.WriteLine("Sign is correct");
        }
        public static void Main(string[] args)
        {

            
            int answ = 1;
          
                Console.WriteLine("Choose the option:\n" +
                "1 - Create sign for message in message.txt\n" +
                "2 - Check the sign for message using saved data\n" +
                "0 - Exit the program\n");
                while (true)
                {
                    if (Int32.TryParse(Console.ReadLine(), out answ))
                    {
                        if (answ >= 0 && answ <= 2)
                        {
                            break;
                        }
                        else
                        {
                            Console.WriteLine("Wrong input! Try again.");
                        }
                    }
                }
                switch (answ)
                {
                    case 0:
                        {
                            answ = 0;
                            break;
                        }
                    case 1:
                        {
                            cryptMessage();
                            Console.WriteLine("Sign created succesfully. ");
                            break;
                        }
                    case 2:
                        {
                            decryptMessage();
                            break;
                        }
                    default:
                        {
                            break;
                        }
                }
     

        }


    }
}

