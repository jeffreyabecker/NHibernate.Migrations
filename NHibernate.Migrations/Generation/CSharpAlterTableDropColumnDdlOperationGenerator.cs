//using System.Text;
//using NHibernate.DdlGen.Operations;

//namespace NHibernate.Migrations.Generation
//{
//    public class CSharpAlterTableDropColumnDdlOperationGenerator : DdlGeneratorBase<AlterTableDropColumnDdlOperation>
//    {
//        protected override void AppendOperation(StringBuilder sb, int index, AlterTableDropColumnDdlOperation op)
//        {
//            sb.Append("builder.Alter.Table(\"")
//              .Append(op.Model.Table)
//              .Append("\").DropColumn(\"")
//              .Append(op.Model.Column).Append("\");");
//            sb.AppendLine();
//        }
//    }
//}