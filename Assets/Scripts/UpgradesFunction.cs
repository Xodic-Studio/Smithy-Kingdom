using UnityEngine;

public class UpgradesFunction : MonoBehaviour
{
    Ore _ore;
    GameManager _gameManager;
    int _oreUpgradeLevel;


    private void Awake()
    {
        _ore = Ore.Instance;
        _gameManager = GameManager.Instance;
    }
    

    public void UpgradeOre()
    {
        _ore.oreDatabase.ores[_oreUpgradeLevel+1].isUnlocked = true;
        _oreUpgradeLevel++;
    }
    
    public void ChangeHammerDamage(int damage)
    {
        _gameManager.ModifyHammerDamage(damage);
    }
}
