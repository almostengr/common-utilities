# Almostengr Common Utilities

C# library with code that I often use with custom applications. This library
includes the Result pattern and generics for database driven applications.

This class library was created for common methods that I use in the applications that I build. 
These include, but are not limited to, methods and classes to make changes to entities, generic 
repository methods, and generic service classes.

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


