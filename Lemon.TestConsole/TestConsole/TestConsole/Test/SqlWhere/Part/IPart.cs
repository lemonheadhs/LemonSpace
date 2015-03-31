using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TestConsole.Test.SqlWhere.Enum;

namespace TestConsole.Test.SqlWhere.Part
{
    public interface IPart
    {
        string SqlStr { get; set; }
        string Prepend { get; set; }
        PartTypeEnum pType { get; set; }
    }
}
