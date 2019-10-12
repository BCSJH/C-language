using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace Thread2
{   class ThreadParam {
        public int value1;
        public int value2;
    }
    class Multi_Thread
    {
        static void Main(string[] args) {
            Thread t = new Thread(threadFunc);

            ThreadParam param = new ThreadParam();
            param.value1 = 10;
            param.value2 = 20;

            t.Start(param);
        }

        static void threadFunc(object initialValue) {
            ThreadParam intValue = (ThreadParam)initialValue;
            Console.WriteLine("{0},{1}", intValue.value1, intValue.value2);
        }
    }
}
