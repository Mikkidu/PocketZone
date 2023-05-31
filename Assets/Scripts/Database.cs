using UnityEngine;
using System.Linq;

public class Database : MonoBehaviour
{
    
    public ItemDatabase items;
    private static Database instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    //Выдаём предмет по ID
    public static Item GetItemByID(string itemID)
    {
        //Debug.Log($"database: {itemID}");
        return instance.items.allItems.FirstOrDefault(i => i.itemID == itemID);
    }
    //Выдаём спрайт по ID
    public static Sprite GetSpriteByID(string itemID)
    {
        return instance.items.allItems.FirstOrDefault(i => i.itemID == itemID).icon;
    }
    public static string GetNameByID(string itemID)
    {
        return instance.items.allItems.FirstOrDefault(i => i.itemID == itemID).name;
    }
}
