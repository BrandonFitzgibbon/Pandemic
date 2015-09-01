using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine.Implementations
{
    public class ActionCard<T> : BaseActionCard
    {
        public Action<T, Card> Action { get; private set; }
        public Func<T, bool> CanAction { get; private set; }

        public ActionCard(string name, string description, Action<T, Card> action, Func<T, bool> canAction) : base(name, description)
        {
            Action = action;
            CanAction = canAction;
        }
    }
}
