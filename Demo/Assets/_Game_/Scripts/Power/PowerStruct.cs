﻿using UnityEngine;
using System;

namespace Pearl.Demo.power
{
    [Serializable]
    public struct PowerStruct
    {
        #region Inspector Fields
        public string name;
        public GameObject prefab;
        public float distanceForInstantiate;
        [Range(0, 255)]
        public byte damage;
        #endregion
    }
}
