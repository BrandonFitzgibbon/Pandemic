using Engine.Contracts;
using Presentation.WPF.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.WPF.Implementations
{
    public class DiseaseCounterViewModel : ViewModelBase, IDiseaseCounterViewModel
    {
        private ICity city;
        public ICity City
        {
            get { return city; }
        }

        private IDisease disease;
        public IDisease Disease
        {
            get { return disease; }
        }

        public int Count
        {
            get { return city.Counters.Single(i => i.Disease == disease).Count; }
        }

        public DiseaseCounterViewModel(ICity city, IDisease disease)
        {
            this.city = city;
            this.disease = disease;
        }
    }
}
