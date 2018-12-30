using UnityEngine;
using it.amalfi.Pearl.multitags;
using it.amalfi.Pearl;

namespace it.demo.power
{
    public static class Power
    {
        private static Vector3 auxVector;
        private static Vector3 translateVector;
        private static GameObject auxObject;

        public static GameObject CreatePrefab(PowerStruct pow, Transform emitter, Vector2 direction, params Tags[] tags)
        {
            translateVector = pow.distanceForInstantiate * direction;
            auxVector = emitter.position + translateVector;
            auxObject = GameObjectExtend.InstantiatePool(pow.prefab, auxVector, Quaternion.identity);
            auxObject.AddTags(tags);
            auxObject.layer = emitter.gameObject.layer;
            return auxObject;
        }
    }
}
