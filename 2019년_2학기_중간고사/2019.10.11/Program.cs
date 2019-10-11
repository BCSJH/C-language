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

    class Program
    {
        int number = 0;
        static void Main(string[] args)
        {

            Program pg = new Program();
            Thread t1 = new Thread(threadFunc);
            Thread t2 = new Thread(threadFunc);
            t1.Start(pg);
            t2.Start(pg);

            t1.Join();
            t2.Join();
            Console.WriteLine(pg.number);

        }

        static void threadFunc(object inst) {
            Program pg = inst as Program;

            for (int i = 0; i < 10; i++)
            {
                pg.number = pg.number + 1;
                //Program 객체의 number 필드 값 증가
            }
            Console.WriteLine("for문 : " + pg.number);
            //C#에서 제공하는 것
            Monitor.Enter(pg);
                try
                {
                    pg.number = pg.number + 1;
                }
                finally {
                    Monitor.Exit(pg);
                }
            Console.WriteLine("Monitor 문 : " + pg.number);
            //C#에서 제공하는것
            lock (pg) {
                    pg.number = pg.number + 1;
                }
            Console.WriteLine("look 문 : " + pg.number);
        }
    }
}
