using it.amalfi.Pearl.events;
using UnityEngine;
using System;

namespace it.amalfi.Pearl.input
{
    public abstract class InputReaderAbstractComponent : LogicalComponent
    {
        protected bool isPause;
        public bool Pause { set { isPause = value; } }

        #region Abstract Methods
        public abstract void UpdateKeyboard();

        public abstract void UpdateJoystick();
        #endregion
    }
}

