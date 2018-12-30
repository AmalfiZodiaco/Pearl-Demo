using System.Collections.Generic;

namespace it.amalfi.Pearl.graph
{
    public class Vertex<T>
    {
        public T value;

        private Dictionary<T, Arch<T>> neighbors;

        public Dictionary<T, Arch<T>>.ValueCollection Neighbors { get { return neighbors.Values; } }

        public Vertex(T value)
        {
            this.value = value;
            neighbors = new Dictionary<T, Arch<T>>();
        }

        public Arch<T> this[T vertex]
        {
            get
            {
                return neighbors[vertex];
            }
        }

        public void AddEdge(Vertex<T> vertex, int weight)
        {
            neighbors.Update(vertex.value, new Arch<T>(vertex, weight));
        }

        public void RemoveEdge(T vertex)
        {
            neighbors.Remove(vertex);
        }

        public bool HasEdge(T vertex)
        {
            return neighbors.ContainsKey(vertex);
        }

        public override bool Equals(object obj)
        {
            if (!(obj is Vertex<T>))
                return false;
            Vertex<T> vertex = obj as Vertex<T>;
            return vertex != null && value.Equals(vertex.value);
        }

        public override int GetHashCode()
        {
            return -1584136870 + EqualityComparer<T>.Default.GetHashCode(value);
        }
    }

}