using UnityEngine;
using it.amalfi.Pearl.multitags;
using it.amalfi.Pearl.pools;
using System.Collections.Generic;

namespace it.amalfi.Pearl
{
    /// <summary>
    /// A class that extends the gameobject class
    /// </summary>
    public static class GameObjectExtend
    {
        #region Public Methods
        /// <summary>
        /// Instantiate a gameobject in a pool manager.
        /// </summary>
        /// <param name = "prefab"> The prefab for pool</param>
        /// <param name = "position"> The position where the prefab will spawn</param>
        /// <param name = "rotation"> The rotation where the prefab will spawn</param>
        /// <param name = "parent"> The parent of new object</param>
        public static GameObject InstantiatePool(GameObject prefab, Vector3 position, Quaternion rotation, Transform parent = null)
        {
            return PoolManager.Instantiate(prefab, position, rotation, parent);
        }

        /// <summary>
        /// Instantiate a gameobject with a name
        /// </summary>
        /// <param name = "prefab"> The prefab for pool</param>
        /// <param name = "name"> The name of the new object</param>
        /// <param name = "position"> The position where the prefab will spawn</param>
        /// <param name = "rotation"> The rotation where the prefab will spawn</param>
        /// <param name = "parent"> The parent of new object</param>
        public static GameObject Instantiate(GameObject prefab, string name, Vector3 position, Quaternion quat, Transform parent = null)
        {
            GameObject aux = GameObject.Instantiate(prefab, position, quat, parent);
            aux.name = name;
            return aux;
        }

        /// <summary>
        /// Destroy gameobject (if it's a pool manager, it behaves accordingly)
        /// </summary>
        /// <param name = "obj"> The object that will destroyed</param>
        public static void Destroy(GameObject obj)
        {
            if (obj.HasTags(Tags.Pool))
                PoolManager.Destroy(obj);
            else
                GameObject.Destroy(obj);
        }

        /// <summary>
        /// Returns a specific component found by a gameobject with the specific name
        /// </summary>
        /// <param name = "name">The name of the gameobject</param>
        public static T FindComponentInGameObject<T>(string name) where T : Component
        {
            return GameObject.Find(name)?.GetComponent<T>();
        }

        /// <summary>
        /// Returns a specific gameobject through multiple tags
        /// </summary>
        /// <param name = "only">This bool specifies whether the Gameobject should have only those tags</param>
        /// <param name = "tags">The tags that must have the GameObject</param>
        public static GameObject FindGameObjectWithTags(bool only, params Tags[] tags)
        {
            return MultiTagsManager.FindGameObjectWithMultiTags(only, tags);
        }

        /// <summary>
        /// Returns a specific gameobjects through multiple tags
        /// </summary>
        /// <param name = "only">This bool specifies whether the Gameobject should have only those tags</param>
        /// <param name = "tags">The tags that must have the object</param>
        public static GameObject[] FindGameObjectsWithTags(bool only, params Tags[] tags)
        {
            return MultiTagsManager.FindGameObjectsWithMultiTags(only, tags);
        }
        #endregion

        #region Extend Methods
        /// <summary>
        /// returns all the specific components in the scene except the the caller
        /// </summary>
        /// <param name = "obj">The object that have the components</param>
        public static T[] FindOtherObjectsOfType<T>(this GameObject obj) where T : MonoBehaviour
        {
            T[] names = GameObject.FindObjectsOfType<T>();
            List<T> list = new List<T>(names);
            list.Remove(list.Find(x => x.gameObject.GetInstanceID() == obj.GetInstanceID()));
            return list.ToArray();
        }

        /// <summary>
        /// Returns the first gameobject in the scene that has the same component as the calling one(except for the caller)
        /// </summary>
        /// <param name = "obj">The object that has the name to be searched</param>
        public static GameObject FindNotMe<T>(this GameObject obj) where T : MonoBehaviour
        {
            List<T> list = new List<T>(obj.FindOtherObjectsOfType<T>());
            return list.Find(x => x.name == obj.name)?.gameObject;
        }

        /// <summary>
        /// Adds only one component of the T type to the gameobject. 
        /// If the component already exists, it returns it.
        /// </summary>
        /// <param name = "obj">The subject in which the component will be added</param>
        public static T AddOnlyOneComponent<T>(this GameObject obj) where T : Component
        {
            T aux = obj.GetComponent<T>();
            if (!aux)
                return obj.AddComponent<T>();
            return aux;
        }
        #endregion
    }
}
