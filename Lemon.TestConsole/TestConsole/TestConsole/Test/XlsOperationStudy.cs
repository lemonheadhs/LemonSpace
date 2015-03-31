using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Office.Interop.Excel;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;

namespace TestConsole
{
    public class XlsOperationStudy
    {
        public static void DoTest()
        {
            GC.Collect();

            string xlsPath = "../../Resource/test1.xlsx";
            string[] dataValiSource = {};//{ "是", "否", "123", "Hello!"};
            DateTime sartDateTime = DateTime.Now;
            //# 打开工作簿
            Application app = new Microsoft.Office.Interop.Excel.Application();
            DateTime afterDateTime = DateTime.Now;

            Workbook workbook = null;
            Worksheet sheet1 = null;
            Worksheet dataSourceSheet = null;

            try
            {
                string pathStr = Path.GetFullPath(xlsPath);
                //workbook = app.Workbooks.Add(pathStr);
                workbook = app.Workbooks.Open(pathStr, Type.Missing, false);   // 一定要指定 以可写方式打开，不然保存时将新建另一个文档

                sheet1 = workbook.Sheets[1] as Microsoft.Office.Interop.Excel.Worksheet;
                dataSourceSheet = workbook.Sheets[2] as Microsoft.Office.Interop.Excel.Worksheet;

                //sheet1.Range["A1"].Value2 = "Hello! Lemom";

                //# 获得 要应用数据有效性验证的 列
                Range rg = sheet1.Range["A1"].EntireColumn;

                //# 清除该列 之前应用的 有效性验证
                rg.Validation.Delete();  // ??
                rg.NumberFormatLocal = "@";  // 设置单元格格式为，文本格式

                //# 获得 保存数据源的 隐藏列，清空该列的数据
                Range dataSourceColumn = dataSourceSheet.Range["A1"].EntireColumn;
                dataSourceColumn.Clear();  // ??
                dataSourceColumn.NumberFormatLocal = "@";

                //# 获取数据源，
                // dataSource = dataValiSource;

                //# 将数据源的数据，保存到工作簿的一列，将该列设为 隐藏
                int index = 1;
                foreach (string str in dataValiSource)
                {
                    dataSourceColumn.Range["A" + index.ToString()].Value2 = str;
                    index++;
                }
                string sourceName = "DS" + Guid.NewGuid().ToString().Replace("-", "_");
                if (index > 1)
                {
                    dataSourceColumn.Range["A1:A" + (index - 1).ToString()].Name = sourceName;   // 如果是指定整个列的话，有效性验证将失效                    
                }
                else
                {
                    dataSourceColumn.Range["A1"].Value2 = " ";
                    dataSourceColumn.Range["A1"].Name = sourceName;
                }
                dataSourceColumn.EntireColumn.Hidden = true;

                //# 获得 将要应用数据有效性验证的 列
                //# 对该列，应用数据有效性验证，有效性验证的规定来源于隐藏列
                rg.Validation.Add(Microsoft.Office.Interop.Excel.XlDVType.xlValidateList,
                                Microsoft.Office.Interop.Excel.XlDVAlertStyle.xlValidAlertStop,
                                Microsoft.Office.Interop.Excel.XlFormatConditionOperator.xlBetween,//Type.Missing,
                                "=" + sourceName,
                                Type.Missing);

                

                sheet1.Activate();
                //workbook.AcceptAllChanges();
                workbook.Save();              
                //workbook.SaveAs(pathStr);
                workbook.Close(false, null, null);
                app.Quit();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                Process[] myProcess = Process.GetProcessesByName("Excel");
                foreach (Process process in myProcess)
                {
                    DateTime nowDatetime = process.StartTime;
                    if (nowDatetime > sartDateTime && nowDatetime < afterDateTime)
                    {
                        process.Kill();
                    }
                }
                workbook = null;
                app = null;
                sheet1 = null;
                dataSourceSheet = null;
                GC.Collect();
            }
        }

        // SaveCopyAs()方法 是否是以覆盖的方式保存文件的？
        public static void DoTest2()
        {
            testBox((wb, ws) => 
            {
                string testXlsName = @"../../Resource/test_SaveCopyAs.xlsx";

                string pathStr = Path.GetFullPath(testXlsName);
                ws.Range["A1:A1"].Value2 = DateTime.Now.ToString();
                wb.SaveCopyAs(pathStr);
            
            }); // 答案是：是！
        }

        // Range.Style 是否可以设置格式？应以什么格式传递这个参数？？
        public static void DoTest3()
        {
            string testXlsName = @"../../Resource/test_Range_Style.xlsx";

            testBox(testXlsName, (wb, ws) =>
            {
                //var block = ws.Range["A1"];
                //var ss = ws.Range["A1:H1"].Cells[1,1].Value as string;
                var styleStr = ws.Range["A1:H1"].Style;

                //var _borders = styleStr.Borders;
                //var we = _borders(XlBordersIndex.xlEdgeBottom).LineStyle == XlLineStyle.xlLineStyleNone;


                var sw = styleStr.Font;
                var sg = sw.Size;
                var ttt = sw.Name;

                string tt = "do nothing";
            });// 答案是：Range.Style是个comObject，.NET中似乎没有对应的类型，貌似不能操作

            //Style style = new 

        }

        // 合并单元格
        public static void DoTest4()
        {
            string testXlsName = @"../../Resource/test_Range_Style.xlsx";

            testBox(testXlsName, (wb, ws) =>
            {
                //var styleStr = wb.Styles.Add("test");
                var styleStr = wb.Styles["test"];
                var rg = ws.Range["A16:H16"];

                styleStr.Font.Size = 14;
                styleStr.Font.Bold = true;
                styleStr.HorizontalAlignment = XlHAlign.xlHAlignCenter;
                styleStr.MergeCells = true;

                rg.Style = styleStr;
                
                //rg.MergeCells = true;
                //rg.Merge(false);// 合并单元格
                rg.Value2 = "采 购 询 价 单";// 如果在合并单元格之前，给range赋值，会弹出窗口

                wb.Save();
                string tt = "do nothing";
            });
        }

