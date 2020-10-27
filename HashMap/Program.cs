using System;
using System.Diagnostics;

namespace HashMapStructure
{
    [DebuggerDisplay("{" + nameof(GetDebuggerDisplay) + "(),nq}")]
    class Program
    {
        static void Main(string[] args)
        {
            HashMap<string, string> hashMap = new HashMap<string, string>("Ben", "Just finished his hash map");
            Console.WriteLine(hashMap["Ben"]);
            hashMap["Chore 1"] = "Pick up dog poos";
            hashMap.NewMap("Mum", "Kelly");
            Console.WriteLine(hashMap["Mum"]);
            hashMap["Mum"] = "Doing the ironing";
            Console.WriteLine(hashMap["Mum"]);
            Console.WriteLine(hashMap["Chore 1"]);
            hashMap["Mum"] = "Finished the dishwasher for her";
            Console.WriteLine(hashMap["Mum"]);
        }

        private string GetDebuggerDisplay()
        {
            return ToString();
        }
    }
}
