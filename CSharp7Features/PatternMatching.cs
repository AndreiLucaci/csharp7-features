using System;

namespace CSharp7Features
{
    public interface IA
    {
        string SomeString { get; set; }
    }

    public interface IB : IA
    {
        string SomeOtherString { get; set; }
    }

    public class A : IA
    {
        public string SomeString { get; set; } = nameof(SomeString);
    }

    public class B : A, IB
    {
        public string SomeOtherString { get; set; } = nameof(SomeOtherString);
    }

    class PatternMatching
    {
        public void TestIsPatternMatching()
        {
            var (obj1, obj2) = GetObjs();

            if (obj2 is A a) { Console.WriteLine(a.SomeString);}
            if (obj1 is B b) { Console.WriteLine(b.SomeOtherString); }
            else { Console.WriteLine($"{obj1.GetType()} is not of type {typeof(B)}"); }

            (IA obj1, IB obj2) GetObjs() => (new A(), new B());
        }

        public void TestSwitchPatternMatching()
        {
            IA obj = null;

            switch (obj)
            {
                case B b:
                    Console.WriteLine(b.SomeOtherString);
                    break;
                case A a:
                    Console.WriteLine(a.SomeString);
                    break;
                case null:
                    Console.WriteLine($"Object is null");
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(obj));
            }
        }
    }
}
