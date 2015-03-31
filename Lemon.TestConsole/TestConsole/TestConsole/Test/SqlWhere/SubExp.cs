using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TestConsole.Test.SqlWhere
{
    public class SubExp : IExpression
    {
        public string Prepend
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public IExpression And_(string partString)
        {
            throw new NotImplementedException();
        }

        public IExpression Or_(string partString)
        {
            throw new NotImplementedException();
        }

        public IExpression _(string partString)
        {
            throw new NotImplementedException();
        }

        public IExpression And_(string partString, Enum.PartTypeEnum pType)
        {
            throw new NotImplementedException();
        }

        public IExpression Or_(string partString, Enum.PartTypeEnum pType)
        {
            throw new NotImplementedException();
        }

        public IExpression _(string partString, Enum.PartTypeEnum pType)
        {
            throw new NotImplementedException();
        }

        public IExpression And_(IExpression subExp)
        {
            throw new NotImplementedException();
        }

        public IExpression Or_(IExpression subExp)
        {
            throw new NotImplementedException();
        }

        public IExpression _(IExpression subExp)
        {
            throw new NotImplementedException();
        }

        public Part.ExpressionCollection Children
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }
    }
}
