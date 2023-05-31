using System;

//Класс предметов в инвентаре
[Serializable]
public class InventoryItem
{
    public string itemID;
    public int amount;

    public InventoryItem()
    {
        this.itemID = null;
        this.amount = 0;
    }
    public InventoryItem(string itemID, int amount)
    {
        this.itemID = itemID;
        this.amount = amount;
    }
}