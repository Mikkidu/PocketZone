using System;

[Serializable]
class ItemData
{
    public string itemID;
    public int amount;

    public ItemData(string id, int itemAmount)
    {
        itemID = id;
        amount = itemAmount;
    }
}