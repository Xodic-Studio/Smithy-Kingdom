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
        if (_oreUpgradeLevel + 1 <= _ore.oreDatabase.ores.Length && _gameManager.GetMoney() >= 1000)
        {
            _ore.oreDatabase.ores[_oreUpgradeLevel+1].isUnlocked = true;
            _oreUpgradeLevel++;
            _gameManager.ModifyMoney(-1000);
        } 

    }
    
    public void ChangeHammerDamage(int damage)
    {
        if (_gameManager.GetMoney() >= 5000)
        {
            _gameManager.ModifyHammerDamage(damage);
            _gameManager.ModifyMoney(-5000);
        }
        
    }
}
