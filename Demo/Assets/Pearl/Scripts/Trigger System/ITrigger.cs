using System.Collections.Generic;
using Pearl.multitags;

namespace Pearl.actionTrigger
{
    public interface ITrigger
    {
        void Trigger(Informations informations, List<Tags> tags);
    }
}
