using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pearl.Demo
{
    public class ForceManagerPlayer : ForceManager
    {
        private readonly int ID;
        private Vector2 colliderCenter;
        private int collisionsNumber;
        private BoxCollider2D boxCollider;
        private readonly Collider2D[] hits;

        public ForceManagerPlayer(Rigidbody2D rigidBody, GameObject gameObject) : base(rigidBody)
        {
            this.boxCollider = gameObject.GetComponent<BoxCollider2D>();
            this.ID = gameObject.GetInstanceID();
            hits = new Collider2D[5];
        }

        protected override void Positioning()
        {
            ConstraintMovement();
            base.Positioning();
        }

        private void ConstraintMovement()
        {
            if (!IsPossibileMovement(forceTotal.x * Vector2.right))
                newPosition.x = rigidBody.position.x;
            if (!IsPossibileMovement(forceTotal.y * Vector2.up))
                newPosition.y = rigidBody.position.y;
        }

        private bool IsPossibileMovement(Vector2 direction)
        {
            colliderCenter = rigidBody.position + boxCollider.offset;
            collisionsNumber = Physics2D.OverlapBoxNonAlloc(colliderCenter + direction, boxCollider.size, 0, hits);
            for (int i = 0; i < collisionsNumber; i++)
            {
                if (!hits[i].isTrigger && hits[i].gameObject.GetInstanceID() != ID)
                    return false;
            }
            return true;
        }
    }
}
