using UnityEngine;

namespace it.amalfi.Pearl
{
    /// <summary>
    /// A structure that represents a range of numbers
    /// </summary>
    public struct Range
    {
        #region private Fields
        [SerializeField]
        private float min;
        [SerializeField]
        private float max;
        #endregion

        public float Min { get { return min; } }
        public float Max { get { return max; } }

        #region Constructors
        public Range(float min, float max)
        {
            if (min < max)
            {
                this.min = min;
                this.max = max;
            }
            else
            {
                this.min = max;
                this.max = min;
            }
        }
        #endregion
    }

}