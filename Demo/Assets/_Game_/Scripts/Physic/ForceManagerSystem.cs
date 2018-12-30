using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using it.amalfi.Pearl;
using it.amalfi.Pearl.events;

namespace it.demo
{
    public class ForceManagerSystem : LogicalSimpleManager
    {
        #region Private Fields
        private Dictionary<int, ForceManager> forcesManager = new Dictionary<int, ForceManager>();
        private Dictionary<int, ForceManager> forcesManagerDisable = new Dictionary<int, ForceManager>();
        #endregion

        #region Unity Callbacks
        // Update is called once per frame
        private void FixedUpdate()
        {
            ManageForces();
        }
        #endregion

        #region Public Methods
        public void DisableForce(int ID)
        {
            forcesManagerDisable.Add(ID, forcesManager[ID]);
            forcesManager.Remove(ID);
        }

        public void EnableForce(int ID)
        {
            forcesManager.Add(ID, forcesManagerDisable[ID]);
            forcesManagerDisable.Remove(ID);
        }

        public void AddManagerForce(int ID, Rigidbody2D rigidBody)
        {
            forcesManager.Add(ID, new ForceManager(rigidBody));
        }

        public void RemoveManagerForce(int ID)
        {
            forcesManager.Remove(ID);
        }

        public void AddManagerForce(int ID, Rigidbody2D rigidBody, GameObject gameObject)
        {
            forcesManager.Add(ID, new ForceManagerPlayer(rigidBody, gameObject));
        }

        public void RemoveForceContinue(int ID, string keyForceContinue)
        {
            forcesManager[ID].RemoveForceContinue(keyForceContinue);
        }

        public void AddForce(int ID, string keyForceContinue, Vector3 force)
        {
            forcesManager[ID].Add(keyForceContinue, force);
        }
        #endregion

        #region Private Methods
        private void ManageForces()
        {
            foreach (ForceManager forceManager in forcesManager.Values)
            {
                forceManager.AddTotalForce();
            }
        }
        #endregion
    }
}
