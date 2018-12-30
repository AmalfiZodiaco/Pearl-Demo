using it.amalfi.Pearl.events;
using UnityEngine;
using System;

namespace it.amalfi.Pearl.input
{
    public class InputReaderComponent : InputReaderAbstractComponent
    {
        #region Public Methods
        public override void UpdateKeyboard()
        {
            if (Input.GetButtonDown("Submit"))
                EventsManager.CallEvent(EventAction.GetInputEntryMenu);
            if (Input.GetButtonDown("Cancel"))
                EventsManager.CallEvent(EventAction.GetInputReturnUI);

            if (!isPause)
            {
                EventsManager.CallEvent(EventAction.GetInputMovement, GetMovement());
                if (Input.GetButtonDown("Fire1"))
                    EventsManager.CallEvent(EventAction.GetInputUse, EventAction.GetInputUse);

                if (Input.GetButtonDown("Fire2"))
                    EventsManager.CallEvent(EventAction.GetInputAttack, EventAction.GetInputAttack);
            }
        }


        public override void UpdateJoystick()
        {
        }
        #endregion

        #region Private Methods
        private Vector2 GetMovement()
        {
            return new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        }
        #endregion
    }
}
