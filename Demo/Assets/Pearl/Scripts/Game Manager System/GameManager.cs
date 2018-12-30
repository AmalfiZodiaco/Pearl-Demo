using System.Collections.Generic;
using UnityEngine;
using System;
using it.amalfi.Pearl.events;

namespace it.amalfi.Pearl.game
{
    public class GameManager : LogicalManager
    {
        #region Inspector Fields
        [SerializeField]
        [Tooltip("Game Version")]
        private string gameVersion = "1";
        [SerializeField]
        [Tooltip("The actual level")]
        private SceneEnum actualLevel;
        [SerializeField]
        [Tooltip("Enable or disable mouse")]
        private bool enableMouse;
        #endregion

        #region Propieties
        public SceneEnum ActualLevel { get { return actualLevel; } }

        public string GameVersion { get { return gameVersion; } }
        #endregion

        #region Unity CallBacks
        protected override void OnAwake()
        {
            actualLevel = GetLogicalComponent<SceneSystemComponent>().ReturnLevel();
        }
        #endregion

        #region Init Methods
        protected override void CreateComponents()
        {
            listComponents = new Dictionary<Type, LogicalComponent>
            {
                { typeof(SceneSystemComponent), new SceneSystemComponent() },
                { typeof(CursorEnableComponent), new CursorEnableComponent(enableMouse) },
            };
        }
        #endregion

        #region Public Singleton Methods
        public static void NewLevel(SceneEnum newLevel)
        {
            GameManager obj = GetIstance();
            obj.actualLevel = newLevel;
            obj.GetLogicalComponent<SceneSystemComponent>().NewScene(newLevel);
        }

        public void EnableMouse(bool enable)
        {
            GameManager obj = GetIstance();
            obj.GetLogicalComponent<CursorEnableComponent>().Enable = enable;
        }
        #endregion

        #region Private Methods
        protected static GameManager GetIstance()
        {
            return SingletonPool.Get<GameManager>();
        }
        #endregion
    }
}
