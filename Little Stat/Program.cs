using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Little_Stat
{
    class Program
    {
        static void Main(string[] args)
        {
            Stats stats = new Stats();
            Console.Clear();
            Console.WriteLine("Little Stat RPG stat Engine - Press any key to start");
            Console.WriteLine("Enter STRENGTH value");
            int tmp = int.Parse(Console.ReadLine());
            stats.setStats("STRENGTH", tmp);
            Console.Clear();
            Console.Write("STR is ");
            Console.Write(stats.STRENGTH);
            Console.ReadKey();
        }
    }
}
