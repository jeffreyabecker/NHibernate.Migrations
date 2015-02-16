using System.CodeDom;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using NHibernate.DdlGen.Operations;

namespace NHibernate.Migrations.Generation
{
    public interface IDdlOperationStatementGenerator
    {
        IEnumerable<CodeStatement> GetStatements(IDdlOperation operation);
    }
}