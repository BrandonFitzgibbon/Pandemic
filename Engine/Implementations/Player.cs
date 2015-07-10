using Engine.Contracts;
using Engine.CustomEventArgs;
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
        
        protected ICity location;
        public ICity Location
        {
            get { return location; }
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

        public virtual void Drive(ICity destinationCity)
        {
            ICity departedCity = this.location;
            this.location = destinationCity;
            if (Moved != null) Moved(this, new PlayerMovedEventArgs(departedCity, destinationCity));
        }

        public virtual void DirectFlight(ICity destinationCity, ICityCard destinationCard)
        {
            ICity departedCity = this.location;
            this.location = destinationCity;
            destinationCard.Discard();
            if (Moved != null) Moved(this, new PlayerMovedEventArgs(departedCity, destinationCity));
        }

        public virtual void CharterFlight(ICity destinationCity, ICityCard locationCard)
        {
            ICity departedCity = this.location;
            this.location = destinationCity;
            locationCard.Discard();
            if (Moved != null) Moved(this, new PlayerMovedEventArgs(departedCity, destinationCity));
        }

        public virtual void ShuttleFlight(ICity destinationCity)
        {
            ICity departedCity = this.location;
            this.location = destinationCity;
            if (Moved != null) Moved(this, new PlayerMovedEventArgs(departedCity, destinationCity));
        }

        public virtual void BuildResearchStation(ICityCard locationCard, ICity dismantledStation = null)
        {
            locationCard.Discard();
            if (dismantledStation != null)
            {
                if (ResearchStationChanged != null)
                    ResearchStationChanged(this, new ResearchStationChangedEventArgs(this.location, dismantledStation));
            }
            else
                if (ResearchStationChanged != null)
                    ResearchStationChanged(this, new ResearchStationChangedEventArgs(this.location, null));
        }

        public virtual void TreatDisease(IDisease disease)
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

        public event EventHandler<PlayerMovedEventArgs> Moved;
        public event EventHandler<ResearchStationChangedEventArgs> ResearchStationChanged;
    }
}
