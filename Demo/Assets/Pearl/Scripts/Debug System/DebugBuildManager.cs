using it.amalfi.Pearl.events;
using UnityEngine;
using UnityEngine.UI;

namespace it.amalfi.Pearl.debug
{
    public class DebugBuildManager : LogicalSimpleManager
    {
        #region Private Fields
        private Text text;
        #endregion

        #region Unity CallBacks
        // Use this for initialization
        protected override void OnAwake()
        {
            text = GetComponentInChildren<Text>();
        }
        #endregion

        #region Public Methods
        public void Log(object obj)
        {
            text.text += obj.ToString() + "\n";
        }
        #endregion
    }
}
