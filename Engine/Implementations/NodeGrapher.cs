using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine.Implementations
{
    public static class NodeGrapher
    {
        public static Queue<Node> GetNodePath(Node startNode, Node endNode)
        {
            Queue<Node> path = new Queue<Node>();
            Queue<Node> Q = new Queue<Node>();
            Dictionary<Node, int> Depth = new Dictionary<Node, int>();

            Q.Enqueue(startNode);
            int i = 1;
            Depth.Add(startNode, i);

            bool found = false;

            while (Q.Count > 0 && !found)
            {
                i++;
                Node n = Q.Dequeue();
                path.Enqueue(n);

                foreach (Node connection in n.Connections)
                {
                    if(!Depth.Keys.Contains(connection))
                    {
                        if (connection == endNode)
                        {
                            path.Enqueue(connection);
                            Depth.Add(connection, i);
                            found = true;
                        }

                        if (found)
                            break;

                        Q.Enqueue(connection);
                        Depth.Add(connection, i);
                    }
                }
            }

            Node hit = endNode;
            List<Node> smallestPath = new List<Node>();
            smallestPath.Add(endNode);
            int layer = Depth[endNode];
            while (layer > 1)
            {
                foreach (Node node in Depth.Keys)
                {
                    if (hit.Connections.Contains(node))
                    {
                        layer = Depth[node];
                        smallestPath.Add(node);
                        hit = node;
                        break;
                    }
                }
            }

            Queue<Node> link = new Queue<Node>();
            smallestPath.Reverse();
            foreach (Node node in smallestPath)
            {
                link.Enqueue(node);
            }

            return link;
        }
    }
}
