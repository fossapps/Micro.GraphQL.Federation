using System;
using System.Threading.Tasks;
using GraphQL.Types;
using GraphQL.Utilities.Federation;

namespace Micro.GraphQL.Federation
{
    public class ObjectGraphType<TSource> : global::GraphQL.Types.ObjectGraphType<TSource>
    {
        protected void ResolveReferenceAsync(Func<FederatedResolveContext, Task<TSource>> resolver)
        {
            ResolveReferenceAsync(new FuncFederatedResolver<TSource>(resolver));
        }

        protected void Key(string fields)
        {
            this.BuildAstMeta("key", fields);
        }

        protected void Extend()
        {
            this.BuildExtensionAstMeta("extends");
        }

        protected void ExtendByKeys(string fields)
        {
            Key(fields);
            Extend();
        }

        private void ResolveReferenceAsync(IFederatedResolver resolver)
        {
            // Metadata[FederatedSchemaBuilder.RESOLVER_METADATA_FIELD] = resolver;
            Metadata["__FedResolver__"] = resolver;
        }
    }
}
