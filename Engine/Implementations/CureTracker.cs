using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine.Implementations
{
    public class CureTracker
    {
        private IEnumerable<Disease> diseases;

        public CureTracker(IEnumerable<Disease> diseases)
        {
            this.diseases = diseases;
            foreach (Disease disease in diseases)
            {
                disease.Cured += Disease_Cured;
            }
        }

        private void Disease_Cured(object sender, EventArgs e)
        {
            foreach (Disease disease in diseases)
            {
                if (!disease.IsCured)
                    return;
            }

            if (GameWon != null) GameWon(this, EventArgs.Empty);
        }

        public event EventHandler GameWon;
    }
}
