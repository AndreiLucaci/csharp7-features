using System;
using System.Linq;

namespace CSharp7Features
{
    class Program
    {
        static void Main(string[] args)
        {
            //var tuples = new Tuples();

            //var (_, _, person) = tuples.MatchPerson("My Name");

            //var name = person.Name;
            
            //var numbers = new[] {1, 2, 3, 4, 5, 6, 7, 8, 9};

            //numbers.FindAll(x => x < 4).ForEach(DisplayStr);

            //void DisplayStr((int nr, int index) input) => Console.WriteLine($"Number {input.nr} found at index {input.index}");

            var patternMatching = new PatternMatching();
            
            patternMatching.TestSwitchPatternMatching();
        }
    }
}
