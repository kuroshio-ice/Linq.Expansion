using System;
using System.Collections.Generic;

namespace Linq.Expansion
{
    class MainClass
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("--- List ---");

            var hoge = new List<int>() { 1, 3, 5 };

            foreach (var (index, value) in hoge.WithIndex())
            {
                Console.WriteLine($"index: {index}, value: {value}");
            }

            foreach (var h in hoge.Chunk(2))
            {
                Console.WriteLine($"hoge: {h.JoinComma()}");
            }

            {
                var rand = new Random();
                var h = hoge.Sample(rand);
                Console.WriteLine($"h: {h}");
            }

            Console.WriteLine("--- Dictionary ---");

            var fuga = new Dictionary<int, string>()
            {
                {1, "aaa"},
                {3, "ccc"},
                {5, "eee"},
            };

#if USE_TUPLE
            foreach (var f in fuga.WithIndex())
            {
                Console.WriteLine($"index: {f.Index}, key: {f.Key}, value: {f.Value}");
            }
#endif

            foreach (var f in fuga.Chunk(2))
            {
                var f1 = f.Keys.JoinComma();
                var f2 = f.Values.JoinComma();

                Console.WriteLine($"f1: {f1}, f2: {f2}");
            }

        }
    }
}
