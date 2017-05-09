using System;

namespace hw1
{
    class Program
    {
        static void Main(string[] args)
        {
            var a22 = new Matrix2x2(1,2,3,4);
            var b22 = new Matrix2x2(2,3,4,5);
            Console.WriteLine(a22 + b22);
            Console.WriteLine(a22 * b22);
            Console.WriteLine(a22 - b22);
            

        }
    }
}
