using Engine.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine.Implementations
{
    public class InfectionCard : Card, IInfectionCard
    {
        private INodeDiseaseCounter nodeDiseaseCounter;

        public string Name
        {
            get { return nodeDiseaseCounter.Node.City.Name; }
        }

        public IDisease Disease
        {
            get { return nodeDiseaseCounter.Node.Disease; }
        }

        public InfectionCard(INodeDiseaseCounter nodeDiseaseCounter)
        {
            this.nodeDiseaseCounter = nodeDiseaseCounter;
        }

        public void Infect(int rate)
        {
            nodeDiseaseCounter.RaiseInfection(rate);
            this.Discard();
        }

        public override string ToString()
        {
            return Name;
        }

        public override int GetHashCode()
        {
            return nodeDiseaseCounter.Node.City.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            IInfectionCard compareCard = (IInfectionCard)obj;
            return compareCard != null ? compareCard.Name == this.Name : false;
        }
    }
}
