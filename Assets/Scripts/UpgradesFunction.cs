using UnityEngine;
using UnityEngine.UI;

public class UpgradesFunction : MonoBehaviour
{
    Ore _ore;
    int _oreUpgradeLevel;


    private void Awake()
    {
        _ore = Ore.Instance;
    }
    
    [Tooltip("Test Function")]
    public void Test()
    {
        Debug.Log("Test");
    }

    public void UpgradeOre()
    {
        _ore.oreDatabase.ores[_oreUpgradeLevel+1].isUnlocked = true;
        _oreUpgradeLevel++;
    }
}
