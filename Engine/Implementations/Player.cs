using Engine.Contracts;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine.Implementations
{
    public abstract class Player : IPlayer
    {
        private string name;
        public string Name
        {
            get { return name; }
        }
        
        private INode location;
        public INode Location
        {
            get { return location; }
            set { location = value; }
        }

        private IHand hand;
        public IHand Hand
        {
            get { return hand; }
        }

        public Player(string name)
        {
            this.name = name;
            this.hand = new Hand();
        }

        public virtual void Drive(INode destination)
        {
            if (this.location.Connections.Contains(destination))
                this.location = destination;
        }

        public void DirectFlight(INode destination)
        {
            foreach (ICityCard card in Hand.Cards)
            {
                if(card.City == destination.City)
                {
                    card.Discard();
                    location = destination;
                    return;
                }
            }
        }

        public void CharterFlight(INode destination)
        {
            foreach (ICityCard card in Hand.Cards)
            {
                if (card.City == location.City)
                {
                    card.Discard();
                    location = destination;
                    return;
                }
            }
        }

        public void ShuttleFlight(INode destination)
        {
            if (location.HasResearchStation && destination.HasResearchStation)
                location = destination;
        }

        public void BuildResearchStation()
        {
            if (!location.HasResearchStation)
            {
                foreach (ICityCard card in Hand.Cards)
                {
                    if (card.City == location.City)
                    {
                        card.Discard();
                        location.BuildReasearchStation();
                        return;
                    }
                }
            }
        }

        public void TreatDisease()
        {
            
        }

        public void TakeKnowledge(IPlayer giver)
        {
            
        }

        public void GiveKnowledge(IPlayer taker, ICard card)
        {
            
        }

        public void DiscoverCure(IList<ICard> cards)
        {
            
        }
    }
}
