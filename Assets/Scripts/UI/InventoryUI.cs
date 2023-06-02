using UnityEngine;

public class InventoryUI : MonoBehaviour
{

    #region Singleton

    public static InventoryUI instance;

    void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("Найдено больше одного примера интерфейса инвентаря");
            return;
        }
        instance = this;
    }

    #endregion

    public Transform itemsParent;
    Inventory inventory;
    InventorySlotUI[] slots;
    public string selectedID;


    void Start()
    {
        inventory = Inventory.instance;
        inventory.OnItemChangedCallback += UpdateUI;
        slots = itemsParent.GetComponentsInChildren<InventorySlotUI>();
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

    public void DeleteItem()
    {
        inventory.DeleteItem(selectedID);
    }
}