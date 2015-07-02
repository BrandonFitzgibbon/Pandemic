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

        private ICounterPool pool;
        public ICounterPool Pool
        {
            get { return pool; }
        }

        public Disease(int id)
        {
            this.id = id;
        }

        public void CreatePool(ICounterPool pool)
        {
            if (this.pool == null)
                this.pool = pool;
        }

        public override string ToString()
        {
            return name;
        }

    }
}
