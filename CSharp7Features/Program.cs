using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharp7Features
{
    class Program
    {
        static void Main(string[] args)
        {
            var tuples = new Tuples();

            var (_, _, person) = tuples.MatchPerson("My Name");

            var name = person.Name;
        }
    }
}
