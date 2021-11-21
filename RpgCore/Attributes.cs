using System;
using System.Collections.Generic;
using System.Text;

namespace RpgCore
{
    public class Attributes
    {
        private int meleeAttack;
        private int meleeDefense;
        private int rangedAttack;
        private int rangedDefense;
        private int magicAttack;
        private int magicDefense;

        public int MeleeAttack
        {
            get { return meleeAttack; }
            set { meleeAttack = value; }
        }

        public int MeleeDefense
        {
            get { return meleeDefense; }
            set { meleeDefense = value; }
        }

        public int RangedAttack
        {
            get { return rangedAttack; }
            set { rangedAttack = value; }
        }

        public int RangedDefense
        {
            get { return rangedDefense; }
            set { rangedDefense = value; }
        }

        public int MagicAttack
        {
            get { return magicAttack; }
            set { magicAttack = value; }
        }

        public int MagicDefense
        {
            get { return magicDefense; }
            set { magicDefense = value; }
        }

        public override string ToString()
        {
            return $"Attributes:\n" +
                $"Melee Attack: {MeleeAttack}\n" +
                $"Melee Defense: {MeleeDefense}\n" +
                $"Ranged Attack: {RangedAttack}\n" +
                $"Ranged Defense: {RangedDefense}\n" +
                $"Magic Attack: {MagicAttack}\n" +
                $"Magic Defense: {MagicDefense}";
        }

        public static Attributes operator + (Attributes att1, Attributes att2)
        {
            Attributes attSum = new Attributes
            {
                meleeAttack = att1.meleeAttack + att2.meleeAttack,
                meleeDefense = att1.meleeDefense + att2.meleeDefense,
                rangedAttack = att1.rangedAttack + att2.rangedAttack,
                rangedDefense = att1.rangedDefense + att2.rangedDefense,
                magicAttack = att1.magicAttack + att2.magicAttack,
                magicDefense = att1.magicDefense + att2.magicDefense
            }; 
            return attSum;
        }
        public static Attributes operator -(Attributes att1, Attributes att2)
        {
            Attributes attSum = new Attributes
            {
                meleeAttack = att1.meleeAttack - att2.meleeAttack,
                meleeDefense = att1.meleeDefense - att2.meleeDefense,
                rangedAttack = att1.rangedAttack - att2.rangedAttack,
                rangedDefense = att1.rangedDefense - att2.rangedDefense,
                magicAttack = att1.magicAttack - att2.magicAttack,
                magicDefense = att1.magicDefense - att2.magicDefense
            };
            return attSum;
        }
    }
}
