using UnityEngine;

public class Item : MonoBehaviour
{
    private ItemStats _itemStats;
    
    public string itemName;
    public Sprite itemIcon;
    public float itemPrice;
    public string itemRarity;
    public string itemDescription;
    public int timesForged;
    
    void UpdateItemStats()
    {
        itemName = _itemStats.itemName;
        itemIcon = _itemStats.itemIcon;
        itemPrice = _itemStats.itemPrice;
        itemRarity = _itemStats.itemRarity.ToString();
        itemDescription = _itemStats.itemDescription;
        timesForged = _itemStats.timesForged;
    }
}


