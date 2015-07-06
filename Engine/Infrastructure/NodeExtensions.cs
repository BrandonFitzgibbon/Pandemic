using Engine.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine.Infrastructure
{
    public static class NodeExtensions
    {
        public static void FormConnection(this INode node, INode connection)
        {
            if (connection != null && !node.Connections.Contains(connection))
                node.Connections.Add(connection);
        }
    }
}
