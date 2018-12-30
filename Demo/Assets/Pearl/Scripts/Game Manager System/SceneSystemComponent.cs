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
        // Use this for initialization
        public SceneSystemComponent()
        {
            ActiveLevelSystem();
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// The method deatcive the level system
        /// </summary>
        public void DeactiveLevelSystem()
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
        /// The method accesses a new scene through the enumerator
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
        /// <summary>
        /// The method returns enumerator of actual scene, if the scene there is't, the method return null.
        /// </summary>
        private SceneEnum GetActualLevel(Scene scene)
        {
            return EnumExtend.ParseEnum<SceneEnum>(scene.name);
        }

        /// <summary>
        /// the method is activated at the event "SceneManager.sceneLoaded": the method 
        /// activates itself to the event: it activates an event that indicates the
        /// beginning of a new scene.
        /// </summary>
        private void ManageNewScene(Scene scene, LoadSceneMode load)
        {
            EventsManager.CallEvent(EventAction.NewScene, GetActualLevel(scene));
        }
        #endregion
    }
}
