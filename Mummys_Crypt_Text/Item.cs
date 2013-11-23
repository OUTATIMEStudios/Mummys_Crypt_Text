using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mummys_Crypt_Text
{
    class Item
    {
        private string title;
        private string pickupText;
        private string itemDescription;   // Describes the item when you look at it in the room
        private string invDescription;
        private int pointValue;
        private int attackValue;
        private int armorValue;
        public bool CanPickUp = true;  // should allow any item to be picked up, unless otherwise specified
        public int Weight = 1;

        #region properties

        public string Title
        {
            get { return title; }
            set { title = value; }
        }

        public string PickupText
        {
            get { return pickupText; }
            set { pickupText = value; }
        }

        public string ItemDescription
        {
            get { return itemDescription; }
            set { itemDescription = value; }
        }

        public string InvDescription
        {
            get { return invDescription; }
            set { invDescription = value; }
        }

        public int PointValue
        {
            get { return pointValue; }
            set { pointValue = value; }
        }

        public int AttackValue
        {
            get { return attackValue; }
            set { attackValue = value; }
        }

        public int ArmorValue
        {
            get { return armorValue; }
            set { armorValue = value; }
        }


        #endregion

    }
}
