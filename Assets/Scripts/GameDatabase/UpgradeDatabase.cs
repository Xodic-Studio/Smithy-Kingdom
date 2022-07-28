using System;
using UnityEngine;
using UnityEngine.Events;

namespace GameDatabase
{
    [CreateAssetMenu(fileName = "New Upgrade Database", menuName = "Game/Database/Upgrades")]
    public class UpgradeDatabase : ScriptableObject
    {
        public UpgradesStats[] stats;
    }

    [Serializable]
    public class UpgradesStats
    {
        public string upgradeName;
        public string upgradeDescription;
        public string upgradeLevels;
        public UnityEvent upgradeEvent;
    }
}