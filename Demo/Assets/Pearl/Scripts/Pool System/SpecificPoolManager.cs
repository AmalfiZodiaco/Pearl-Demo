using System.Collections.Generic;
using UnityEngine;
using Pearl.multitags;
using Pearl;

namespace Pearl.pools
{
    /// <summary>
    /// The class tha manages the specific pool
    /// </summary>
    public class SpecificPoolManager
    {
        #region Private Fields
        /// <summary>
        /// The list contains the disable components
        /// </summary>
        private List<GameObject> listDisable;
        /// <summary>
        /// The list contains the enable components
        /// </summary>
        private List<GameObject> listAble;
        /// <summary>
        /// An auxiliary gameobject 
        /// </summary>
        private GameObject auxGameobject;
        /// <summary>
        /// The prefab component of the pool
        /// </summary>
        private GameObject prefab;
        /// <summary>
        /// The max number of object of that prefab
        /// </summary>
        private int max;
        #endregion

        #region Properties
        /// <summary>
        /// The Gameobject that contains all specific components on scene
        /// </summary>
        public GameObject Pool { get; private set; }
        #endregion

        #region Constructor
        /// <summary>
        /// Setting variables and create pool
        /// </summary>
        /// <param name = "pool">The Gameobject that contains all specific components on scene</param>
        /// <param name = "prefab"> The object that represents the prefab</param>
        /// <param name = "max"> The maximum number of the components of the pool</param>
        public SpecificPoolManager(GameObject pool, GameObject prefab, int max)
        {
            listDisable = new List<GameObject>();
            listAble = new List<GameObject>();
            this.Pool = pool;
            CreatePool(prefab, max);
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Disable all GameObjects
        /// </summary>
        public void AllDisable()
        {
            for (int i = 0; i < listAble.Count; i++)
            {
                Disable(listAble[i]);
            }
        }

        /// <summary>
        /// Create or Recycle GameObject
        /// </summary>
        /// <param name = "position"> The position where the prefab will spawn</param>
        /// <param name = "rotation"> The rotation where the prefab will spawn</param>
        /// <param name = "parent"> The parent of new object</param>
        public GameObject InstantiateObject(Vector3 position, Quaternion rotation, Transform parent)
        {
            if (listAble.Count == this.max)
                Disable(listAble[0]);

            if (parent != null)
                position += parent.position;

            return RecycleObject(position, rotation);
        }

        /// <summary>
        /// Disable GameObject
        /// </summary>
        /// <param name = "obj"> The gameobject that must be disabled</param>
        public void Disable(GameObject obj)
        {
            obj.SetActive(false);
            listDisable.Add(obj);
            listAble.Remove(obj);
        }
        #endregion

        #region Private Methods
        /// <summary>
        /// Crate the pools with all the elements
        /// </summary>
        /// <param name = "prefab"> The object that represents the prefab</param>
        /// <param name = "max"> The maximum number of the components of the pool</param>
        public void CreatePool(GameObject prefab, int max)
        {
            this.max = max;
            this.prefab = prefab;
            for (int i = 0; i < this.max; i++)
            {
                auxGameobject = IstantiateNewObject(prefab.name);
                auxGameobject.AddOnlyOneComponent<MultiTags>();
                auxGameobject.AddTags(Tags.Pool);
                Disable(auxGameobject);
            }
        }

        /// <summary>
        /// Recycle GameObject
        /// </summary>
        /// <param name = "position"> The position where the prefab will spawn</param>
        /// <param name = "rotation"> The rotation where the prefab will spawn</param>
        private GameObject RecycleObject(Vector3 pos, Quaternion rotation)
        {
            auxGameobject = listDisable[0];
            auxGameobject = ResetElementInWorld(auxGameobject, pos, rotation);
            auxGameobject.SetActive(true);
            listDisable.Remove(auxGameobject);
            listAble.Add(auxGameobject);

            return auxGameobject;
        }

        /// <summary>
        /// Sect attributes for the recycled object
        /// </summary>
        /// <param name = "aux"> The recycled object</param>
        /// <param name = "position"> The position where the prefab will spawn</param>
        /// <param name = "rotation"> The rotation where the prefab will spawn</param>
        private GameObject ResetElementInWorld(GameObject aux, Vector3 position, Quaternion quat)
        {
            aux.transform.position = position;
            aux.transform.rotation = quat;
            if (aux.GetComponent<Rigidbody>())
                aux.GetComponent<Rigidbody>().velocity = Vector3.zero;
            if (aux.GetComponent<Rigidbody2D>())
                aux.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
            return aux;
        }

        /// <summary>
        /// Create GameObject in the pool
        /// </summary>
        /// <param name = "name"> The name of Gameobject</param>
        private GameObject IstantiateNewObject(string name)
        {
            return GameObjectExtend.Instantiate(prefab, name, Vector3.zero, Quaternion.identity, Pool.transform);
        }
        #endregion
    }
}

