using it.amalfi.Pearl.events;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace it.amalfi.Pearl.level
{
    public class LevelManager : LogicalManager
    {
        [SerializeField]
        private GameObject poolPrefab;

        protected override void OnAwake()
        {
            if (poolPrefab)
            {
                GameObject obj = GameObject.Instantiate(poolPrefab);
                obj.name = obj.name.Split('(')[0];
            }
        }

        protected override void CreateComponents()
        {
            listComponents = new Dictionary<Type, LogicalComponent>
            {
                { typeof(PauseComponents), new PauseComponents() },
            };
        }

        protected override void RemoveEvents()
        {
            EventsManager.RemoveMethod<bool>(EventAction.CallPause, PauseControl);
        }

        protected override void SubscribEvents()
        {
            EventsManager.AddMethod<bool>(EventAction.CallPause, PauseControl);
        }

        private void PauseControl(bool pause)
        {
            GetLogicalComponent<PauseComponents>().PauseControl(pause);
        }
    }
}
