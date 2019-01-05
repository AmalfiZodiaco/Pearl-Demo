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
            for (int i = 0; i < prefabs.Length; i++)
            {
                PoolManager.Create(prefabs[i].Obj, prefabs[i].NumberElementsInPool);
            }
        }
        #endregion

        /// <summary>
        /// Auxiliary class that associates a pooled prefab with the maximum number of instantiations
        /// </summary>
        [Serializable]
        public struct ElementPool
        {
            /// <summary>
            /// The pooled preab
            /// </summary>
            [SerializeField]
            private GameObject obj;
            /// <summary>
            /// The maximum number of instantiations
            /// </summary>
            [SerializeField]
            private int numberElementsInPool;

            public GameObject Obj { get {return obj;} }
            public int NumberElementsInPool { get { return numberElementsInPool; } }
        }
    }
}
