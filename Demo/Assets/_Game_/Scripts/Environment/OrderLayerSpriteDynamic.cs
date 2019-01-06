using UnityEngine;

namespace Pearl.Demo.graphic
{
    public class OrderLayerSpriteDynamic : OrderLayerSpriteStatic
    {
        #region Private Fields
        private SpriteRenderer spriteRender;
        #endregion

        #region Unity CallBacks
        protected override void OnAwake()
        {
            spriteRender = GetComponent<SpriteRenderer>();
        }

        private void Update()
        {
            ChangeOrderLayer();
        }
        #endregion

        #region Override Methods
        protected override void ChangeOrderLayer()
        {
            spriteRender.sortingOrder = Mathf.RoundToInt(transform.position.y * granularity) * -1;
        }
        #endregion
    }
}
