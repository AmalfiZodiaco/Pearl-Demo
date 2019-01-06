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
            currentLevel = sceneSystem.ReturnLevel();
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
        /// <summary>
        /// Starts a new scene
        /// </summary>
        /// <param name = "newLevel"> The new scene to be enabled</param>
        public void NewLevel(SceneEnum newLevel)
        {
            currentLevel = newLevel;
            sceneSystem.NewScene(newLevel);
        }

        /// <summary>
        /// Enables or disables the mouse
        /// </summary>
        public void EnableMouse(bool enable)
        {
            cursorSystem.Enable = enable;
        }
        #endregion
    }
}
