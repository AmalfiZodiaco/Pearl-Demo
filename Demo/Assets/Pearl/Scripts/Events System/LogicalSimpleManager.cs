using UnityEngine;

namespace it.amalfi.Pearl.events
{
    public abstract class LogicalSimpleManager : MonoBehaviour
    {
        #region Unity CallBacks
        protected virtual void Awake()
        {
            SubscribEvents();
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
        protected virtual void SubscribEvents()
        {

        }

        protected virtual void RemoveEvents()
        {

        }
        #endregion
    }
}