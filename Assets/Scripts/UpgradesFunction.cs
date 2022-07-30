using System;
using GameDatabase;
using Manager;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UpgradesFunction : MonoBehaviour
{
    Ore _ore;
    GameManager _gameManager;
    UIManager _uiManager;
    SoundManager _soundManager;
    int _oreUpgradeLevel = 1;
    
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
        _soundManager = SoundManager.Instance;
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

    public void UpgradeOre()
    {
        if (_oreUpgradeLevel < _ore.oreDatabase.ores.Length)
        {
            if (_gameManager.HasMoney(5000) )
            {
                _ore.oreDatabase.ores[_oreUpgradeLevel].isUnlocked = true;
                _uiManager.AddNotification(UIManager.NotificationType.Ore, 1);
                _oreUpgradeLevel++;
                _soundManager.PlayOneShot(_soundManager.soundDatabase.GetSfx(SoundDatabase.SfxType.Upgrade)[0]);
            }
        }
        
    }
    
    public void ChangeHammerDamage(int damage)
    {
        if (_gameManager.HasMoney(1000))
        {
            _gameManager.ModifyHammerDamage(damage);
            _soundManager.PlayOneShot(_soundManager.soundDatabase.GetSfx(SoundDatabase.SfxType.Upgrade)[0]);
        }
        
    }
}
