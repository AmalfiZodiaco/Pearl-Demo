using UnityEngine;

namespace it.amalfi.Pearl
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
            Transform parent = transform.parent;
            if (parent)
            {
                int i = 0;
                foreach (Transform child in parent)
                {
                    if (child.Equals(transform))
                        return i;
                    i++;
                }
            }
            return -1;
        }
        #endregion
    }
}
