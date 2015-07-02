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
                IDataAccess<ICity> cities = new Cities();
                foreach (ICity city in cities.GetAll())
                {
                    await Task.Delay(100);
                    Console.Write(city.Name + ", ");
                    Console.Write(city.Country);
                    Console.WriteLine(" (Population: " + city.Population.ToString("##,#") + ")");
                }
            }
        }
    }
}
