using Pearl.events;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pearl.level
{
    /// <summary>
    /// The class manages the pause in the level
    /// </summary>
    public class PauseComponents : LogicalComponent
    {
        /// <summary>
        /// Manages the pause in the level
        /// </summary>
        public void PauseControl(bool pause)
        {
            if (pause)
                Time.timeScale = 0;
            else
                Time.timeScale = 1;
        }
    }

}