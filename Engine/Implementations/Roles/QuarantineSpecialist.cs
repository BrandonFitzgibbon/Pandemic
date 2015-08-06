using Engine.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine.Implementations.Roles
{
    internal class QuarantineSpecialist : Player
    {
        public QuarantineSpecialist(string name) : base(name) { Role = "Quarantine Specialist"; }
    }
}
