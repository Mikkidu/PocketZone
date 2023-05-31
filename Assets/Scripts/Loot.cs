using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Loot : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _icon;

    [SerializeField] private string _itemID;
    [SerializeField]private int _amount;

    public string GetID => _itemID;

    public void Initialize(string itemID, int itemAmount)
    {
        _icon.sprite = Database.GetSpriteByID(itemID);
        _itemID = itemID;
        _amount = itemAmount;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Inventory.instance.Add(_itemID, _amount);
            Destroy(gameObject);
        }
    }
}
