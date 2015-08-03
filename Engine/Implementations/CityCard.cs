using Engine.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine.Implementations
{
    public class CityCard : Card, ICityCard
    {
        public INode Node { get; private set; }

        public CityCard(INode node)
        {
            Node = node;
        }

        public override string ToString()
        {
            return Node.ToString();
        }

        public override int GetHashCode()
        {
            return Node.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            ICityCard compare = obj as ICityCard;
            return compare != null ? compare.Node == Node : false;
        }
    }
}
