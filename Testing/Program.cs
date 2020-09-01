using System;

namespace Testing
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");


            if(args != null && args.Length != 0)
            {
                Console.WriteLine(args[0]);
            }


            Esine laatikko = new Esine("Pentti", 0);

            PrintName(laatikko);

            Console.Read();
        }

        static void PrintName(INameable nameable) { Console.WriteLine(nameable.Name); }
    }



    class Esine : INameable , IIndexable
    {
        public string Name { get; set;}
        
        public int Index { get; set; }

        public Esine(string name, int index)
        {
            Name = name;
            Index = index;
        }
    }
}
