using Engine.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine.Implementations
{
    public class Turn : ITurn
    {
        public Func<INode, bool> CanDrive { get; private set; }

        private int actions;
        public int Actions
        {
            get { return actions; }
            set
            {
                actions = value;
                if (value == 0)
                    if (ActionsDepleted != null) ActionsDepleted(this, EventArgs.Empty);
            }
        }

        private int cardDraws;
        public int CardDraws
        {
            get { return cardDraws; }
            set
            {
                cardDraws = value;
                if (value == 0)
                    if (CardDrawsDepleted != null) CardDrawsDepleted(this, EventArgs.Empty);
            }
        }

        internal Turn()
        {

        }

        public event EventHandler ActionsDepleted;
        public event EventHandler CardDrawsDepleted;
    }
}
