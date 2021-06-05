using GraphQL.Types;

namespace Micro.GraphQL.Federation.Types
{
    public abstract class EntityType  : UnionGraphType
    {
        public EntityType()
        {
            Name = "_Entity";
        }
    }
}
