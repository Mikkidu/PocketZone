using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Inventory : MonoBehaviour
{
    #region Singleton
    public static Inventory instance;

    void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("Найдено больше одного примера инвентаря");
            return;
        }
        instance = this;
    }
    #endregion


    public delegate void OnItemChanged();
    public OnItemChanged OnItemChangedCallback;
    private List<InventoryItem> items = new List<InventoryItem>();
    private PlayerController _player;

    private void Start()
    {
        GameHandler.instance.FillInventory();
    }

    public void Add(string itemID, int amount)
    {
        if (items.FirstOrDefault(i => i.itemID == itemID) != null)
        {
            items.FirstOrDefault(i => i.itemID == itemID).amount += amount;
        }
        else
        {
            items.Add(new InventoryItem(itemID, amount));
        }
        if (OnItemChangedCallback != null)
            OnItemChangedCallback.Invoke();
    }

    public void Spend(string itemID, int amount)
    {
        items.FirstOrDefault(i => i.itemID == itemID).amount -= amount;
        if (items.FirstOrDefault(i => i.itemID == itemID).amount < 1)
        {
            items.RemoveAll(i => i.itemID == itemID);
        }
        if (OnItemChangedCallback != null)
            OnItemChangedCallback.Invoke();
    }

    public void DeleteItem(string itemID)
    {
        items.RemoveAll(i => i.itemID == itemID);
        if (OnItemChangedCallback != null)
            OnItemChangedCallback.Invoke();
    }


    public int GetAmountByID(string itemID)
    {
        if (!CheckItemByID(itemID)) return 0;
        else return items.FirstOrDefault(i => i.itemID == itemID).amount;
    }


    public bool CheckItemByID(string itemID)
    {
        if (items.FirstOrDefault(i => i.itemID == itemID) == null)
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    public string[] GetItemsID()
    {
        string[] arrayID = new string[items.Count];
        for (int i = 0; i < items.Count; i++)
        {
            arrayID[i] = items[i].itemID;
        }
        return arrayID;
    }

}