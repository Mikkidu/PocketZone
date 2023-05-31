using System;
using System.Collections.Generic;
using UnityEngine;

public class GameHandler : MonoBehaviour
{
    #region Singleton
    public static GameHandler instance;

    void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("Найено больше одного примера загрузчика");
            return;
        }
        instance = this;
    }
    #endregion


    ArrayItemData loadedItemdata;
    //Для инвентаря сбор информации удался
    void Start()
    {
        TextAsset jsonItemData = Resources.Load<TextAsset>("StartItems");

        loadedItemdata = JsonUtility.FromJson<ArrayItemData>(jsonItemData.text);

    }
    //передаём стартовые предметы в инвентарь
    public void FillInventory()
    {
        foreach (ItemData item in loadedItemdata.startItems)
        {
            Inventory.instance.Add(item.itemID, item.amount);
        }
    }


    //Класс предмета для сбора данныиз json
    [Serializable]
    class ItemData
    {
        public string itemID;
        public int amount;
    }
    [Serializable]

    //Класс списка предметов для загрузки с json
    class ArrayItemData
    {
        public List<ItemData> startItems;
    }




}
