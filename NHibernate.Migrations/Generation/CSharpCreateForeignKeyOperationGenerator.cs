//using System;
//using System.Text;
//using NHibernate.DdlGen.Model;
//using NHibernate.DdlGen.Operations;

//namespace NHibernate.Migrations.Generation
//{
//    public class CSharpCreateForeignKeyOperationGenerator : DdlGeneratorBase<CreateForeignKeyOperation>
//    {
//        protected override void AppendOperation(StringBuilder sb, int index, CreateForeignKeyOperation op)
//        {
//            sb.AppendFormat("            builder.Alter.Table(\"{0}\")", op.Model.DependentTable).AppendLine();
//            if (!String.IsNullOrEmpty(op.Model.Name))
//            {
//                sb.AppendFormat("                .Named(\"{0}\")", op.Model.Name).AppendLine();
//            }
//            sb.AppendFormat("                .References(\"{0}\"", op.Model.ReferencedTable);
//            if (!op.Model.IsReferenceToPrimaryKey)
//            {
//                foreach (var c in op.Model.ForeignKeyColumns)
//                {
//                    sb.AppendFormat(", \"{0}\"", c);
//                }
//            }
//            sb.AppendLine(");");
//        }
//    }
//}