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
        #region Protected fields
        protected bool isPause;
        #endregion

        #region Proprieties
        /// <summary>
        /// Sets true if the game is paused
        /// </summary>
        public bool Pause { set { isPause = value; } }
        #endregion

        #region Abstract Methods
        public abstract void DoUpdateKeyboard();

        public abstract void DoUpdateJoystick();
        #endregion
    }
}

