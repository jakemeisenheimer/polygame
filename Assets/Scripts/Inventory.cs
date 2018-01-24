using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour {

    public PlayerController playerController;

    public int slotsX, slotsY;
    public GUISkin skin;
    public List<Item> inventory = new List<Item>();
    public List<Item> slots = new List<Item>();
    private bool showInventory;
    private ItemDatabase database;
    private bool showTooltip;
    private string toolTip;
    private bool draggingItem;
    private Item draggedItem;
    private int prevIndex;
    public int slotWidth;
    public int slotSpace;
    public int invenPosY;
    public int invenPosX;
    private int invenW;
    private int invenH;
    public int activeSpace = 0;

    void Start()
    {
        PositionInventory();
        showInventory = true;
        for (int i = 0; i < (slotsX * slotsY); i++)
        {
            slots.Add(new Item());
            inventory.Add(new Item());
        }
        database = GameObject.FindGameObjectWithTag("Item Database").GetComponent<ItemDatabase>();
        // adds in weapons to inventory
        for (int i = 6; i < 14; i++)
        {
            inventory[i-5] = database.items[i];
        }
    }

    private void PositionInventory()
    {
        invenW = (slotsX * slotWidth) + (slotSpace * (slotsX - 1));
        invenPosX = (Screen.width / 2) - (invenW / 2);

        invenH = (slotsY * slotWidth) + (slotSpace * (slotsY - 1));
        invenPosY = Screen.height - (3 * invenH / 2);
    }

    void Update()
    {
        // sets the active inventory space to be the button pressed on the number bar and if that is occpied by a weapon equip it
        for (int i = 1; i < 10; ++i)
        {
            if (Input.GetKeyDown("" + i))
            {
                activeSpace = i-1;
                if (inventory[i-1].item_Type == Item.Item_Type.Weapon)
                {
                    Debug.Log(i);
                    playerController.equipWeapons(inventory[i-1].item_ID - 5);
                }
                else {
                    playerController.equipWeapons(0);
                }
            }
        }
        //if (Input.GetButtonDown("Inventory"))
        //{
        //    showInventory = !showInventory;
        //}
        if (Input.GetKeyDown(KeyCode.H))
        {
            AddItem(1);
        }
        if (Input.GetKeyDown(KeyCode.J))
        {
            AddItem(3);
        }
    }

    void OnGUI()
    {
        toolTip = "";
        GUI.skin = skin;
        if (showInventory)
        {
            DrawInventory();
            //if (GUI.Button(new Rect(40, 400, 100, 40), "Save"))
            //    SaveInventory();
            //if (GUI.Button(new Rect(40, 450, 100, 40), "Load"))
            //    LoadInventory();
            if (showTooltip)
                GUI.Box(new Rect(Event.current.mousePosition.x + 5.0f, Event.current.mousePosition.y - 205.0f, 200, 200), toolTip, skin.GetStyle("Tooltip"));
        }
        if (draggingItem)
        {
            GUI.DrawTexture(new Rect(Event.current.mousePosition.x, Event.current.mousePosition.y, 50, 50), draggedItem.item_Icon);
        }
    }

    void DrawInventory()
    {
        slotWidth = Screen.width / (slotsX + 10);
        slotSpace = Screen.width / (10 * (slotsX + 10));
        PositionInventory();
        Rect invenBox = new Rect(invenPosX - 5, invenPosY - 5, invenW + 10, invenH + 10);
        GUI.Box(invenBox, "", skin.GetStyle("Inven"));

        Event e = Event.current;
        int i = 0;
        for (int y = 0; y < slotsY; y++)
        {
            for (int x = 0; x < slotsX; x++)
            {
                Rect slotRect = new Rect(invenPosX + (x * (slotWidth + slotSpace)), invenPosY + (y * (slotWidth + slotSpace)), slotWidth, slotWidth);
                if (x == activeSpace)
                {
                    GUI.Box(slotRect, "", skin.GetStyle("activeSlot"));
                }
                else {
                    GUI.Box(slotRect, "", skin.GetStyle("Slot"));
                }
                slots[i] = inventory[i];
                Item item = slots[i];
                //if (item.item_Type == Item.Item_Type.Drop)
                //{
                //    Rect textRect = new Rect(invenPosX + (x * (slotWidth + slotSpace)), invenPosY + 10 + (y * (slotWidth + slotSpace)), slotWidth, slotWidth);
                //    if (item.item_Lock == false)
                //    {
                //        item.item_Stack++;
                //    }
                //    GUI.Label(textRect, item.item_Stack.ToString());
                //    item.item_Lock = true;
                //}
                if (item.item_Name != null)
                {
                    GUI.DrawTexture(slotRect, slots[i].item_Icon);
                    if (slotRect.Contains(e.mousePosition))
                    {
                        CreateTooltip(item);
                        showTooltip = true;
                        if (e.button == 0 && e.type == EventType.MouseDrag && !draggingItem)
                        {
                            draggingItem = true;
                            prevIndex = i;
                            draggedItem = item;
                            inventory[i] = new Item();
                        }
                        if (e.type == EventType.MouseUp && draggingItem)
                        {
                            inventory[prevIndex] = inventory[i];
                            inventory[i] = draggedItem;
                            draggingItem = false;
                            draggedItem = null;
                        }
                        if (e.isMouse && e.type == EventType.MouseDown && e.button == 1)
                        {
                            if (item.item_Type == Item.Item_Type.Drop)
                            {
                                Drop(item, i, true);
                            }
                        }
                    }
                    if (item.item_Type == Item.Item_Type.Drop)
                    {
                        Rect textRect = new Rect(invenPosX + (x * (slotWidth + slotSpace)), invenPosY + 10 + (y * (slotWidth + slotSpace)), slotWidth, slotWidth);
                        if (item.item_Lock == false)
                        {
                            item.item_Stack++;
                        }
                        GUI.Label(textRect, item.item_Stack.ToString());
                        item.item_Lock = true;
                    }
                }
                else
                {
                    if (slotRect.Contains(e.mousePosition))
                    {
                        if (e.type == EventType.MouseUp && draggingItem)
                        {
                            inventory[i] = draggedItem;
                            draggingItem = false;
                            draggedItem = null;
                        }
                    }
                }
                if (toolTip == "")
                {
                    showTooltip = false;
                }
                i++;
            }
        }
    }

    string CreateTooltip(Item item)
    {
        toolTip = "<color=#FFFFFF>" + item.item_Name + "</color>\n\n" + "<color=#FFFFFF>" + item.item_Desc + "</color>";
        return toolTip;
    }

    void RemoveItem(int id)
    {
        for (int i = 0; i < inventory.Count; i++)
        {
            if (inventory[i].item_ID == id)
            {
                inventory[i] = new Item();
                break;
            }
        }
    }

    public void AddItem(int id)
    {
        for (int i = 0; i < inventory.Count; i++)
        {
            if (inventory[i].item_Name == null)
            {
                for (int j = 0; j < database.items.Count; j++)
                {
                    if (database.items[j].item_ID == id)
                    {
                        if (database.items[j].item_Type == Item.Item_Type.Drop)
                        {
                            Stack(inventory, j - 1);
                            if (i == 0)
                            {
                                inventory[i] = database.items[j];
                            }
                            else
                            {
                                inventory[i-1] = database.items[j];
                            }
                        }
                        else
                        {
                            inventory[i] = database.items[j];
                        }
                    }
                }
                break;
            }
        }
    }

    bool InventoryContains(int id)
    {
        foreach (Item item in inventory)
        {
            if (item.item_ID == id) return true;
        }
        return false;
    }

    private void Drop(Item item, int slot, bool deleteItem)
    {
        print("Dropped " + item.item_Name);
        if (deleteItem)
        {
            if (item.item_Stack <= 1)
            {
                inventory[slot] = new Item();
            }
            else
            {
                item.item_Stack--;
            }
            SpawnDrop(item);
        }
    }

    void SaveInventory()
    {
        for (int i = 0; i < inventory.Count; i++)
            PlayerPrefs.SetInt("Inventory " + i, inventory[i].item_ID);
    }

    void LoadInventory()
    {
        for (int i = 0; i < inventory.Count; i++)
            inventory[i] = PlayerPrefs.GetInt("Inventory " + i, -1) >= 0 ? database.items[PlayerPrefs.GetInt("Inventory " + i)] : new Item();
    }

    void SpawnDrop(Item item)
    {
        Vector3 playerPos = new Vector3(playerController.transform.position.x + 2, playerController.transform.position.y + 1, playerController.transform.position.z);
        Instantiate(item.item_Prefab, playerPos, Quaternion.identity);
    }

    void AddByName(string name)
    {
        for (int i = 0; i < database.items.Count; i++)
        {
            if (name.Contains(database.items[i].item_Name))
            {
                AddItem(i);
                break;
            }
        }
    }

    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Stone" && Input.GetKeyDown(KeyCode.V) && InventoryContains(-1))
        {
            Destroy(other.gameObject);
            AddByName(other.gameObject.name);
        }
    }

    void Stack(List<Item> inven, int index)
    {
        inven[index].item_Stack += 1;
    }
}