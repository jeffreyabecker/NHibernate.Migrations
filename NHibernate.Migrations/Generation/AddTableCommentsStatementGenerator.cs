using System.CodeDom;
using System.Collections.Generic;
using System.Text;
using NHibernate.DdlGen.Model;
using NHibernate.DdlGen.Operations;
using NHibernate.Mapping;
using NHibernate.SqlTypes;

namespace NHibernate.Migrations.Generation
{
    public class AddTableCommentsStatementGenerator : DdlOperationStatementGeneratorBase<AddTableCommentsDdlOperation>
    {
        protected override IEnumerable<CodeStatement> GetStatements(AddTableCommentsDdlOperation operation)
        {
            var model = new CodeVariableDeclarationStatement(typeof(TableCommentsModel), GetRandomVariableName())
            {
                InitExpression = new CodeObjectCreateExpression(typeof(TableCommentsModel))
            };
            yield return model;
            yield return new CodeAssignStatement(new CodePropertyReferenceExpression(new CodeVariableReferenceExpression(model.Name), "Comment"), new CodePrimitiveExpression(operation.Model.Comment));
            yield return new CodeAssignStatement(new CodePropertyReferenceExpression(new CodeVariableReferenceExpression(model.Name), "TableName"), GetDbNameExpression(operation.Model.TableName));

            var colsRef = new CodePropertyReferenceExpression(new CodeVariableReferenceExpression(model.Name), "Columns");
            yield return new CodeAssignStatement(colsRef, new CodeObjectCreateExpression(typeof(List<ColumnCommentModel>)));
            foreach (var cm in operation.Model.Columns)
            {
                var colModel = new CodeObjectCreateExpression(typeof(ColumnCommentModel), new CodePrimitiveExpression(cm.ColumnName), new CodePrimitiveExpression(cm.Comment));
                yield return new CodeExpressionStatement(new CodeMethodInvokeExpression(new CodeMethodReferenceExpression(colsRef, "Add"), colModel));
            }

            var createOp = new CodeObjectCreateExpression(typeof (AddTableCommentsDdlOperation),
                new CodeVariableReferenceExpression(model.Name));
            yield return new CodeExpressionStatement(new CodeMethodInvokeExpression(new CodeVariableReferenceExpression("builder"), "Run", createOp));


        }


    }

}