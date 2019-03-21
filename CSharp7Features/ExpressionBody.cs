using System;
using System.Collections.Concurrent;

namespace CSharp7Features
{
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
}
