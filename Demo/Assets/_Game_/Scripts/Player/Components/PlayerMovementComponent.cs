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
        #endregion

        #region Propieties
        public Vector2 Direction { get { return direction; } }
        #endregion

        #region Constrctors
        public PlayerMovementComponent(int ID, float speed, Vector2 direction, int countOldDirection)
        {
            InitMovementVar(ID, speed, direction, countOldDirection);
            InitDirectionSystem();
        }
        #endregion

        private void InitMovementVar(int ID, float speed, Vector2 direction, int countOldDirection)
        {
            this.speed = speed;
            this.direction = direction;
            this.countOldDirection = countOldDirection;
            this.ID = ID;
        }

        #region Private Method
        public void SetMovement(Vector2 valueInput)
        {
            CalculateDirection(valueInput);
            SingletonPool.Get<ForceManagerSystem>().AddForce(ID, "movement", valueInput * speed);
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
            //calcola la direzione. Questo metodo è stato creato per avere la giusta direzione
            //anche se il giocatore rilascia i due pulsante in diagonale
            if (value != Vector2.zero)
            {
                oldDirection.RemoveAt(0);
                oldDirection.Add(value);
            }

            //caso di rilascio pulsante diagonali, prende la direzione di qualche frame precedente
            if (value == Vector2.zero && oldDirection[0].x != 0 && oldDirection[0].y != 0)
                direction = oldDirection[0];
            //caso normale: prende il frame attuale
            else
                direction = oldDirection[oldDirection.Count - 1];
        }
        #endregion
    }
}
