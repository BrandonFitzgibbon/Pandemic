using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;
using System.Windows.Shapes;

namespace Presentation.WPF.Controls
{
    public class ConnectionCanvas : Canvas
    {
        private Collection<ContentConnection> connections;
        public Collection<ContentConnection> Connections
        {
            get { return connections; }
            set { connections = value; }
        }

        public ConnectionCanvas()
        {
            connections = new Collection<ContentConnection>();
            this.Loaded += ConnectionCanvas_Loaded;
        }

        void ConnectionCanvas_Loaded(object sender, RoutedEventArgs e)
        {
            foreach (ContentConnection con in Connections)
            {
                double x1 = 0;
                double x2 = 0;
                double y1 = 0;
                double y2 = 0;

                switch(con.FirstConnectionType)
                {
                    case ContentConnection.ConnectionType.BottomLeftCorner:
                        x1 = Canvas.GetLeft(con.FirstControl);
                        y1 = Canvas.GetTop(con.FirstControl) + con.FirstControl.DesiredSize.Height;
                        break;
                    case ContentConnection.ConnectionType.BottomMiddle:
                        x1 = Canvas.GetLeft(con.FirstControl) + (con.FirstControl.DesiredSize.Width / 2);
                        y1 = Canvas.GetTop(con.FirstControl) + con.FirstControl.DesiredSize.Height;
                        break;
                    case ContentConnection.ConnectionType.BottomRightCorner:
                        x1 = Canvas.GetLeft(con.FirstControl) + con.FirstControl.DesiredSize.Width;
                        y1 = Canvas.GetTop(con.FirstControl) + con.FirstControl.DesiredSize.Height;
                        break;
                    case ContentConnection.ConnectionType.LeftMiddle:
                        x1 = Canvas.GetLeft(con.FirstControl);
                        y1 = Canvas.GetTop(con.FirstControl) + (con.FirstControl.DesiredSize.Height / 2);
                        break;
                    case ContentConnection.ConnectionType.RightMiddle:
                        x1 = Canvas.GetLeft(con.FirstControl) + con.FirstControl.DesiredSize.Width;
                        y1 = Canvas.GetTop(con.FirstControl) + (con.FirstControl.DesiredSize.Height / 2);
                        break;
                    case ContentConnection.ConnectionType.TopLeftCorner:
                        x1 = Canvas.GetLeft(con.FirstControl);
                        y1 = Canvas.GetTop(con.FirstControl);
                        break;
                    case ContentConnection.ConnectionType.TopMiddle:
                        x1 = Canvas.GetLeft(con.FirstControl) + (con.FirstControl.DesiredSize.Width / 2);
                        y1 = Canvas.GetTop(con.FirstControl);
                        break;
                    case ContentConnection.ConnectionType.TopRightCorner:
                        x1 = Canvas.GetLeft(con.FirstControl) + con.FirstControl.DesiredSize.Width;
                        y1 = Canvas.GetTop(con.FirstControl);
                        break;
                }

                switch (con.SecondConnectionType)
                {
                    case ContentConnection.ConnectionType.BottomLeftCorner:
                        x2 = Canvas.GetLeft(con.SecondControl);
                        y2 = Canvas.GetTop(con.SecondControl) + con.SecondControl.DesiredSize.Height;
                        break;
                    case ContentConnection.ConnectionType.BottomMiddle:
                        x2 = Canvas.GetLeft(con.SecondControl) + (con.SecondControl.DesiredSize.Width / 2);
                        y2 = Canvas.GetTop(con.SecondControl) + con.SecondControl.DesiredSize.Height;
                        break;
                    case ContentConnection.ConnectionType.BottomRightCorner:
                        x2 = Canvas.GetLeft(con.SecondControl) + con.SecondControl.DesiredSize.Width;
                        y2 = Canvas.GetTop(con.SecondControl) + con.SecondControl.DesiredSize.Height;
                        break;
                    case ContentConnection.ConnectionType.LeftMiddle:
                        x2 = Canvas.GetLeft(con.SecondControl);
                        y2 = Canvas.GetTop(con.SecondControl) + (con.SecondControl.DesiredSize.Height / 2);
                        break;
                    case ContentConnection.ConnectionType.RightMiddle:
                        x2 = Canvas.GetLeft(con.SecondControl) + con.SecondControl.DesiredSize.Width;
                        y2 = Canvas.GetTop(con.SecondControl) + (con.SecondControl.DesiredSize.Height / 2);
                        break;
                    case ContentConnection.ConnectionType.TopLeftCorner:
                        x2 = Canvas.GetLeft(con.SecondControl);
                        y2 = Canvas.GetTop(con.SecondControl);
                        break;
                    case ContentConnection.ConnectionType.TopMiddle:
                        x2 = Canvas.GetLeft(con.SecondControl) + (con.SecondControl.DesiredSize.Width / 2);
                        y2 = Canvas.GetTop(con.SecondControl);
                        break;
                    case ContentConnection.ConnectionType.TopRightCorner:
                        x2 = Canvas.GetLeft(con.SecondControl) + con.SecondControl.DesiredSize.Width;
                        y2 = Canvas.GetTop(con.SecondControl);
                        break;
                }

                this.Children.Add(new Line() { X1 = x1, X2 = x2, Y1 = y1, Y2 = y2, Stroke = System.Windows.Media.Brushes.White, StrokeThickness = 1 });
            }
        }
    }
}
