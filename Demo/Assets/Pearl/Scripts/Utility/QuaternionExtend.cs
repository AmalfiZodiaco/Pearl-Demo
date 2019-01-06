using UnityEngine;

namespace Pearl
{
    /// <summary>
    /// A class that extends the Quaternion class
    /// </summary>
    public class QuaternionExtend : MonoBehaviour
    {
        /// <summary>
        /// Returns the specific rotation from a particular direction vector
        /// </summary>
        /// <param name = "transform"> The specific component transform</param>
        public static Quaternion CalculateRotation2D(Vector2 direction)
        {
            return Quaternion.AngleAxis(Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg, Vector3.forward);
        }
    }
}
