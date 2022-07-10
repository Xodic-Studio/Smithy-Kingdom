using UnityEngine;

public class UpgradesFunction : MonoBehaviour
{
    Ore _ore;
    int _oreUpgradeLevel;

    private void Awake()
    {
        _ore = Ore.Instance;
    }

    public void UpgradeOre()
    {
        _ore.oreDatabase.ores[_oreUpgradeLevel+1].isUnlocked = true;
        _oreUpgradeLevel++;
    }
}
