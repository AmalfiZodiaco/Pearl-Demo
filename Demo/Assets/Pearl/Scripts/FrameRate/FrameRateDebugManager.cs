using UnityEngine.UI;
using UnityEngine;
using it.amalfi.Pearl.events;

namespace it.amalfi.Pearl.frameRate
{
    [RequireComponent(typeof(Text))]
    public class FrameRateDebugManager : LogicalSimpleManager
    {
        #region Private Fields
        private Text frameRateText;
        #endregion

        #region Unity CallBacks
        // Use this for initialization
        protected override void OnAwake()
        {
            frameRateText = GetComponent<Text>();
        }
        #endregion

        #region Private Methods
        private void SeeFPS(int FPS)
        {
            frameRateText.text = FPS.ToString() + " " + "FPS";
        }
        #endregion

        protected override void SubscribEvents()
        {
            EventsManager.AddMethod<int>(EventAction.CallFrameRate, SeeFPS);
        }

        protected override void RemoveEvents()
        {
            EventsManager.RemoveMethod<int>(EventAction.CallFrameRate, SeeFPS);
        }
    }
}
