using System;
using System.Text;
using NHibernate.DdlGen.Operations;
using NHibernate.Mapping;
using NHibernate.SqlTypes;

namespace NHibernate.Migrations.Generation
{
    public class CSharpAddTableCommentsDdlOperationGenerator : DdlGeneratorBase<AddTableCommentsDdlOperation>
    {
        protected override void AppendOperation(StringBuilder sb, int index, AddTableCommentsDdlOperation op)
        {
            sb.AppendFormat("            alter.Table(\"{0}\").AddComment(\"{1}\").", op.Model.TableName, op.Model.Comment).AppendLine();
        }
    }
}