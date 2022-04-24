using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace Lab_6
{
    public static class Power
    {
        public static uint power(uint a, uint b, uint n)
        {// a^b mod n
            uint tmp = a;
            uint sum = tmp;
            for (int i = 1; i < b; i++)
            {
                for (int j = 1; j < a; j++)
                {
                    sum += tmp;
                    if (sum >= n)
                    {
                        sum -= n;
                    }
                }
                tmp = sum;
            }
            return tmp;
        }

    }
    public class PrimeNumberGenerator
    {
        private readonly Random _random = new Random();
        public uint Generate()
        {

            uint number = (uint)_random.Next(300);

            while (!IsPrime(number))
            {
                unchecked
                {
                    number++;
                }
            }
            return number;
        }

        private static bool IsPrime(uint number)
        {
            if ((number & 1) == 0) return (number == 2);

            var limit = (int)Math.Sqrt(number);
            for (int i = 3; i <= limit; i += 2)
            {
                if ((number % i) == 0) return false;
            }
            return true;
        }
    }
    class Alice
    {
        public static uint primeNumber;
        public static uint aliceNumber;
        private static uint secretKey;
        public static uint openKey;
        private static uint generalSecretKey;

        public static void GenerateNumber()
        {
            Random ranGenerator;
            ranGenerator = new Random();
            aliceNumber = (uint)ranGenerator.Next((int)primeNumber);
            Bob.aliceNumber = aliceNumber;
        }
        public static void GenerateSecretKey()
        {
            Random ranGenerator;
            ranGenerator = new Random();
            secretKey = (uint)ranGenerator.Next((int)primeNumber);
        }
        public static void GenerateOpenKey()
        {
            openKey = Power.power(aliceNumber, secretKey, primeNumber);
        }
        public static void GenerateGeneralSecretKey()
        {
            generalSecretKey = Power.power(Bob.openKey, secretKey, primeNumber);
        }
        public static void Output()
        {
            Console.WriteLine($"Случайное число меньшее {primeNumber}: {aliceNumber}");
            Console.WriteLine($"Секретный ключ Алисы {secretKey}");
            Console.WriteLine($"Открытый ключ Алисы {openKey}");
            Console.WriteLine($"Общий секретный ключ, который получила Алиса {generalSecretKey}");
        }
    }
    public static class Bob
    {
        public static uint primeNumber;
        public static uint aliceNumber;
        private static uint secretKey;
        public static uint openKey;
        private static uint generalSecretKey;
        public static void GeneratePrimeNumber()
        {
            PrimeNumberGenerator generator;
            generator = new PrimeNumberGenerator();
            primeNumber = generator.Generate();
            Alice.primeNumber = primeNumber;
        }
        public static void GenerateSecretKey()
        {
            Random ranGenerator;
            ranGenerator = new Random();
            secretKey = (uint)ranGenerator.Next((int)primeNumber);
        }
        public static void GenerateOpenKey()
        {
            openKey = Power.power(aliceNumber, secretKey, primeNumber);
        }
        public static void GenerateGeneralSecretKey()
        {
            generalSecretKey = Power.power(Alice.openKey, secretKey, primeNumber);
        }
        public static void Output()
        {
            Console.WriteLine($"Простое число: {primeNumber}");
            Console.WriteLine($"Секретный ключ Боба: {secretKey}");
            Console.WriteLine($"Открытый ключ Боба: {openKey}");
            Console.WriteLine($"Общий секретный ключ, который получил Боб: {generalSecretKey}");
        }
    }
    public class DiffieHellman
    {
        public DiffieHellman()
        {
            Bob.GeneratePrimeNumber();
            Alice.GenerateNumber();
            Bob.GenerateSecretKey();
            Thread.Sleep(1000);
            Alice.GenerateSecretKey();
            Bob.GenerateOpenKey();
            Alice.GenerateOpenKey();
            Bob.GenerateGeneralSecretKey();
            Alice.GenerateGeneralSecretKey();
            Bob.Output();
            Alice.Output();
        }
    }
}
