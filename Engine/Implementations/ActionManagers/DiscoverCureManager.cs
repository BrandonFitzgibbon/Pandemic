using Engine.Implementations.ActionItems;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine.Implementations.ActionManagers
{
    internal class DiscoverCureManager
    {
        private Player player;
        private IEnumerable<Disease> diseases;

        internal IEnumerable<DiscoverCureItem> Targets { get; private set; }

        internal DiscoverCureManager(Player player, IEnumerable<Disease> diseases)
        {
            this.player = player;
            this.diseases = diseases;

            foreach (Disease disease in diseases)
            {
                disease.Cured += Cured;
            }

            Update();
        }

        internal bool CanDiscoverCure(DiscoverCureItem discoverCureItem)
        {
            if (discoverCureItem == null || discoverCureItem.Cards == null)
                return false;

            if (player is Scientist)
                return discoverCureItem.Cards.Where(i => i.Node.Disease == discoverCureItem.Disease).Count() == 4;
            else
                return discoverCureItem.Cards.Where(i => i.Node.Disease == discoverCureItem.Disease).Count() == 5;
        }

        internal void DiscoverCure(DiscoverCureItem discoverCureItem)
        {
            if(CanDiscoverCure(discoverCureItem))
            {
                foreach (Card card in discoverCureItem.Cards)
                {
                    card.Discard();
                }
                discoverCureItem.Disease.Cure();
                player.ActionCounter.UseAction(1);
            }
        }

        private void Update()
        {
            Targets = GetTargets();
        }

        private void Cured(object sender, EventArgs e)
        {
            Update();
        }

        private IEnumerable<DiscoverCureItem> GetTargets()
        {
            List<DiscoverCureItem> targets = new List<DiscoverCureItem>();

            if (player.ActionCounter.Count < 1 || !player.Location.ResearchStation)
                return targets;

            foreach (Disease disease in diseases)
            {
                if (!disease.IsCured)
                    targets.Add(new DiscoverCureItem(disease));
            }

            return targets;
        }
    }
}
