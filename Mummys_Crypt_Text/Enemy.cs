using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mummys_Crypt_Text
{
    class Enemy : Item   // The enemy class is essentially a different version of the item class, so it will derive from the item class
    {
        // since Enemy derives from the item class, we only need to define properties unique to the Enemy class, like health and attack.
        private string enemyDescription;  //I didn't like the idea of having "itemDescription" for Enemy, so I created this.
        private int healthValue;
        //private int attackValue;

        #region properties

        public string EnemyDescription
        {
            get { return enemyDescription; }
            set { enemyDescription = value; }
        }

        public int HealthValue
        {
            get { return healthValue; }
            set { healthValue = value; }
        }

        //public int AttackValue
        //{
        //    get { return attackValue; }
        //    set { attackValue = value; }
        //}


        #endregion
    }
}
