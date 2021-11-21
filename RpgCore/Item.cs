using System;

namespace RpgCore
{
    public enum ItemType
    {
        Consumable,
        Weapon,
        Armor,
        Quest
    }

    [Serializable]
    public class Item
    {
        private int id;
        private string name;
        private string description;
        private string effect;
        private Attributes attributes;
        private ItemType type;
        private int stack;
        private int cost;

        public int ID
        {
            get { return id; }
            set { id = value; }
        }

        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        public string Description
        {
            get { return description; }
            set { description = value; }
        }

        public string Effect
        {
            get { return effect; }
            set { effect = value; }
        }

        public Attributes Attributes
        {
            get { return attributes; }
            set { attributes = value; }
        }

        public ItemType Type
        {
            get { return type; }
            set { type = value; }
        }

        public int Stack
        {
            get { return stack; }
            set { stack = value; }
        }

        public int Cost
        {
            get { return cost; }
            set { cost = value; }
        }

        public Item()
        {

        }

        public Item(int _id = 0, string _name = "", string _description = "", string _effect = "", Attributes _attributes = default(Attributes), ItemType _type = ItemType.Consumable, int _stack = 1, int _cost = 0)
        {
            id = _id;
            name = _name;
            description = _description;
            effect = _effect;
            if (_attributes == null)
            {
                attributes = new Attributes();
            }
            else
            {
                attributes = _attributes;
            }
            type = _type;
            stack = _stack;
            cost = _cost;
        }

        public void Use(Player player)
        {
            var effectParse = effect.Split(';', StringSplitOptions.RemoveEmptyEntries);

            for (int i = 0; i < effectParse.Length; i++)
            {
                var effectInner = effectParse[i].Split(':', StringSplitOptions.RemoveEmptyEntries);
                if(effectInner.Length > 1)
                {
                    Console.WriteLine($"EFFECT: {effectInner[0]} AMOUNT: {effectInner[1]}");
                    switch (effectInner[0].ToUpper())
                    {
                        case "HEAL":
                            player.CurrentHealth += int.Parse(effectInner[1]);
                            break;
                        default:
                            break;
                    }
                }
                else
                {
                    Console.WriteLine($"EFFECT: {effectParse[i]}");
                    switch (effectParse[i].ToUpper())
                    {
                        case "LEVELUP":
                            player.Stats.Level.GainLevel(1);
                            break;
                        case "EQUIP":
                            if(type == ItemType.Weapon)
                            {
                                if(player.EquippedWeapon != null && player.EquippedWeapon != this)
                                {
                                    player.Attributes -= player.EquippedWeapon.Attributes;
                                }
                                player.EquippedWeapon = this;
                                player.Attributes += player.EquippedWeapon.Attributes;
                            }
                            if (type == ItemType.Armor)
                            {
                                if (player.EquippedArmor != null && player.EquippedArmor != this)
                                {
                                    player.Attributes -= player.EquippedArmor.Attributes;
                                }
                                player.EquippedArmor = this;
                                player.Attributes += player.EquippedArmor.Attributes;
                            }
                            break;
                        default:
                            break;
                    }
                }

            }

            if(type == ItemType.Consumable)
            {
                stack--;
                if(stack <= 0)
                {
                    player.Inventory.Remove(this);
                }
            }
        }
    }
}
