using System;
using System.Collections.Generic;
using System.Text;

namespace RpgCore
{
    [Serializable]
    public class Player
    {
        private string name;
        private int currentHealth;
        private int maxHealth;
        private Stats stats;
        private Attributes attributes;
        private List<Item> inventory;
        private Item equippedWeapon;
        private Item equippedArmor;

        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        public int CurrentHealth
        {
            get { return currentHealth; }
            set { 
                if(value <= 0) { currentHealth = 0; }
                else if(value >= maxHealth) { currentHealth = maxHealth; }
                else { currentHealth = value; }
            }
        }

        public int MaxHealth
        {
            get { return maxHealth; }
            set { maxHealth = value; }
        }

        public Stats Stats
        {
            get { return stats; }
            set { stats = value; }
        }

        public Attributes Attributes
        {
            get { return attributes; }
            set { attributes = value; }
        }

        public List<Item> Inventory
        {
            get { return inventory; }
            set { inventory = value; }
        }

        public Item EquippedWeapon
        {
            get { return equippedWeapon; }
            set { equippedWeapon = value; }
        }

        public Item EquippedArmor
        {
            get { return equippedArmor; }
            set { equippedArmor = value; }
        }

        public Player()
        {

        }

        public Player(string _name = "", int _currentHealth = 100, int _maxHealth = 100, Stats _stats = default(Stats), Attributes _attributes = default(Attributes), List<Item> _inventory = default(List<Item>))
        {
            name = _name;
            currentHealth = _currentHealth;
            maxHealth = _maxHealth;
            if(_stats == null)
            {
                stats = new Stats();
            }
            else
            {
                stats = _stats;
            }
            if (_attributes == null)
            {
                attributes = new Attributes();
            }
            else
            {
                attributes = _attributes;
            }
            if (_inventory == null)
            {
                inventory = new List<Item>();
            }
            else
            {
                inventory = _inventory;
            }
        }

        public override string ToString()
        {
            string wepText = "";
            string armorText = "";
            string inv = "";
            if (equippedWeapon != null)
            {
                wepText = $"Equipped Weapon: {equippedWeapon.Name}\n" +
                $"Weapon Attributes:\n" +
                $"Melee Attack: {equippedWeapon.Attributes.MeleeAttack}\n" +
                $"Melee Defense: {equippedWeapon.Attributes.MeleeDefense}\n" +
                $"Ranged Attack: {equippedWeapon.Attributes.RangedAttack}\n" +
                $"Ranged Defense: {equippedWeapon.Attributes.RangedDefense}\n" +
                $"Magic Attack: {equippedWeapon.Attributes.MagicAttack}\n" +
                $"Magic Defense: {equippedWeapon.Attributes.MagicDefense}";
            }
            else
            {
                wepText = "Equipped Weapon: None";
            }

            if(equippedArmor != null)
            {
                armorText = $"Equipped Armor: {equippedArmor.Name}\n" +
                $"Armor Attributes:\n" +
                $"Melee Attack: {equippedArmor.Attributes.MeleeAttack}\n" +
                $"Melee Defense: {equippedArmor.Attributes.MeleeDefense}\n" +
                $"Ranged Attack: {equippedArmor.Attributes.RangedAttack}\n" +
                $"Ranged Defense: {equippedArmor.Attributes.RangedDefense}\n" +
                $"Magic Attack: {equippedArmor.Attributes.MagicAttack}\n" +
                $"Magic Defense: {equippedArmor.Attributes.MagicDefense}";
            }
            else
            {
                armorText = "Equipped Armor: None";
            }

            if(inventory.Count > 0)
            {
                inv = "Inventory:\n";
                for (int i = 0; i < inventory.Count; i++)
                {
                    inv += $"{inventory[i].Name} ID: {inventory[i].ID}\n";
                }
            }
            else
            {
                inv = "Inventory Empty";
            }

            return $"{Name} Current Health: {CurrentHealth} / Max Health: {MaxHealth}\n" +
                $"Stats:\n" +
                $"Level: {Stats.Level.Level}\n" +
                $"Strength: {Stats.Strength.Level}\n" +
                $"Dexterity: {Stats.Dexterity.Level}\n" +
                $"Constitution: {Stats.Constitution.Level}\n" +
                $"Intelligence: {Stats.Intelligence.Level}\n" +
                $"Wisdom: {Stats.Wisdom.Level}\n" +
                $"Charisma: {Stats.Charisma.Level}\n" +
                $"Attributes:\n" +
                $"Melee Attack: {Attributes.MeleeAttack}\n" +
                $"Melee Defense: {Attributes.MeleeDefense}\n" +
                $"Ranged Attack: {Attributes.RangedAttack}\n" +
                $"Ranged Defense: {Attributes.RangedDefense}\n" +
                $"Magic Attack: {Attributes.MagicAttack}\n" +
                $"Magic Defense: {Attributes.MagicDefense}\n{wepText}\n{armorText}\n{inv}";
        }
    }
}
