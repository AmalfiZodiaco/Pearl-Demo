using UnityEngine;

namespace it.amalfi.Pearl.clock
{
    /// <summary>
    /// The class performs a timer
    /// </summary>
    public class Timer : ClockAbstract
    {
        #region Properties
        /// <summary>
        /// How much time has passed in percentage[0 - 1]
        /// </summary>
        public float TimeInPercent
        {
            get
            {
                Debug.Assert(this.on);
                if (this.pause)
                    return Mathf.Clamp01(preservedTime / this.limit);
                else
                    return Mathf.Clamp01(AuxTime / this.limit);
            }
        }

        /// <summary>
        /// How much time has passed without limits
        /// </summary>
        public float TimeWithoutLimit
        {
            get
            {
                Debug.Assert(this.on);
                if (this.pause)
                    return preservedTime;
                else
                    return AuxTime;
            }
        }

        /// <summary>
        /// How much time has passed in percentage[0 - 1] in countdown
        /// </summary>
        public float TimeInPercentReversed
        {
            get
            {
                Debug.Assert(this.on);
                if (this.pause)
                    return 1 - Mathf.Clamp01(preservedTime / this.limit);
                else
                    return 1 - Mathf.Clamp01(AuxTime / this.limit);
            }
        }

        /// <summary>
        /// How much time has passed in countDown [limit-0]
        /// </summary>
        public float TimeReversed
        {
            get
            {
                Debug.Assert(this.on);
                if (this.pause)
                    return limit - preservedTime;
                else
                    return limit - Mathf.Min(preservedTime + UnityEngine.Time.time - this.timestart, limit);
            }
        }
        #endregion

        #region Constructors
        /// <summary>
        /// This costructor creates a Timer in on.
        /// </summary>
        /// <param name = "duration"> The duration of timer</param>
        public Timer(float duration)
        {
            Debug.Assert(duration > 0 && duration != Mathf.Infinity);
            ResetOn(duration);
        }

        /// <summary>
        /// This costructor create a Timer in off.
        /// </summary>
        public Timer()
        {
            ResetOff();
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Is the timer finish?
        /// </summary>
        public bool IsFinish()
        {
            Debug.Assert(this.on);

            return this.Time >= this.limit;
        }

        /// <summary>
        /// Reset the timer in on for the new perform
        /// </summary>
        /// <param name = "duration"> The duration of timer</param>
        public void ResetOn(float duration, float preservedTime = 0)
        {
            Debug.Assert(duration > 0 && duration != Mathf.Infinity);
            this.preservedTime = preservedTime;
            this.on = true;
            this.timestart = UnityEngine.Time.time;
            this.limit = duration;
        }
        #endregion
    }
}