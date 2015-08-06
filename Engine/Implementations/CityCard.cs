using Engine.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine.Implementations
{
    public class CityCard : Card
    {
        public Node Node { get; internal set; }

        public CityCard(Node node)
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
            CityCard compare = obj as CityCard;
            return compare != null ? compare.Node == Node : false;
        }
    }
}
