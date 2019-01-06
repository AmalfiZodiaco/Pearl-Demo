using UnityEngine.SceneManagement;
using Pearl.events;

namespace Pearl.game
{
    /// <summary>
    /// A class that manages the scene change
    /// </summary>
    public class SceneSystemComponent : LogicalComponent
    {
        #region Constructors
        public SceneSystemComponent()
        {
            SceneManager.sceneLoaded += ReceiveNewScene;
        }
        #endregion

        #region Override Methods
        /// <summary>
        /// A method called when the manager is destroyed
        /// </summary>
        public override void OnDestroy()
        {
            SceneManager.sceneLoaded -= ReceiveNewScene;
        }
        #endregion

        #region Obey Methods
        /// <summary>
        /// The method accesses a new scene through the name of scene
        /// </summary>
        /// <param name = "newLevel">The new scene in string</param>
        public void ObeyEnterNewLevel(string newLevel)
        {
            SceneManager.LoadScene(newLevel);
        }

        /// <summary>
        /// The method accesses a new scene through the SceneEnum enumerator
        /// </summary>
        /// <param name = "newLevel">The new scene in enumerator</param>
        public void ObeyEnterNewLevel(SceneEnum newLevel)
        {
            SceneManager.LoadScene(newLevel.ToString());
        }

        /// <summary>
        /// The method returns enumerator of current scene
        /// </summary>
        public SceneEnum ObeyReturnCurrentLevel()
        {
            return GetActualLevel(SceneManager.GetActiveScene().name);
        }
        #endregion

        #region Private Methods
        private SceneEnum GetActualLevel(string scene)
        {
            return EnumExtend.ParseEnum<SceneEnum>(scene);
        }

        /// <summary>
        /// the method is activated at the event "SceneManager.sceneLoaded": 
        /// the method starts to the event that activates when there is a new scene.
        /// </summary>
        private void ReceiveNewScene(Scene scene, LoadSceneMode load)
        {
            EventsManager.CallEvent(EventAction.NewScene, GetActualLevel(scene.name));
        }
        #endregion
    }
}
