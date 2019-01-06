using Pearl.events;
using System.Collections.Generic;
using UnityEngine;

namespace Pearl.actionTrigger
{
    public abstract class ComplexAction : LogicalSimpleManager
    {
        #region Properties
        public Dictionary<string, object> Informations { get; private set; }
        #endregion

        #region Unity CallBacks
        protected override void OnAwake()
        {
            Informations = new Dictionary<string, object>();
        }

        private void OnEnable()
        {
            Informations.Clear();
        }
        #endregion

        #region Public Methods
        public void Add<T>(string name, T value)
        {
            Informations.Update(name, value);
        }

        public T Take<T>(string name)
        {
            return (T)Informations[name];
        }
        #endregion

        #region Abstract Methods
        public abstract void SetAction();
        #endregion
    }
}
