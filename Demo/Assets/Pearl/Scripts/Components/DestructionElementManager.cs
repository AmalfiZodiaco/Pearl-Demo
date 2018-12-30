using System.Collections;
using UnityEngine;
using System;
using it.amalfi.Pearl.events;

namespace it.amalfi.Pearl
{
    /// <summary>
    /// This class created a game object that will be destroyed after a specific time.
    /// The Destruction activates an event that represents action.
    /// </summary>
    public class DestructionElementManager : LogicalSimpleManager
    {
        #region Inspector Fields
        [SerializeField]
        [Range(0f, 10f)]
        private float time = 0f;
        #endregion

        #region Events
        public event genericDelegate<GameObject> OnDestruction;
        #endregion

        #region Unity CallBacks
        private void OnEnable()
        {
            if (time != 0)
                StartCoroutine(DetroyGameObject(time));
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