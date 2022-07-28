using System;
using UnityEngine;

namespace GameDatabase
{
    [CreateAssetMenu(fileName = "New Item Database", menuName = "Game/Database/Item")]
    public class ItemDatabase : ScriptableObject
    {
        public ItemCollection[] collections;
    }

    [Serializable]
    public class ItemCollection
    {
        public string collectionName;
        public ItemStats[] items;
    }

    [Serializable]
    public class ItemStats
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
        public Sprite itemSprite;
        public float itemPrice;
        public Rarity itemRarity;
        [TextArea(3, 10)]
        public string itemDescription;

        public float dropChance;
    
        [Header("Player Stats")]
        public int timesForged;
        public string itemFirstForged;
        public bool isUnlocked;
    }
}