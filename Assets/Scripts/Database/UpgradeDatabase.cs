using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

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
