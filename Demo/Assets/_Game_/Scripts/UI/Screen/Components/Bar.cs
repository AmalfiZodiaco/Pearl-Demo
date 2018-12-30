using UnityEngine;
using UnityEngine.UI;

namespace it.demo.UI
{
    [RequireComponent(typeof(RectTransform))]
    public class Bar : MonoBehaviour
    {
        #region Protected Fields
        protected RectTransform rect;
        #endregion

        #region Private Fields
        private float maxSixe;
        #endregion

        #region Unity CallBacks
        private void Awake()
        {
            rect = GetComponent<RectTransform>();
            maxSixe = rect.sizeDelta.x;
        }
        #endregion

        #region Public Methods

        public void SetBar(float actualValue)
        {
            rect.sizeDelta = new Vector2(maxSixe * actualValue, rect.sizeDelta.y);
        }
        #endregion
    }
}
