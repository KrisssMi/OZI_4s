using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace Lab_6
{
    public class ElGamal
    {
        public int p;
        public int x;
        public int g;
        public int y;

        public string input;
        public List<string> encoded;
        public string decoded;
        private readonly Random _random = new Random();

        public int GetPRoot(int p)
        {
            for (int i = 0; i < p; i++)
                if (IsPRoot(p, i))
                    return i;
            return 0;
        }

        public bool IsPRoot(int p, int a)
        {
            if (a == 0 || a == 1)
                return false;
            int last = 1;
            HashSet<int> set = new HashSet<int>();
            for (int i = 0; i < p - 1; i++)
            {
                last = (last * a) % p;
                if (set.Contains(last)) // Если повтор
                    return false;
                set.Add(last);
            }
            return true;
        }
        public int Power(int a, int b, int n)
        {// a^b mod n 
            int tmp=a; 
            int sum=tmp; 
            for(int i=1;i<b;i++)
            { 
                for(int j=1;j<a;j++)
                { sum+=tmp;
                    if(sum>=n)
                    { 
                        sum-=n;
                    } 
                } tmp=sum;
            } 
            return tmp;
        }
           
        public int Mul(int a, int b, int n)
        {// a*b mod n 
            int sum=0; 
            for(int i=0;i<b;i++)
            { 
                sum+=a; 
                if(sum>=n)
                {
                    sum-=n;
                } 
            } 
            return sum; 
        }
        public ElGamal()
        {
            bool ok = false;
            while(ok == false){
                p = _random.Next(1000);
                int l = GetPRoot(p);
                g = l;
                if (g > 0)
                    ok = true;
            }
            x = _random.Next(1, p - 1);

            y = Power(g, x, p);
            Console.WriteLine($"Открытые ключи: g = {g}, p = {p}, y = {y}        {x} "); //
            Console.WriteLine("Введите шифруемый текст");
            input = Console.ReadLine();

            encoded = Encode(p, g, x, input);

            foreach (string i in encoded)
                Console.WriteLine(i);

            decoded = Decode(p, x, encoded);

            Console.WriteLine(decoded);
        }

        public List<string> Encode(int p, int g, int x, string input)
        {
            List<string> result = new List<string>();
            for (int i = 0; i < input.Length; i++)
            {
                int k = _random.Next(1, p - 1);
                Console.WriteLine($"Сессионный ключ: k = {k}");
                int m = Convert.ToInt32(input[i]);
                Console.WriteLine(m);
                int a = Power(g, k, p);
                int b = Mul(Power(y, k, p), m, p);
                result.Add($"{a},{b}");
            }
            return result;
        }
        public string Decode(int p, int x, List<string> input)
        {
            string result = "";
            foreach(string item in input)
            {
                string[] nums = item.Split(',');
                int a = int.Parse(nums[0]);
                int b = int.Parse(nums[1]);

                int dM = Mul(b, Power(a, p - 1 - x, p), p);

                result += (char)dM;
            }
            return result;
        }
    }
}
