using System.Collections.Generic;
using UnityEngine;
using System;
using Pearl.events;
using Pearl.audio;

namespace Pearl.game
{
    /// <summary>
    /// The general gamemanager. It will be the father of every game-specific game manager
    /// </summary>
    public class GameManager : LogicalManager
    {
        #region Inspector Fields
        /// <summary>
        /// The version of the game
        /// </summary>
        [SerializeField]
        [Tooltip("Game Version")]
        private string gameVersion = "1";
        /// <summary>
        /// The current scene in the game
        /// </summary>
        [SerializeField]
        [Tooltip("The actual level")]
        private SceneEnum currentLevel;
        /// <summary>
        ///The mouse is enabled or disabled
        /// </summary>
        [SerializeField]
        [Tooltip("Enable or disable mouse")]
        private bool enableMouse;
        /// <summary>
        ///if the Boolean is true, the game is tested in debug mode
        /// </summary>
        [SerializeField]
        [Tooltip("Is the debug mode?")]
        private bool debugMode;
        #endregion

        #region Propieties
        /// <summary>
        /// The version of the game
        /// </summary>
        public SceneEnum ActualLevel { get { return currentLevel; } }
        /// <summary>
        /// The current scene in the game
        /// </summary>
        public string GameVersion { get { return gameVersion; } }
        #endregion

        #region Protected Fields
        protected SceneSystemComponent sceneSystem;
        protected CursorEnableComponent cursorSystem;
        #endregion

        #region Unity CallBacks
        protected override void OnAwake()
        {
            sceneSystem = GetLogicalComponent<SceneSystemComponent>();
            cursorSystem = GetLogicalComponent<CursorEnableComponent>();
            currentLevel = sceneSystem.ObeyReturnCurrentLevel();
            transform.Find("FrameRateUI").gameObject.SetActive(debugMode);
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

        #region Interface Methods

        #region Singleton Methods
        /// <summary>
        /// Starts a new scene
        /// </summary>
        /// <param name = "newLevel"> The new scene to be enabled</param>
        public void EnterNewLevel(SceneEnum newLevel)
        {
            DoEnterNewLevel(newLevel);
        }

        /// <summary>
        /// Starts a new scene
        /// </summary>
        /// <param name = "newLevel"> The new scene to be enabled</param>
        public void EnterNewLevel(string newLevel)
        {
            DoEnterNewLevel(newLevel);
        }

        /// <summary>
        /// Enables or disables the mouse
        /// </summary>
        public void EnableMouse(bool enable)
        {
            DoEnableMouse(enable);
        }
        #endregion

        #region Add/Remove Methods
        protected override void SubscribeEvents()
        {
            EventsManager.AddMethod<SceneEnum>(EventAction.NewScene, ReceiveCurrentLevel);
        }

        protected override void RemoveEvents()
        {
            EventsManager.RemoveMethod<SceneEnum>(EventAction.NewScene, ReceiveCurrentLevel);
        }
        #endregion

        #region Receive Methods
        private void ReceiveCurrentLevel(SceneEnum newScene)
        {
            DoCurrentLevel(newScene);
        }
        #endregion

        #endregion

        #region Logical Methods
        private void DoCurrentLevel(SceneEnum newScene)
        {
            currentLevel = newScene;
        }

        private void DoEnterNewLevel(SceneEnum newLevel)
        {
            sceneSystem.ObeyEnterNewLevel(newLevel);
        }

        private void DoEnterNewLevel(string newLevel)
        {
            sceneSystem.ObeyEnterNewLevel(newLevel);
        }

        private void DoEnableMouse(bool enable)
        {
            cursorSystem.Enable = enable;
        }
        #endregion
    }
}
