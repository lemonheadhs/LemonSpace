using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TestConsole
{
    public class StringTokenizerStudy
    {
        public static void DoTest()
        {
            Console.WriteLine("StringTokenizerStudy Start");

            string testStr = "select * from employee where person_name = ##person##  and 1=1";
            string delimiter = "##";
            StrTokenizer st = new StrTokenizer(testStr, delimiter, false);

            IEnumerator<string> enumerator = st.GetEnumerator();

            string result = string.Empty;
            while (enumerator.MoveNext())
            {
                result = enumerator.Current;
            }
        }
    }

    public class StrTokenizer : IEnumerable<string>
    {
        private readonly string originalString = null;
        private readonly string delimiter = null;
        private readonly bool returnDelimiter = true;

        public string OriginanString  { get { return originalString; } }
        public string Delimiter       { get { return delimiter; } }
        public bool   ReturnDelimiter { get { return returnDelimiter; } }

        public StrTokenizer(string origin, string delimiter, bool returnDelimiter)
        {
            if (string.IsNullOrEmpty(delimiter))
            {
                throw new ArgumentNullException("delimiter", "传入参数 delimiter不能为空");
            }

            this.originalString = origin;
            this.delimiter = delimiter;
            this.returnDelimiter = returnDelimiter;
        }

        public IEnumerator<string> GetEnumerator()
        {
            return new TokenizerEnumerator(this);
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }

    internal class TokenizerEnumerator : IEnumerator<string>
    {
        private readonly StrTokenizer master = null;
        private int cursor = 0;
        private string nextStr = string.Empty;

        public TokenizerEnumerator(StrTokenizer master)
        {
            this.master = master;
        }

        #region IEnumerator
        public string Current
        {
            get { return nextStr; }
        }

        public void Dispose()
        { }

        public bool MoveNext()
        {
            nextStr = GetNext();
            return !string.IsNullOrEmpty(nextStr);
        }
        
        public void Reset()
        {
            cursor = 0;
            nextStr = string.Empty;
        }
        #endregion

        private string GetNext()
        {
            // 首先检查游标是否已越界
            if (cursor >= master.OriginanString.Length) // 游标的索引数是指向下一个元素的头，而不是前一个元素的尾，所以cursor==length时，已经越界了
            {
                return null;
            }

            string partalStr = master.OriginanString.Substring(cursor);
            // 在串的未检索部分检验分隔符的位置，找到匹配就返回所遇到的元素
            if (partalStr.IndexOf(master.Delimiter) > -1)
            {
                int pos = partalStr.IndexOf(master.Delimiter);
                if (pos == 0)// 此元素是分隔符
                {
                    cursor += master.Delimiter.Length;
                    if (master.ReturnDelimiter)
                    {
                        return master.Delimiter;
                    }
                    else
                    {
                        return GetNext();
                    }
                }
                else
                {
                    cursor += pos;
                    return partalStr.Substring(0, pos);
                }
            }
            else// 未找到分隔符，就将剩下的串当做一个元素整体输出
            {
                cursor += partalStr.Length;
                return partalStr;
            }
        }

        object System.Collections.IEnumerator.Current
        {
            get { return Current; }
        }
    }
}
