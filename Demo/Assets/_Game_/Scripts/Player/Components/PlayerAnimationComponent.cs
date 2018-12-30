using System.Collections.Generic;
using UnityEngine;
using it.amalfi.Pearl.events;
using it.amalfi.Pearl;

namespace it.demo.player
{
    public class PlayerAnimationComponent : LogicalComponent
    {
        #region Private Fields
        private Animator animator;
        private SpriteRenderer spriteRender;
        #endregion

        #region Constructors
        public PlayerAnimationComponent(SpriteRenderer spriteRender, Animator animator)
        {
            this.spriteRender = spriteRender;
            this.animator = animator;
        }
        #endregion

        #region Public Methods
        public void SetMovementVar(Vector2 direction, bool isMovement)
        {
            AnimateMovement(direction, isMovement);
            Flip(direction);
            animator.SetBool("movement", isMovement);
        }

        //solo l'ospite dell'istanza riceve questo metodo: esso si gira in base a com'è girato
        //la sua copia in locale.
        public void ChangeFlipFromOnline(bool flip)
        {
            spriteRender.flipX = flip;
        }
        #endregion

        #region Private Method
        private void AnimateMovement(Vector2 direction, bool isMovement)
        {
            animator.SetInteger("horizontal", MathfExtend.Sign(direction.x));
            animator.SetInteger("vertical", MathfExtend.Sign(direction.y));
            animator.SetBool("movement", isMovement);
        }

        private void Flip(Vector2 direction)
        {
            spriteRender.flipX = MathfExtend.Sign(direction.x) < 0;
        }
        #endregion
    }
}
