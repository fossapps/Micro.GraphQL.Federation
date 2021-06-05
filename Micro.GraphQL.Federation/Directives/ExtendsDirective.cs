using GraphQL.Types;

namespace Micro.GraphQL.Federation.Directives
{
    public class ExtendsDirective : DirectiveGraphType
    {
        public const string DirectiveName = "extends";
        public override bool? Introspectable => true;

        public ExtendsDirective() : base(DirectiveName, DirectiveLocation.Object, DirectiveLocation.Interface)
        {
        }
    }
}
