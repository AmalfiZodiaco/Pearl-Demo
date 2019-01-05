using System.Collections.Generic;

namespace it.amalfi.Pearl.graph
{
    public class Graph<T>
    {
        #region private Fields
        // The list of vertices in the graph
        private Dictionary<T, Vertex<T>> vertices;
        private readonly TypeGraph type;
        #endregion

        #region Properties
        public int Size { get { return vertices.Count; } }
        public Dictionary<T, Vertex<T>>.KeyCollection Vertices { get { return vertices.Keys; } }
        #endregion

        #region Constructors
        public Graph(TypeGraph type)
        {
            vertices = new Dictionary<T, Vertex<T>>();
            this.type = type;
        }
        #endregion

        #region Index
        public Vertex<T> this[T key] { get { return vertices[key]; }  }
        #endregion

        #region Public Methods
        public void AddVertex(T vertex)
        {
            vertices.Add(vertex, new Vertex<T>(vertex));
        }

        public void RemoveVertex(T vertex)
        {
            vertices.Remove(vertex);
            foreach (Vertex<T> v in vertices.Values)
            {
                v.RemoveArch(vertex);
            }
        }

        public void AddArch(T vertex, T vertexEdge, int weight)
        {
            if (!vertices.ContainsKey(vertex) || !vertices.ContainsKey(vertexEdge))
                return;
            vertices[vertex].AddArch(vertices[vertexEdge], weight);
            if (type == TypeGraph.Undirected)
                vertices[vertexEdge].AddArch(vertices[vertex], weight);
        }

        public void RemoveArch(T vertex, T vertex2)
        {
            if (!vertices.ContainsKey(vertex) || !vertices.ContainsKey(vertex2))
                return;
            vertices[vertex].RemoveArch(vertex);
            if (type == TypeGraph.Undirected)
                vertices[vertex2].RemoveArch(vertex);
        }

        public bool HasVertex(T vertex)
        {
            return vertices.ContainsKey(vertex);
        }

        public bool HasHarch(T vertex1, T vertex2)
        {
            if (!vertices.ContainsKey(vertex1) || !vertices.ContainsKey(vertex2))
                return false;
            return vertices[vertex1].HasArch(vertex2);
        }
        #endregion
    }
}
