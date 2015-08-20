using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Shapes;

namespace Presentation.WPF.Controls
{
    public class ContentConnection : FrameworkElement
    {
        public enum ConnectionType { TopLeftCorner, TopMiddle, TopRightCorner, RightMiddle, BottomRightCorner, BottomMiddle, BottomLeftCorner, LeftMiddle}

        private ConnectionType firstConnectionType;
        public ConnectionType FirstConnectionType
        {
            get { return firstConnectionType; }
            set { firstConnectionType = value; }
        }

        private ConnectionType secondConnectionType;
        public ConnectionType SecondConnectionType
        {
            get { return secondConnectionType; }
            set { secondConnectionType = value; }
        }

        public UIElement FirstControl
        {
            get { return (UIElement)GetValue(FirstControlProperty); }
            set { SetValue(FirstControlProperty, value); }
        }

        public static readonly DependencyProperty FirstControlProperty =
            DependencyProperty.Register("FirstControl", typeof(UIElement), typeof(ContentConnection), new PropertyMetadata());

        public UIElement SecondControl
        {
            get { return (UIElement)GetValue(SecondControlProperty); }
            set { SetValue(SecondControlProperty, value); }
        }

        public static readonly DependencyProperty SecondControlProperty =
            DependencyProperty.Register("SecondControl", typeof(UIElement), typeof(ContentConnection), new PropertyMetadata());
    }
}
