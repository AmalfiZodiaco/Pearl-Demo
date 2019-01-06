using UnityEngine;
using Pearl.events;

namespace Pearl.game
{
    /// <summary>
    //  The class blocks and makes invisible the cursor mouse.
    /// </summary>
    public class CursorEnableComponent : LogicalComponent
    {
        #region Property
        /// <summary>
        /// Enables or don't enables the mouse
        /// </summary>
        public bool Enable { set { EnableMouse(value); } }
        #endregion

        #region Constructors
        /// <summary>
        /// This constructor enables or doesn't enable the mouse
        /// </summary>
        /// <param name = "enable"> This bool indicates if the mouse must be enable or disable</param>
        public CursorEnableComponent(bool enable)
        {
            EnableMouse(enable);
        }
        #endregion

        #region Private Method
        private void EnableMouse(bool enable)
        {
            Cursor.visible = enable;
            if (enable)
                Cursor.lockState = CursorLockMode.None;
            else
                Cursor.lockState = CursorLockMode.Locked;
        }
        #endregion
    }
}
