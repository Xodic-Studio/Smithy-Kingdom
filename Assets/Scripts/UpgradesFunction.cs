using System;
using GameDatabase;
using Manager;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UpgradesFunction : Singleton<UpgradesFunction>
{
    Ore _ore;
    GameManager _gameManager;
    UIManager _uiManager;
    SoundManagerr _soundManager;
    public UpgradeDatabase upgradeDatabase;
    public AchievementDatabase achievementDatabase;

    readonly float[] _oreUpgradeCost = new float[5] { 50000, 5000000, 5000000000, 5000000000000, 5000000000000000};
    public int upgradeCount;
    
    [Header("Upgrade Function Database")]
    public UpgradeFunction[] upgradeFunctionList;
    public UpgradeFunction[] premiumUpgradeFunctionList;


    [Serializable]
    public class UpgradeFunction
    {
        public string name;
        public UnityEvent upgradesFunction;
    }
    
    
    private void Awake()
    {
        _soundManager = SoundManagerr.Instance;
        _uiManager = UIManager.Instance;
        _ore = Ore.Instance;
        _gameManager = GameManager.Instance;
        achievementDatabase = _gameManager.achievementDatabase;
    }

    private void Start()
    {
        UpdateUpgradeButton();
        UpdatePriceStart();
    }
    
    private void UpdateUpgradeButton()
    {
        var i = 0;
        foreach (var function in upgradeFunctionList)
        {
            _uiManager.upgradeList.transform.GetChild(i).GetComponentInChildren<Button>().onClick.AddListener(delegate
            {
                function.upgradesFunction.Invoke();
                achievementDatabase.ModifyProgress("One small step for a man, one giant leap for the smithy",1);
            });
            i++;
        }

        i = 0;
        // foreach (var function in premiumUpgradeFunctionList)
        // {
        //     _uiManager.upgradeList.transform.GetChild(i).GetComponentInChildren<Button>().onClick.AddListener(delegate
        //     {
        //         function.upgradesFunction.Invoke();
        //         achievementDatabase.ModifyProgress("One small step for a man, one giant leap for the smithy",1);
        //     });
        // }
    }

    private void UpdatePriceStart()
    {
        upgradeDatabase.stats[0].upgradeCost = _oreUpgradeCost[upgradeDatabase.stats[0].upgradeLevel - 1];
        upgradeDatabase.stats[1].upgradeCost = upgradeDatabase.stats[1].upgradeBaseCost;
        upgradeDatabase.stats[2].upgradeCost = upgradeDatabase.stats[2].upgradeBaseCost;
        upgradeDatabase.stats[3].upgradeCost = upgradeDatabase.stats[3].upgradeBaseCost;
        upgradeDatabase.stats[4].upgradeCost = upgradeDatabase.stats[4].upgradeBaseCost * Mathf.Pow(100, upgradeDatabase.stats[4].upgradeLevel + 1);
        upgradeDatabase.stats[5].upgradeCost = upgradeDatabase.stats[5].upgradeBaseCost * Mathf.Pow(2, upgradeDatabase.stats[5].upgradeLevel + 1);
        upgradeDatabase.stats[6].upgradeCost = upgradeDatabase.stats[6].upgradeBaseCost * Mathf.Pow(1.15f, upgradeDatabase.stats[6].upgradeLevel + 1);
        upgradeDatabase.stats[7].upgradeCost = upgradeDatabase.stats[7].upgradeBaseCost * Mathf.Pow(1.15f, upgradeDatabase.stats[7].upgradeLevel + 1);
        upgradeDatabase.stats[8].upgradeCost = upgradeDatabase.stats[8].upgradeBaseCost * Mathf.Pow(1.15f, upgradeDatabase.stats[8].upgradeLevel + 1);
        upgradeDatabase.stats[9].upgradeCost = upgradeDatabase.stats[9].upgradeBaseCost * Mathf.Pow(1.15f, upgradeDatabase.stats[9].upgradeLevel + 1);
        upgradeDatabase.stats[10].upgradeCost = upgradeDatabase.stats[10].upgradeBaseCost * Mathf.Pow(1.15f, upgradeDatabase.stats[10].upgradeLevel + 1);
        upgradeDatabase.stats[11].upgradeCost = upgradeDatabase.stats[11].upgradeBaseCost * Mathf.Pow(1.15f, upgradeDatabase.stats[11].upgradeLevel + 1);
        upgradeDatabase.stats[12].upgradeCost = upgradeDatabase.stats[12].upgradeBaseCost * Mathf.Pow(1.15f, upgradeDatabase.stats[12].upgradeLevel + 1);
        upgradeDatabase.stats[13].upgradeCost = upgradeDatabase.stats[13].upgradeBaseCost * Mathf.Pow(1.15f, upgradeDatabase.stats[13].upgradeLevel + 1);
        upgradeDatabase.stats[14].upgradeCost = upgradeDatabase.stats[14].upgradeBaseCost * Mathf.Pow(1.15f, upgradeDatabase.stats[14].upgradeLevel + 1);
        upgradeDatabase.stats[15].upgradeCost = upgradeDatabase.stats[15].upgradeBaseCost * Mathf.Pow(1.15f, upgradeDatabase.stats[15].upgradeLevel + 1);
        upgradeDatabase.stats[16].upgradeCost = upgradeDatabase.stats[16].upgradeBaseCost * Mathf.Pow(1.15f, upgradeDatabase.stats[16].upgradeLevel + 1);
        upgradeDatabase.stats[17].upgradeCost = upgradeDatabase.stats[17].upgradeBaseCost * Mathf.Pow(1.15f, upgradeDatabase.stats[17].upgradeLevel + 1);
        upgradeDatabase.stats[18].upgradeCost = upgradeDatabase.stats[18].upgradeBaseCost * Mathf.Pow(1.15f, upgradeDatabase.stats[18].upgradeLevel + 1);
        upgradeDatabase.stats[19].upgradeCost = upgradeDatabase.stats[19].upgradeBaseCost * Mathf.Pow(1.15f, upgradeDatabase.stats[19].upgradeLevel + 1);
        upgradeDatabase.stats[20].upgradeCost = upgradeDatabase.stats[20].upgradeBaseCost * Mathf.Pow(1.15f, upgradeDatabase.stats[20].upgradeLevel + 1);
        upgradeDatabase.stats[21].upgradeCost = upgradeDatabase.stats[21].upgradeBaseCost * Mathf.Pow(1.15f, upgradeDatabase.stats[21].upgradeLevel + 1);
        upgradeDatabase.stats[22].upgradeCost = upgradeDatabase.stats[22].upgradeBaseCost * Mathf.Pow(1.15f, upgradeDatabase.stats[22].upgradeLevel + 1);
        upgradeDatabase.stats[23].upgradeCost = upgradeDatabase.stats[23].upgradeBaseCost * Mathf.Pow(1.15f, upgradeDatabase.stats[23].upgradeLevel + 1);
        upgradeDatabase.stats[24].upgradeCost = upgradeDatabase.stats[24].upgradeBaseCost * Mathf.Pow(1.15f, upgradeDatabase.stats[24].upgradeLevel + 1);
        upgradeDatabase.stats[25].upgradeCost = upgradeDatabase.stats[25].upgradeBaseCost * Mathf.Pow(1.15f, upgradeDatabase.stats[25].upgradeLevel + 1);
        
        foreach (var upgrade in upgradeDatabase.stats)
        {
            var upgradePriceText = _uiManager.upgradeList.transform.GetChild(upgrade.id).GetChild(2).GetChild(1).GetComponent<TextMeshProUGUI>();
            upgradePriceText.text = _gameManager.NumberToString((decimal) upgrade.upgradeCost);
        }

        foreach (Transform upgrade in _uiManager.upgradeList.transform)
        {
            upgrade.GetChild(2).GetChild(0).GetComponent<ButtonHold>().onHold = upgradeFunctionList[upgrade.GetSiblingIndex()].upgradesFunction;
        }
        
        _gameManager.CheckMoneyForUpgrades();
    }
    
    
    private void UpdateUpgradePrice(float price)
    {
        var upgradePriceText = EventSystem.current.currentSelectedGameObject.transform.parent.parent.GetChild(2).GetChild(1)
            .GetComponent<TMP_Text>();
        upgradePriceText.text = _gameManager.NumberToString((decimal)price);
        _gameManager.CheckMoneyForUpgrades();
    }

    private void UpdateUpgradeDescription (string baseDescription,string description)
    {
        Debug.Log(EventSystem.current.currentSelectedGameObject);
        var upgradeDescriptionText = EventSystem.current.currentSelectedGameObject.transform.parent.parent.GetChild(0).GetChild(1).GetComponent<TMP_Text>();
        upgradeDescriptionText.text = baseDescription + "\n" +
                                      description;
    }
    
    private int CheckLevel(int level)
    {
        if (level is >= 10 and < 25)
        {
            return 1;
        }
        if (level is >= 25 and < 50)
        {
            return 2;
        }
        if (level is >= 50 and < 100)
        {
            return 3;
        }
        if (level is >= 100 and < 250)
        {
            return 4;
        }
        if (level is >= 250 and < 500)
        {
            return 5;
        }
        if (level is >= 500 and < 1000)
        {
            return 6;
        }
        return 0;
    }


    #region NormalUpgrade
    public void UpgradeOre()
    {
        if (upgradeDatabase.stats[0].upgradeLevel < _ore.oreDatabase.ores.Length)
        {
            if (_gameManager.HasMoney(_oreUpgradeCost[upgradeDatabase.stats[0].upgradeLevel - 1]) )
            {
                _ore.oreDatabase.ores[upgradeDatabase.stats[0].upgradeLevel].isUnlocked = true;
                if (_ore.oreDatabase.ores[upgradeDatabase.stats[0].upgradeLevel].oreName == _ore.oreDatabase.ores[3].oreName)
                {
                    achievementDatabase.ModifyProgress("Power of the Falling Star",1);
                }
                _uiManager.AddNotification(UIManager.NotificationType.Ore, 1);
                upgradeDatabase.stats[0].upgradeLevel++;
                UpdateUpgradePrice(_oreUpgradeCost[upgradeDatabase.stats[0].upgradeLevel]);
                _soundManager.PlaySound("Upgrade");
                upgradeCount++;
            }
        }
    }

    private bool _hammerDamage1, _hammerDamage2, _hammerDamage3;
    public int hammerTier;
    
    public void ChangeHammerDamage1()
    {
        if (!_hammerDamage1)
        {
            if (_gameManager.HasMoney(upgradeDatabase.stats[1].upgradeBaseCost))
            {
                _hammerDamage1 = true;
                _gameManager.ModifyHammerDamage(2);
                upgradeCount++;
                hammerTier ++;
                EventSystem.current.currentSelectedGameObject.transform.parent.parent.gameObject.SetActive(false);
               // _soundManager.PlayOneShot(_soundManager.soundDatabase.GetSfx(SoundDatabase.SfxType.Upgrade)[0]);
               _soundManager.PlaySound("Upgrade");
            }
        }
    }
    
    public void ChangeHammerDamage2()
    {
        if (!_hammerDamage2)
        {
            if (_gameManager.HasMoney(upgradeDatabase.stats[2].upgradeBaseCost))
            {
                _hammerDamage2 = true;
                _gameManager.ModifyHammerDamage(4);
                upgradeCount++;
                hammerTier ++;
                EventSystem.current.currentSelectedGameObject.transform.parent.parent.gameObject.SetActive(false);
                _soundManager.PlaySound("Upgrade");
            }
        }
    }
    
    public void ChangeHammerDamage3()
    {
        if (!_hammerDamage3)
        {
            if (_gameManager.HasMoney(upgradeDatabase.stats[3].upgradeBaseCost))
            {
                _hammerDamage3 = true;
                _gameManager.ModifyHammerDamage(8);
                upgradeCount++;
                hammerTier ++;
                EventSystem.current.currentSelectedGameObject.transform.parent.parent.gameObject.SetActive(false);
                _soundManager.PlaySound("Upgrade");
            }
        }
    }

    public void HammerEnhancement()
    {
        upgradeDatabase.stats[4].upgradeCost = upgradeDatabase.stats[4].upgradeBaseCost * Mathf.Pow(100, upgradeDatabase.stats[4].upgradeLevel);
        if (_gameManager.HasMoney(upgradeDatabase.stats[4].upgradeCost))
        {
            upgradeDatabase.stats[4].upgradeLevel++; 
            upgradeCount++;
            UpdateUpgradePrice(upgradeDatabase.stats[4].upgradeCost);
            _soundManager.PlaySound("Upgrade");
        }
    }
    
    public void HammerEnvironment()
    {
        upgradeDatabase.stats[5].upgradeCost = upgradeDatabase.stats[5].upgradeBaseCost * Mathf.Pow(2, upgradeDatabase.stats[5].upgradeLevel);
        if (_gameManager.HasMoney(upgradeDatabase.stats[5].upgradeBaseCost * Mathf.Pow(2, upgradeDatabase.stats[5].upgradeLevel)))
        {
            upgradeDatabase.stats[5].upgradeLevel++;
            upgradeCount++;
            UpdateUpgradePrice(upgradeDatabase.stats[5].upgradeCost);
            _soundManager.PlaySound("Upgrade");
        }
    }

    public float passiveDamage;

    void UpdateDamagePassive()
    {
        passiveDamage = upgradeDatabase.stats[6].float1 + upgradeDatabase.stats[7].float1 +
                        upgradeDatabase.stats[8].float1 +
                        upgradeDatabase.stats[9].float1 + upgradeDatabase.stats[10].float1 +
                        upgradeDatabase.stats[11].float1 +
                        upgradeDatabase.stats[12].float1 + upgradeDatabase.stats[13].float1 +
                        upgradeDatabase.stats[14].float1 +
                        upgradeDatabase.stats[15].float1;
                        achievementDatabase.ModifyProgress("Enthusiastic Helper", passiveDamage);
    }

    public void HelpingHand()
    {
        upgradeDatabase.stats[6].upgradeCost = upgradeDatabase.stats[6].upgradeBaseCost *
                                               Mathf.Pow(1.15f, upgradeDatabase.stats[6].upgradeLevel);
        if (_gameManager.HasMoney(upgradeDatabase.stats[6].upgradeCost))
        {
            upgradeDatabase.stats[6].upgradeLevel++;
            upgradeDatabase.stats[6].upgradeTier = CheckLevel(upgradeDatabase.stats[6].upgradeLevel);
            upgradeDatabase.stats[6].float1 = upgradeDatabase.stats[6].baseFloat1 * upgradeDatabase.stats[6].upgradeLevel * Mathf.Pow(2,upgradeDatabase.stats[6].upgradeTier) * (1 + 0.02f * _gameManager.reputation) ;
            UpdateDamagePassive();
            UpdateUpgradeDescription($"Decrease Ore Hp By {_gameManager.NumberToString((decimal)upgradeDatabase.stats[6].baseFloat1)} per second",$"-{_gameManager.NumberToString((decimal)upgradeDatabase.stats[6].float1)}/sec");
            UpdateUpgradePrice(upgradeDatabase.stats[6].upgradeCost);
            upgradeCount++;
            _soundManager.PlaySound("Upgrade");
        }
    }
    
    public void MoreHelpers()
    {
        upgradeDatabase.stats[7].upgradeCost = upgradeDatabase.stats[7].upgradeBaseCost *
                                               Mathf.Pow(1.15f, upgradeDatabase.stats[7].upgradeLevel);
        if (_gameManager.HasMoney(upgradeDatabase.stats[7].upgradeCost))
        {
            upgradeDatabase.stats[7].upgradeLevel++;
            upgradeDatabase.stats[7].upgradeTier = CheckLevel(upgradeDatabase.stats[7].upgradeLevel);
            upgradeDatabase.stats[7].float1 = upgradeDatabase.stats[7].baseFloat1 * upgradeDatabase.stats[7].upgradeLevel * Mathf.Pow(2,upgradeDatabase.stats[7].upgradeTier) * (1 + 0.02f * _gameManager.reputation) ;
            upgradeCount++;
            UpdateDamagePassive();
            UpdateUpgradeDescription($"Decrease Ore Hp By {_gameManager.NumberToString((decimal)upgradeDatabase.stats[7].baseFloat1)} per second",$"+{_gameManager.NumberToString((decimal)upgradeDatabase.stats[7].float1)}/sec");
            UpdateUpgradePrice(upgradeDatabase.stats[7].upgradeCost);
            _soundManager.PlaySound("Upgrade");
        }
    }
    
    public void LingeringFlame()
    {
        upgradeDatabase.stats[8].upgradeCost = upgradeDatabase.stats[8].upgradeBaseCost *
                                               Mathf.Pow(1.15f, upgradeDatabase.stats[8].upgradeLevel);
        if (_gameManager.HasMoney(upgradeDatabase.stats[8].upgradeCost))
        {
            upgradeDatabase.stats[8].upgradeLevel++;
            upgradeDatabase.stats[8].upgradeTier = CheckLevel(upgradeDatabase.stats[8].upgradeLevel);
            upgradeDatabase.stats[8].float1 = upgradeDatabase.stats[8].baseFloat1 * upgradeDatabase.stats[8].upgradeLevel * Mathf.Pow(2,upgradeDatabase.stats[8].upgradeTier) * (1 + 0.02f * _gameManager.reputation) ;
            upgradeCount++;
            UpdateDamagePassive();
            UpdateUpgradeDescription($"Decrease Ore Hp By {_gameManager.NumberToString((decimal)upgradeDatabase.stats[8].baseFloat1)} per second",$"+{_gameManager.NumberToString((decimal)upgradeDatabase.stats[8].float1)}/sec");
            UpdateUpgradePrice(upgradeDatabase.stats[8].upgradeCost);
            _soundManager.PlaySound("Upgrade");
        }
    }
    
    public void SelfForgingHammer()
    {
        upgradeDatabase.stats[9].upgradeCost = upgradeDatabase.stats[9].upgradeBaseCost *
                                               Mathf.Pow(1.15f, upgradeDatabase.stats[9].upgradeLevel);
        if (_gameManager.HasMoney(upgradeDatabase.stats[9].upgradeCost))
        {
            upgradeDatabase.stats[9].upgradeLevel++;
            upgradeDatabase.stats[9].upgradeTier = CheckLevel(upgradeDatabase.stats[9].upgradeLevel);
            upgradeDatabase.stats[9].float1 = upgradeDatabase.stats[9].baseFloat1 * upgradeDatabase.stats[9].upgradeLevel * Mathf.Pow(2,upgradeDatabase.stats[9].upgradeTier) * (1 + 0.02f * _gameManager.reputation) ;
            upgradeCount++;
            UpdateDamagePassive();
            UpdateUpgradeDescription($"Decrease Ore Hp By {_gameManager.NumberToString((decimal)upgradeDatabase.stats[9].baseFloat1)} per second",$"+{_gameManager.NumberToString((decimal)upgradeDatabase.stats[9].float1)}/sec");
            UpdateUpgradePrice(upgradeDatabase.stats[9].upgradeCost);
            _soundManager.PlaySound("Upgrade");
        }
    }
    
    public void SelfForgingAnvil()
    {
        upgradeDatabase.stats[10].upgradeCost = upgradeDatabase.stats[10].upgradeBaseCost *
                                                Mathf.Pow(1.15f, upgradeDatabase.stats[10].upgradeLevel);
        if (_gameManager.HasMoney(upgradeDatabase.stats[10].upgradeCost))
        {
            upgradeDatabase.stats[10].upgradeLevel++;
            upgradeDatabase.stats[10].upgradeTier = CheckLevel(upgradeDatabase.stats[10].upgradeLevel);
            upgradeDatabase.stats[10].float1 = upgradeDatabase.stats[10].baseFloat1 * upgradeDatabase.stats[10].upgradeLevel * Mathf.Pow(2,upgradeDatabase.stats[10].upgradeTier) * (1 + 0.02f * _gameManager.reputation) ;
            upgradeCount++;
            UpdateDamagePassive();
            UpdateUpgradeDescription($"Decrease Ore Hp By {_gameManager.NumberToString((decimal)upgradeDatabase.stats[10].baseFloat1)} per second",$"+{_gameManager.NumberToString((decimal)upgradeDatabase.stats[10].float1)}/sec");
            UpdateUpgradePrice(upgradeDatabase.stats[10].upgradeCost);
            _soundManager.PlaySound("Upgrade");
        }
    }
    
    public void SelfForgingOre()
    {
        upgradeDatabase.stats[11].upgradeCost = upgradeDatabase.stats[11].upgradeBaseCost *
                                                Mathf.Pow(1.15f, upgradeDatabase.stats[11].upgradeLevel);
        if (_gameManager.HasMoney(upgradeDatabase.stats[11].upgradeCost))
        {
            upgradeDatabase.stats[11].upgradeLevel++;
            upgradeDatabase.stats[11].upgradeTier = CheckLevel(upgradeDatabase.stats[11].upgradeLevel);
            upgradeDatabase.stats[11].float1 = upgradeDatabase.stats[11].baseFloat1 * upgradeDatabase.stats[11].upgradeLevel * Mathf.Pow(2,upgradeDatabase.stats[11].upgradeTier) * (1 + 0.02f * _gameManager.reputation) ;
            upgradeCount++;
            UpdateDamagePassive();
            UpdateUpgradeDescription($"Decrease Ore Hp By {_gameManager.NumberToString((decimal)upgradeDatabase.stats[11].baseFloat1)} per second",$"+{_gameManager.NumberToString((decimal)upgradeDatabase.stats[11].float1)}/sec");
            UpdateUpgradePrice(upgradeDatabase.stats[11].upgradeCost);
            _soundManager.PlaySound("Upgrade");
        }
    }
    
    public void SentientSmithy()
    {
        upgradeDatabase.stats[12].upgradeCost = upgradeDatabase.stats[12].upgradeBaseCost *
                                                Mathf.Pow(1.15f, upgradeDatabase.stats[12].upgradeLevel);
        if (_gameManager.HasMoney(upgradeDatabase.stats[12].upgradeCost))
        {
            upgradeDatabase.stats[12].upgradeLevel++;
            upgradeDatabase.stats[12].upgradeTier = CheckLevel(upgradeDatabase.stats[12].upgradeLevel);
            upgradeDatabase.stats[12].float1 = upgradeDatabase.stats[12].baseFloat1 * upgradeDatabase.stats[12].upgradeLevel * Mathf.Pow(2,upgradeDatabase.stats[12].upgradeTier) * (1 + 0.02f * _gameManager.reputation) ;
            upgradeCount++;
            UpdateDamagePassive();
            UpdateUpgradeDescription($"Decrease Ore Hp By {_gameManager.NumberToString((decimal)upgradeDatabase.stats[12].baseFloat1)} per second",$"+{_gameManager.NumberToString((decimal)upgradeDatabase.stats[12].float1)}/sec");
            UpdateUpgradePrice(upgradeDatabase.stats[12].upgradeCost);
            _soundManager.PlaySound("Upgrade");
        }
    }
    
    public void AncestralSpirits()
    {
        upgradeDatabase.stats[13].upgradeCost = upgradeDatabase.stats[13].upgradeBaseCost *
                                                Mathf.Pow(1.15f, upgradeDatabase.stats[13].upgradeLevel);
        if (_gameManager.HasMoney(upgradeDatabase.stats[13].upgradeCost))
        {
            upgradeDatabase.stats[13].upgradeLevel++;
            upgradeDatabase.stats[13].upgradeTier = CheckLevel(upgradeDatabase.stats[13].upgradeLevel);
            upgradeDatabase.stats[13].float1 = upgradeDatabase.stats[13].baseFloat1 * upgradeDatabase.stats[13].upgradeLevel * Mathf.Pow(2,upgradeDatabase.stats[13].upgradeTier) * (1 + 0.02f * _gameManager.reputation) ;
            upgradeCount++;
            UpdateDamagePassive();
            UpdateUpgradeDescription($"Decrease Ore Hp By {_gameManager.NumberToString((decimal)upgradeDatabase.stats[13].baseFloat1)} per second",$"+{_gameManager.NumberToString((decimal)upgradeDatabase.stats[13].float1)}/sec");
            UpdateUpgradePrice(upgradeDatabase.stats[13].upgradeCost);
            _soundManager.PlaySound("Upgrade");
        }
    }
    
    public void FutureGenerations()
    {
        upgradeDatabase.stats[14].upgradeCost = upgradeDatabase.stats[14].upgradeBaseCost *
                                                Mathf.Pow(1.15f, upgradeDatabase.stats[14].upgradeLevel);
        if (_gameManager.HasMoney(upgradeDatabase.stats[14].upgradeCost))
        {
            upgradeDatabase.stats[14].upgradeLevel++;
            upgradeDatabase.stats[14].upgradeTier = CheckLevel(upgradeDatabase.stats[14].upgradeLevel);
            upgradeDatabase.stats[14].float1 = upgradeDatabase.stats[14].baseFloat1 * upgradeDatabase.stats[14].upgradeLevel * Mathf.Pow(2,upgradeDatabase.stats[14].upgradeTier) * (1 + 0.02f * _gameManager.reputation) ;
            upgradeCount++;
            UpdateDamagePassive();
            UpdateUpgradeDescription($"Decrease Ore Hp By {_gameManager.NumberToString((decimal)upgradeDatabase.stats[14].baseFloat1)} per second",$"+{_gameManager.NumberToString((decimal)upgradeDatabase.stats[14].float1)}/sec");
            UpdateUpgradePrice(upgradeDatabase.stats[14].upgradeCost);
            _soundManager.PlaySound("Upgrade");
        }
    }
    
    public void CsharpHammer()
    {
        upgradeDatabase.stats[15].upgradeCost = upgradeDatabase.stats[15].upgradeBaseCost *
                                                Mathf.Pow(1.15f, upgradeDatabase.stats[15].upgradeLevel);
        if (_gameManager.HasMoney(upgradeDatabase.stats[15].upgradeCost))
        {
            upgradeDatabase.stats[15].upgradeLevel++;
            upgradeDatabase.stats[15].upgradeTier = CheckLevel(upgradeDatabase.stats[15].upgradeLevel);
            upgradeDatabase.stats[15].float1 = upgradeDatabase.stats[15].baseFloat1 * upgradeDatabase.stats[15].upgradeLevel * Mathf.Pow(2,upgradeDatabase.stats[15].upgradeTier) * (1 + 0.02f * _gameManager.reputation) ;
            upgradeCount++;
            UpdateDamagePassive();
            UpdateUpgradeDescription($"Decrease Ore Hp By {_gameManager.NumberToString((decimal)upgradeDatabase.stats[15].baseFloat1)} per second",$"+{_gameManager.NumberToString((decimal)upgradeDatabase.stats[15].float1)}/sec");
            UpdateUpgradePrice(upgradeDatabase.stats[15].upgradeCost);
            _soundManager.PlaySound("Upgrade");
        }
    }

    public float passiveMoney;

    private void UpdatePassiveMoney()
    {
        passiveMoney = upgradeDatabase.stats[16].float1 + upgradeDatabase.stats[17].float1 +
                       upgradeDatabase.stats[18].float1 + upgradeDatabase.stats[19].float1 +
                       upgradeDatabase.stats[20].float1 + upgradeDatabase.stats[21].float1 +
                       upgradeDatabase.stats[22].float1 + upgradeDatabase.stats[23].float1 +
                       upgradeDatabase.stats[24].float1 + upgradeDatabase.stats[25].float1;
    }
    
    public void DonationBox()
    {
        upgradeDatabase.stats[16].upgradeCost = upgradeDatabase.stats[16].upgradeBaseCost *
                                                Mathf.Pow(1.15f, upgradeDatabase.stats[16].upgradeLevel);
        if (_gameManager.HasMoney(upgradeDatabase.stats[16].upgradeCost))
        {
            upgradeDatabase.stats[16].upgradeLevel++;
            upgradeDatabase.stats[16].upgradeTier = CheckLevel(upgradeDatabase.stats[16].upgradeLevel);
            upgradeDatabase.stats[16].float1 = upgradeDatabase.stats[16].baseFloat1 * upgradeDatabase.stats[16].upgradeLevel * Mathf.Pow(2,upgradeDatabase.stats[16].upgradeTier) * (1 + 0.02f * _gameManager.reputation) ;
            upgradeCount++;
            UpdatePassiveMoney();
            UpdateUpgradeDescription($"Increase Passive Money By {_gameManager.NumberToString((decimal)upgradeDatabase.stats[16].baseFloat1)} per second",$"+{_gameManager.NumberToString((decimal)upgradeDatabase.stats[16].float1)}/sec");
            UpdateUpgradePrice(upgradeDatabase.stats[16].upgradeCost);
            _soundManager.PlaySound("Upgrade");
        }
    }
    
    public void CommunityService()
    {
        upgradeDatabase.stats[17].upgradeCost = upgradeDatabase.stats[17].upgradeBaseCost *
                                                Mathf.Pow(1.15f, upgradeDatabase.stats[17].upgradeLevel);
        if (_gameManager.HasMoney(upgradeDatabase.stats[17].upgradeCost))
        {
            upgradeDatabase.stats[17].upgradeLevel++;
            upgradeDatabase.stats[17].upgradeTier = CheckLevel(upgradeDatabase.stats[17].upgradeLevel);
            upgradeDatabase.stats[17].float1 = upgradeDatabase.stats[17].baseFloat1 * upgradeDatabase.stats[17].upgradeLevel * Mathf.Pow(2,upgradeDatabase.stats[17].upgradeTier) * (1 + 0.02f * _gameManager.reputation) ;
            upgradeCount++;
            UpdatePassiveMoney();
            UpdateUpgradeDescription($"Increase Passive Money By {_gameManager.NumberToString((decimal)upgradeDatabase.stats[17].baseFloat1)} per second",$"+{_gameManager.NumberToString((decimal)upgradeDatabase.stats[17].float1)}/sec");
            UpdateUpgradePrice(upgradeDatabase.stats[17].upgradeCost);
            _soundManager.PlaySound("Upgrade");
        }
    }
    
    public void SwordBetting()
    {
        upgradeDatabase.stats[18].upgradeCost = upgradeDatabase.stats[18].upgradeBaseCost *
                                                Mathf.Pow(1.15f, upgradeDatabase.stats[18].upgradeLevel);
        if (_gameManager.HasMoney(upgradeDatabase.stats[18].upgradeCost))
        {
            upgradeDatabase.stats[18].upgradeLevel++;
            upgradeDatabase.stats[18].upgradeTier = CheckLevel(upgradeDatabase.stats[18].upgradeLevel);
            upgradeDatabase.stats[18].float1 = upgradeDatabase.stats[18].baseFloat1 * upgradeDatabase.stats[18].upgradeLevel * Mathf.Pow(2,upgradeDatabase.stats[18].upgradeTier) * (1 + 0.02f * _gameManager.reputation) ;
            upgradeCount++;
            UpdatePassiveMoney();
            UpdateUpgradeDescription($"Increase Passive Money By {_gameManager.NumberToString((decimal)upgradeDatabase.stats[18].baseFloat1)} per second",$"+{_gameManager.NumberToString((decimal)upgradeDatabase.stats[18].float1)}/sec");
            UpdateUpgradePrice(upgradeDatabase.stats[18].upgradeCost);
            _soundManager.PlaySound("Upgrade");
        }
    }
    
    public void SwordReselling()
    {
        upgradeDatabase.stats[19].upgradeCost = upgradeDatabase.stats[19].upgradeBaseCost *
                                                Mathf.Pow(1.15f, upgradeDatabase.stats[19].upgradeLevel);
        if (_gameManager.HasMoney(upgradeDatabase.stats[19].upgradeCost))
        {
            upgradeDatabase.stats[19].upgradeLevel++;
            upgradeDatabase.stats[19].upgradeTier = CheckLevel(upgradeDatabase.stats[19].upgradeLevel);
            upgradeDatabase.stats[19].float1 = upgradeDatabase.stats[19].baseFloat1 * upgradeDatabase.stats[19].upgradeLevel * Mathf.Pow(2,upgradeDatabase.stats[19].upgradeTier) * (1 + 0.02f * _gameManager.reputation) ;
            upgradeCount++;
            UpdatePassiveMoney();
            UpdateUpgradeDescription($"Increase Passive Money By {_gameManager.NumberToString((decimal)upgradeDatabase.stats[19].baseFloat1)} per second",$"+{_gameManager.NumberToString((decimal)upgradeDatabase.stats[19].float1)}/sec");
            UpdateUpgradePrice(upgradeDatabase.stats[19].upgradeCost);
            _soundManager.PlaySound("Upgrade");
        }
    }
    
    public void SmithyNetwork()
    {
        upgradeDatabase.stats[20].upgradeCost = upgradeDatabase.stats[20].upgradeBaseCost *
                                                Mathf.Pow(1.15f, upgradeDatabase.stats[20].upgradeLevel);
        if (_gameManager.HasMoney(upgradeDatabase.stats[20].upgradeCost))
        {
            upgradeDatabase.stats[20].upgradeLevel++;
            upgradeDatabase.stats[20].upgradeTier = CheckLevel(upgradeDatabase.stats[20].upgradeLevel);
            upgradeDatabase.stats[20].float1 = upgradeDatabase.stats[20].baseFloat1 * upgradeDatabase.stats[20].upgradeLevel * Mathf.Pow(2,upgradeDatabase.stats[20].upgradeTier) * (1 + 0.02f * _gameManager.reputation) ;
            upgradeCount++;
            UpdatePassiveMoney();
            UpdateUpgradeDescription($"Increase Passive Money By {_gameManager.NumberToString((decimal)upgradeDatabase.stats[20].baseFloat1)} per second",$"+{_gameManager.NumberToString((decimal)upgradeDatabase.stats[20].float1)}/sec");
            UpdateUpgradePrice(upgradeDatabase.stats[20].upgradeCost);
            _soundManager.PlaySound("Upgrade");
        }
    }
    
    public void SmithyCooperation()
    {
        upgradeDatabase.stats[21].upgradeCost = upgradeDatabase.stats[21].upgradeBaseCost *
                                                Mathf.Pow(1.15f, upgradeDatabase.stats[21].upgradeLevel);
        if (_gameManager.HasMoney(upgradeDatabase.stats[21].upgradeCost))
        {
            upgradeDatabase.stats[21].upgradeLevel++;
            upgradeDatabase.stats[21].upgradeTier = CheckLevel(upgradeDatabase.stats[21].upgradeLevel);
            upgradeDatabase.stats[21].float1 = upgradeDatabase.stats[21].baseFloat1 * upgradeDatabase.stats[21].upgradeLevel * Mathf.Pow(2,upgradeDatabase.stats[21].upgradeTier) * (1 + 0.02f * _gameManager.reputation) ;
            upgradeCount++;
            UpdatePassiveMoney();
            UpdateUpgradeDescription($"Increase Passive Money By {_gameManager.NumberToString((decimal)upgradeDatabase.stats[21].baseFloat1)} per second",$"+{_gameManager.NumberToString((decimal)upgradeDatabase.stats[21].float1)}/sec");
            UpdateUpgradePrice(upgradeDatabase.stats[21].upgradeCost);
            _soundManager.PlaySound("Upgrade");
        }
    }
    
    public void InternationalSmithy()
    {
        upgradeDatabase.stats[22].upgradeCost = upgradeDatabase.stats[22].upgradeBaseCost *
                                                Mathf.Pow(1.15f, upgradeDatabase.stats[22].upgradeLevel);
        if (_gameManager.HasMoney(upgradeDatabase.stats[22].upgradeCost))
        {
            upgradeDatabase.stats[22].upgradeLevel++;
            upgradeDatabase.stats[22].upgradeTier = CheckLevel(upgradeDatabase.stats[22].upgradeLevel);
            upgradeDatabase.stats[22].float1 = upgradeDatabase.stats[22].baseFloat1 * upgradeDatabase.stats[22].upgradeLevel * Mathf.Pow(2,upgradeDatabase.stats[22].upgradeTier) * (1 + 0.02f * _gameManager.reputation) ;
            upgradeCount++;
            UpdatePassiveMoney();
            UpdateUpgradeDescription($"Increase Passive Money By {_gameManager.NumberToString((decimal)upgradeDatabase.stats[22].baseFloat1)} per second",$"+{_gameManager.NumberToString((decimal)upgradeDatabase.stats[22].float1)}/sec");
            UpdateUpgradePrice(upgradeDatabase.stats[22].upgradeCost);
            _soundManager.PlaySound("Upgrade");
        }
    }
    
    public void NonFungibleWeapons()
    {
        upgradeDatabase.stats[23].upgradeCost = upgradeDatabase.stats[23].upgradeBaseCost *
                                                Mathf.Pow(1.15f, upgradeDatabase.stats[23].upgradeLevel);
        if (_gameManager.HasMoney(upgradeDatabase.stats[23].upgradeCost))
        {
            upgradeDatabase.stats[23].upgradeLevel++;
            upgradeDatabase.stats[23].upgradeTier = CheckLevel(upgradeDatabase.stats[23].upgradeLevel);
            upgradeDatabase.stats[23].float1 = upgradeDatabase.stats[23].baseFloat1 * upgradeDatabase.stats[23].upgradeLevel * Mathf.Pow(2,upgradeDatabase.stats[23].upgradeTier) * (1 + 0.02f * _gameManager.reputation) ;
            upgradeCount++;
            UpdatePassiveMoney();
            UpdateUpgradeDescription($"Increase Passive Money By {_gameManager.NumberToString((decimal)upgradeDatabase.stats[23].baseFloat1)} per second",$"+{_gameManager.NumberToString((decimal)upgradeDatabase.stats[23].float1)}/sec");
            UpdateUpgradePrice(upgradeDatabase.stats[23].upgradeCost);
            _soundManager.PlaySound("Upgrade");
        }
    }
    
    public void CoinDuplicationGlitch()
    {
        upgradeDatabase.stats[24].upgradeCost = upgradeDatabase.stats[24].upgradeBaseCost *
                                                Mathf.Pow(1.15f, upgradeDatabase.stats[24].upgradeLevel);
        if (_gameManager.HasMoney(upgradeDatabase.stats[24].upgradeCost))
        {
            upgradeDatabase.stats[24].upgradeLevel++;
            upgradeDatabase.stats[24].upgradeTier = CheckLevel(upgradeDatabase.stats[24].upgradeLevel);
            upgradeDatabase.stats[24].float1 = upgradeDatabase.stats[24].baseFloat1 * upgradeDatabase.stats[24].upgradeLevel * Mathf.Pow(2,upgradeDatabase.stats[24].upgradeTier) * (1 + 0.02f * _gameManager.reputation) ;
            upgradeCount++;
            UpdatePassiveMoney();
            UpdateUpgradeDescription($"Increase Passive Money By {_gameManager.NumberToString((decimal)upgradeDatabase.stats[24].baseFloat1)} per second",$"+{_gameManager.NumberToString((decimal)upgradeDatabase.stats[24].float1)}/sec");
            UpdateUpgradePrice(upgradeDatabase.stats[24].upgradeCost);
            _soundManager.PlaySound("Upgrade");
        }
    }
    
    public void WeaponDuplicationGlitch()
    {
        upgradeDatabase.stats[25].upgradeCost = upgradeDatabase.stats[25].upgradeBaseCost *
                                                Mathf.Pow(1.15f, upgradeDatabase.stats[25].upgradeLevel);
        if (_gameManager.HasMoney(upgradeDatabase.stats[25].upgradeCost))
        {
            upgradeDatabase.stats[25].upgradeLevel++;
            upgradeDatabase.stats[25].upgradeTier = CheckLevel(upgradeDatabase.stats[25].upgradeLevel);
            upgradeDatabase.stats[25].float1 = upgradeDatabase.stats[25].baseFloat1 * upgradeDatabase.stats[25].upgradeLevel * Mathf.Pow(2,upgradeDatabase.stats[25].upgradeTier) * (1 + 0.02f * _gameManager.reputation) ;
            upgradeCount++;
            UpdatePassiveMoney();
            UpdateUpgradeDescription($"Increase Passive Money By {_gameManager.NumberToString((decimal)upgradeDatabase.stats[25].baseFloat1)} per second",$"+{_gameManager.NumberToString((decimal)upgradeDatabase.stats[25].float1)}/sec");
            UpdateUpgradePrice(upgradeDatabase.stats[25].upgradeCost);
            _soundManager.PlaySound("Upgrade");
        }
    }
    
    #endregion

    #region Premium Upgrades

    public void AutoClicker()
    {
        
    }

    public void AmazingLuck()
    {
        
    }

    public void VipMailService()
    {
        
    }

    public void UnbelievableReputation()
    {
        
    }
    

    #endregion
    
    
}