        // 边框
        public static void DoTest5()
        {
            string testXlsName = @"../../Resource/test_Range_Style.xlsx";

            testBox(testXlsName, (wb, ws) => 
            {
                var todel = wb.Styles["test2"];
                if (todel != null)
                {
                    todel.Delete();
                }

                var styleStr = wb.Styles.Add("test2");
                //var styleStr = wb.Styles["test2"];
                //if (styleStr == null) { styleStr = wb.Styles.Add("test2"); }
                var rg = ws.Range["A17:H17"];

                //rg.Borders.Weight = 2;// 单独这就可以了，为什么？
                //rg.Borders[XlBordersIndex.xlEdgeBottom].LineStyle = XlLineStyle.xlContinuous;// 也没效果

                //styleStr.Borders[XlBordersIndex.xlEdgeTop].LineStyle = XlLineStyle.xlContinuous;// 居然没效果
                //styleStr.Borders[XlBordersIndex.xlEdgeBottom].LineStyle = XlLineStyle.xlContinuous;// 居然没效果
                //styleStr.Borders[XlBordersIndex.xlEdgeLeft].LineStyle = XlLineStyle.xlContinuous;// 有效
                //var tttt = styleStr.Borders[XlBordersIndex.xlEdgeRight];
                //var tttt1 = styleStr.Borders[XlBordersIndex.xlEdgeLeft];
                //styleStr.Borders[XlBordersIndex.xlEdgeRight].LineStyle = XlLineStyle.xlContinuous;// 居然没效果
                //styleStr.Borders[XlBordersIndex.xlEdgeRight].Weight = 2;

                //styleStr.Borders.get_Item(XlBordersIndex.xlEdgeRight).LineStyle = XlLineStyle.xlContinuous;// 也没效果

                //var topBorder = styleStr.Borders[XlBordersIndex.xlEdgeLeft];

                styleStr.Borders.LineStyle = 1;
                styleStr.Borders[XlBordersIndex.xlDiagonalDown].LineStyle = XlLineStyle.xlLineStyleNone;
                styleStr.Borders[XlBordersIndex.xlDiagonalUp].LineStyle = XlLineStyle.xlLineStyleNone;
                //styleStr.Borders.LineStyle = XlLineStyle.xlContinuous;   // 对区域内的所有单元格，应用所有的边框(包括斜线，可能单元格内有内容是，斜线就不展示了)
                //styleStr.Borders.LineStyle = XlLineStyle.xlLineStyleNone; 

                // 在style里设置了 Borders.LineStyle， 然后 Borders[].LineStyle 就没效果了，这个说明，这两个是独立的可能起冲突
                // Borders.LineStyle 可能优先级比 Borders[].LineStyle 高

                rg.Style = styleStr;
                
                wb.Save();
                string tt = "do nothing";
            });
        }

        // 可读写方式打开文档的测试框架
        private static void testBox(string xlsFilepath, System.Action<_Workbook, _Worksheet> action)
        {
            // 准备工作
            GC.Collect();
            DateTime now = DateTime.Now;
            Application o = new Microsoft.Office.Interop.Excel.Application(); //new ApplicationClass();
            DateTime time2 = DateTime.Now;

            _Workbook workbook = null;
            _Worksheet activeSheet = null;

            string pathStr = Path.GetFullPath(xlsFilepath);
            try
            {
                workbook = o.Workbooks.Open(pathStr, Type.Missing, false);   // 一定要指定 以可写方式打开，不然保存时将新建另一个文档
                activeSheet = (_Worksheet)workbook.Sheets[1];

                //
                action(workbook, activeSheet);

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.ReadKey();
            }
            finally
            {
                // 清理工作
                if (workbook != null)
                {
                    workbook.Close(false, null, null);
                }
                o.Quit();
                if (activeSheet != null)
                {
                    Marshal.ReleaseComObject(activeSheet);
                }
                if (workbook != null)
                {
                    Marshal.ReleaseComObject(workbook);
                }
                Marshal.ReleaseComObject(o);
                foreach (Process process in Process.GetProcessesByName("Excel"))
                {
                    DateTime startTime = process.StartTime;
                    if ((startTime > now) && (startTime < time2))
                    {
                        process.Kill();
                    }
                }
                activeSheet = null;
                workbook = null;
                o = null;
                GC.Collect();
            }
        }

        // 新增方式打开文档的测试框架
        private static void testBox(System.Action<_Workbook, _Worksheet> action)
        {
            // 准备工作
            GC.Collect();
            DateTime now = DateTime.Now;
            Application o = new Microsoft.Office.Interop.Excel.Application(); //new ApplicationClass();
            DateTime time2 = DateTime.Now;
            _Workbook workbook = o.Workbooks.Add(true);
            _Worksheet activeSheet = (_Worksheet)workbook.ActiveSheet;

            //
            action(workbook, activeSheet);
            

            // 清理工作
            workbook.Close(false, null, null);
            o.Quit();
            Marshal.ReleaseComObject(activeSheet);
            Marshal.ReleaseComObject(workbook);
            Marshal.ReleaseComObject(o);
            foreach (Process process in Process.GetProcessesByName("Excel"))
            {
                DateTime startTime = process.StartTime;
                if ((startTime > now) && (startTime < time2))
                {
                    process.Kill();
                }
            }
            activeSheet = null;
            workbook = null;
            o = null;
            GC.Collect();

            //return ("" + ".xls");
        }
    }
}
