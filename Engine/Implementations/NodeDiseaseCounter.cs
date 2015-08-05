using Engine.Contracts;
using Engine.Contracts.Roles;
using Engine.CustomEventArgs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine.Implementations
{
    public class NodeDiseaseCounter : INodeDiseaseCounter
    {
        public INode Node { get; private set; }
        public IDisease Disease { get; private set; }
        public int Count { get; private set; }

        public NodeDiseaseCounter(IDisease disease, INode node)
        {
            Disease = disease;
            Node = node;
            Count = 0;
        }

        private bool CanInfect()
        {
            foreach (IPlayer player in Node.Players)
            {
                if (Disease.IsCured == true && player is IMedic)
                    return false;
                if (player is IQuarantineSpecialist)
                    return false;
            }

            foreach (INode node in Node.Connections)
            {
                foreach (IPlayer player in node.Players)
                {
                    if (player is IQuarantineSpecialist)
                        return false;
                }
            }

            return true;
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
                    if (Infected != null) Infected(this, EventArgs.Empty);
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
                    if (Treated != null) Treated(this, EventArgs.Empty);
                }
            }
        }

        public event EventHandler<OutbreakEventArgs> Outbreak;
        public event EventHandler Infected;
        public event EventHandler Treated;

        public override string ToString()
        {
            return Node.City + " {" + Disease + ": " + Count + "}";
        }
    }
}
