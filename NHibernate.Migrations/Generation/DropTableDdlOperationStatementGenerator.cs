using System.CodeDom;
using System.Collections.Generic;
using System.Text;
using NHibernate.DdlGen.Operations;

namespace NHibernate.Migrations.Generation
{
    public class DropTableDdlOperationStatementGenerator : DdlOperationStatementGeneratorBase<DropTableDdlOperation>
    {

        protected override IEnumerable<CodeStatement> GetStatements(DropTableDdlOperation operation)
        {
            var op = new CodeObjectCreateExpression(typeof(DropTableDdlOperation), GetDbNameExpression(operation.TableName));
            yield return new CodeExpressionStatement(new CodeMethodInvokeExpression(new CodeVariableReferenceExpression("builder"), "Run", op));
        }
    }
}