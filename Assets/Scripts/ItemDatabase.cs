using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDatabase : MonoBehaviour {

    public List<Item> items = new List<Item>();

    public GameObject[] prefabs;

    //Item(string name, int id, string desc, int sp1, Item_Type type)
    //Item(string name, int id, string desc, Item_Type type)
    //Item()

    //items.Add(new Item());

    void Awake()
    {
        //items.Add(new Item("White Shirt", 0, "A white shirt.", 0, 0, Item.Item_Type.Weapon));
        //items.Add(new Item("Amulet of Prayers", 1, "An amulet enchanted by prayers.", 1, 1, Item.Item_Type.Armor));
        //items.Add(new Item("Power Potion",2,"A potion that temporarily increases your power.", 2, 2, Item.Item_Type.Drop));
        items.Add(new Item("Grass", 0, "A tuft of dried grass", Item.Item_Type.Drop, prefabs[0], 0, false));
        items.Add(new Item("Stone", 1, "A small stone", Item.Item_Type.Drop, prefabs[1], 0, false));
        items.Add(new Item("Flint", 2, "A fire-starting stone", Item.Item_Type.Drop, prefabs[2], 0, false));
        items.Add(new Item("Wood", 3, "A log of wood", Item.Item_Type.Drop, prefabs[3], 0, false));
        items.Add(new Item("Crystal", 4, "A shiny gem", Item.Item_Type.Drop, prefabs[4], 0, false));
        items.Add(new Item("Health Potion", 5, "An elixer that will restore health", 10, Item.Item_Type.Consumable));
        items.Add(new Item("Wooden Sword", 6, "A wooden sword", 10, Item.Item_Type.Weapon));
        items.Add(new Item("Sword", 7, "A nice sword", 10, Item.Item_Type.Weapon));
        items.Add(new Item("Fancy Sword", 8, "A very nice sword", 10, Item.Item_Type.Weapon));
        //  items.Add(new Item("Shield", 7, "A sturdy shield", 10, Item.Item_Type.Armor));
        items.Add(new Item("Club", 9, "A blunt club", 15, Item.Item_Type.Weapon));
        items.Add(new Item("Big Club", 10, "A bigger blunt club", 15, Item.Item_Type.Weapon));
        items.Add(new Item("Axe", 11, "A sharp axe", 15, Item.Item_Type.Weapon));
        items.Add(new Item("Pickaxe", 12, "A mining tool", 10, Item.Item_Type.Weapon));
        items.Add(new Item("Bow", 13, "A noraml bow", 15, Item.Item_Type.Weapon));
    }
}
