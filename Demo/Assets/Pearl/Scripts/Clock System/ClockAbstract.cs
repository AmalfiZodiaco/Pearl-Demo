﻿using System.Collections.Generic;
using UnityEngine;

namespace it.amalfi.Pearl.clock
{
    /// <summary>
    /// The class is the abstract clock, it has the  fundamental methods 
    /// that any clock must have
    /// </summary>
    public abstract class ClockAbstract
    {
        #region Protected Fields
        /// <summary>
        /// The time when the clock starts
        /// </summary>
        protected float timestart;
        /// <summary>
        /// Determines if the clock is paused
        /// </summary>
        protected bool pause;
        /// <summary>
        /// The time preserved for pause and other.
        /// </summary>
        protected float preservedTime;
        /// <summary>
        /// The maximum limit of clock
        /// </summary>
        protected float limit = Mathf.Infinity;
        /// <summary>
        /// The clock is on or off?
        /// </summary>
        protected bool on;
        #endregion

        #region Properties
        /// <summary>
        /// The maximum limit of clock
        /// </summary>
        public float Limit
        {
            get
            {
                Debug.Assert(this.on);

                return limit;
            }
        }

        /// <summary>
        /// How much time has passed
        /// </summary>
        public float ActualTime
        {
            get
            {
                Debug.Assert(this.on);

                if (this.pause)
                    return preservedTime;
                else
                    return Mathf.Min(ActualTimeWithoutLimit, limit);
            }
        }

        protected float ActualTimeWithoutLimit
        {
            get
            {
                Debug.Assert(this.on);

                return preservedTime + Time.time - this.timestart;
            }
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Pause or Unpuase the clock
        /// </summary>
        /// <param name = "pause"> The bool rappresent if the clock must be pause or unpause.</param>
        public void Pause(bool pause)
        {
            Debug.Assert(this.on);

            if (this.pause != pause)
            {
                if (pause)
                    this.preservedTime = ActualTime;
                else
                    this.timestart = Time.time;
                this.pause = pause;
            }
        }

        /// <summary>
        /// Reset the timer in off
        /// </summary>
        public void ResetOff()
        {
            this.on = false;
        }
        #endregion
    }
}
