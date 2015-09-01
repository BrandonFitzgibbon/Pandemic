using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine.Implementations
{
    public class ActionCard<T> : BaseActionCard
    {
        public Action<T> Action { get; internal set; }
        public Func<T, bool> CanAction { get; internal set; }

        public ActionCard(string name, string description) : base(name, description) { }
    }
}
