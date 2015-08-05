using Engine.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine.Implementations
{
    public class Dispatcher : Player
    {
        public Dispatcher(string name, IHand hand) : base(name, hand) { }
    }
}
