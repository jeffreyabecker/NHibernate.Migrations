using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using NHibernate.DdlGen.Model;
using NHibernate.DdlGen.Operations;

namespace NHibernate.Migrations.Generation
{
    public class AlterTableAddColumnDdlOperationStatementGenerator :
        DdlOperationStatementGeneratorBase<AlterTableAddColumnDdlOperation>
    {
        protected override IEnumerable<CodeStatement> GetStatements(AlterTableAddColumnDdlOperation operation)
        {
            var model = new CodeVariableDeclarationStatement(typeof(AddOrAlterColumnModel), GetRandomVariableName())
            {
                InitExpression = new CodeObjectCreateExpression(typeof(AddOrAlterColumnModel))
            };
            yield return model;
            yield return new CodeAssignStatement(new CodePropertyReferenceExpression(new CodeVariableReferenceExpression(model.Name), "TableName"), GetDbNameExpression(operation.Model.Table));
            var colModel = new CodeVariableDeclarationStatement(typeof (ColumnCommentModel), GetRandomVariableName());



            var createOp = new CodeObjectCreateExpression(typeof(AlterTableAddColumnDdlOperation),
                new CodeVariableReferenceExpression(model.Name));
            yield return new CodeExpressionStatement(new CodeMethodInvokeExpression(new CodeVariableReferenceExpression("builder"), "Run", createOp));
        }
    }
    
}