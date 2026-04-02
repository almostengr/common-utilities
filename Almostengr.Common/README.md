# Almostengr Common Utilities

C# library with code that I often use with custom applications. This library
includes the Result pattern and generics for database driven applications.
These include, but are not limited to, methods and classes to make changes to entities, generic 
repository methods, and generic service classes.

By using this class library, it reduces the needs to code common functionality and features in every
application that I am creating. 

## Entity Types

### LookupEntity

This type should be used as a base class for any lookup values. Examples include, but is not limited to, 
states, gender, bank account types, etc. It has predefined properties that are often used with lookup 
types, but can be extended to fit the type of your creation.

### Entity 

Entity should be used for any other type of entity that does not meet any of the above criteria. Examples of
this includes, but is not limited to, people, car brands, and other real world objects that need to be 
modeled in your application.

## Program.cs Updates

Below is the code that you will need to add to your Program.cs file. Ideally add it after any database configuration, 
but before any dependent services, such as controllers.

```cs
builder.Services.AddCommonServices();
```

## Example Entity Implementation

```cs
public class Person : Entity
{
    public string FirstName {get;private set;}

    // other properties related to the entity
}
```

## Example Repository Layer Implementation

```cs
public sealed class PersonRepository<Person> : QueryRepository<Person>, IQueryRepository<Person>
{
    public PersonRepository(ApplicationDbContext dbContext) : base(dbContext)
    {}

    public async Task<Person> GetByFirstNameAsync(string firstName)
    {
        return await _dbSet
            .Where(p => p.FirstName == firstName)
            .SingleOrDefaultAsync();
    }

    // additional service methods here
}
```

## Example Service Layer Implementation

```cs
public sealed class QueryPersonService<Person, PersonResource> : IQueryService<Person, PersonResource>
{
    private readonly IPersonRepository<Person> _repository;

    public QueryPersonService(IPersonRepository<Person> repository)
    {
        _repository = repository;
    }

    // additional service methods here
}
```

## Example API Key Middleware

In the Program.cs file, add the below:

```cs
app.UserMiddleware<ApiKeyMiddleware>();
```

Also add one of the following (not both) to the Program.cs file: 

```cs
builder.Services.AddApiKeySettingsServices();
```

or 

```cs
builder.Services.AddApiKeyDbServices();
```

In your ApplicationDbContext.cs file, or similarly named file for your DbContext, add the following: 

```cs
public required DbSet<ApiKey> ApiKeys {get; set;}
```

## Issues and Feature Requests

Any issues, bugs, or feature requests for this library, should be submitted to its Github repository.
When submitting the request, be sure to use the appropriate template. Requests that are not submitted 
using the appropriate template, may be ignored or rejected.
