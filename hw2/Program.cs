using System;

namespace hw2
{
    class Program
    {
        static void Main(string[] args)
        {   

            SortedSet<int> set = new SortedSet<int>();
            Console.WriteLine(set);
            set.Add(1);
            set.Add(2);
            Console.WriteLine(set);
            set.Remove(2);
            Console.WriteLine(set);
        }
    }
}
