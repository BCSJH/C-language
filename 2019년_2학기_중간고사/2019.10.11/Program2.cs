using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
namespace ConsoleApplication11
{
    //int socket(int family, int type, int protocol)
    //family -> AF_LINET : 인터넷 주소 체계
    //       -> AF_UNIX : 유닉스 주소 체계
    //       -> AF_INET6 : 128비트 IPv6 주소 체계
    //type -> 서비스 타입을 의미
    //     -> 연결형 : SOCK_STREAM
    //     -> 비연결형 : SOCK_DGRAM
    //protocol -> 소켓 지원 프로그램 지정 보통 0사용


    class MyData
    {
        int number = 0;
        public int Number { get { return number; } }
        public void Increment()
        {
            number++;
        }
    }
    class Program2
    {
        static void Main(string[] args)
        {
            MyData data = new MyData();
            Thread t1 = new Thread(threadFunc);
            Thread t2 = new Thread(threadFunc);

            t1.Start(data);
            t2.Start(data);

            t1.Join();
            t2.Join();
            Console.WriteLine(data.Number);

        }

        static void threadFunc(object inst)
        {
            MyData data = inst as MyData;
            for (int i = 0; i < 10000; i++)
            {
                data.Increment();
            }
        }
    }
}
