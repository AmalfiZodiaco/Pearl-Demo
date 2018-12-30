using System.Collections.Generic;

namespace it.amalfi.Pearl.graph
{
    public class Graph<T>
    {
        // The list of vertices in the graph
        private Dictionary<T, Vertex<T>> vertices;
        private readonly TypeGraph type;

        public int Size { get { return vertices.Count; } }
        public Dictionary<T, Vertex<T>>.KeyCollection Vertices { get { return vertices.Keys; } }

        public Graph(TypeGraph type)
        {
            vertices = new Dictionary<T, Vertex<T>>();
            this.type = type;
        }

        public Vertex<T> this[T index]
        {
            get
            {
                return vertices[index];
            }
        }

        public void AddVertex(T vertex)
        {
            vertices.Add(vertex, new Vertex<T>(vertex));
        }

        public void RemoveEdge(T vertex, T vertexEdge)
        {
            if (!vertices.ContainsKey(vertex) || !vertices.ContainsKey(vertexEdge))
                return;
            vertices[vertex].RemoveEdge(vertex);
            if (type == TypeGraph.Undirected)
                vertices[vertexEdge].RemoveEdge(vertex);
        }

        public void AddEdge(T vertex, T vertexEdge, int weight)
        {
            if (!vertices.ContainsKey(vertex) || !vertices.ContainsKey(vertexEdge))
                return;
            vertices[vertex].AddEdge(vertices[vertexEdge], weight);
            if (type == TypeGraph.Undirected)
                vertices[vertexEdge].AddEdge(vertices[vertex], weight);
        }

        public void RemoveVertex(T vertex)
        {
            vertices.Remove(vertex);
            foreach (Vertex<T> v in vertices.Values)
            {
                v.RemoveEdge(vertex);
            }
        }

        public bool HasVertex(T vertex)
        {
            return vertices.ContainsKey(vertex);
        }
    }
}
