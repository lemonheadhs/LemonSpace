using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TestConsole.Test.SqlWhere.Part;
using TestConsole.Test.SqlWhere.Enum;

namespace TestConsole.Test.SqlWhere
{
    public class ExpressionBase : IExpression
    {
        public string Prepend { get; set; }
        public ExpressionCollection Children { get; set; }

        public ExpressionBase()
        {
            Children = new ExpressionCollection();
        }

        public IExpression And_(string partString)
        {
            return And_(partString, PartTypeEnum.IsNotNull);
        }

        public IExpression Or_(string partString)
        {
            return Or_(partString, PartTypeEnum.IsNotNull);
        }

        public IExpression _(string partString)
        {
            return _(partString, PartTypeEnum.IsNotNull);
        }

        public IExpression And_(string partString, Enum.PartTypeEnum pType)
        {
            SqlPart part = new SqlPart(partString, "and", pType);
            Children.Add(part);
            return this;
        }

        public IExpression Or_(string partString, Enum.PartTypeEnum pType)
        {
            SqlPart part = new SqlPart(partString, "or", pType);
            Children.Add(part);
            return this;
        }

        public IExpression _(string partString, Enum.PartTypeEnum pType)
        {
            SqlPart part = new SqlPart(partString, " ", pType);
            Children.Add(part);
            return this;
        }

        public IExpression And_(IExpression subExp)
        {
            subExp.Prepend = "and";
            Children.Add(subExp);
            return this;
        }

        public IExpression Or_(IExpression subExp)
        {
            subExp.Prepend = "or";
            Children.Add(subExp);
            return this;
        }

        public IExpression _(IExpression subExp)
        {
            subExp.Prepend = " ";
            Children.Add(subExp);
            return this;
        }
    }
}
