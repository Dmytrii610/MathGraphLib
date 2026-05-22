using System.Reflection.Metadata.Ecma335;

namespace MathGraphLib
{
    /// <summary>
    /// Structure which represents a Node in Graph Data Structure
    /// </summary>
    public readonly struct Node
    {
        internal readonly int Id;
        internal Node(int id) => Id = id;
    }
    public class FastGraph<TNodeData,TEdgeData>
    {
        private TNodeData[] nodes;
        private TEdgeData[] edgeData;
        private int[] destination;
        private int[] head; // head - dla każdego węzła przechowujemy indeks pierwszej wychodzącej krawędzi.
        private int[] next; // next - tablica, wskazująca na kolejne krawędzie.
        private int nodeCount;
        private int edgeCount;

        public FastGraph()
        {
            nodes = new TNodeData[16];
            edgeData = new TEdgeData[32];
            destination = new int[32];
            head = new int[16];
            next = new int[32];
            Array.Fill(head, -1);
        }
        public Node AddNode(TNodeData node)
        {
            if(nodeCount >= nodes.Length)
            {
                int size = nodes.Length;
                Array.Resize(ref nodes, size * 2);
                Array.Resize(ref head, size * 2);
                Array.Fill(head, -1, nodeCount, size - nodeCount);
            }
            nodes[nodeCount] = node;
            return new Node(nodeCount++);
        }
        public void AddEdge(Node source, Node target, TEdgeData data)
        {
            if (edgeCount >= destination.Length)
            {
                Array.Resize(ref destination, destination.Length * 2);
                Array.Resize(ref edgeData, edgeData.Length * 2);
                Array.Resize(ref next, next.Length * 2);
            }
            destination[edgeCount] = target.Id;
            edgeData[edgeCount] = data;
            next[edgeCount] = head[source.Id];
            head[source.Id] = edgeCount;
            edgeCount++;
        }
        public ref TNodeData GetNodeData(Node node)
        {
            return ref nodes[node.Id];
        } 
    }
    public class Graph
    {

    }
}
