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
        private string name;
        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        private DiseaseType type;
        public DiseaseType Type
        {
            get { return type; }
        }

        private int count;
        public int Count
        {
            get { return count; }
        }

        private bool isCured;
        public bool IsCured
        {
            get { return isCured; }
        }

        private bool isEradicated;
        public bool IsEradicated
        {
            get { return isEradicated; }
        }

        public Disease(string name, DiseaseType type)
        {
            this.name = name;
            this.type = type;
            count = 24;
        }

        public override string ToString()
        {
            return name;
        }

        public override int GetHashCode()
        {
            return name.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            IDisease equalsTarget = (IDisease)obj;
            if (equalsTarget != null)
                return equalsTarget.Type == this.Type;
            return false;
        }

        public void Decrease()
        {
            if (count > 0)
                count--;
            else
                if (GameOver != null) GameOver(this, EventArgs.Empty);
        }

        public void Increase()
        {
            if (count < 24)
                count++;
        }

        public event EventHandler GameOver;
    }
}
