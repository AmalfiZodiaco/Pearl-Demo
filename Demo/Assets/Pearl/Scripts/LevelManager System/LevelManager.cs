using it.amalfi.Pearl.events;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace it.amalfi.Pearl.level
{
    /// <summary>
    /// The class that implements the generic level manager.
    /// Each level has this class or a derived class (specific by level)
    /// </summary>
    public class LevelManager : LogicalManager
    {
        #region Inspector fields
        /// <summary>
        /// The pool creator prefab.The prefab is used to instantiate and store all objects in the pool
        /// </summary>
        [SerializeField]
        private GameObject poolPrefab;
        #endregion

        private PauseComponents pauseComponent;

        #region Unity CallBacks
        protected override void OnAwake()
        {
            if (poolPrefab)
            {
                GameObject obj = GameObject.Instantiate(poolPrefab);
                obj.name = obj.name.Split('(')[0];
            }
            pauseComponent = GetLogicalComponent<PauseComponents>();

        }
        #endregion

        #region Init Methods
        protected override void CreateComponents()
        {
            listComponents = new Dictionary<Type, LogicalComponent>
            {
                { typeof(PauseComponents), new PauseComponents() },
            };
        }
        #endregion

        #region Add/Remove methods in events
        protected override void SubscribeEvents()
        {
            EventsManager.AddMethod<bool>(EventAction.CallPause, ReceivePause);
        }

        protected override void RemoveEvents()
        {
            EventsManager.RemoveMethod<bool>(EventAction.CallPause, ReceivePause);
        }
        #endregion

        #region Receive Methods
        private void ReceivePause(bool pause)
        {
            pauseComponent.PauseControl(pause);
        }
        #endregion
    }
}
