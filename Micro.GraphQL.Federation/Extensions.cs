using System.Collections.Generic;
using GraphQL.Builders;
using GraphQL.Types;
using GraphQL.Utilities.Federation;
using GraphQLParser.AST;
using Micro.GraphQL.Federation.Types;
using Microsoft.Extensions.DependencyInjection;

namespace Micro.GraphQL.Federation
{
    public static class Extensions
    {
        public static void EnableFederation<TEntityType>(this IServiceCollection services) where TEntityType : EntityType
        {
            services.AddTransient<AnyScalarGraphType>();
            services.AddTransient<ServiceGraphType>();
            services.AddTransient<TEntityType>();
        }
        private static FieldBuilder<TSourceType, TReturnType> BuildAstMeta<TSourceType, TReturnType>(FieldBuilder<TSourceType, TReturnType> fieldBuilder, string name, string value = null)
        {
            fieldBuilder.FieldType.BuildAstMeta(name, value);
            return fieldBuilder;
        }
        public static FieldBuilder<TSourceType, TReturnType> Requires<TSourceType, TReturnType>(this FieldBuilder<TSourceType, TReturnType> fieldBuilder, string fields) => BuildAstMeta(fieldBuilder, "requires", fields);
        public static FieldBuilder<TSourceType, TReturnType> Provides<TSourceType, TReturnType>(this FieldBuilder<TSourceType, TReturnType> fieldBuilder, string fields) => BuildAstMeta(fieldBuilder, "provides", fields);
        public static FieldBuilder<TSourceType, TReturnType> External<TSourceType, TReturnType>(this FieldBuilder<TSourceType, TReturnType> fieldBuilder) => BuildAstMeta(fieldBuilder, "external");
        public static void BuildAstMeta(this IProvideMetadata type, string name, string value = null)
        {
            var definition = (GraphQLObjectTypeDefinition)type.GetMetadata<ASTNode>("__AST_MetaField__", () => BuildGraphQLObjectTypeDefinition());
            var directive = BuildGraphQLDirective(name, value);
            AddDirective(definition, directive);
            //type.SetAstType(definition);
            type.Metadata["__AST_MetaField__"] = definition;
        }
        private static void AddDirective(GraphQLObjectTypeDefinition definition, GraphQLDirective directive) => ((List<GraphQLDirective>)definition.Directives).Add(directive);
        private static GraphQLObjectTypeDefinition BuildGraphQLObjectTypeDefinition() => new GraphQLObjectTypeDefinition
        {
            Directives = new List<GraphQLDirective>(),
            Location = new GraphQLLocation(),
            Fields = new List<GraphQLFieldDefinition>()
        };
        private static GraphQLDirective BuildGraphQLDirective(string name, string value = null, ASTNodeKind kind = ASTNodeKind.StringValue) => new GraphQLDirective
        {
            Name = new GraphQLName
            {
                Value = name,
                Location = new GraphQLLocation()
            },
            Arguments = string.IsNullOrEmpty(value) ? new List<GraphQLArgument>() : new List<GraphQLArgument>() {
                new GraphQLArgument
                {
                    Name = new GraphQLName {
                        Value = "fields",
                        Location = new GraphQLLocation()
                    },
                    Value = new GraphQLScalarValue(kind) {
                        Value = value,
                        Location = new GraphQLLocation()
                    },
                    Location = new GraphQLLocation()
                }
            },
            Location = new GraphQLLocation()
        };
    }
}
