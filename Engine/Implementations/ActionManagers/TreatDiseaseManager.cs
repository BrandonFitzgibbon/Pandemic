using Engine.Contracts;
using Engine.CustomEventArgs;
using Engine.Implementations.ActionItems;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine.Implementations.ActionManagers
{
    public class TreatDiseaseManager
    {
        private IEnumerable<NodeDiseaseCounter> nodeDiseaseCounters;
        private Player player;

        internal IEnumerable<TreatDiseaseItem> Targets { get; private set; }

        internal TreatDiseaseManager(Player player, IEnumerable<NodeDiseaseCounter> nodeDiseaseCounters)
        {
            this.player = player;
            this.nodeDiseaseCounters = nodeDiseaseCounters;
            this.player.Moved += PlayerMoved;
            Update();
        }

        internal void Treat(TreatDiseaseItem treatDiseaseItem)
        {
        }

        internal void Update()
        {
            Targets = GetTargets();
        }

        private void PlayerMoved(object sender, PlayerMovedEventArgs e)
        {
            Update();
        }

        private void ActionUsed(object sender, EventArgs e)
        {
            Update();
        }

        private IEnumerable<TreatDiseaseItem> GetTargets()
        {
            List<TreatDiseaseItem> targets = new List<TreatDiseaseItem>();

                return targets;

            {
                if (ndc.Count > 0)
                {
                    if(ndc.Disease.IsCured || player is Medic)
                    {
                        targets.Add(new TreatDiseaseItem(ndc, ndc.Count, 1));
                    }
                    else
                    {
                        for (int i = 1; i <= ndc.Count; i++)
                        {
                            targets.Add(new TreatDiseaseItem(ndc, i, i));
                        }
                    }
                }
            }

            return targets;
        }
    }
}

