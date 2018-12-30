using System.Collections.Generic;
using System;

namespace it.amalfi.Pearl.events
{
    public abstract class LogicalManager : LogicalSimpleManager
    {
        #region Protected Fields
        protected Dictionary<Type, LogicalComponent> listComponents;
        #endregion

        #region Unity CallBacks
        protected sealed override void Awake()
        {
            listComponents = new Dictionary<Type, LogicalComponent>();
            CreateComponents();
            base.Awake();
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
            foreach (LogicalComponent component in listComponents.Values)
            {
                component.OnDestroy();
            }
        }
        #endregion

        #region Protected Methods
        protected  T GetLogicalComponent<T>() where T : LogicalComponent
        {
            return (T)listComponents[typeof(T)];
        }
        #endregion

        #region Abstract Methods
        protected abstract void CreateComponents();
        #endregion
    }

}