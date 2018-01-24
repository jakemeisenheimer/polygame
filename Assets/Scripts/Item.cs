using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Item {

    public string item_Name;
    public int item_ID;
    public string item_Desc;
    public Texture2D item_Icon;
    public int item_Sp1;
    public Item_Type item_Type;
    public GameObject item_Prefab;
    public int item_Stack;
    public bool item_Lock;

    public enum Item_Type
    {
        Empty,
        Weapon,
        Armor,
        Drop,
        Consumable
    }

    public Item(string name, int id, string desc, int sp1, Item_Type type)
    {
        item_Name = name;
        item_ID = id;
        item_Desc = desc;
        item_Icon = Resources.Load<Texture2D>("Item Icons/" + name);
        item_Sp1 = sp1;
        item_Type = type;
    }

    public Item(string name, int id, string desc, Item_Type type, GameObject prefab, int stack, bool l0ck)
    {
        item_Name = name;
        item_ID = id;
        item_Desc = desc;
        item_Icon = Resources.Load<Texture2D>("Item Icons/" + name);
        item_Type = type;
        item_Prefab = prefab;
        item_Stack = stack;
        item_Lock = l0ck;
    }

    public Item()
    {
        item_ID = -1;
    }
}