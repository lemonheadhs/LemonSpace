using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ERStudio;
using System.Dynamic;

namespace MP_ERStudio.TestTasks
{
    public class FirstBlood
    {
        //当前还没有一个活动的ERStudio进程时，启动一个进程
        public static void DoTest() 
        {
            ERStudio.Application app = new ERStudio.Application();
            app.ShowWindow();
        }
        
        public static void DoTest1() 
        {
            ERStudio.Application app = new ERStudio.Application();
            Diagram diag = app.ActiveDiagram();
            Console.WriteLine(diag.FileName);
            Model currModel = diag.ActiveModel();
            SubModel currSModel = currModel.ActiveSubModel();

            dynamic names = Type.Missing;//new DynamicObject();
            dynamic num = Type.Missing; //0;
            currSModel.EntityNames(ref names, ref num);   //静态语言进行COM互操作的巨大鸿沟显现了出来，，要继续跨域这个鸿沟，需要学习一下DLR
            //dynamic vnames = currSModel.ViewNames();
            //foreach (dynamic nstr in vnames)
            //{
            //    Console.WriteLine(nstr);
            //}

            Console.ReadKey();
        }

        //打开指定的.dm1文件
        public static void DoTest2()
        {
            string filePath = @"E:\WorkSpace\DtscMcs\trunk\项目管理\数据库设计\标准化.DM1";

            ERStudio.Application app = new ERStudio.Application();
            app.OpenFile(filePath);
        }




    }
}
