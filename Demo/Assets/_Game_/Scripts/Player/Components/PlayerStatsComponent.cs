using UnityEngine;
using System;
using it.amalfi.Pearl.events;
using it.amalfi.Pearl;

namespace it.demo.player
{
    public class PlayerStatsComponent : LogicalComponent
    {
        #region Private Fields
        private int maxHealth = 100;
        private int actualHealt;
        #endregion

        #region Constructors
        public PlayerStatsComponent(byte maxHealth)
        {
            InitStats(maxHealth);
        }
        #endregion

        #region Private Methods
        private void InitStats(byte maxHealth)
        {
            this.maxHealth = maxHealth;
            actualHealt = maxHealth;
        }

        public void OnChangeHealth(float actualHealth)
        {
            actualHealt = (int)MathfExtend.ChangeRange(actualHealth, maxHealth);
        }
        #endregion
    }
}
