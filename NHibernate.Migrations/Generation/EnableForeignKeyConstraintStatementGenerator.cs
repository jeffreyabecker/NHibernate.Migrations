using System.CodeDom;
using System.Collections.Generic;
using System.Text;
using NHibernate.DdlGen.Operations;

namespace NHibernate.Migrations.Generation
{
    public class EnableForeignKeyConstraintStatementGenerator : DdlOperationStatementGeneratorBase<EnableForeignKeyConstratintDdlOperation>
    {

        protected override IEnumerable<CodeStatement> GetStatements(EnableForeignKeyConstratintDdlOperation operation)
        {
            var op = new CodeObjectCreateExpression(typeof (EnableForeignKeyConstratintDdlOperation));
            yield return new CodeExpressionStatement(new CodeMethodInvokeExpression(new CodeVariableReferenceExpression("builder"),"Run",op));
        }
    }

    public class DisableForeignKeyConstraintStatementGenerator : DdlOperationStatementGeneratorBase<DisableForeignKeyConstraintDdlOperation>
    {

        protected override IEnumerable<CodeStatement> GetStatements(DisableForeignKeyConstraintDdlOperation operation)
        {
            var op = new CodeObjectCreateExpression(typeof(DisableForeignKeyConstraintDdlOperation));
            yield return new CodeExpressionStatement(new CodeMethodInvokeExpression(new CodeVariableReferenceExpression("builder"), "Run", op));
        }
    }
}