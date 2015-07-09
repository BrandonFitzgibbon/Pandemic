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
        
        private ICity location;
        public ICity Location
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

        public virtual void Drive(ICity destination)
        {
            if (this.location.Connections.Contains(destination))
                this.location = destination;
        }

        public virtual void DirectFlight(ICity destination, ICityCard destinationCard)
        {
            if (destinationCard.City == destination)
            {
                location = destination;
                destinationCard.Discard();
            }
        }

        public virtual void CharterFlight(ICity destination, ICityCard locationCard)
        {
            if (locationCard.City == location)
            {
                location = destination;
                locationCard.Discard();
            }
        }

        public void ShuttleFlight(ICity destination)
        {
            if (location.HasResearchStation && destination.HasResearchStation)
                location = destination;
        }

        public void BuildResearchStation(ICityCard locationCard)
        {
            if (!location.HasResearchStation)
            {
                if(locationCard.City == location)
                {
                    
                }
            }
        }

        public void TreatDisease(IDisease disease)
        {
            ICounter treatTarget = location.Counters.SingleOrDefault(i => i.Disease == disease);
            if (treatTarget != null)
            {
                
            }
        }

        public void TakeKnowledge(IPlayer giver)
        {
            
        }

        public void GiveKnowledge(IPlayer taker)
        {
            
        }

        public void DiscoverCure(IList<ICard> cards)
        {
            
        }


        public void BuildResearchStation()
        {
            throw new NotImplementedException();
        }
    }
}
