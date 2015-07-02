using Engine.Contracts;
using Engine.Implementations;
using Engine.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public static class Cities
    {
        public static IList<ICity> GetAll()
        {
            return new List<ICity>()
            {
                //Yellow
                new City("Bogota", "Colombia", 8702000),
                new City("Buenos Aires", "Argentina", 1363900),
                new City("Johannesburg", "South Africa", 3888000),
                new City("Khartoum", "Sudan", 4887000),
                new City("Kinshasa", "Democratic Republic of Congo", 9046000),
                new City("Lagos", "Nigeria", 11547000),
                new City("Lima", "Peru", 9121000),
                new City("Mexico City", "Mexico", 19463000),
                new City("Miami", "United States", 5582000),
                new City("Santiago", "Chile", 6015000),
                new City("Sao Paulo", "Brazil", 20186000),

                //Red
                new City("Bangkok", "Thailand", 7151000),
                new City("Beijing", "People's Republic of China", 17311000),
                new City("Ho Chi Minh City", "Vietnam", 8314000),
                new City("Hong Kong", "People's Republic of China", 7106000),
                new City("Jakarta", "Indonesia", 23063000),
                new City("Manila", "Philippines", 20767000),
                new City("Osaka", "Japan", 2871000),
                new City("Seoul", "South Korea", 22547000),
                new City("Shanghai", "People's Republic of China", 13482000),
                new City("Sydney", "Australia", 3785000),
                new City("Taipei", "Taiwan", 8338000),
                new City("Tokyo", "Japan", 13189000),

                //Blue
                new City("Atlanta", "United States", 4715000),
                new City("Chicago", "United States", 9121000),
                new City("Essen", "Germany", 575000),
                new City("London", "United Kingdom", 8586000),
                new City("Madrid", "Spain", 5427000),
                new City("Milan", "Italy", 5232000),
                new City("Montreal", "Canada", 3429000),
                new City("New York", "United States", 20464000),
                new City("Paris", "France", 10755000),
                new City("San Francisco", "United States", 5864000),
                new City("St. Petersburg", "Russia", 4879000),
                new City("Washington", "United States", 4679000),

                //Black
                new City("Algiers", "Algeria", 2946000),
                new City("Baghdad", "Iraq", 6204000),
                new City("Cairo", "Egypt", 14718000),
                new City("Chennai", "India", 8865000),
                new City("Delhi", "India", 22242000),
                new City("Istanbul", "Turkey", 13576000),
                new City("Karachi", "Pakistan", 20711000),
                new City("Kolkata", "India", 14374000),
                new City("Moscow", "Russia", 15512000),
                new City("Mumbai", "India", 16910000),
                new City("Riyadh", "Saudi Arabia", 5037000),
                new City("Tehran", "Iran", 7419000)
            };
        }
    }
}
