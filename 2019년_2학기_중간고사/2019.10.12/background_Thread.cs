using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
namespace Thread2
{
    class background_Thread
    {
        static void main(string[] args)
        {
            Thread t1 = new Thread(threadFunc);
            t1.IsBackground = true;
            //t1.Isbackground는 실행 종료에 영향을 주지 않는다.
            //t1.Join();을 넣지 않는다면 그냥 종료 됨...

            t1.Start();
            t1.Join();

        }
        static void threadFunc(object inst)
        {
            Console.WriteLine("60초 후에 프로그램 종료");
            Thread.Sleep(100 * 60);
            Console.WriteLine("종료");

        }
    }
}

