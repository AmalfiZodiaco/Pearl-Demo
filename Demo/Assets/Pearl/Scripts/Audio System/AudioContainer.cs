using UnityEngine;
using it.amalfi.Pearl.clock;

namespace it.amalfi.Pearl.audio
{
    public class AudioContainer
    {
        #region Private Fields
        private Timer timer;
        private Range volumeRange;
        private static Range lerpRange = new Range(0, 1);
        private AnimationCurve curve;
        private float auxVolume;
        private bool decrease;
        #endregion

        #region Properties
        public string Name { get; private set; }
        #endregion

        #region Constructors
        public AudioContainer(string name)
        {
            this.Name = name;
            this.timer = new Timer();
        }
        #endregion

        #region Public Methods
        public float ObeyReset(float volumeActualValue, float volumeNewValue, float time, AnimationCurve curve = null)
        {
            Debug.Assert(time >= 0);

            if (volumeActualValue > volumeNewValue)
            {
                volumeRange.Set(volumeNewValue, volumeActualValue);
                decrease = true;
            }
            else
            {
                volumeRange.Set(volumeActualValue, volumeNewValue);
                decrease = false;
            }
            this.timer.ResetOn(time);
            this.curve = curve;
            return ObeyReturnVolume();
        }

        public float ObeyReturnVolume()
        {
            auxVolume = Evalutate();
            if (decrease)
                auxVolume = 1 - auxVolume;
            return MathfExtend.ChangeRange(auxVolume, lerpRange, volumeRange);
        }

        public bool ObeyIsFinish()
        {
            Debug.Assert(timer != null);

            return timer.IsFinish();
        }
        #endregion

        #region Private Methods
        private float Evalutate()
        {
            if (curve != null)
                return curve.Evaluate(timer.ActualTimeInPercent);
            else
                return timer.ActualTimeInPercent;
        }
        #endregion
    }
}
