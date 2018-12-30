using System.Collections.Generic;

namespace it.amalfi.Pearl.actionTrigger
{
    public struct Informations
    {
        private readonly Dictionary<string, object> informations;

        public Informations(Dictionary<string, object> informations)
        {
            this.informations = informations;
        }

        public T Take<T>(string name)
        {
            return (T)informations[name];
        }
    }
}