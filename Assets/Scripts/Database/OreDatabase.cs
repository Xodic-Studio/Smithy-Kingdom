using System;
using UnityEngine;

[CreateAssetMenu(fileName = "New Ore Database", menuName = "Game/Database/Ore")]
public class OreDatabase : ScriptableObject
{
        public OreStats[] ores;
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
    
        [Header("Default Values")]
        public int defaultHardness;
        public bool isPremium;
}
