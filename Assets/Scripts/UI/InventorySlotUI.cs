using UnityEngine;
using UnityEngine.UI;

public class InventorySlotUI : MonoBehaviour
{
    public Image icon;
    public Text textAmount;
    public string slotItemID;
    public bool isChosen;
    private Button _button;

    private void Awake()
    {
        _button = GetComponentInChildren<Button>();
    }

    public void AddItem(string itemID, int amount)
    {
        if (amount > 1)
        {
            textAmount.text = amount.ToString();
            textAmount.enabled = true;
            textAmount.transform.parent.gameObject.GetComponent<Image>().enabled = true;
        }
        else
        {
            textAmount.enabled = false;
            textAmount.transform.parent.gameObject.GetComponent<Image>().enabled = false;
        }

        slotItemID = itemID;

        icon.sprite = Database.GetSpriteByID(itemID);
        icon.enabled = true;
        _button.interactable = true;
    }

    public void ClearSlot()
    {
        textAmount.text = null;
        textAmount.enabled = false;
        textAmount.transform.parent.gameObject.GetComponent<Image>().enabled = false;

        slotItemID = null;

        icon.sprite = null;
        icon.enabled = false;
        _button.interactable = false;
    }

    public void SelectItem()
    {
        InventoryUI.instance.selectedID = slotItemID;
    }

}
