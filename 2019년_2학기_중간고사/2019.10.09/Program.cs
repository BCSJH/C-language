using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
namespace Thread2
{
    class ThreadParam
    {
        public int value1;
        public int value2;
    }
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("입력한 숫자까지의 소수 개수 출력 (종료: 'x' + Enter)");
            while (true)
            {
                Console.Write("숫자 입력 : ");
                string userNumber = Console.ReadLine();
                if (userNumber.Equals("x", StringComparison.OrdinalIgnoreCase) == true)
                {
                    Console.WriteLine("종료");
                    break;
                }
                countPrimeNumbers(userNumber);
            }

        }
        static void countPrimeNumbers(object initialValue)
        {
            string value = (string)initialValue;
            int primeCamdidate = int.Parse(value);
            int totalPrimes = 0;

            Console.Write("소수 : ");
            for (int i = 2; i < primeCamdidate; i++)
                if (IsPrime(i) == true)
                {
                    totalPrimes++;
                    Console.Write(i + " ");
                }

            Console.WriteLine("\n숫자 {0}까지의 소수 개수? {1}\n", value, totalPrimes);
        }
        static bool IsPrime(int candidate)
        {
            if ((candidate & 1) == 0)
                return candidate == 2;
            for (int i = 3; (i * i) <= candidate; i += 2)
                if ((candidate % i) == 0)
                    return false;
            return candidate != 1;
        }
    }
}
