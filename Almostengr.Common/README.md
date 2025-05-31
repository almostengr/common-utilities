# Almostengr Common Utilities

C# library with code that I often use with custom applications. This library
includes the Result pattern and generics for database driven applications.

This class library was created for common methods that I use in the applications that I build. 
These include, but are not limited to, methods and classes to make changes to entities, generic 
repository methods, and generic service classes.

## Entity Types

### BaseLookupEntity

This type should be used as a base class for any lookup values. Examples include, but is not limited to, 
states, gender, bank account types, etc. It has predefined properties that are often used with lookup 
types, but can be extended to fit the type of your creation.

### BaseEntity 

BaseEntity should be used for any other type of entity that does not meet any of the above criteria. Examples of
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
public class Person : BaseEntity
{
    public string FirstName {get;private set;}

    // other properties related to the entity
}
```

## Example Repository Implementation

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

## Example Service Implementation

```cs
public sealed class QueryPersonService<Person, PersonResource> : IQueryService<Person, PersonResource>
{
    private readonly IPersonRepository<Person> _repository;

    public QueryPersonService(
        IPersonRepository<Person> repository
    )
    {
        _repository = repository;
    }

    // additional service methods here
}
```


