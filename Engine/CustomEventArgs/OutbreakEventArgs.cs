using Engine.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine.CustomEventArgs
{
    public class OutbreakEventArgs
    {
        private ICity outbreakOrigin;
        public ICity OutbreakOrigin
        {
            get { return OutbreakOrigin; }
        }

        private IDisease outbreakDisease;
        public IDisease OutbreakDisease
        {
            get { return outbreakDisease; }
        }

        public OutbreakEventArgs(ICity outbreakOrigin, IDisease outbreakDisease)
        {
            this.outbreakOrigin = outbreakOrigin;
            this.outbreakDisease = outbreakDisease;
        }
    }
}
