using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace Thread2
{
    class MyData {
        int number = 0;
        public int Number { get { return number; } }
        public void Increment() {
            number++;
        }
    }
    class Thread_safe
    {
        static void Main(string[] args)
        {
            MyData data = new MyData();
            Thread t1 = new Thread(threadFunc);
            Thread t2 = new Thread(threadFunc);
            t1.Start();
            t2.Start();

            t1.Join();
            t2.Join();
            Console.WriteLine("number : " + data.Number);
        }
        static void threadFunc(object inst)
        {
            MyData data = inst as MyData;
            for (int i = 0; i < 10000; i++) {
                data.Increment();
            }

        }
    }

}
