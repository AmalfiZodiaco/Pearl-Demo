using it.amalfi.Pearl.multitags;
using UnityEngine;

namespace it.amalfi.Pearl.events
{
    /// <summary>
    /// The abstract father of all the simple Manager (manager without components)
    /// </summary>
    public abstract class LogicalSimpleManager : MonoBehaviour
    {
        #region Unity CallBacks
        protected virtual void Awake()
        {
            SubscribeEvents();
            OnAwake();
        }

        protected virtual void OnDestroy()
        {
            RemoveEvents();
        }
        #endregion

        #region Init Methods
        protected virtual void OnAwake()
        {

        }
        #endregion

        #region Add/Remove Methods
        protected virtual void SubscribeEvents()
        {

        }

        protected virtual void RemoveEvents()
        {

        }
        #endregion
    }
}