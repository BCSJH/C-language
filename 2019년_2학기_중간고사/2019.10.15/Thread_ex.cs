using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
namespace safe_thread
{
    class MyData {

        int number = 0;
       // public object _numberLock = new object();
        public int Number { get { return number; } }
        public void Increament() {
            number++;
            /*
            lock (_numberLock) {
                number++;
            }
            */
        }
    }
   
    class Thread_ex
    {
        static void Main(string[] arg) {
            MyData data = new MyData();

            Thread t1 = new Thread(threadFunc);
            Thread t2 = new Thread(threadFunc);

            t1.Start(data);
            t2.Start(data);

            t1.Join();
            t2.Join();

            Console.WriteLine(data.Number);
        }
        static void threadFunc(object inst) {
            MyData data = inst as MyData;

            for (int i = 0; i < 100000; i++) {
                //data.Increament();
                lock (data) {
                    data.Increament();
                }
            }
        }
    }
}
