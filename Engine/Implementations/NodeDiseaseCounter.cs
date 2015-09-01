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

        public void OutbreakInfection(OutbreakEventArgs e)
        {
            foreach (Player player in Node.Players)
            {
                if (Disease.IsCured == true && player is Medic)
                    return;
                if (player is QuarantineSpecialist)
                    return;
            }

            foreach (Node node in Node.Connections)
            {
                foreach (Player player in node.Players)
                {
                    if (player is QuarantineSpecialist)
                        return;
                }
            }

            if (Count < 3)
            {
                Count++;
                if (ChainInfected != null) ChainInfected(this, new InfectionEventArgs(this, 1));
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
            foreach (Player player in Node.Players)
            {
                if ((Disease.IsCured == true && player is Medic) || player is QuarantineSpecialist)
                {
                    if (Prevented != null) Prevented(this, new PreventionEventArgs(player, Disease, Node));
                    return;
                }
            }

            foreach (Node node in Node.Connections)
            {
                foreach (Player player in node.Players)
                {
                    if (player is QuarantineSpecialist)
                    {
                        if (Prevented != null) Prevented(this, new PreventionEventArgs(player, Disease, Node));
                        return;
                    }
                }
            }

            int j = 0;
            for (int i = 0; i < rate; i++)
            {
                if (Count < 3)
                {
                    Count++;
                    j++;
                }
                else
                {
                    if (j > 0)
                        if (Infected != null) Infected(this, new InfectionEventArgs(this, j));
                    if (Outbreak != null) Outbreak(this, new OutbreakEventArgs(this));
                }
            }

            if (j > 0)
                if (Infected != null) Infected(this, new InfectionEventArgs(this, j));
        }

        public void Treatment(int rate, Player treater)
        {
            int j = 0;
            for (int i = 0; i < rate; i++)
            {
                if (Count > 0)
                {
                    Count--;
                    j++;
                }
            }
            if(j > 0)
                if (Treated != null) Treated(this, new TreatedEventArgs(this, treater, j));
        }

        public event EventHandler<OutbreakEventArgs> Outbreak;
        public event EventHandler<OutbreakEventArgs> ChainOutbreak;
        public event EventHandler<InfectionEventArgs> Infected;
        public event EventHandler<InfectionEventArgs> ChainInfected;
        public event EventHandler<TreatedEventArgs> Treated;
        public event EventHandler<PreventionEventArgs> Prevented;

        public override string ToString()
        {
            return Node.City + " (" + Disease + ": " + Count + ")";
        }
    }
}
