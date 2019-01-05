using UnityEngine.SceneManagement;
using it.amalfi.Pearl.events;

namespace it.amalfi.Pearl.game
{
    /// <summary>
    /// A class that manages the scene change
    /// </summary>
    public class SceneSystemComponent : LogicalComponent
    {
        #region Constructors
        public SceneSystemComponent()
        {
            ActiveLevelSystem();
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// A method called when the manager is destroyed
        /// </summary>
        public override void OnDestroy()
        {
            SceneManager.sceneLoaded -= ManageNewScene;
        }

        /// <summary>
        /// The method atcive the level system
        /// </summary>
        public void ActiveLevelSystem()
        {
            SceneManager.sceneLoaded += ManageNewScene;
        }

        /// <summary>
        /// The method accesses a new scene through the SceneEnum enumerator
        /// </summary>
        /// <param name = "newLevel">The new scene in enumerator</param>
        public void NewScene(SceneEnum newLevel)
        {
            SceneManager.LoadScene(newLevel.ToString());
        }

        /// <summary>
        /// The method returns enumerator of actual scene
        /// </summary>
        public SceneEnum ReturnLevel()
        {
            return GetActualLevel(SceneManager.GetActiveScene());
        }
        #endregion

        #region Private Methods
        private SceneEnum GetActualLevel(Scene scene)
        {
            return EnumExtend.ParseEnum<SceneEnum>(scene.name);
        }

        /// <summary>
        /// the method is activated at the event "SceneManager.sceneLoaded": 
        /// the method starts to the event that activates when there is a new scene.
        /// </summary>
        private void ManageNewScene(Scene scene, LoadSceneMode load)
        {
            EventsManager.CallEvent(EventAction.NewScene, GetActualLevel(scene));
        }
        #endregion
    }
}
