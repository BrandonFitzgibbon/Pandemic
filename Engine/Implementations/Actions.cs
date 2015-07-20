using Engine.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine.Implementations
{
    public class Actions : IActions
    {
        private Action<ICity> drive;
        public Action<ICity> Drive
        {
            get { return drive; }
        }

        private Action<IDisease> treatDisease;
        public Action<IDisease> TreatDisease
        {
            get { return treatDisease; }
        }

        public Actions(IPlayer player)
        {
            this.drive = player.Drive;
            this.treatDisease = player.TreatDisease;
        }
    }
}
