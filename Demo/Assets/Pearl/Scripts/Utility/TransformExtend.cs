using UnityEngine;

namespace Pearl
{
    /// <summary>
    /// A class that extends the Transform class
    /// </summary>
    public static class TransformExtend
    {
        #region Public Methods
        /// <summary>
        /// Returns the number that identifies the specific transform-child (if he is not a son of anyone, he returns -1)
        /// </summary>
        /// <param name = "transform"> The specific component transform</param>
        public static int NumberChild(this Transform transform)
        {
            if (transform.parent)
            {
                for (int i = 0; i < transform.parent.childCount; i++)
                {
                    if (transform.parent.GetChild(i).Equals(transform))
                        return i;
                }
            }
            return -1;
        }
        #endregion
    }
}
