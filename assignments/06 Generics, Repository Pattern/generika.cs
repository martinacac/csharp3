using System;
using System.Linq;
using System.Collections.Generic;

class Generika
{
    //1. Implementujte generickou metodu Max<T>, která porovná dva parametry typu IComparable<T> a vrátí jejich maximum.
    //Řešení přes CompareTo()?
    static T Max<T>(T item1, T item2)
    where T : IComparable<T>
    {
        //return items.Max();
        return item1.CompareTo(item2) > 0 ? item1 : item2;
    }
    //2. Implementujte generickou metodu Max<T>, která přijme IEnumerable<IComparable<T>> a vrátí jejich maximum.
    //Řešení přes .Max()?
    static T Max<T>(List<T> items)
    where T : IComparable<T>
    {
        return items.Max();
    }
    //3. Implementujte generickou metodu IsDistinct<T>, která přijme IEnumerable<IComparable<T>> a vrátí true, pokud neobsahuje duplikáty
    //Řešení přes .Distinct()?
    static bool IsDistinct<T>(List<T> items) where T : IComparable<T>
    {
        return items.Distinct().Count() == items.Count();
    }

}

// class Program
// {
//     static void Main()
//     {
//         var numbers = new List<int>
//         {
//             1,
//             4,
//             7,
//             10
//         };
//         var words = new List<string>
//         {
//             "Ahoj",
//             "Czechitas",
//             "Programovani"
//         };
//         Console.WriteLine("Čísla větší než 5:");
//         PrintGreaterThanInt(numbers, 5);

//         Console.WriteLine("\nSlova 'větší' než 'Czechitas':");
//         PrintGreaterThanString(words, "Czechitas");

//         Console.WriteLine("\n(Přes generiku) Čísla větší než 5:");
//         PrintGreaterThan(numbers, 5);

//         Console.WriteLine("\n(Přes generiku) Slova 'větší' než 'Czechitas':");
//         PrintGreaterThan(words, "Czechitas");
//     }

//     static void PrintGreaterThanInt(List<int> items, int minValue)
//     {
//         foreach (var item in items)
//         {
//             if (item > minValue) //NEBO if(item.CompareTo(minValue)>0)
//                 Console.WriteLine(item);
//         }
//     }

//     static void PrintGreaterThanString(List<string> items, string minValue)
//     {
//         foreach (var item in items)
//         {
//             if (string.Compare(item, minValue) > 0)
//                 Console.WriteLine(item);
//         }
//     }

//     static void PrintGreaterThan<T>(List<T> items, T minValue)
//         where T : IComparable<T>
//     {
//         foreach (var item in items)
//         {
//             if (item.CompareTo(minValue) > 0)
//                 Console.WriteLine(item);
//         }
//     }

//     static T Max<T>(T a, T b) where T : IComparable<T>
//     {
//         return a.CompareTo(b) > 0 ? a : b;
//     }

//     static T Max<T>(List<T> items) where T : IComparable<T>
//     {
//         return items.Max();
//     }

//     static bool IsDistinct<T>(List<T> items) where T : IComparable<T>
//     {
//         return items.Distinct().Count() == items.Count();
//     }
// }
