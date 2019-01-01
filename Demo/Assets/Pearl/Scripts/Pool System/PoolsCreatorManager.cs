using it.amalfi.Pearl.events;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace it.amalfi.Pearl.pools
{
    /// <summary>
    /// This class creates all the pools in the scene
    /// </summary>
    public class PoolsCreatorManager : LogicalSimpleManager
    {
        #region Inspector Fields
        /// <summary>
        /// The prefab of each pool
        /// </summary>
        [SerializeField]
        private ElementPool[] prefabs;
        #endregion

        #region Unity CallBacks
        protected override void OnAwake()
        {
            CreatePool();
        }
        #endregion

        #region Private Methods
        private void CreatePool()
        {
            foreach (ElementPool elementPool in prefabs)
            {
                PoolManager.Create(elementPool.Obj, elementPool.NumberElementsInPool);
            }
        }
        #endregion

        [Serializable]
        public struct ElementPool
        {
            [SerializeField]
            private GameObject obj;
            [SerializeField]
            private int numberElementsInPool;

            public GameObject Obj { get {return obj;} }
            public int NumberElementsInPool { get { return numberElementsInPool; } }
        }
    }
}
