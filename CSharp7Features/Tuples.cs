namespace CSharp7Features
{
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
        //public (int, bool, IPerson) MatchPerson(string name) // the type of the return value is a tuple type
        //{
        //    // do some matching here

        //    return (1, true, new Person { Name = name }); // the type that is returned here is a tuple literal
        //}

        public (int numberOfFriends, bool isActive, IPerson matchedPerson) MatchPerson(string name)
        {
            // do some matching here

            return (1, true, new Person { Name = name });
        }

        public (string first, string second) AllString()
        {
            return ("a", "b");
        }
    }
}
