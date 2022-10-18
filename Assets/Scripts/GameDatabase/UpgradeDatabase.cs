using System;
using UnityEngine;
using UnityEngine.Events;

namespace GameDatabase
{
    [CreateAssetMenu(fileName = "New Upgrade Database", menuName = "Game/Database/Upgrades")]
    public class UpgradeDatabase : ScriptableObject
    {
        public UpgradesStats[] stats;

        public void ResetDatabase()
        {
            foreach (var upgrade in stats)
            {
                upgrade.upgradeCost = 0;
                upgrade.upgradeLevel = 0;
                upgrade.upgradeTier = 0;
                upgrade.float1 = 0;
            }
        }

        private void OnValidate()
        {
            foreach (var upgrade in stats)
            {
                upgrade.id = Array.IndexOf(stats, upgrade);
            }
        }
    }

    [Serializable]
    public class UpgradesStats 
    {
        public string upgradeName;
        public string upgradeDescription;
        public Sprite upgradeIcon;
        public float upgradeCost;
        public int upgradeLevel;
        public int upgradeTier;
        public float float1;
        
        public float upgradeBaseCost;
        public float baseFloat1;
        public int id;
    }
}