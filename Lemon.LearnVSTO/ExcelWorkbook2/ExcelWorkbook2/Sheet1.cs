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
using Microsoft.Office.Interop.Excel;
using Microsoft.Office.Core;

namespace ExcelWorkbook2
{
    public partial class Sheet1
    {
        private void Sheet1_Startup(object sender, System.EventArgs e)
        {
            
        }

        private void Sheet1_Shutdown(object sender, System.EventArgs e)
        {
        }

        private void DrawOrgChart()
        {
            Microsoft.Office.Interop.Excel.Shape orgChart = this.Shapes.AddSmartArt(this.Application.SmartArtLayouts[88]);
            foreach (SmartArtNode nd in orgChart.SmartArt.AllNodes)
            {
                nd.Delete();
            }

            SmartArtNode rootNd = orgChart.SmartArt.AllNodes.Add();
            rootNd.SetInnerText("lemon");

            RecurAddNode(rootNd);
        }

        private void RecurAddNode(SmartArtNode nd)
        {
            var subNodesData = ChartItemData.TestFamily.Where(n => n.ParentName.Equals(nd.GetInnerText()));
            foreach (var sNdData in subNodesData)
            {
                var sNd = nd.AddNode(MsoSmartArtNodePosition.msoSmartArtNodeBelow, MsoSmartArtNodeType.msoSmartArtNodeTypeDefault);
                sNd.SetInnerText(sNdData.Name);
                RecurAddNode(sNd);
            }
        }

        #region VSTO 设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InternalStartup()
        {
            this.button1.Click += new System.EventHandler(this.button1_Click);
            this.Startup += new System.EventHandler(this.Sheet1_Startup);
            this.Shutdown += new System.EventHandler(this.Sheet1_Shutdown);

        }

        #endregion

        private void button1_Click(object sender, EventArgs e)
        {
            DrawOrgChart();
            TestHasSmartArt();
        }

        private void TestHasSmartArt()
        {
            Microsoft.Office.Interop.Excel.Shape shp = this.Shapes.Item(1);
            if (shp.HasSmartArt.Equals(MsoTriState.msoTrue))
            {
                this.Cells[2, 1].value2 = shp.SmartArt.Layout.Name;
            }
        }

    }

    public class ChartItemData
    {
        public string Name { get; set; }
        public string ParentName { get; set; }

        static private readonly ChartItemData[] list = 
            { 
                new ChartItemData{ Name="zp", ParentName="lemon"},
                new ChartItemData{ Name="jiujiu", ParentName="zp"},
                new ChartItemData{ Name="pupy", ParentName="jiujiu"},
                new ChartItemData{ Name="kitty", ParentName="jiujiu"},
                new ChartItemData{ Name="sun flower", ParentName="zp"},
                //new ChartItemData{ Name="zp", ParentName="lemon"},
                //new ChartItemData{ Name="zp", ParentName="lemon"},
            };

        public static IList<ChartItemData> TestFamily { get { return list; } }

    }

    public static class SmartArtNodeHelper
    {
        public static string GetInnerText(this SmartArtNode san)
        {
            return san.TextFrame2.TextRange.Text;
        }

        public static void SetInnerText(this SmartArtNode san, string text)
        {
            san.TextFrame2.TextRange.Text = text;
        }
    }

}
