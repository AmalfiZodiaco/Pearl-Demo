using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pearl;

namespace Pearl.Demo
{
    public class ForceManager
    {
        #region Private Fields
        private static readonly float scaleFixedUpdate = 1f;
        private Dictionary<string, Vector2> forcesContinues;
        protected Vector2 forceTotal;
        protected Rigidbody2D rigidBody;
        protected Vector2 newPosition;
        #endregion

        #region Constructors
        public ForceManager(Rigidbody2D rigidBody)
        {
            forcesContinues = new Dictionary<string, Vector2>();
            this.rigidBody = rigidBody;
        }
        #endregion

        #region Public Region
        public void AddTotalForce()
        {
            if (forcesContinues.Count != 0)
                AddTotalForce(forcesContinues.Values, scaleFixedUpdate, Time.fixedDeltaTime);
        }

        public void RemoveForceContinue(string keyForceContinue)
        {
            forcesContinues.Remove(keyForceContinue);
        }

        public void Add(string keyForceContinue, Vector3 force)
        {
            forcesContinues?.Update(keyForceContinue, force);
        }
        #endregion

        #region Private Methods
        private void AddTotalForce(IEnumerable enumerable, float scale, float deltaTime)
        {
            forceTotal = Vector2.zero;
            foreach (Vector2 force in enumerable)
            {
                forceTotal += force;
            }
            forceTotal *= scale * deltaTime;
            newPosition = rigidBody.position + forceTotal;

            Positioning();
        }

        protected virtual void Positioning()
        {
            rigidBody.MovePosition(newPosition);
        }
        #endregion
    }
}
