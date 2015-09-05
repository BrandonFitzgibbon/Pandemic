using Engine.Implementations;
using Presentation.WPF.Context;
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
        private DiseaseCounter counter;

        public DiseaseCounterViewModel(DiseaseCounter counter, Notifier notifier)
        {
            this.counter = counter;
            notifier.SubscribeToViewModel(this);
        }

        public int Count
        {
            get { return counter != null ? counter.Count : 0; }
        }

        public Disease Disease
        {
            get { return counter != null ? counter.Disease : null; }
        }

        public bool IsEradicated
        {
            get { return counter != null && counter.Disease != null ? counter.Disease.IsEradicated : false; }
        }

        public bool IsCured
        {
            get { return counter != null && counter.Disease != null ? counter.Disease.IsCured : false; }
        }
    }
}
