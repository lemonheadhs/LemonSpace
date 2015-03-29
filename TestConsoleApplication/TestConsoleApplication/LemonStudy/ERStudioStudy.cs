using ERStudio;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestConsoleApplication.LemonStudy
{
    public class ERStudioStudy
    {
        public static void DoTest()
        {
            ERStudio.Application erApp = new ERStudio.Application();

            Diagram currDiag = erApp.ActiveDiagram();
            Model currModel = currDiag.ActiveModel();
            SubModel currSModel = currModel.ActiveSubModel();

            dynamic names = new ExpandoObject();
            dynamic num = new ExpandoObject();

            currSModel.EntityNames(ref names, ref num);

            Console.ReadKey();

        }
        
    }
}
