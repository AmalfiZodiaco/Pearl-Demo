using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;
using Pearl.events;
using System;

namespace Pearl.Demo.UI
{
    public partial class ScreenUIManager : LogicalManager
    {
        #region Inspector Fields
        [SerializeField]
        private bool enableBarsInitial = false;
        [SerializeField]
        private float timeForDisableBars = 3f;
        #endregion

        #region Subscrive Event
        protected override void SubscribeEvents()
        {
            EventsManager.AddMethod<float>(EventAction.SendHealth, ReceiveHealth);
        }

        protected override void RemoveEvents()
        {
            EventsManager.RemoveMethod<float>(EventAction.SendHealth, ReceiveHealth);
        }
        #endregion

        #region Override Methods
        protected override void CreateComponents()
        {
            GameObject healthBar = transform.Find("HealthBar").gameObject;
            listComponents = new Dictionary<Type, LogicalComponent>
            {
                { typeof(BarComponent), new BarComponent(healthBar, enableBarsInitial) },
            };
        }
        #endregion

        #region Receive Methods
        private void ReceiveHealth(float actualHealth)
        {
            GetLogicalComponent<BarComponent>().OnScreenHealthBar(actualHealth);
            Invoke("OffScreenBars", timeForDisableBars);
        }
        #endregion

        #region Do Actions
        private void OffScreenBars()
        {
            GetLogicalComponent<BarComponent>().OffScreenBars();
        }
        #endregion
    }
}
