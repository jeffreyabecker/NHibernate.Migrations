using System.Text;
using NHibernate.DdlGen.Operations;

namespace NHibernate.Migrations.Generation
{
    public class CSharpDropSequenceDdlOperationGenerator : DdlGeneratorBase<DropSequenceDdlOperation>
    {

        protected override void AppendOperation(StringBuilder sb, int index, DropSequenceDdlOperation op)
        {
            sb.AppendFormat("            builder.Drop.Sequence(\"{0}\");", op.Name);
            sb.AppendLine();
        }
    }
}