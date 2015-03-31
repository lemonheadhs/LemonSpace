#define lemon

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TestConsole.Test
{
    public class EventStudy
    {
        private Action _method = null;

#if !lemon
        public event Action OneMethod
        {
            add { _method += value; }
            remove { _method -= value; }
        }
#else
        public event Action OneMethod;
#endif
        private void SayHello()
        {
            Console.WriteLine("Hello,lemon!");
        }

        public static void DoTest()
        {
            Console.WriteLine("EventStudy Start.");

            EventStudy obj = new EventStudy();

            obj.OneMethod += obj.SayHello;

            if (obj._method != null)
            {
                obj._method();                
            }
            obj.OneMethod();
        }
    }
}
