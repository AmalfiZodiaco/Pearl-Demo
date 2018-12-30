using System.Collections.Generic;
using UnityEngine;
using System;
using it.amalfi.Pearl.events;
using it.amalfi.Pearl.multitags;
using it.amalfi.Pearl;
using it.amalfi.Pearl.input;
using it.demo.power;
using System.Reflection;

namespace it.demo.player
{
    [RequireComponent(typeof(Animator))]
    [RequireComponent(typeof(SpriteRenderer))]
    public class PlayerManager : LogicalManager
    {
        private PlayerMovementComponent mvComponent;

        #region Inspector Fields
        [Header("Movement Settings")]
        [Range(5, 10)]
        [SerializeField]
        private float speed = 5;
        [SerializeField]
        private Vector2 direction = Vector2.right;
        [Range(2, 10)]
        [SerializeField]
        private int countOldDirection = 4;

        [Header("Status Settings")]
        [SerializeField]
        private byte maxHealth = 100;

        [Header("Powers Settings")]
        [SerializeField]
        private PowerStruct power1;
        [SerializeField]
        private PowerStruct power2;
        #endregion

        #region Unity CallBacks
        protected override void OnAwake()
        {
            mvComponent = GetLogicalComponent<PlayerMovementComponent>();
            ForceManagerSystem forceManager = SingletonPool.Get<ForceManagerSystem>();
            forceManager.AddManagerForce(gameObject.GetInstanceID(), GetComponent<Rigidbody2D>(), gameObject);
        }

        private void Start()
        {
            SendCreatePlayer();
        }
        #endregion

        #region Init Methods
        protected override void CreateComponents()
        {
            listComponents = new Dictionary<Type, LogicalComponent>
            {
                { typeof(PlayerPowerComponent), new PlayerPowerComponent(power1, power2) },
                { typeof(PlayerAnimationComponent), new PlayerAnimationComponent(GetComponent<SpriteRenderer>(), GetComponent<Animator>()) },
                { typeof(PlayerMovementComponent), new PlayerMovementComponent(gameObject.GetInstanceID(), speed, direction, countOldDirection) },
                { typeof(PlayerStatsComponent), new PlayerStatsComponent(maxHealth) },
            };
        }
        #endregion

        #region Interface Methods

        #region Add/Remove Methods in Events
        protected override void SubscribEvents()
        {
            EventsManager.AddMethod<float>(EventAction.SendHealth, ReceiveHealth);

            EventsManager.AddMethod<Vector2>(EventAction.GetInputMovement, ReceiveInputMovement);
            EventsManager.AddMethod<EventAction>(EventAction.GetInputUse, ReceiveInputPower);
            EventsManager.AddMethod<EventAction>(EventAction.GetInputAttack, ReceiveInputPower);
        }

        protected override void RemoveEvents()
        {
            EventsManager.RemoveMethod<float>(EventAction.SendHealth, ReceiveHealth);

            EventsManager.RemoveMethod<Vector2>(EventAction.GetInputMovement, ReceiveInputMovement);
            EventsManager.RemoveMethod<EventAction>(EventAction.GetInputUse, ReceiveInputPower);
            EventsManager.RemoveMethod<EventAction>(EventAction.GetInputAttack, ReceiveInputPower);
        }
        #endregion

        #region Receive Methods
        private void ReceiveHealth(float actualHealth)
        {
            DoChangeHealth(actualHealth);
        }

        private void ReceiveInputMovement(Vector2 input)
        {
            DoSetMovement(input);
        }

        private void ReceiveInputPower(EventAction action)
        {
            Vector2 direction = GetLogicalComponent<PlayerMovementComponent>().Direction;
            GetLogicalComponent<PlayerPowerComponent>().UsePower(transform, action, direction);
        }
        #endregion

        #region Send Methods
        public void SendCreatePlayer()
        {
            EventsManager.CallEvent(EventAction.CreatePlayer, transform);
        }

        public void SendHealth(float actualHealth)
        {
            EventsManager.CallEvent(EventAction.SendHealth, actualHealth);
        }
        #endregion

        #endregion

        #region Logical Methods
        private void DoChangeHealth(float actualHealth)
        {
            GetLogicalComponent<PlayerStatsComponent>().OnChangeHealth(actualHealth);
        }

        private void DoSetMovement(Vector2 input)
        {
            mvComponent.SetMovement(input);
            GetLogicalComponent<PlayerAnimationComponent>().SetMovementVar(mvComponent.Direction, input != Vector2.zero);
        }

        private void DoPower(EventAction actionInput)
        {
            Vector2 direction = mvComponent.Direction;
            GetLogicalComponent<PlayerPowerComponent>().UsePower(transform, actionInput, direction);
        }
        #endregion
    }
}
