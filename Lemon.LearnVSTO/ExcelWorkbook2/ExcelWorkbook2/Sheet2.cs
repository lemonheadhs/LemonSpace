using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml.Linq;
using Microsoft.Office.Tools.Excel;
using Microsoft.VisualStudio.Tools.Applications.Runtime;
using Excel = Microsoft.Office.Interop.Excel;
using Office = Microsoft.Office.Core;
using System.Diagnostics;
using Microsoft.Office.Interop.Excel;

namespace ExcelWorkbook2
{
    public partial class Sheet2
    {
        private void Sheet2_Startup(object sender, System.EventArgs e)
        {
            list1.Change += (tr, cr) => 
            { 
                Trace.WriteLine("lemon");
                Trace.WriteLine(tr.Address);
                Trace.WriteLine(cr.ToString());
            };

            list1.BeforeAddDataBoundRow += (s, arg) => 
            {
                Trace.WriteLine("lemon");//在拖动右下角的时候没有引发事件
            };
        }

        private void Sheet2_Shutdown(object sender, System.EventArgs e)
        {
        }

        #region VSTO 设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InternalStartup()
        {
            this.button1.Click += new System.EventHandler(this.button1_Click);
            this.Startup += new System.EventHandler(this.Sheet2_Startup);
            this.Shutdown += new System.EventHandler(this.Sheet2_Shutdown);

        }

        #endregion

        private void button1_Click(object sender, EventArgs e)
        {
            ListColumn lc = list1.ListColumns[1];
            Trace.WriteLine(lc.Index);//1    --说明，索引的起点是1不是0
            Trace.WriteLine(lc.Name);//列1

            //for (int n = 0; n < list1.ListRows.Count; n++)
            //{
            //    string content = lc.Range[n].value;
            //    Trace.WriteLine(content);
            //}
            Trace.WriteLine(list1.ListRows.Count);

        }

        /*
         要做验证：
         1.id列新增时，检查是否是输入了重复的id，验证失败时给出提示
         2.pid列新增时，检查新增的pid是否是已存在的id并且不是本行的id，验证失败时给出提示
         
         
         
         */
    }
}
