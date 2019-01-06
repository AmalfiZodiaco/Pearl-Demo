using Pearl.events;
using UnityEngine;

namespace Pearl.Demo.UI
{
    public class BarComponent : LogicalComponent
    {
        #region Private Fields
        private Bar healthBar;
        #endregion

        #region Constructors
        public BarComponent(GameObject gameObjectHealth, bool enableBarsInitial)
        {
            healthBar = gameObjectHealth.GetComponent<Bar>();
            gameObjectHealth.SetActive(enableBarsInitial);
        }
        #endregion

        #region Public Methods
        public void OnScreenHealthBar(float health)
        {
            OpenScreenBar();
            healthBar.SetBar(health);
        }

        public void OffScreenBars()
        {
            healthBar.gameObject.SetActive(false);
        }
        #endregion

        #region Private Methods
        private void OpenScreenBar()
        {
            healthBar.gameObject.SetActive(true);
        }
        #endregion
    }
}