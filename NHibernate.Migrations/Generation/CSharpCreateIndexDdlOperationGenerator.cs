//using System;
//using System.Linq;
//using System.Text;
//using NHibernate.DdlGen.Operations;

//namespace NHibernate.Migrations.Generation
//{
//    public class CSharpCreateIndexDdlOperationGenerator : DdlGeneratorBase<CreateIndexDdlOperation>
//    {
//        protected override void AppendOperation(StringBuilder sb, int index, CreateIndexDdlOperation op)
//        {
//            sb.Append("builder.Create.Index(\"")
//              .Append(op.Model.Name)
//              .Append("\").On(\"")
//              .Append(op.Model.TableName)
//              .Append("\").Columns(")
//              .Append(String.Join(", ", op.Model.Columns.Select(x => "\"" + x + "\"")))
//              .Append(");");
//        }
//    }
//}