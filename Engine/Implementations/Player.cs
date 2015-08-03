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
    public abstract class Player : IPlayer, IMove, IAct
    {
        protected IGame game;

        public string Name { get; protected set; }
        public INode Location { get; protected set; }
        public IHand Hand { get; protected set; }

        public Player(IGame game, string name, IHand hand)
        {
            this.game = game;
            Name = name;
            Hand = hand;
        }

        public void SetStartingLocation(INode startingNode)
        {
            Location = startingNode;
            if (Moved != null) Moved(this, new PlayerMovedEventArgs(null, startingNode));
        }

        public void Drive()
        {
            throw new NotImplementedException();
        }

        public void DirectFlight()
        {
            throw new NotImplementedException();
        }

        public void CharterFlight()
        {
            throw new NotImplementedException();
        }

        public void ShuttleFlight()
        {
            throw new NotImplementedException();
        }

        public void BuildResearchStation()
        {
            throw new NotImplementedException();
        }

        public virtual void TreatDisease(IDisease disease)
        {
            INodeDiseaseCounter treatTarget = game.NodeCounters.Single(i => i.Node == Location);
            if (treatTarget != null)
            {
                treatTarget.RaiseTreatment(1);
            }
        }

        public void GiveKnowledge()
        {
            throw new NotImplementedException();
        }

        public void TakeKnowledge()
        {
            throw new NotImplementedException();
        }

        public void DiscoverCure()
        {
            throw new NotImplementedException();
        }

        public override string ToString()
        {
            return Name;
        }

        public event EventHandler<PlayerMovedEventArgs> Moved;
    }
}
