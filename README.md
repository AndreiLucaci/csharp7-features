# csharp7-features

# Here are some information about the new features that are added to C# 7.0 vs C# 6.0

<a id="tb"></a>
# Table of contents:
1. [Tuple types and tuple literals](#tuple-type)
2. [Variable deconstruction](#var-dec)
3. [Local functions](#lcl-func)

<a id="tuple-type"></a>
## 1. Tuple types and tuple literals | [Top](#tb) | [Next section](#var-dec)
C# 7.0 adds a new way of returning multiple values when returning from a method. This is done by having the return value of the method a `tuple type` and the actual variable that is returned a `tuple literal`. Here's a small example:

```csharp

public interface IPerson
{
    string Name { get; set; }
}

public class Person : IPerson
{
    public string Name { get; set; }
}

public class Tuples
{
    public (int, bool, IPerson) MatchPerson(string name) // the type of the return value is a tuple type
    {
        // do some matching here

        return (1, true, new Person { Name = name }); // the type that is returned here is a tuple literal
    }
}

```

With this method we now can return multiple values using the `tuple type` and `tuple literal`.

The return value can be used just like tuples, the items are written as `Item1`, `Item2` etc.

![](https://i.imgur.com/gdRBxKa.png)

```csharp
var result = tuples.MatchPerson("My Name");

var name = result.Item3.Name;
```

We can also give `aliases` to the items inside of the `tuple literal`.

```csharp

public (int numberOfFriends, bool isActive, IPerson matchedPerson) MatchPerson(string name)
{
    // do some matching here

    return (1, true, new Person { Name = name });
}

```

which will generate a a result something like

```csharp
var result = tuples.MatchPerson("My Name");

var name = result.matchedPerson.Name;
```

![](https://i.imgur.com/bABoYdm.gif)

<a id="var-dec"></a>
## 2. Variable deconstruction | [Top](#tb) | [Next section](#lcl-func) | [Prev section](#tuple-type)
Using the previous information, C# 7.0 now introduces variable deconstruction. A deconstruction declaration splits a tuple into its underlying parts, and then assigns those values to variables.

With the previous version of the example, now we can achieve this:

```csharp
(int number, bool isActive, IPerson person) = tuples.MatchPerson("My Name");

var name = person.Name;
```

As we can see, there are three new variables that are created (using the same syntax of the `tuple type`). 

We can also make use of the generic `var` keyword, and have the code as such:

```csharp
(var number, var isActive, var person) = tuples.MatchPerson("My Name");

var name = person.Name;
```
or even combine the statement and extract the `var` keyword in front of the whole asignment as such:
```csharp
var (number, isActive, person) = tuples.MatchPerson("My Name");

var name = person.Name;
```

If we want to 'skip' variables from the tuple chain, we can use the "`_`" character.
```csharp
var (_, _, person) = tuples.MatchPerson("My Name"); // I'm interested only in person here

var name = person.Name;
```

<a id="lcl-func"></a>
## 3. Local functions | [Top](#tb) | [Next section]() | [Prev section](#var-dec)
C# 7.0 introduces the idea of local functions, "helper functions" that make sense inside methods, and nowhere else.
