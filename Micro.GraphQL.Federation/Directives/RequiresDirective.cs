using GraphQL.Types;

namespace Micro.GraphQL.Federation.Directives
{
    public class RequiresDirective : DirectiveGraphType
    {
        public const string DirectiveName = "requires";
        public override bool? Introspectable => true;

        public RequiresDirective() : base(DirectiveName, DirectiveLocation.FieldDefinition)
        {
            Arguments = new QueryArguments(new QueryArgument<NonNullGraphType<StringGraphType>>
            {
                Name = "fields",
                Description = "_FieldSet"
            });
        }
    }
}
