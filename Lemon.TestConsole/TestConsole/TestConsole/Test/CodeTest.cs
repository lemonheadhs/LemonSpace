using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace TestConsole
{
    public class CodeTest
    {
        private const string BINDING_BEGIN_TOKEN = "~{";
        private const string BINDING_END_TOKEN = "}";
        private const string MARK_TOKEN = "?";
        private const string NEW_BEGIN_TOKEN = "@{";
        private const string NEW_END_TOKEN = "}";
        private const string PARAM_DELIM = ":";
        private const string PARAMETER_TOKEN = "#";

        public static void DoTest()
        {
            string sqlStatement = "select * from employee where person_name = #person#  and 1=1";

            StringTokenizer parser = new StringTokenizer(sqlStatement, PARAMETER_TOKEN, true);
            StringBuilder newSqlBuffer = new StringBuilder();

            string token = null;
            string lastToken = null;

            IEnumerator enumerator = parser.GetEnumerator();

            while (enumerator.MoveNext())
            {
                token = (string)enumerator.Current;

                if (PARAMETER_TOKEN.Equals(lastToken))
                {
                    // Double token ## = #
                    if (PARAMETER_TOKEN.Equals(token))
                    {
                        newSqlBuffer.Append(PARAMETER_TOKEN);
                        token = null;
                    }
                    else
                    {
                        //ParameterProperty mapping = null;
                        //if (token.IndexOf(PARAM_DELIM) > -1)
                        //{
                        //    mapping = OldParseMapping(token, parameterClassType, dataExchangeFactory);
                        //}
                        //else
                        //{
                        //    mapping = NewParseMapping(token, parameterClassType, dataExchangeFactory, statementId);
                        //}

                        //mappingList.Add(mapping);
                        newSqlBuffer.Append(MARK_TOKEN + " ");

                        enumerator.MoveNext();
                        token = (string)enumerator.Current;
                        if (!PARAMETER_TOKEN.Equals(token))
                        {
                            //throw new DataMapperException("Unterminated inline parameter in mapped statement (" + statementId + ").");
                        }
                        token = null;
                    }
                }
                else
                {
                    if (!PARAMETER_TOKEN.Equals(token))
                    {
                        newSqlBuffer.Append(token);
                    }
                }

                lastToken = token;
            }

            string newSql = newSqlBuffer.ToString();
        }

        public static void DoTest1()
        {
            string newSql = "select * from employee where person_name = @{person}  and 1=1";

            if (newSql != null)
            {
                string toAnalyse = newSql;
                int start = toAnalyse.IndexOf(NEW_BEGIN_TOKEN);
                int end = toAnalyse.IndexOf(NEW_END_TOKEN);
                StringBuilder newSqlBuffer = new StringBuilder();

                while (start > -1 && end > start)
                {
                    string prepend = toAnalyse.Substring(0, start);
                    string append = toAnalyse.Substring(end + NEW_END_TOKEN.Length);

                    //EmailAddress,column=string,type=string,dbType=Varchar,nullValue=no_email@provided.com
                    string parameter = toAnalyse.Substring(start + NEW_BEGIN_TOKEN.Length, end - start - NEW_BEGIN_TOKEN.Length);
                    //ParameterProperty mapping = NewParseMapping(parameter, parameterClassType, dataExchangeFactory, statementId);
                    //mappingList.Add(mapping);
                    newSqlBuffer.Append(prepend);
                    newSqlBuffer.Append(MARK_TOKEN);
                    toAnalyse = append;
                    start = toAnalyse.IndexOf(NEW_BEGIN_TOKEN);
                    end = toAnalyse.IndexOf(NEW_END_TOKEN);
                }
                newSqlBuffer.Append(toAnalyse);
                newSql = newSqlBuffer.ToString();
            }
        }
    }

    /// <summary>
    /// A StringTokenizer java like object 
    /// Allows to break a string into tokens
    /// </summary>
    public sealed class StringTokenizer : IEnumerable
    {
        private const string DEFAULT_DELIM = " \t\n\r\f";
        private readonly string origin = string.Empty;
        private readonly string delimiters = string.Empty;
        private readonly bool returnDelimiters = false;

        /// <summary>
        /// Constructs a StringTokenizer on the specified String, using the
        /// default delimiter set (which is " \t\n\r\f").
        /// </summary>
        /// <param name="str">The input String</param>
        public StringTokenizer(string str)
        {
            origin = str;
            delimiters = DEFAULT_DELIM;
            returnDelimiters = false;
        }


        /// <summary>
        /// Constructs a StringTokenizer on the specified String, 
        /// using the specified delimiter set.
        /// </summary>
        /// <param name="str">The input String</param>
        /// <param name="delimiters">The delimiter String</param>
        public StringTokenizer(string str, string delimiters)
        {
            origin = str;
            this.delimiters = delimiters;
            returnDelimiters = false;
        }


        /// <summary>
        /// Constructs a StringTokenizer on the specified String, 
        /// using the specified delimiter set.
        /// </summary>
        /// <param name="str">The input String</param>
        /// <param name="delimiters">The delimiter String</param>
        /// <param name="returnDelimiters">Returns delimiters as tokens or skip them</param>
        public StringTokenizer(string str, string delimiters, bool returnDelimiters)
        {
            origin = str;
            this.delimiters = delimiters;
            this.returnDelimiters = returnDelimiters;
        }


        /// <summary>
        /// Returns an enumerator that iterates through a collection.
        /// </summary>
        /// <returns>
        /// An <see cref="T:System.Collections.IEnumerator"/> object that can be used to iterate through the collection.
        /// </returns>
        public IEnumerator GetEnumerator()
        {
            return new StringTokenizerEnumerator(this);
        }


        /// <summary>
        /// Returns the number of tokens in the String using
        /// the current deliminter set.  This is the number of times
        /// nextToken() can return before it will generate an exception.
        /// Use of this routine to count the number of tokens is faster
        /// than repeatedly calling nextToken() because the substrings
        /// are not constructed and returned for each token.
        /// </summary>
        public int TokenNumber
        {
            get
            {
                int count = 0;
                int currpos = 0;
                int maxPosition = origin.Length;

                while (currpos < maxPosition)
                {
                    while (!returnDelimiters &&                    // 不返回分隔符
                        (currpos < maxPosition) &&                 // 未越界
                        (delimiters.IndexOf(origin[currpos]) >= 0))// 当前字符匹配了分隔符
                    {
                        currpos++;
                    }

                    if (currpos >= maxPosition)
                    {
                        break;
                    }

                    int start = currpos;
                    while ((currpos < maxPosition) &&             // 未越界
                        (delimiters.IndexOf(origin[currpos]) < 0))// 当前字符 不能匹配分隔符
                    {
                        currpos++;
                    }
                    if (returnDelimiters && (start == currpos) &&
                        (delimiters.IndexOf(origin[currpos]) >= 0))
                    {
                        currpos++;
                    }
                    count++;
                }
                return count;

            }

        }


        private sealed class StringTokenizerEnumerator : IEnumerator
        {
            private readonly StringTokenizer stokenizer;
            private int cursor = 0;
            private string next = null;

            public StringTokenizerEnumerator(StringTokenizer stok)
            {
                stokenizer = stok;
            }

            public bool MoveNext()
            {
                next = GetNext();
                return next != null;
            }

            public void Reset()
            {
                cursor = 0;
            }

            public object Current
            {
                get
                {
                    return next;
                }
            }

            private string GetNext()
            {
                if (cursor >= stokenizer.origin.Length)
                    return null;

                char c = stokenizer.origin[cursor];
                bool isDelim = (stokenizer.delimiters.IndexOf(c) != -1);

                if (isDelim)
                {
                    cursor++;
                    if (stokenizer.returnDelimiters)
                    {
                        return c.ToString();
                    }
                    return GetNext();
                }

                int nextDelimPos = stokenizer.origin.IndexOfAny(stokenizer.delimiters.ToCharArray(), cursor);
                if (nextDelimPos == -1)
                {
                    nextDelimPos = stokenizer.origin.Length;
                }

                string nextToken = stokenizer.origin.Substring(cursor, nextDelimPos - cursor);
                cursor = nextDelimPos;
                return nextToken;
            }

        }
    }
}
