using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mummys_Crypt_Text
{
    class Combat
    {
        private static int enemyHealth;
        private static int seed;
        private static int rollHit;
        private static int rollDamage;

        public static Random rnd = new Random();

        #region properties

        public static int EnemyHealth
        {
            get { return enemyHealth; }
            set { enemyHealth = value; }
        }

        #endregion


        public Combat()
        {

        }

        public static void PlayerAttack(String enemyName)  // Enemy won't attack first until the player attacks.
        {
            Room room = Player.GetCurrentRoom();
            Enemy enemy = room.GetEnemy(enemyName);

            Random random = new Random((int)DateTime.Now.Ticks & (0x0000FFFF + seed));  //default seed is system time; this compensates for that

            if (enemy != null)
            {
                rollHit = random.Next(20);
                //rollHit = rnd.Next(20);  //Determine if you can hit the enemy
                if (rollHit > enemy.ArmorValue)  // if the Hit roll is higher than the enemy's AC, then you can hit them...
                {
                    rollDamage = random.Next(1, Player.AttackValue) * Player.MultiplierValue;
                    if (rollDamage > 7)
                        Text.Add("You strike a critical blow dealing " + rollDamage + " points of damage to the " + enemyName + "!");
                    else if (rollDamage % 2 == 0)
                        Text.Add("You attack the " + enemyName + " and do " + rollDamage + " points of damage.");
                    else
                        Text.Add("Your hit deals " + rollDamage + " points of damage to the " + enemyName + ".");
                    enemy.HealthValue -= rollDamage;
                }
                else
                {
                    if (rollHit % 2 == 0)
                        Text.Add("You thrust at the " + enemyName + " but it dodges your attack.");  //...otherwise, you miss.
                    else
                        Text.Add("You attack the " + enemyName + " and miss.");
                }
            }
            else
                Text.Add("There is no " + enemy + "here to fight.");

            seed += 1;
        }

        public static void EnemyAttack(String enemyName)
        {
            Room room = Player.GetCurrentRoom();
            Enemy enemy = room.GetEnemy(enemyName);

            Random random = new Random((int)DateTime.Now.Ticks & (0x0000FFFF + seed));  //default seed is system time; this compensates for that

            if (enemy != null)
            {
                rollHit = random.Next(20);
                if (rollHit > Player.ArmorValue)
                {
                    rollDamage = random.Next(enemy.AttackValue);
                    if (rollDamage % 2 == 0)
                        Text.Add("The " + enemyName + " attacks you and does " + rollDamage + " points of damage.");
                    else
                        Text.Add("The " + enemyName + " hits you dealing " + rollDamage + " points of damage.");
                    Player.HealthValue -= rollDamage;
                }
                else
                {
                    if (rollHit % 2 == 0)
                        Text.Add("The " + enemyName + " attacks you and misses.");
                    else
                        Text.Add("You sidestep the " + enemyName + "'s attack.");
                }
                seed = seed + 1;
            }
        }

        public static void Shuffle(int[] array)
        {
            // populate array with random numbers using a Fisher-Yates shuffle
            Random rand = new Random();
            for (int i = array.Length; i > 1; i--)
            {
                int nextQ = rand.Next(i);
                int temp = array[nextQ];
                array[nextQ] = array[i - 1];
                array[i - 1] = temp;
            }
        }
    }
}
