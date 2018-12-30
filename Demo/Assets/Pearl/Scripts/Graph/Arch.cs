namespace it.amalfi.Pearl.graph
{
    public struct Arch<T>
    {
        public Vertex<T> vertex;
        public int weight;

        public Arch(Vertex<T> vertex, int weight)
        {
            this.vertex = vertex;
            this.weight = weight;
        }
    }
}
