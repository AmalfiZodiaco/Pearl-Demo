using it.amalfi.Pearl.events;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace it.amalfi.Pearl
{
    public static class SingletonPool
    {
        #region Protected Fields
        private static Dictionary<Type, MonoBehaviour> singletons;
        private static readonly object _lock = new object();
        #endregion

        #region Constuctors
        static SingletonPool()
        {
            singletons = new Dictionary<Type, MonoBehaviour>();
        }
        #endregion

        #region Public Methods
        public static T Get<T>() where T : LogicalSimpleManager
        {
            T instance;
            if (!singletons.ContainsKey(typeof(T)))
                FindAndAdd<T>();
            else
            {
                instance = (T)singletons[typeof(T)];
                if (instance == null)
                    FindAndAdd<T>();
            }
            return (T)singletons[typeof(T)];
        }
        #endregion

        #region Private Methods
        private static void Add<T>(T instance) where T : MonoBehaviour
        {
            singletons.Update(typeof(T), instance);
        }

        private static void FindAndAdd<T>() where T : MonoBehaviour
        {
            T instance = FindInstance<T>();
            Add<T>(instance);
        }

        private static T FindInstance<T>() where T : MonoBehaviour
        {
            lock (_lock)
            {
                T[] types = (T[])GameObject.FindObjectsOfType(typeof(T));
                if (types.Length > 1)
                {
                    Debug.LogError("[Singleton] Something went really wrong " +
                        " - there should never be more than 1 singleton!");
                    return null;
                }
                else if (types.Length < 1)
                    return null;
                else
                    return types[0];
            }
        }
        #endregion
    }
}
