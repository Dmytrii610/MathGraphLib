using System.Reflection.Metadata.Ecma335;
using System.Xml;

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
    public struct OutBoundedEdgeEnumerator
    {
        private readonly int[] next;
        private int currentEdgeId;
        private int nextEdgeId;

        public OutBoundedEdgeEnumerator(int startEdge, int[] next)
        {
            this.next = next;
            currentEdgeId = -1;
            nextEdgeId = startEdge;
        }
        public int Current => currentEdgeId;
        public bool MoveNext()
        {
            if (nextEdgeId == -1) return false;
            currentEdgeId = nextEdgeId;
            nextEdgeId = next[currentEdgeId];
            return true;
        }

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
        public void AddDirectedEdge(Node source, Node target, TEdgeData data)
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
        public void AddUndirectedEdge(Node nodeA, Node nodeB, TEdgeData data)
        {
            if(edgeCount + 1 >= destination.Length)
            {
                int newSize = destination.Length * 2;
                Array.Resize(ref destination, newSize);
                Array.Resize(ref edgeData, newSize);
                Array.Resize(ref next, newSize);
            }
            int edgeId1 = edgeCount++;
            destination[edgeId1] = nodeB.Id;
            edgeData[edgeId1] = data;
            next[edgeId1] = head[nodeA.Id];
            head[nodeA.Id] = edgeId1;

            int edgeId2 = edgeCount++;
            destination[edgeId2] = nodeA.Id;
            edgeData[edgeId2] = data;
            next[edgeId2] = head[nodeB.Id];
            head[nodeB.Id] = edgeId2;
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
