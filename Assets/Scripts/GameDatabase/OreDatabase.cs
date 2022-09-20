using System;
using UnityEngine;

namespace GameDatabase
{
        [CreateAssetMenu(fileName = "New Ore Database", menuName = "Game/Database/Ore")]
        public class OreDatabase : ScriptableObject
        {
                public OreStats[] ores;
                public OreStats[] premiumOres;
                
                public void ResetDatabase()
                {
                        foreach (var ore in ores) 
                        {
                                ore.isUnlocked = false;
                        }
                        ores[0].isUnlocked = true;
                }
        }
    
        [Serializable]
        public class OreStats
        {
                [Header("Changeable Values")]
                public string oreName;
                public Sprite oreSprite;
                public string oreDescription;
                public decimal currentHardness;
                public bool isUnlocked;

                public int amount;
    
                [Header("Default Values")]
                public decimal defaultHardness;
                public bool isPremium;
        }
}