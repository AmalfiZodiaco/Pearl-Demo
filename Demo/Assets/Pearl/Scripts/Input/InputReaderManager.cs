using System.Collections.Generic;
using UnityEngine;
using it.amalfi.Pearl.events;
using System;

namespace it.amalfi.Pearl.input
{
    public class InputReaderManager : LogicalManager
    {
        #region private fields
        private ControllerEnum controller = ControllerEnum.PC;
        private const string nullable = "null";
        #endregion

        #region Unity Callbacks
        private void Update()
        {
            switch (controller)
            {
                case ControllerEnum.PC:
                    GetLogicalComponent<InputReaderComponent>().UpdateKeyboard();
                    break;
                case ControllerEnum.JOYSTICK:
                    GetLogicalComponent<InputReaderComponent>().UpdateJoystick();
                    break;
            }
        }
        #endregion

        #region Init Methods
        protected override void CreateComponents()
        {
            listComponents = new Dictionary<Type, LogicalComponent>
            {
                { typeof(InputReaderComponent), new InputReaderComponent() },
            };
        }
        #endregion

        #region Interface Methods

        #region Add/Remove method in events
        protected override void SubscribEvents()
        {
            EventsManager.AddMethod<bool>(EventAction.CallPause, ReceivePause);
        }

        protected override void RemoveEvents()
        {
            EventsManager.RemoveMethod<bool>(EventAction.CallPause, ReceivePause);
        }
        
        #endregion

        #region Receive Methods
        private void ReceivePause(bool pause)
        {
            GetLogicalComponent<InputReaderComponent>().Pause = pause;
        }
        #endregion

        #endregion

        #region Private Methods
        private static InputReaderManager GetIstance()
        {
            return SingletonPool.Get<InputReaderManager>();
        }
        #endregion
    }
}
