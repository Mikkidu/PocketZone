using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    public Image icon;
    public Text textAmount;
    public string slotItemID;

    //Заполняем слот инвентаря
    public void AddItem(string itemID, int amount)
    {
        if (amount > 1)
        {
            textAmount.text = amount.ToString();
            textAmount.enabled = true;
            textAmount.transform.parent.gameObject.GetComponent<Image>().enabled = true;
        }

        slotItemID = itemID;

        icon.sprite = Database.GetSpriteByID(itemID);
        icon.enabled = true;
    }

    //Очищаем слот инвентаря
    public void ClearSlot()
    {
        textAmount.text = null;
        textAmount.enabled = false;
        textAmount.transform.parent.gameObject.GetComponent<Image>().enabled = false;

        slotItemID = null;

        icon.sprite = null;
        icon.enabled = false;
    }
}
