using Engine.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine.Implementations
{
    public class Disease : IDisease
    {
        public string Name { get; private set; }
        public DiseaseType Type { get; private set; }
        public bool IsCured { get; internal set; }
        public bool IsEradicated { get; internal set; }

        public Disease(string name, DiseaseType type)
        {
            this.Name = name;
            this.Type = type;
        }

        public override string ToString()
        {
            return Name;
        }

        public override int GetHashCode()
        {
            return Type.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            IDisease compareDisease = (IDisease)obj;
            return compareDisease != null ? compareDisease.Type == this.Type : false;
        }
    }
}
