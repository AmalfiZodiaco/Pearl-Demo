using Pearl.events;
using UnityEngine;

namespace Pearl
{
    /// <summary>
    //  The class makes the unique and indestructible gameobject between the scenes
    /// </summary>
    public class DontDestroyOnLoadManager : LogicalSimpleManager
    {
        #region Inspector Fields
        /// <summary>
        //  If the Boolean is true, the GameObject will be unique and each of its clne will be eliminated in the scene
        /// </summary>
        [SerializeField]
        private bool isUnique;
        #endregion

        #region Unity CallBacks
        protected override void OnAwake()
        {
            if (isUnique && ControlRepeat())
                GameObject.DestroyImmediate(gameObject);
            else
                DontDestroyOnLoad(gameObject);
        }
        #endregion

        #region Init Methods
        /// <summary>
        //  The method check if in the scene there are two gameObject whit the same name
        /// </summary>
        private bool ControlRepeat()
        {
            return gameObject.FindNotMe<DontDestroyOnLoadManager>() != null;
        }
        #endregion
    }
}
