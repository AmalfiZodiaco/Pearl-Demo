using System.Collections.Generic;
using UnityEngine;

namespace Pearl.pools
{
    /// <summary>
    /// This class manages every pool of each prefab
    /// </summary>
    public static class PoolManager
    {
        #region Private Fields
        /// <summary>
        /// This list contains all the pools
        /// </summary>
        private static Dictionary<string, SpecificPoolManager> listPool = new Dictionary<string, SpecificPoolManager>();
        #endregion

        #region Public Methods
        /// <summary>
        /// Create specific pool
        /// </summary>
        /// <param name = "prefab"> The prefab for pool</param>
        public static void Create(GameObject prefab, int max = 10)
        {
            Debug.Assert(prefab != null);

            SetPool(prefab, max);
        }

        /// <summary>
        /// Recycles a element for specific Pool
        /// </summary>
        /// <param name = "prefab"> The prefab for pool</param>
        public static GameObject Instantiate(GameObject prefab)
        {
            Debug.Assert(prefab != null);

            return listPool[prefab.name].InstantiateObject(Vector3.zero, Quaternion.identity, null);
        }

        /// <summary>
        /// Recycles a element for specific Pool
        /// </summary>
        /// <param name = "prefab"> The prefab for pool</param>
        /// <param name = "position"> The position where the prefab will spawn</param>
        /// <param name = "rotation"> The rotation where the prefab will spawn</param>
        /// <param name = "parent"> The parent of new object</param>
        public static GameObject Instantiate(GameObject prefab, Vector3 position, Quaternion quat, Transform parent = null)
        {
            Debug.Assert(prefab != null);

            return listPool[prefab.name].InstantiateObject(position, quat, parent);
        }

        /// <summary>
        /// Destroy/Disable for specific Pool
        /// </summary>
        /// <param name = "obj">The specific pool that will be destroyed</param>
        public static void Destroy(GameObject obj)
        {
            if (listPool.ContainsKey(obj.name))
                listPool[obj.name]?.Disable(obj);
        }

        /// <summary>
        /// Remove specific Pool for obj
        /// </summary>
        /// <param name = "prefab"> The prefab that contain the pool</param>
        public static void RemovePool(GameObject prefab)
        {
            Debug.Assert(prefab != null);

            if (listPool.ContainsKey(prefab.name))
            {
                GameObject.Destroy(listPool[prefab.name].Pool);
                listPool.Remove(prefab.name);
            }
        }

        /// <summary>
        /// Remove all Pools
        /// </summary>
        public static void RemoveAllPool()
        {
            listPool.Clear();
        }
        #endregion

        #region Private Methods
        /// <summary>
        /// Set specific Pool for obj
        /// </summary>
        /// <param name = "prefab"> The prefab that will contain the pool</param>
        /// <param name = "max"> The maximum number of the components of the pool</param>
        private static SpecificPoolManager SetPool(GameObject obj, int max = 10)
        {
            Debug.Assert(max >= 0 && obj != null);

            if (listPool.ContainsKey(obj.name))
                return listPool[obj.name];
            GameObject auxGameObject = new GameObject(obj.name + "Pool");
            auxGameObject.transform.parent = GameObject.FindObjectOfType<PoolsCreatorManager>().gameObject.transform;
            listPool[obj.name] = new SpecificPoolManager(auxGameObject, obj, max);
            return listPool[obj.name];
        }
        #endregion
    }
}


