using Ivony.Html.Parser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ivony.Html;
using System.IO;

namespace LearnJumony.Test
{
    public class FirstJumonyTest
    {
        public static void DoTest()
        {
            var parser = new JumonyParser();

            var document = parser.Parse("<html><body><p class=content>Test Content</body></html>");

            var teststr = document.Render();
            var teststr2 = document.ToString();
        }

        public static void DoTest2()
        {
            var str = AppDomain.CurrentDomain.BaseDirectory;
            var str2 = Directory.GetCurrentDirectory();

            var str3 = Path.Combine(str, @"..\..\Resource\发文拟办.html");

            var parser = new JumonyParser();

            var document = parser.LoadDocument(str3);

            var inputs = document.Find("input[type=text]").ToList();

            foreach (var node in inputs)
            {
                node.ReplaceWith("<asp:textbox ></asp:textbox>");
            }

            var r = document.Render();
        }
    }
}
