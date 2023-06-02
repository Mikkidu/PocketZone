using System.IO;
using System.Collections.Generic;
using UnityEngine;

public class GameHandler : MonoBehaviour
{

    public static GameHandler instance;

    void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("Найено больше одного примера загрузчика");
            return;
        }
        instance = this;

        LoadInventory();
    }

    private void Start()
    {
        _inventory = Inventory.instance;
        _inventory.OnItemChangedCallback += SaveInventory;
    }

    private Inventory _inventory;
    private ArrayItemData _loadedItemdata;

    public void FillInventory()
    {
        foreach (ItemData item in _loadedItemdata.startItems)
        {
            _inventory.Add(item.itemID, item.amount);
        }
    }

    public void DefaultLoad()
    {
        TextAsset jsonItemData = Resources.Load<TextAsset>("StartItems");
        _loadedItemdata = JsonUtility.FromJson<ArrayItemData>(jsonItemData.text);
        string[] itemsID = _inventory.GetItemsID();
        foreach (string id in itemsID)
        {
            _inventory.DeleteItem(id);
        }
        FillInventory();
    }

    private void LoadInventory()
    {
        string path = Application.persistentDataPath + "/Inventory.json";
        if (File.Exists(path))
        {
            string loadData = File.ReadAllText(path);
            _loadedItemdata = JsonUtility.FromJson<ArrayItemData>(loadData);
            return;
        }
        else
        {
            TextAsset jsonItemData = Resources.Load<TextAsset>("StartItems");
            _loadedItemdata = JsonUtility.FromJson<ArrayItemData>(jsonItemData.text);
        }
    }

    public void SaveInventory()
    {
        ArrayItemData save = new ArrayItemData
        {
            startItems = new List<ItemData>()
        };
        string[] itemsID = _inventory.GetItemsID();
        for (int i = 0; i < itemsID.Length; i++)
        {
            save.startItems.Add(new ItemData(itemsID[i], Inventory.instance.GetAmountByID(itemsID[i])));
        }
        string saveData = JsonUtility.ToJson(save);
        File.WriteAllText(Application.persistentDataPath + "/inventory.json", saveData);
    }


}
