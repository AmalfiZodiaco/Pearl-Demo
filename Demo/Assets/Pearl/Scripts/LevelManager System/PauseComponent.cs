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
        #region Obey methods
        /// <summary>
        /// Manages the pause in the level
        /// </summary>
        public void DoControlPause(bool pause)
        {
            if (pause)
                Time.timeScale = 0;
            else
                Time.timeScale = 1;
        }
        #endregion
    }

}