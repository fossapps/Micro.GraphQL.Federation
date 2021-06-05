using GraphQL.Types;

namespace Micro.GraphQL.Federation.Directives
{
    public class ExternalDirective : DirectiveGraphType
    {

        public const string DirectiveName = "external";
        public override bool? Introspectable => true;

        public ExternalDirective() : base(DirectiveName, DirectiveLocation.FieldDefinition)
        {
        }
    }
}
