using GraphQL.Types;

namespace Micro.GraphQL.Federation.Directives
{
    public class ProvidesDirective : DirectiveGraphType
    {
        public const string DirectiveName = "provides";
        public override bool? Introspectable => true;

        public ProvidesDirective() : base(DirectiveName, DirectiveLocation.FieldDefinition)
        {
            Arguments = new QueryArguments(new QueryArgument<NonNullGraphType<StringGraphType>>
            {
                Name = "fields",
                Description = "_FieldSet"
            });
        }
    }
}
