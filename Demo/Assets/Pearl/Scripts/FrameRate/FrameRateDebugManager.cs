using UnityEngine.UI;
using UnityEngine;
using Pearl.events;

namespace Pearl.frameRate
{
    /// <summary>
    /// The class that writes the frameRate in UI
    /// </summary>
    [RequireComponent(typeof(Text))]
    public class FrameRateDebugManager : LogicalSimpleManager
    {
        #region Private Fields
        private Text frameRateText;
        private FrameRateManager frameRateManager;
        #endregion

        #region Unity CallBacks
        // Use this for initialization
        protected override void OnAwake()
        {
            frameRateText = GetComponent<Text>();
            frameRateManager = EventsManager.GetIstance<FrameRateManager>();
        }

        private void Update()
        {
            DoSeeFPS(frameRateManager.FrameRate);
        }
        #endregion

        #region Logical Methods
        /// <summary>
        /// Writes the framRate in the component text
        /// </summary>
        /// <param name = "FPS"> The current FrameRate</param>
        private void DoSeeFPS(int FPS)
        {
            frameRateText.text = FPS.ToString() + " " + "FPS";
        }
        #endregion
    }
}
