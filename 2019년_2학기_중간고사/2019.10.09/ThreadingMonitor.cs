using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace Thread2
{
    class ThreadingMonitor
    {
        int number = 0;

        static void Main(string[] args)
        {
            ThreadingMonitor pg = new ThreadingMonitor();
            Thread t1 = new Thread(threadFunc);
            Thread t2 = new Thread(threadFunc);
            t1.Start(pg);
            t2.Start(pg);
            t1.Join();
            t2.Join();
            Console.WriteLine("number : " + pg.number);
        }
        static void threadFunc(object inst)
        {
            ThreadingMonitor pg = inst as ThreadingMonitor;
            for (int i = 0; i < 10; i++)
            {
                pg.number = pg.number + 1;

            }
        }
    }
}
