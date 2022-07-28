using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UpgradesFunction : MonoBehaviour
{
    Ore _ore;
    GameManager _gameManager;
    UIManager _uiManager;
    int _oreUpgradeLevel;
    
    [Header("Upgrade Function Database")]
    public UpgradeFunction[] upgradeFunctionList;


    [Serializable]
    public class UpgradeFunction
    {
        public string name;
        public UnityEvent upgradesFunction;
    }
    
    
    private void Awake()
    {
        _uiManager = UIManager.Instance;
        _ore = Ore.Instance;
        _gameManager = GameManager.Instance;
    }

    private void Start()
    {
        UpdateUpgradeButton();
    }
    
    private void UpdateUpgradeButton()
    {
        var i = 0;
        foreach (var function in upgradeFunctionList)
        {
            _uiManager.upgradeList.transform.GetChild(i).GetComponentInChildren<Button>().onClick.AddListener(function.upgradesFunction.Invoke);
            i++;
        }
    }

    private bool HasMoney(int amount)
    {
        if (_gameManager.GetMoney() < amount)
        {
            Debug.Log("Not enough money");
            return false;
        }
        _gameManager.ModifyMoney(-amount);
        return true;
    }

    public void UpgradeOre()
    {
        if (_oreUpgradeLevel + 1 <= _ore.oreDatabase.ores.Length && HasMoney(1000) )
        {
            _ore.oreDatabase.ores[_oreUpgradeLevel+1].isUnlocked = true;
            _oreUpgradeLevel++;
        }
    }
    
    public void ChangeHammerDamage(int damage)
    {
        if (HasMoney(5000))
        {
            _gameManager.ModifyHammerDamage(damage);
        }
        
    }
}
