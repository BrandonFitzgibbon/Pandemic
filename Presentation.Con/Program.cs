using Data;
using Engine.Contracts;
using Engine.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.Con
{
    class Program
    {
        static void Main(string[] args)
        {
            Methods.WriteCities();
            Console.ReadKey();
        }

        public static class Methods
        {
            public static async void WriteCities()
            {
                foreach (INode node in Nodes.GetAll())
                {
                    Console.Write(node.City.Name + ", ");
                    Console.Write(node.City.Country);
                    Console.WriteLine(" (Population: " + node.City.Population.ToString("##,#") + ")");
                    Console.WriteLine("\tNative Disease: " + node.Disease);
                    Console.WriteLine("\tConnections: ");
                    foreach (INode con in node.Connections)
                    {
                        Console.WriteLine("\t\t" + con);
                    }
                    Console.WriteLine();
                }
            }
        }
    }
}
