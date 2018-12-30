using UnityEngine;

namespace it.amalfi.Pearl.debug
{
    public static class DebugExtend
    {
        #region Public Methods
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
            SingletonPool.Get<DebugBuildManager>().Log(obj);
        }

        private static void CreateDebugBuild()
        {
            GameObject aux = Resources.Load<GameObject>("DebugBuild");
            aux = GameObject.Instantiate<GameObject>(aux);
            aux.name = "DebugUI";
        }
        #endregion
    }
}
