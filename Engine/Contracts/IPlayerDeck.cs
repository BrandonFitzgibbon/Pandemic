using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine.Contracts
{
    public interface IPlayerDeck : IDeck
    {
        event EventHandler<EventArgs> GameOver;
        void Setup(IList<IPlayer> players, int difficulty);
    }
}
