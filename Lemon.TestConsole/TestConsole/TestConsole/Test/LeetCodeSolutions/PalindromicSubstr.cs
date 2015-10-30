using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TestConsole.Test.LeetCodeSolutions
{
    public class PalindromicSubstr
    {
        public static void DoTest()
        {
            var a = new PalindromicSubstr();
            var tt = a.LongestPalindrome("vommleztyrbrnoij");
            Console.WriteLine(tt);
            Console.ReadKey();
        }


        public string LongestPalindrome(string s)
        {
            var finder = new PalindromeFinder(s);
            finder.SearchThrough();
            return finder.MaxPalindromicSubstr;
        }

        public class PalindromeFinder
        {
            private string _raw_s;
            private List<StartPoint> StartPoints { get; set; }
            protected StartPoint MaxSp { get; set; }

            public PalindromeFinder(string s)
            {
                _raw_s = s;
                StartPoints = new List<StartPoint>();
            }

            public int MaxPalindromicLength
            {
                get
                {
                    if (MaxSp == null)
                    {
                        if (string.IsNullOrEmpty(_raw_s)) return 0;
                        return 1;
                    }
                    return MaxSp.MaxPalindromicLength;
                }
            }
            public string MaxPalindromicSubstr 
            {
                get
                {
                    if (MaxSp==null)
                    {
                        if (string.IsNullOrEmpty(_raw_s)) return "";
                        return _raw_s.Substring(0, 1);
                    }
                    return _raw_s.Substring(MaxSp.Left - MaxSp.Radius, MaxSp.MaxPalindromicLength);
                }
            }

            public void SearchThrough()
            {
                FindStartPoints();
                foreach (var sp in StartPoints)
                {
                    while (TryExpand(sp))
                    { }
                }
            }

            private void FindStartPoints()
            {
                int length = _raw_s.Length;
                for (int n = 0; n < length; n++)
                {
                    char c = _raw_s[n];
                    if (n+1<length)
                    {
                        if (c == _raw_s[n + 1])
                        {
                            StartPoints.Add(new StartPoint(n, n + 1));
                        }
                    }
                    if (n+2<length)
                    {
                        if (c == _raw_s[n + 2])
                        {
                            StartPoints.Add(new StartPoint(n, n + 2));
                        }
                    }
                }
            }
            private bool TryExpand(StartPoint sp)
            {
                if (MaxSp == null)
                {
                    MaxSp = sp;
                }
                if (sp.Left-sp.Radius-1<0
                    || sp.Right+sp.Radius+1>=_raw_s.Length)
                {
                    if (sp != MaxSp && sp.MaxPalindromicLength > MaxSp.MaxPalindromicLength)
                    {
                        MaxSp = sp;
                    }
                    return false;
                }

                bool expandSuccess = _raw_s[sp.Left - sp.Radius - 1] == _raw_s[sp.Right + sp.Radius + 1];
                if (expandSuccess)
                {
                    sp.Radius += 1;
                }
                if (sp != MaxSp && sp.MaxPalindromicLength > MaxSp.MaxPalindromicLength)
                {
                    MaxSp = sp;
                }

                return expandSuccess;
            }
        }



        public class StartPoint
        {
            public int Left { get; private set; }
            public int Right { get; private set; }
            public int Radius { get; set; }

            public StartPoint(int left, int right)
            {
                Left = left;
                Right = right;
                Radius = 0;
            }

            public int MaxPalindromicLength { get { return 2*Radius + Right - Left + 1; } }
        }
    }
}
