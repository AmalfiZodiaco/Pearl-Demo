using System.Collections.Generic;

namespace it.amalfi.Pearl.graph
{
    public class Vertex<T>
    {
        #region Private Fields
        private T value;
        private Dictionary<T, Arch<T>> neighbors;
        #endregion

        #region Constructors
        public Vertex(T value)
        {
            this.value = value;
            neighbors = new Dictionary<T, Arch<T>>();
        }
        #endregion

        #region Properties
        public Dictionary<T, Arch<T>>.ValueCollection Neighbors { get { return neighbors.Values; } }
        public Arch<T> this[T vertex] { get { return neighbors[vertex]; } }
        #endregion

        #region Public methods
        public void AddArch(Vertex<T> vertex, int weight)
        {
            neighbors.Update(vertex.value, new Arch<T>(vertex, weight));
        }

        public void RemoveArch(T vertex)
        {
            neighbors.Remove(vertex);
        }

        public bool HasArch(T vertex)
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
        #endregion
    }

}