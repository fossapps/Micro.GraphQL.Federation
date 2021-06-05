using System;
using GraphQL.Utilities.Federation;
using Micro.GraphQL.Federation.Directives;
using Micro.GraphQL.Federation.Types;

namespace Micro.GraphQL.Federation
{
    public class Schema<TEntityType> : global::GraphQL.Types.Schema where TEntityType : EntityType
    {
        protected Schema(IServiceProvider services) : base(services)
        {
            RegisterType(typeof(AnyScalarGraphType));
            RegisterType(typeof(ServiceGraphType));
            RegisterType(typeof(TEntityType));
            Directives.Register(new ExternalDirective());
            Directives.Register(new RequiresDirective());
            Directives.Register(new ProvidesDirective());
            Directives.Register(new KeyDirective());
        }
    }
}
