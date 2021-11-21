using System;
using System.Threading.Tasks;
using RpgCore;

namespace SimpleRPG
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var item = new Item(0, "Potion", "A healing potion", "HEAL:25;LEVELUP;", null, ItemType.Consumable, 1, 10);
            var item2 = new Item(1, "Sword", "A basic sword", "EQUIP;", new Attributes { MeleeAttack = 3 }, ItemType.Weapon, 1, 50);
            var item3 = new Item(2, "Chain", "A basic chainbody", "EQUIP;", new Attributes { MeleeDefense = 3, MagicDefense = -5, RangedDefense = 1 }, ItemType.Armor, 1, 100);
            var item4 = new Item(3, "Staff", "A basic staff", "EQUIP;", new Attributes { MeleeAttack = 5 }, ItemType.Weapon, 1, 25);
            var item5 = new Item(4, "Leather", "A basic leatherbody", "EQUIP;", new Attributes { MeleeDefense = 5 }, ItemType.Armor, 1, 75);
            var player = new Player("Mony");
            Console.WriteLine($"{player}");

            player.Inventory.Add(item);
            player.Inventory.Add(item2);
            player.Inventory.Add(item3);
            player.Inventory.Add(item4);
            player.Inventory.Add(item5);
            INV:
            Console.WriteLine($"{player}");
            Console.Write("Enter Item ID to use:");
            var inputItem = Console.ReadLine();
            var itemIdInput = int.Parse(inputItem);
            var itemVal = player.Inventory.Find(x => x.ID == itemIdInput);
            if(itemVal != null)
            {
                itemVal.Use(player);
            }
            Console.WriteLine($"{player}");
            player.CurrentHealth -= 50;
            goto INV;
        }
    }
}
