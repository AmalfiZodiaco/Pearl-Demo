using System.Collections.Generic;
using UnityEngine;
using it.amalfi.Pearl.events;
using it.amalfi.Pearl;

namespace it.demo.player
{
    public class PlayerMovementComponent : LogicalComponent
    {
        #region Private Fields
        private float speed = 5;
        private Vector2 direction = Vector2.right;
        private int countOldDirection = 4;
        private int ID;
        private List<Vector2> oldDirection;
        private ForceManagerSystem forceManager;
        #endregion

        #region Propieties
        public Vector2 Direction { get { return direction; } }
        #endregion

        #region Constrctors
        public PlayerMovementComponent(int ID, float speed, Vector2 direction, int countOldDirection)
        {
            InitMovementVar(ID, speed, direction, countOldDirection);
            InitDirectionSystem();
            forceManager = EventsManager.GetIstance<ForceManagerSystem>();
        }
        #endregion

        private void InitMovementVar(int ID, float speed, Vector2 direction, int countOldDirection)
        {
            this.speed = speed;
            this.direction = direction;
            this.countOldDirection = countOldDirection;
            this.ID = ID;
        }

        #region Public Method
        public void SetMovement(Vector2 valueInput)
        {
            CalculateDirection(valueInput);
            forceManager.AddForce(ID, "movement", valueInput * speed);
        }
        #endregion

        #region DirectionSystem
        private void InitDirectionSystem()
        {
            oldDirection = new List<Vector2>();
            for (int i = 0; i < countOldDirection; i++)
            {
                oldDirection.Add(direction);
            }
        }

        private void CalculateDirection(Vector2 value)
        {
            if (value != Vector2.zero)
            {
                oldDirection.RemoveAt(0);
                oldDirection.Add(value);
            }

            if (value == Vector2.zero && oldDirection[0].x != 0 && oldDirection[0].y != 0)
                direction = oldDirection[0];
            else
                direction = oldDirection[oldDirection.Count - 1];
        }
        #endregion
    }
}
