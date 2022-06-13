using System;
using UnityEngine;


[CreateAssetMenu(fileName = "New Item", menuName = "Game/Item")]
public class ItemStats : ScriptableObject
{
    public enum Rarity
    {
        Common,
        Uncommon,
        Rare,
        Epic,
        Legendary,
        Mythic
    }

    [Header("Item Stats")]
    public string itemName;
    public Sprite itemIcon;
    public float itemPrice;
    public Rarity itemRarity;
    [TextArea(3, 10)]
    public string itemDescription;
    
    [Header("Player Stats")]
    public int timesForged;

}
