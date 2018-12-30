using it.amalfi.Pearl.events;
using UnityEngine;

namespace it.amalfi.Pearl
{
    /// <summary>
    //  The class makes the unique and indestructible gameobject between the scenes
    /// </summary>
    public class DontDestroyOnLoadManager : LogicalSimpleManager
    {
        #region Inspector Fields
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
            GameObject aux = gameObject.FindNotMe<DontDestroyOnLoadManager>();
            return aux != null;
        }
        #endregion
    }
}
