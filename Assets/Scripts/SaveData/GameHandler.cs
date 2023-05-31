using UnityEngine;

public class GameHandler : MonoBehaviour
{
    #region Singleton
    public static GameHandler instance;

    void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("������� ������ ������ ������� ����������");
            return;
        }
        instance = this;
    }
    #endregion


    ArrayItemData loadedItemdata;

    void Start()
    {
        TextAsset jsonItemData = Resources.Load<TextAsset>("StartItems");

        loadedItemdata = JsonUtility.FromJson<ArrayItemData>(jsonItemData.text);

    }
    //������� ��������� �������� � ���������
    public void FillInventory()
    {
        foreach (ItemData item in loadedItemdata.startItems)
        {
            Inventory.instance.Add(item.itemID, item.amount);
        }
    }


    /*//����� �������� ��� ����� ������ json
    [Serializable]
    class ItemData
    {
        public string itemID;
        public int amount;
    }
    [Serializable]

    //����� ������ ��������� ��� �������� � json
    class ArrayItemData
    {
        public List<ItemData> startItems;
    }*/






}