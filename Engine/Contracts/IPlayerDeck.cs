using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine.Contracts
{
    public interface IPlayerDeck : IDeck
    {
        void AddEpidemics(int difficulty, Stack<IEpidemicCard> epidemicCards);
        event EventHandler<EventArgs> GameOver;
    }
}
