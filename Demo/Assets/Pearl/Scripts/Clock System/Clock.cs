using System.Collections.Generic;
using UnityEngine;

namespace it.amalfi.Pearl.clock
{
    /// <summary>
    /// The class performs a simple clock
    /// </summary>
    public class Clock : ClockAbstract
    {
        #region Constructors
        /// <summary>
        /// This constructor creates the clock
        /// </summary>
        /// <param name = "on">The bool rappresent if the clock must be on or off.</param>
        public Clock(bool on = true)
        {
            if (on)
                ResetOn();
            else
                ResetOff();
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Reset the timer for the new perform
        /// </summary>
        public void ResetOn(float preservedTime = 0)
        {
            this.preservedTime = preservedTime;
            this.on = true;
            this.timestart = Time.time;
        }
        #endregion
    }

}