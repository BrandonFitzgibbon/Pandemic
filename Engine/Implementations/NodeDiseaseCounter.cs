using Engine.Contracts;
using Engine.CustomEventArgs;
using Engine.Implementations.Roles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine.Implementations
{
    public class NodeDiseaseCounter
    {
        public Node Node { get; private set; }
        public Disease Disease { get; private set; }
        public int Count { get; private set; }

        public NodeDiseaseCounter(Disease disease, Node node)
        {
            Disease = disease;
            Node = node;
            Count = 0;
        }

        private bool CanInfect()
        {
            foreach (Player player in Node.Players)
            {
                if (Disease.IsCured == true && player is Medic)
                    return false;
                if (player is QuarantineSpecialist)
                    return false;
            }

            foreach (Node node in Node.Connections)
            {
                foreach (Player player in node.Players)
                {
                    if (player is QuarantineSpecialist)
                        return false;
                }
            }

            return true;
        }

        public void OutbreakInfection(OutbreakEventArgs e)
        {
            if (!CanInfect())
                return;

            if (Count < 3)
            {
                Count++;
                e.AffectedCities.Add(this);
            }
            else
            {
                e.ChainCities.Add(this);
                e.OriginList.Add(this);
                if (ChainOutbreak != null) ChainOutbreak(this, e);
            }
        }

        public void Infection(int rate)
        {
            if (!CanInfect())
                return;

            for (int i = 0; i < rate; i++)
            {
                if (Count < 3)
                {
                    Count++;
                    if (Infected != null) Infected(this, new InfectionEventArgs(this));
                }
                else
                    if (Outbreak != null) Outbreak(this, new OutbreakEventArgs(this));
            }
        }

        public void Treatment(int rate)
        {
            for (int i = 0; i < rate; i++)
            {
                if (Count > 0)
                {
                    Count--;
                    if (Treated != null) Treated(this, new TreatedEventArgs(this));
                }
            }
        }

        public event EventHandler<OutbreakEventArgs> Outbreak;
        public event EventHandler<OutbreakEventArgs> ChainOutbreak;
        public event EventHandler<InfectionEventArgs> Infected;
        public event EventHandler<TreatedEventArgs> Treated;

        public override string ToString()
        {
            return Node.City + " (" + Disease + ": " + Count + ")";
        }
    }
}
