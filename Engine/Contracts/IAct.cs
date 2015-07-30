using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine.Contracts
{
    internal interface IAct
    {
        void Drive();
        void DirectFlight();
        void CharterFlight();
        void ShuttleFlight();
        void BuildResearchStation();
        void TreatDisease(IDisease disease);
        void GiveKnowledge();
        void TakeKnowledge();
        void DiscoverCure();
    }
}
