using Pearl.clock;
using Pearl.events;
using UnityEngine;

namespace Pearl.frameRate
{
    /// <summary>
    /// This singleton class calculates the frameRate
    /// </summary>
    public class FrameRateManager : LogicalSimpleManager
    {
        #region Inspector Fields
        /// <summary>
        /// The refresh time is the time for calcolate the new frameRate
        /// </summary>
        [SerializeField]
        private float refreshTime = 0.5f;
        /// <summary>
        /// The limit frame rate is the upper limit for not making the game go too fast
        /// </summary>
        [SerializeField]
        private int limitFrameRate = 60;
        #endregion

        #region Private Fields
        private Timer timer;
        private int frameCounter = 0;
        private int lastFramerate = 0;
        #endregion

        #region Properties
        /// <summary>
        /// This actual FrameRate
        /// </summary>
        public int FrameRate { get { return lastFramerate; } }
        #endregion

        #region Unity CallBacks
        protected override void OnAwake()
        {
            timer = new Timer(this.refreshTime);
            DoSettingLimitFrameRate();
        }

        private void Update()
        {
            DoCalculateFrameRate();
        }
        #endregion

        #region Logical Methods
        /// <summary>
        /// This method calculate the actual frame rate 
        /// </summary>
        private void DoCalculateFrameRate()
        {
            if (timer.IsFinish())
            {
                lastFramerate = Mathf.RoundToInt((float)frameCounter / timer.TimeWithoutLimit);
                DoResetCalculateFrame();
            }
            else
                frameCounter++;
        }

        /// <summary>
        /// This method set the limit of the desidered frame rate
        /// </summary>
        private void DoSettingLimitFrameRate()
        {
            QualitySettings.vSyncCount = 0;
            Application.targetFrameRate = limitFrameRate;
        }

        private void DoResetCalculateFrame()
        {
            frameCounter = 0;
            timer.ResetOn(this.refreshTime);
        }
        #endregion
    }
}
