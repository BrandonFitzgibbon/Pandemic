using Engine.Contracts;
using Engine.Factories;
using Engine.Implementations;
using Engine.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Presentation.Con
{
    class Program
    {
        static void Main(string[] args)
        {
            IGame game = new Game(new DataAccess.Data(), new PlayerFactory(), new List<string>(), Difficulty.Standard);
            Console.Write(game.NumberOfResearchStations);
            Console.ReadKey();
        }
    }
}
