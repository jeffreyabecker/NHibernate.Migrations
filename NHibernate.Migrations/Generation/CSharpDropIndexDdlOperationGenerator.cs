using System.Text;
using NHibernate.DdlGen.Operations;

namespace NHibernate.Migrations.Generation
{
    public class CSharpDropIndexDdlOperationGenerator : DdlGeneratorBase<DropIndexDdlOperation>
    {


        protected override void AppendOperation(StringBuilder sb, int index, DropIndexDdlOperation op)
        {
            sb.AppendFormat("            builder.Drop.Index(\"{0}\",\"{1}\");", op.Model.Name, op.Model.TableName);
            sb.AppendLine();
        }
    }
}