using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TestConsole
{
    public class StringTokenizerStudy1
    {
        public static void DoTest()
        {
            Console.WriteLine("StringTokenizerStudy1 Start");

            string testStr = "select * from employee where person_name = ${person}  and 1=1";
            string startDelimiter = "${";
            string endDelimiter = "}";

            InlineParamParser parser = new InlineParamParser(testStr, startDelimiter, endDelimiter);
            parser.InnerHandler  = (s) => { Console.WriteLine("inner: " + s); };
            parser.OutterHandler = (s) => { Console.WriteLine("outter: " + s); };

            parser.Process();

            Console.WriteLine("StringTokenizerStudy1 End");
        }
    }

    public delegate void ElementHandler(string element);

    public class InlineParamParser
    {
        private readonly string originStr = null;
        private readonly string startDelimiter = null;
        private readonly string endDelimiter = null;

        private ElementHandler outterHandler = null;
        private ElementHandler innerHandler = null;

        public ElementHandler OutterHandler { set { outterHandler = value; } }
        public ElementHandler InnerHandler { set { innerHandler = value; } }

        public InlineParamParser(string originStr, string startDelimiter, string endDelimiter)
        {
            // 验证首尾分隔符必须不同
            if (startDelimiter.Equals(endDelimiter))
            {
                throw new ArgumentException("首尾分隔符相同导致无法对字符串进行分析");
            }

            this.originStr = originStr;
            this.startDelimiter = startDelimiter;
            this.endDelimiter = endDelimiter;
        }

        public void Process()
        {
            int startDeliPos = originStr.IndexOf(startDelimiter);
            int endDeliPos = originStr.IndexOf(endDelimiter);
            string partalStr = originStr;

            while (startDeliPos < originStr.Length && startDeliPos < endDeliPos)// 找到成对的分隔符
            {
                string outter = partalStr.Substring(0, startDeliPos);
                string inner = partalStr.Substring(startDeliPos + startDelimiter.Length, endDeliPos - startDeliPos - startDelimiter.Length);

                RaiseOutterHandler(outter);
                RaiseInnerHandler(inner);

                partalStr = partalStr.Substring(endDeliPos + endDelimiter.Length, partalStr.Length - endDeliPos - endDelimiter.Length);
                startDeliPos = partalStr.IndexOf(startDelimiter);
                endDeliPos = partalStr.IndexOf(endDelimiter);
            }
            RaiseOutterHandler(partalStr);
        }

        private void RaiseInnerHandler(string inner)
        {
            if (innerHandler != null)
            {
                innerHandler(inner);
            }
        }

        private void RaiseOutterHandler(string outter)
        {
            if (outterHandler != null)
            {
                outterHandler(outter);
            }
        }
    }
}
