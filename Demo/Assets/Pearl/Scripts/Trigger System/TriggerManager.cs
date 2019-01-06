using System.Collections.Generic;
using UnityEngine;
using Pearl.multitags;
using Pearl.events;

namespace Pearl.actionTrigger
{
    /// <summary>
    /// This class examine every trigger events and sort them in specific components
    /// </summary>
    public class TriggerManager : LogicalSimpleManager
    {
        #region Inspector Fields
        [Header("Trigger Manager")]
        /// <summary>
        /// The objects waiting for the events
        /// </summary>
        [SerializeField]
        private List<GameObject> listeners;
        /// <summary>
        /// The tags that activate the trigger class.
        /// </summary>
        [SerializeField]
        private Tags[] tagForTriggered;
        #endregion

        #region Private Fields
        private List<int> listGameobjectTriggeredActived;
        private Informations informations;
        private TriggerManager triggerCollider;
        private DestructionElementManager destructionElement;
        private byte synchronized = 0;
        private ITrigger[] auxTrigger;
        #endregion

        #region Unity CallBacks
        protected override void OnAwake()
        {
            listGameobjectTriggeredActived = new List<int>();
        }

        private void OnDisable()
        {
            listGameobjectTriggeredActived.Clear();
        }

        private void OnTriggerEnter2D(Collider2D collider)
        {
            Inpact(collider.gameObject);
        }

        private void OnTriggerExit2D(Collider2D collider)
        {
            RemoveGameObjectActive(collider.gameObject);
        }

        private void OnTriggerEnter(Collider collider)
        {
            Inpact(collider.gameObject);
        }

        private void OnTriggerExit(Collider collider)
        {
            RemoveGameObjectActive(collider.gameObject);
        }
        #endregion

        #region Trigger
        private void AddActiveTrigger(GameObject obj)
        {
            int ID = obj.GetInstanceID();
            listGameobjectTriggeredActived.Add(ID);
        }

        private void RemoveGameObjectActive(GameObject obj)
        {
            int ID = obj.GetInstanceID();
            if (listGameobjectTriggeredActived.Contains(ID))
                listGameobjectTriggeredActived.Remove(ID);
            destructionElement = obj.GetComponent<DestructionElementManager>();
            if (destructionElement)
                destructionElement.OnDestruction -= RemoveGameObjectActive;
        }

        private void Inpact(GameObject obj)
        {
            if (obj.HasTags(tagForTriggered) && !listGameobjectTriggeredActived.Contains(obj.GetInstanceID()))
            {
                destructionElement = obj.GetComponent<DestructionElementManager>();
                AddActiveTrigger(obj);
                if (destructionElement)
                    destructionElement.OnDestruction += RemoveGameObjectActive;
                SetSynchronized(obj);
            }
        }

        private void TriggerEvent(GameObject obj)
        {
            informations = new Informations(obj.GetComponent<ComplexAction>()?.Informations);
            foreach (GameObject element in listeners)
            {
                auxTrigger = element.GetComponents<ITrigger>();
                for (int i = 0; i < auxTrigger.Length; i++)
                    auxTrigger[i].Trigger(informations, obj.ReturnTags());
            }
        }
        #endregion

        #region Synchronized
        private void SetSynchronized(GameObject obj)
        {
            triggerCollider = obj.GetComponent<TriggerManager>();
            synchronized++;
            if (IsSynchronized() || !triggerCollider)
                Postsynchronized(obj);
            if (triggerCollider)
                triggerCollider.CallSynchronized(gameObject);

        }

        private void CallSynchronized(GameObject obj)
        {
            synchronized++;
            if (IsSynchronized())
                Postsynchronized(obj);
        }

        private void Postsynchronized(GameObject obj)
        {
            TriggerEvent(obj);
            synchronized = 0;
        }

        private bool IsSynchronized()
        {
            return synchronized == 2;
        }
        #endregion
    }
}
