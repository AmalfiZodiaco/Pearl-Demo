using Pearl.events;
using UnityEngine;

namespace Pearl.debug
{
    /// <summary>
    /// The static class that is able to debug even in Build version.
    /// </summary>
    public static class DebugExtend
    {
        #region Public Static Methods
        /// <summary>
        /// Writes Log in the console or build UI
        /// </summary>
        /// <param name = "obj">The object that will be written</param>
        public static void Log(object obj)
        {
            #if UNITY_EDITOR
                Debug.Log(obj);
                return;
            #endif

            #if UNITY_STANDALONE
                #pragma warning disable CS0162
                LogBuild(obj);
                #pragma warning restore CS0162 
            #endif
        }
        #endregion

        #region Private Methods
        private static void LogBuild(object obj)
        {
            if (GameObject.FindObjectOfType<DebugBuildManager>() == null)
                CreateDebugBuild();
            EventsManager.GetIstance<DebugBuildManager>().Log(obj);
        }

        private static void CreateDebugBuild()
        {
            GameObjectExtend.Instantiate(Resources.Load<GameObject>("DebugBuild"), "DebugUI");
        }
        #endregion
    }
}
