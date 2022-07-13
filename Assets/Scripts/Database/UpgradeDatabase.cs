using System;
using UnityEngine;

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
}
