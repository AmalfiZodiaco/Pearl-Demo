using System.Collections;
using UnityEngine;
using System;
using Pearl.events;

namespace Pearl
{
    /// <summary>
    /// This component handles the destruction of a GameObject: it can destroy 
    /// the GameObject after specific time and trigger an event to destroy it.
    /// </summary>
    public class DestructionElementManager : LogicalSimpleManager
    {
        #region Inspector Fields
        /// <summary>
        /// The time it takes to destroy the GameObject; if the time is zero, 
        /// the GameObject is not destroyed.
        /// </summary>
        [SerializeField]
        [Range(0f, 10f)]
        private float timeForDestroy = 0f;
        #endregion

        #region Events
        /// <summary>
        /// The event that is activated at the distraction of the gameobject
        /// </summary>
        public event genericDelegate<GameObject> OnDestruction;
        #endregion

        #region Unity CallBacks
        private void OnEnable()
        {
            if (timeForDestroy != 0)
                StartCoroutine(DetroyGameObject(timeForDestroy));
        }

        private void OnDisable()
        {
            WarnsAdvice();
        }

        protected override void OnDestroy()
        {
            WarnsAdvice();
        }
        #endregion

        #region Private Methods
        private IEnumerator DetroyGameObject(float time)
        {
            yield return new WaitForSeconds(time);
            GameObjectExtend.Destroy(gameObject);
        }

        private void WarnsAdvice()
        {
            StopAllCoroutines();
            OnDestruction?.Invoke(gameObject);
        }
        #endregion
    }
}