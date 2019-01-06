using System.Collections.Generic;
using UnityEngine;
using Pearl.actionTrigger;
using Pearl.events;
using Pearl.input;
using Pearl.Demo.power;

namespace Pearl.Demo.player
{
    public class PlayerPowerComponent : LogicalComponent
    {
        #region Private Fields
        private ComplexAction action;
        private Dictionary<EventAction, PowerStruct> actionToPower;
        private List<PowerStruct> powers;
        #endregion

        #region Constructors
        public PlayerPowerComponent(PowerStruct power1, PowerStruct power2)
        {
            actionToPower = new Dictionary<EventAction, PowerStruct>
            {
                { EventAction.GetInputUse, power1 },
                { EventAction.GetInputAttack, power2 }
            };
        }
        #endregion

        #region Public Method
        public void UsePower(Transform transform, EventAction ev, Vector2 direction)
        {
            action = Power.CreatePrefab(actionToPower[ev], transform, direction).GetComponent<ComplexAction>();
            action?.Add<byte>("damage", actionToPower[ev].damage);
            action?.Add<Vector2>("direction", direction);
            action?.SetAction();
        }
        #endregion
    }
}
