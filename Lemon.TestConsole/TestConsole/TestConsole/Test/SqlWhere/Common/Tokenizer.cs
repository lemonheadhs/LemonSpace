using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TestConsole.Test.SqlWhere.Common
{
    public class Tokenizer : IEnumerable<string>
    {
        public readonly string original = string.Empty;
        public readonly string delimeter = string.Empty;
        public readonly bool returnDeli = true;

        public Tokenizer(string original, string delimeter)
            : this(original, delimeter, true)
        { }

        public Tokenizer(string original, string delimeter, bool returnDeli)
        {
            this.original = original;
            this.delimeter = delimeter;
            this.returnDeli = returnDeli; 
        }

        public IEnumerator<string> GetEnumerator()
        {
            return new TokenEnumerator(this);
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        private class TokenEnumerator : IEnumerator<string>
        {
            private readonly Tokenizer master = null;
            private string next = null;
            private int cursor = 0;

            public TokenEnumerator(Tokenizer tokenizer)
            {
                this.master = tokenizer;
            }

            #region IEnumerator
            public string Current
            {
                get { return next; }
            }

            public void Dispose()
            {
                next = null;
            }

            object System.Collections.IEnumerator.Current
            {
                get { return Current; }
            }

            public bool MoveNext()
            {
                next = GetNext();
                return (!string.IsNullOrEmpty(next));
            }

            public void Reset()
            {
                next = null;
                cursor = 0;
            }
            #endregion

            private string GetNext()
            {
                // 首先检查游标有没有越界
                if (cursor >= master.original.Length)
                {
                    return null;
                }
                string partalStr = master.original.Substring(cursor);

                // 在当前待处理的串中查找分隔符
                int pos = partalStr.IndexOf(master.delimeter);
                if (pos > 0)
                {
                    // 根据找到的分隔符的位置将串分为两段，前段输出为token，后端待分析
                    cursor += pos;
                    return partalStr.Substring(0, pos);
                }
                else if (pos == 0)// 分隔符前是空串，则分隔符应做token输出
                {
                    cursor += master.delimeter.Length;
                    if (master.returnDeli)
                    {
                        return master.delimeter;
                    }
                    else
                    {
                        return GetNext();
                    }
                }
                // 串中没有分隔符了，将串整体输出
                cursor += partalStr.Length;
                return partalStr;
            }
        }
    }

}
