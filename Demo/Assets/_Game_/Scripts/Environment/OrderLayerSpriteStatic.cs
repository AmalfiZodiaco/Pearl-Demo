using it.amalfi.Pearl.events;
using UnityEngine;

namespace it.demo.graphic
{
    public class OrderLayerSpriteStatic : LogicalSimpleManager
    {
        #region Protected Fields
        protected const int granularity = 100;
        #endregion

        #region UnityCallBacks
        // Use this for initialization
        private void OnEnable()
        {
            ChangeOrderLayer();
        }
        #endregion

        #region Protected Methods
        protected virtual void ChangeOrderLayer()
        {
            GetComponent<SpriteRenderer>().sortingOrder = Mathf.RoundToInt(transform.position.y * granularity) * -1;
        }
        #endregion
    }
}
