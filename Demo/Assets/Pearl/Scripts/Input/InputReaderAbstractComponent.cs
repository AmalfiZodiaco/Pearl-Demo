using Pearl.events;
using UnityEngine;
using System;

namespace Pearl.input
{
    /// <summary>
    /// The abstract version of the class that actually reads the input(this class must implement it)
    /// </summary>
    public abstract class InputReaderAbstractComponent : LogicalComponent
    {
        protected bool isPause;
        /// <summary>
        /// Sets true if the game is paused
        /// </summary>
        public bool Pause { set { isPause = value; } }

        #region Abstract Methods
        public abstract void UpdateKeyboard();

        public abstract void UpdateJoystick();
        #endregion
    }
}

