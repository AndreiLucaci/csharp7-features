# csharp7-features

# Here are some information about the new features that are added to C# 7.0 vs C# 6.0

<a id="tb"></a>
# Table of contents:
1. [`tuple types` and `tuple literals`](#tuple-type)
2. [Variable deconstruction](#var-dec)
3. [Local functions](#lcl-func)
4. [Pattern matching](#pat-match)
5. [`ref` as a return value](#ref)
6. [Expression body modifiers](#exp)
7. [others](#oth)

[Sources](#source)

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
## 3. Local functions | [Top](#tb) | [Next section](#pat-match) | [Prev section](#var-dec)
C# 7.0 introduces the idea of local functions, "helper functions" that make sense inside methods, and nowhere else.

Here's a small example:

For this: 
```csharp
public static IEnumerable<(T, int index)> FindAll<T>(this IEnumerable<T> inputSource, Func<T, bool> what)
{
    return Find();

    IEnumerable<(T, int index)> Find() // this is a local function
    {
        var counter = -1;
        foreach (var item in inputSource)
        {
            counter++;
            if (what(item)) yield return (item, counter);
        }
    }
}

public static void ForEach<T>(this IEnumerable<T> input, Action<T> action)
{
    foreach (var item in input)
    {
        action(item);
    }
}
```

we could have something like this:

```csharp
var numbers = new[] {1, 2, 3, 4, 5, 6, 7, 8, 9};

numbers.FindAll(x => x < 4).ForEach(DisplayStr);

void DisplayStr((int nr, int index) input) => Console.WriteLine($"Number {input.nr} found at index {input.index}"); // this is also a local function, but without body
```

<a id="pat-match"></a>
## 4. Pattern matching | [Top](#tb) | [Next section](#ref) | [Prev section](#lcl-func)
In C# 7.0 we now have the idea of pattern patching, which extends over the existing pattern matching that C# offers.

### 1. `is`-expressions

Here's a small example of the pattern matching

Consider we have this structure
```csharp
public interface IA { string SomeString { get; set; } }

public interface IB : IA { string SomeOtherString { get; set; } }

public class A : IA { public string SomeString { get; set; } = nameof(SomeString); }

public class B : A, IB { public string SomeOtherString { get; set; } = nameof(SomeOtherString); }
```

The pattern matching automatically creates and assigns the value to the variable in one go:

```csharp 
var (obj1, obj2) = GetObjs();

if (obj2 is A a) { Console.WriteLine(a.SomeString);} // this will hit
if (obj1 is B b) { Console.WriteLine(b.SomeOtherString); } // this is false
else { Console.WriteLine($"{obj1.GetType()} is not of type {typeof(B)}"); } // if we are to use the "b" variable in the else clause we would get a compilation error as such: 
                                                                            // Error	CS0165	Use of unassigned local variable 'b'	


(IA obj1, IB obj2) GetObjs() => (new A(), new B());
```

and the output
```console
SomeString
CSharp7Features.A is not of type CSharp7Features.B
```

Note: there are two types of pattern matching: `constant pattern` and `type pattern`. 
- `constant pattern` matches to a constant such as `obj is null`.
- `type pattern` matches to a type such as `obj is IA a`

### 2. `swich` statements
The guys from Microsoft also extended the `switch` statement so that we can switch also on any type, and introduced pattern matching in the `case` clauses. Using the same structure as above, we can have.

```csharp
IA obj = new B();

switch (obj)
{
    case null:
        Console.WriteLine($"Object is null");
        break;
    case B b:
        Console.WriteLine(b.SomeOtherString);
        break;
    case A a:
        Console.WriteLine(a.SomeString);
        break;
    default:
        throw new ArgumentOutOfRangeException(nameof(obj));
}
```

Also, it's worth noticing that the `order` of the case clauses now matters, since in the above example, switching the `case B b:` with `case A a:`, for `IA obj = new B()` would always trigger the `case A a:`, and never get to `case B b:`. Compiling with such a case would then yield this:  `Error	CS8120	The switch case has already been handled by a previous case.`.

The pattern variables in the `case`s clauses are scope only to the switch section.

<a id="ref"></a>
## 5. `ref` as a return value | [Top](#tb) | [Next section](#exp) | [Prev section](#pat-match)
With C# 7.0 we are now able to return values having the `ref` modifier, and store them in local variables.

Here's an example:
Having:
```csharp
private ref int GetRefItem(int[] nrs, int index)
{
    if (index < 0 || index > nrs.Length) throw new ArgumentOutOfRangeException();

    return ref nrs[index];
}
```

we now can do this:

```csharp
void PrintItems (IEnumerable<int> nrs) => Console.WriteLine(string.Join(", ", nrs));

var items = new[] {1, 2, 3, 4};

PrintItems(items); // 1, 2, 3, 4

ref var item = ref GetRefItem(items, 1);
item = int.MaxValue;

PrintItems(items); // 1, 2147483647. 3, 4
```

and the output:
```
1, 2, 3, 4
1, 2147483647, 3, 4
```

<a id="exp"></a>
## 6. Expression body modifiers | [Top](#tb) | [Next section](#oth) | [Prev section](#ref)
C# 7.0 extends the default expresion bodied methods to `construtor`s, `finalizer`s and `accessor`s. Here's an example:

```csharp
public class ExpressionBody
{
    private static readonly ConcurrentDictionary<int, string> Names = new ConcurrentDictionary<int, string>();
    private readonly int _id = new Random().Next(int.MaxValue);

    // ctor
    public ExpressionBody(string name) => Names.TryAdd(_id, name); 

    // dctor
    ~ExpressionBody() => Names.TryRemove(_id, out _);              

    // accesors
    public string Name
    {
        get => Names[_id];                                
        set => Names[_id] = value;                        
    }
}
```
<a id="oth"></a>
## + other features, breafly mentioned: | [Top](#tb) | [Next section](#source) | [Prev section](#exp)
- Throw expressions
- Generalized async return types
- Literal improvements
- Out variables

<a id="source"></a>
# Sources and good information links with more in depth | <img src="https://i.kym-cdn.com/photos/images/original/001/376/663/aa6.jpg" width="100" /> 


You can find more information about this on these links:
- [dev blog post](https://devblogs.microsoft.com/dotnet/new-features-in-c-7-0/)
- [official github page of C# lang](https://github.com/dotnet/csharplang)
- [in depth explanation](https://www.c-sharpcorner.com/article/c-sharp-7-0-and-c-sharp-7-1-new-features-part-two/)
- [c# 7.x](https://www.dotnetcurry.com/csharp/1437/csharp-7-1-7-2-7-3-new-features)