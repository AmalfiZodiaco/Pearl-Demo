using UnityEngine;
using it.amalfi.Pearl.events;

namespace it.amalfi.Pearl.game
{
    /// <summary>
    //  Block and the make invisible the cursor mouse.
    /// </summary>
    public class CursorEnableComponent : LogicalComponent
    {
        #region Property
        /// <summary>
        /// Enable or not enable the mouse
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
        /// <summary>
        /// This method enables or doesn't enable the mouse
        /// </summary>
        /// <param name = "enable"> This bool indicates if the mouse must be enable or disable</param>
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
