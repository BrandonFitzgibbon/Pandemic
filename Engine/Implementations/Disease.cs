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
        private int id;
        public int Id
        {
            get { return id; }
        }

        private string name;
        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        private int count;
        public int Count
        {
            get { return count; }
        }

        public Disease(int id)
        {
            this.id = id;
            count = 24;
        }

        public override string ToString()
        {
            return name;
        }

        public override int GetHashCode()
        {
            return id.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            IDisease equalsTarget = (IDisease)obj;
            if (equalsTarget != null)
                return equalsTarget.Id == this.id;
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
