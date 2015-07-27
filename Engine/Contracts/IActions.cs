using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine.Contracts
{
    public interface IActions
    {
        int ActionCount { get; }
        IDictionary<ICity, int> DriveDestinations { get; }
        IDictionary<IDisease, int> DiseaseTreatmentOptions { get; }
        Func<ICity, bool> CanDrive { get; }
        Action<ICity> Drive { get; }
        Action<IDisease, ICity> TreatDisease { get; }
        event EventHandler ActionsDepleted;
    }
}
