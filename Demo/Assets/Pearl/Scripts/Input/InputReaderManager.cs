using System.Collections.Generic;
using UnityEngine;
using Pearl.events;
using System;

namespace Pearl.input
{
    /// <summary>
    /// The class that manages the input. In the update, it calls the 
    /// component that actually reads the input based on the type of controller
    /// </summary>
    public class InputReaderManager : LogicalManager
    {
        #region private fields
        private ControllerEnum controller = ControllerEnum.PC;
        private const string nullable = "null";
        private InputReaderComponent inputComponent;
        #endregion

        #region Unity Callbacks
        protected override void OnAwake()
        {
            inputComponent = GetLogicalComponent<InputReaderComponent>();
        }

        private void Update()
        {
            switch (controller)
            {
                case ControllerEnum.PC:
                    inputComponent.DoUpdateKeyboard();
                    break;
                case ControllerEnum.JOYSTICK:
                    inputComponent.DoUpdateJoystick();
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
        protected override void SubscribeEvents()
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
            DoReceivePause(pause);
        }
        #endregion

        #endregion

        #region Logical methods
        private void DoReceivePause(bool pause)
        {
            GetLogicalComponent<InputReaderComponent>().Pause = pause;
        }
        #endregion
    }
}
