using Engine.Contracts;
using Engine.Contracts.Roles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine.Implementations.Roles
{
    public class QuarantineSpecialist : Player, IQuarantineSpecialist
    {
        public QuarantineSpecialist(string name, IHand hand) : base(name, hand) { }
    }
}
