using System.Collections.Generic;
using UnityEngine;
using it.amalfi.Pearl.actionTrigger;
using it.amalfi.Pearl.multitags;
using it.amalfi.Pearl;

namespace it.demo.power
{
    public class Arrow : ComplexAction, ITrigger
    {
        [SerializeField]
        private float speed;
        private const float timeForDestroy = 0.01f;

        private void OnDisable()
        {
            ForceManagerSystem forceManager = SingletonPool.Get<ForceManagerSystem>();
            if (forceManager)
                forceManager.DisableForce(gameObject.GetInstanceID());
        }

        public override void SetAction()
        {
            ForceManagerSystem forceManager = SingletonPool.Get<ForceManagerSystem>();
            transform.rotation = QuaternionExtend.CalculateRotation2D(Take<Vector2>("direction"));
            forceManager.EnableForce(gameObject.GetInstanceID());
            forceManager.AddForce(gameObject.GetInstanceID(), "movement", Take<Vector2>("direction") * speed);
        }

        public void Trigger(Informations informations, List<Tags> tags)
        {
            GameObjectExtend.Destroy(gameObject);
        }

        public override void SetAwake()
        {
            ForceManagerSystem forceManager = SingletonPool.Get<ForceManagerSystem>();
            forceManager.AddManagerForce(gameObject.GetInstanceID(), GetComponent<Rigidbody2D>());
        }
    }
}
