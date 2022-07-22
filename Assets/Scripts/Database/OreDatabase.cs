using System;
using UnityEngine;

[CreateAssetMenu(fileName = "New Ore Database", menuName = "Game/Database/Ore")]
public class OreDatabase : ScriptableObject
{
        public OreStats[] ores;
        public OreStats[] premiumOres;
}
    
[Serializable]
public class OreStats
{
        [Header("Changeable Values")]
        public string oreName;
        public Sprite oreSprite;
        public string oreDescription;
        public int currentHardness;
        public bool isUnlocked;

        public int amount;
    
        [Header("Default Values")]
        public int defaultHardness;
        public bool isPremium;
}
