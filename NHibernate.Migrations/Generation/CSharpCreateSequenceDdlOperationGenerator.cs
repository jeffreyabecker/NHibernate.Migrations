using System;
using System.Text;
using NHibernate.DdlGen.Operations;

namespace NHibernate.Migrations.Generation
{
    public class CSharpCreateSequenceDdlOperationGenerator : DdlGeneratorBase<CreateSequenceDdlOperation>
    {
   

        protected override void AppendOperation(StringBuilder sb, int index, CreateSequenceDdlOperation op)
        {
            if (String.IsNullOrEmpty(op.Model.Parameters))
            {
                sb.AppendFormat("            builder.Create.Sequence(\"{0}\", initialValue: {1}, increment: {2});",
                                op.Model.Name, op.Model.InitialValue, op.Model.IncrementSize);

            }
            else
            {
                sb.AppendFormat("            builder.Create.Sequence(\"{0}\", parameters:\"{1}\");", op.Model.Name, op.Model.Parameters);
            }
            sb.AppendLine();
        }
    }
}