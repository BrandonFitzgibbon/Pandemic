using Engine.CustomEventArgs;
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
    public class MessageViewModel : ViewModelBase, IMessageViewModel
    {
        private IContext<StringBuilder> message;

        public string Message
        {
            get { return message != null && message.Context != null ? message.Context.ToString() : null; }
        }

        public MessageViewModel(IContext<StringBuilder> message, IEnumerable<NodeDiseaseCounter> nodeDiseaseCounters)
        {
            this.message = message;
            this.message.ContextChanged += message_ContextChanged;

            foreach (NodeDiseaseCounter ndc in nodeDiseaseCounters)
            {
                ndc.Infected += ndc_Infected;
                ndc.Treated += ndc_Treated;
                ndc.Outbreak += ndc_Outbreak;
                ndc.Prevented += ndc_Prevented;
            }
        }

        private void message_ContextChanged(object sender, ContextChangedEventArgs<StringBuilder> e)
        {
            NotifyPropertyChanged("Message");
        }

        private void ndc_Prevented(object sender, PreventionEventArgs e)
        {
            message.Context.AppendLine(e.Player.Role + " in " + e.Player.Location.City + " has prevented a " + e.Disease + " infection from occuring in " + e.Node.City);
        }

        private void ndc_Outbreak(object sender, OutbreakEventArgs e)
        {
            message.Context.AppendLine("An outbreak has occurred in " + e.OriginCounter.Node.City + "!");

            foreach (NodeDiseaseCounter ndc in e.ChainCities)
            {
                message.Context.AppendLine("\tA chain outbreak has occured in " + ndc.Node.City);
            }
            message.Context.AppendLine("\t\tAffected Cities");
            foreach (NodeDiseaseCounter ndc in e.AffectedCities.Distinct())
            {
                message.Context.AppendLine("\t\t\t" + ndc.Node.City);
            }
        }

        private void ndc_Treated(object sender, TreatedEventArgs e)
        {
            message.Context.AppendLine(e.Treater.Role + " has treated " + e.Value + " unit(s) of " + e.NodeDiseaseCounter.Disease + " infection in " + e.NodeDiseaseCounter.Node.City);
        }

        private void ndc_Infected(object sender, InfectionEventArgs e)
        {
            message.Context.AppendLine(e.Value + " unit(s) of " + e.NodeDiseaseCounter.Disease + " have infected " + e.NodeDiseaseCounter.Node.City);
        }
    }
}
