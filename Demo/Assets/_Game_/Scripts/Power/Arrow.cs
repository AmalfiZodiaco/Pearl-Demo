using System.Collections.Generic;
using UnityEngine;
using Pearl.actionTrigger;
using Pearl.multitags;
using Pearl;
using Pearl.events;

namespace Pearl.Demo.power
{
    public class Arrow : ComplexAction, ITrigger
    {
        [SerializeField]
        private float speed;
        private const float timeForDestroy = 0.01f;

        private void OnDisable()
        {
            ForceManagerSystem forceManager = EventsManager.GetIstance<ForceManagerSystem>();
            if (forceManager)
                forceManager.DisableForce(gameObject.GetInstanceID());
        }

        public override void SetAction()
        {
            ForceManagerSystem forceManager = EventsManager.GetIstance<ForceManagerSystem>();
            transform.rotation = QuaternionExtend.CalculateRotation2D(Take<Vector2>("direction"));
            forceManager.EnableForce(gameObject.GetInstanceID());
            forceManager.AddForce(gameObject.GetInstanceID(), "movement", Take<Vector2>("direction") * speed);
        }

        public void Trigger(Informations informations, List<Tags> tags)
        {
            GameObjectExtend.Destroy(gameObject);
        }

        protected override void OnAwake()
        {
            base.OnAwake();
            EventsManager.GetIstance<ForceManagerSystem>().AddManagerForce(gameObject.GetInstanceID(), GetComponent<Rigidbody2D>());
        }
    }
}
