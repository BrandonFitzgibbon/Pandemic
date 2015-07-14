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
            IGame game = new Game(new DataAccess.Data(), new PlayerFactory(), new List<string>() { "John", "Jane" }, new OutbreakCounter(), new InfectionRateCounter(), Difficulty.Standard);
            Play(game);
        }

        public static async void Play(IGame game)
        {
            do
            {
                Actions actions = new Actions(game.CurrentPlayer);
                for (int i = 0; i < 4; i++)
                {
                    Console.Clear();
                    Console.WriteLine("Waiting for " + game.CurrentPlayer + " to act. (Actions taken: " + i + ")");
                    DisplayGameOption(game);
                    int option = int.Parse(Console.ReadLine());
                    switch (option)
                    {
                        case 1:
                            ICity city = DisplayDriveDestinations(game);
                            actions.Drive.Invoke(city);
                            break;
                        case 2:
                            IDisease disease = DisplayDiseaseTreatmentOptions(game);
                            actions.TreatDisease.Invoke(disease);
                            break;
                        case 3:
                            DisplayCurrentCity(game);
                            i--;
                            break;
                        case 4:
                            DisplayCurrentHand(game);
                            i--;
                            break;
                    }
                }
                game.DrawPhase();
                game.DrawPhase();
                game.InfectionPhase();
                game.NextPlayer();
            } while (game.IsGameOn);
        }

        public static void DisplayGameState(IGame game)
        {
            Console.WriteLine();
            Console.WriteLine("Players: ");
            foreach (IPlayer player in game.Players)
            {
                Console.WriteLine(player + " is in " + player.Location);
            }
            Console.WriteLine();
        }

        public static void DisplayGameOption(IGame game)
        {
            Console.WriteLine("1. Drive");
            Console.WriteLine("2. Treat Disease");
            Console.WriteLine("3. Display Current City");
            Console.WriteLine("4. Look at hand");
        }

        public static void DisplayCityOptions(ICity city)
        {
            Console.WriteLine(city);
            Console.WriteLine("Native Disease: " + city.Disease);
            Console.WriteLine("Counters:");
            foreach (IDiseaseCounter counter in city.Counters)
            {
                Console.WriteLine("\t" + counter.Disease + ": " + counter.Count);
            }
            Console.WriteLine("Has research station: " + city.HasResearchStation);
            Console.WriteLine("Players in city: ");
            foreach (IPlayer player in city.PlayersInCity)
            {
                Console.WriteLine("\t" + player);
            }
        }

        public static void DisplayCurrentCity(IGame game)
        {
            Console.Clear();
            DisplayCityOptions(game.CurrentPlayer.Location);
            Console.ReadKey();
        }

        public static ICity DisplayDriveDestinations(IGame game)
        {
            Console.Clear();
            Console.WriteLine(game.CurrentPlayer + " will drive to: ");
            int i = 0;
            foreach (ICity city in game.CurrentPlayer.Location.Connections)
            {
                Console.WriteLine(i + ": " + city);
                i++;
            }
            int option = int.Parse(Console.ReadLine());
            return game.CurrentPlayer.Location.Connections.ElementAt(option);
        }

        public static IDisease DisplayDiseaseTreatmentOptions(IGame game)
        {
            Console.Clear();
            int i = 0;
            foreach (IDisease disease in game.Diseases)
            {
                Console.WriteLine(i + ": " + disease);
                i++;
            }
            int option = int.Parse(Console.ReadLine());
            return game.Diseases.ElementAt(option);
        }

        public static void DisplayCurrentHand(IGame game)
        {
            Console.Clear();
            foreach (ICard card in game.CurrentPlayer.Hand.Cards)
            {
                Console.WriteLine(card);
            }
            Console.ReadKey();
        }
    }
}
