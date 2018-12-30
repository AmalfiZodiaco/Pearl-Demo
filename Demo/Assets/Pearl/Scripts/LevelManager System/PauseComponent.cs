using it.amalfi.Pearl.events;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace it.amalfi.Pearl.level
{
    public class PauseComponents : LogicalComponent
    {
        public void PauseControl(bool pause)
        {
            if (pause)
                Time.timeScale = 0;
            else
                Time.timeScale = 1;
        }
    }

}