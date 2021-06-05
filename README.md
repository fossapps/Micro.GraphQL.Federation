# What
I'm using GraphQL package which is really good. While it does support schema first federation, it doesn't support code first federation.

# How
Looking at the specs, all we need here is a few things that our graphql server does. We need to define some scalars and some queries. Schema first federation already includes a FederatedSchemaPrinter we can use that to print SDL

# Why
I've multiple projects and I'm sure other people are also trying to do federation. Even though it's not a lot of code in this project, we'll always have to copy same code into all projects untill GraphQL project include federation.

Getting started:

First install Micro.GraphQL.Federation from nuget.

Create EntityType:
```c#
public class EntityType : Micro.GraphQL.Federation.Types.EntityType
{
    public EntityType()
    {
        // register all types which uses @key directive
        Type<ApplicationType>();
        Type<UserType>();
    }
}
```
Enable federation on Startup.ConfigureServices:
```c#
services.EnableFederation<EntityType>();
```

Extend your schema:
```c#
public class AppRegistrationSchema : Schema<EntityType>
{
    public AppRegistrationSchema(IServiceProvider services, Query query) : base(services)
    {
        Query = query;
    }
}

```

extend your query:
```c#
public class Query : Query<EntityType>
{
}
```

extend your types:
```c#
public sealed class UserType : ObjectGraphType<User>
{
  public UserType() {
      ExtendByKeys("id email username");
  }
}
```
will produce
```graphql
extend type User @key(fields: "id email username") {

}
```

if it's core type and don't want to extend on gql schema
```c#
public sealed class UserType : ObjectGraphType<User>
{
  public UserType() {
      Key("id email username");
  }
}
```

will produce
```graphql
type User @key(fields: "id email username") {}
```

for more info, open a issue
