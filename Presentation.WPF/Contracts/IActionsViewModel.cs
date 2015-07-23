﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Presentation.WPF.Contracts
{
    public interface IActionsViewModel : IViewModelBase
    {
        ICommand DriveCommand { get; }
        ICommand TreatDiseaseCommand { get; }
    }
}
