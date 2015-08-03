using Engine.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine.Implementations
{
    public class EpidemicCard : Card, IEpidemicCard
    {
        public event EventHandler Increase;
        public event EventHandler Infect;
        public event EventHandler Intensify;
    }
}
