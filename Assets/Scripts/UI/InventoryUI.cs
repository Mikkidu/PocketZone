using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    public Transform itemsParent;
    Inventory inventory;
    InventorySlot[] slots;
    bool i = true;

    void Start()
    {
        inventory = Inventory.instance;
        inventory.OnItemChangedCallback += UpdateUI;

        slots = itemsParent.GetComponentsInChildren<InventorySlot>();


    }

    void Update()
    {
        //Костыль. запускаем заполнение инвентаря после старта
        if (i)
        {
            i = false;
            GameHandler.instance.FillInventory();
        }
    }

    void UpdateUI()
    {
        string[] itemsID = inventory.GetItemsID();
        for (int i = 0; i < slots.Length; i++)
        {
            if (i < itemsID.Length)
            {
                slots[i].AddItem(itemsID[i], inventory.GetAmountByID(itemsID[i]));
            }
            else
            {
                slots[i].ClearSlot();
            }
        }
    }
}