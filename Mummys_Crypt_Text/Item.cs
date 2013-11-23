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

        #endregion

    }
}
