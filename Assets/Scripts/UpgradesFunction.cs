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
    SoundManager _soundManager;
    public UpgradeDatabase upgradeDatabase;
    public AchievementDatabase achievementDatabase;
    
    int _oreUpgradeLevel = 1;
    float[] _oreUpgradeCost = new float[5] { 50000, 5000000, 5000000000, 5000000000000, 5000000000000000};
    public int upgradeCount = 0;
    
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
        achievementDatabase = _gameManager.achievementDatabase;
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
            _uiManager.upgradeList.transform.GetChild(i).GetComponentInChildren<Button>().onClick.AddListener(delegate
            {
                function.upgradesFunction.Invoke();
                achievementDatabase.ModifyProgress("One small step for a man, one giant leap for the smithy",1);
            });
            i++;
        }
    }

    private void UpdateUpgradePrice(float price)
    {
        var upgradePriceText = EventSystem.current.currentSelectedGameObject.transform.parent.parent.GetChild(2).GetChild(1)
            .GetComponent<TMP_Text>();
        upgradePriceText.text = _gameManager.NumberToString((decimal)price);
        if (price > _gameManager.GetMoney())
        {
            var parent = EventSystem.current.currentSelectedGameObject.transform.parent;
            var parent1 = parent.parent;
            parent1.GetChild(2).GetChild(0).GetComponent<Image>().color = Color.red;
        }
        else
        {
            EventSystem.current.currentSelectedGameObject.transform.parent.parent.GetChild(2).GetChild(0).GetComponent<Image>().color = Color.white;
        }
    }

    private void UpdateUpgradeDescription (string baseDescription,string description)
    {
        var upgradeDescriptionText = EventSystem.current.currentSelectedGameObject.transform.parent.parent.GetChild(0).GetChild(1).GetComponent<TMP_Text>();
        upgradeDescriptionText.text = baseDescription + "\n" +
                                      description;
    }



    public void UpgradeOre()
    {
        if (_oreUpgradeLevel < _ore.oreDatabase.ores.Length)
        {
            if (_gameManager.HasMoney(_oreUpgradeCost[_oreUpgradeLevel - 1]) )
            {
                _ore.oreDatabase.ores[_oreUpgradeLevel].isUnlocked = true;
                if (_ore.oreDatabase.ores[_oreUpgradeLevel].oreName == _ore.oreDatabase.ores[3].oreName)
                {
                    achievementDatabase.ModifyProgress("Power of the Falling Star",1);
                }
                _uiManager.AddNotification(UIManager.NotificationType.Ore, 1);
                UpdateUpgradePrice(_oreUpgradeCost[_oreUpgradeLevel - 1]);
                _oreUpgradeLevel++;
                _soundManager.PlayOneShot(_soundManager.soundDatabase.GetSfx(SoundDatabase.SfxType.Upgrade)[0]);
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
                _soundManager.PlayOneShot(_soundManager.soundDatabase.GetSfx(SoundDatabase.SfxType.Upgrade)[0]);
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
                _soundManager.PlayOneShot(_soundManager.soundDatabase.GetSfx(SoundDatabase.SfxType.Upgrade)[0]);
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
                _soundManager.PlayOneShot(_soundManager.soundDatabase.GetSfx(SoundDatabase.SfxType.Upgrade)[0]);
            }
        }
    }

    public int hammerEnhancementLevel;

    public void HammerEnhancement()
    {
        upgradeDatabase.stats[4].upgradeCost = upgradeDatabase.stats[4].upgradeBaseCost * Mathf.Pow(100, hammerEnhancementLevel);
        if (_gameManager.HasMoney(upgradeDatabase.stats[4].upgradeCost))
        {
            hammerEnhancementLevel++; 
            upgradeCount++;
            UpdateUpgradePrice(upgradeDatabase.stats[4].upgradeCost);
            _soundManager.PlayOneShot(_soundManager.soundDatabase.GetSfx(SoundDatabase.SfxType.Upgrade)[0]);
        }
    }
    
    public int hammerEnvironmentLevel;
    public void HammerEnvironment()
    {
        upgradeDatabase.stats[5].upgradeCost = upgradeDatabase.stats[5].upgradeBaseCost * Mathf.Pow(2, hammerEnvironmentLevel);
        if (_gameManager.HasMoney(upgradeDatabase.stats[5].upgradeBaseCost * Mathf.Pow(2, hammerEnvironmentLevel)))
        {
            hammerEnvironmentLevel++;
            upgradeCount++;
            UpdateUpgradePrice(upgradeDatabase.stats[5].upgradeBaseCost * Mathf.Pow(2, hammerEnvironmentLevel));
            _soundManager.PlayOneShot(_soundManager.soundDatabase.GetSfx(SoundDatabase.SfxType.Upgrade)[0]);
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
                                               Mathf.Pow(1.15f, upgradeDatabase.stats[6].upgradeLevels);
        if (_gameManager.HasMoney(upgradeDatabase.stats[6].upgradeCost))
        {
            upgradeDatabase.stats[6].upgradeLevels++;
            upgradeDatabase.stats[6].upgradeTier = CheckLevel(upgradeDatabase.stats[6].upgradeLevels);
            upgradeDatabase.stats[6].float1 = upgradeDatabase.stats[6].baseFloat1 * upgradeDatabase.stats[6].upgradeLevels * Mathf.Pow(2,upgradeDatabase.stats[6].upgradeTier) * (1 + 0.02f * _gameManager.reputation) ;
            upgradeCount++;
            UpdateDamagePassive();
            UpdateUpgradeDescription($"Decrease Ore Hp By {upgradeDatabase.stats[6].baseFloat1} per second",$"-{upgradeDatabase.stats[6].float1}/sec");
            UpdateUpgradePrice(upgradeDatabase.stats[6].upgradeCost);
            _soundManager.PlayOneShot(_soundManager.soundDatabase.GetSfx(SoundDatabase.SfxType.Upgrade)[0]);
        }
    }
    
    public void MoreHelpers()
    {
        upgradeDatabase.stats[7].upgradeCost = upgradeDatabase.stats[7].upgradeBaseCost *
                                               Mathf.Pow(1.15f, upgradeDatabase.stats[7].upgradeLevels);
        if (_gameManager.HasMoney(upgradeDatabase.stats[7].upgradeCost))
        {
            upgradeDatabase.stats[7].upgradeLevels++;
            upgradeDatabase.stats[7].upgradeTier = CheckLevel(upgradeDatabase.stats[7].upgradeLevels);
            upgradeDatabase.stats[7].float1 = upgradeDatabase.stats[7].baseFloat1 * upgradeDatabase.stats[7].upgradeLevels * Mathf.Pow(2,upgradeDatabase.stats[7].upgradeTier) * (1 + 0.02f * _gameManager.reputation) ;
            upgradeCount++;
            UpdateDamagePassive();
            UpdateUpgradeDescription($"Increase Helping Hand Damage By {upgradeDatabase.stats[7].baseFloat1} per second",$"+{upgradeDatabase.stats[7].float1}/sec");
            UpdateUpgradePrice(upgradeDatabase.stats[7].upgradeCost);
            _soundManager.PlayOneShot(_soundManager.soundDatabase.GetSfx(SoundDatabase.SfxType.Upgrade)[0]);
        }
    }
    
    public void LingeringFlame()
    {
        upgradeDatabase.stats[8].upgradeCost = upgradeDatabase.stats[8].upgradeBaseCost *
                                               Mathf.Pow(1.15f, upgradeDatabase.stats[8].upgradeLevels);
        if (_gameManager.HasMoney(upgradeDatabase.stats[8].upgradeCost))
        {
            upgradeDatabase.stats[8].upgradeLevels++;
            upgradeDatabase.stats[8].upgradeTier = CheckLevel(upgradeDatabase.stats[8].upgradeLevels);
            upgradeDatabase.stats[8].float1 = upgradeDatabase.stats[8].baseFloat1 * upgradeDatabase.stats[8].upgradeLevels * Mathf.Pow(2,upgradeDatabase.stats[8].upgradeTier) * (1 + 0.02f * _gameManager.reputation) ;
            upgradeCount++;
            UpdateDamagePassive();
            UpdateUpgradeDescription($"Increase Lingering Flame Damage By {upgradeDatabase.stats[8].baseFloat1} per second",$"+{upgradeDatabase.stats[8].float1}/sec");
            UpdateUpgradePrice(upgradeDatabase.stats[8].upgradeCost);
            _soundManager.PlayOneShot(_soundManager.soundDatabase.GetSfx(SoundDatabase.SfxType.Upgrade)[0]);
        }
    }
    
    public void SelfForgingHammer()
    {
        upgradeDatabase.stats[9].upgradeCost = upgradeDatabase.stats[9].upgradeBaseCost *
                                               Mathf.Pow(1.15f, upgradeDatabase.stats[9].upgradeLevels);
        if (_gameManager.HasMoney(upgradeDatabase.stats[9].upgradeCost))
        {
            upgradeDatabase.stats[9].upgradeLevels++;
            upgradeDatabase.stats[9].upgradeTier = CheckLevel(upgradeDatabase.stats[9].upgradeLevels);
            upgradeDatabase.stats[9].float1 = upgradeDatabase.stats[9].baseFloat1 * upgradeDatabase.stats[9].upgradeLevels * Mathf.Pow(2,upgradeDatabase.stats[9].upgradeTier) * (1 + 0.02f * _gameManager.reputation) ;
            upgradeCount++;
            UpdateDamagePassive();
            UpdateUpgradeDescription($"Increase Self Forging Hammer Damage By {upgradeDatabase.stats[9].baseFloat1} per second",$"+{upgradeDatabase.stats[9].float1}/sec");
            UpdateUpgradePrice(upgradeDatabase.stats[9].upgradeCost);
            _soundManager.PlayOneShot(_soundManager.soundDatabase.GetSfx(SoundDatabase.SfxType.Upgrade)[0]);
        }
    }
    
    public void SelfForgingAnvil()
    {
        upgradeDatabase.stats[10].upgradeCost = upgradeDatabase.stats[10].upgradeBaseCost *
                                                Mathf.Pow(1.15f, upgradeDatabase.stats[10].upgradeLevels);
        if (_gameManager.HasMoney(upgradeDatabase.stats[10].upgradeCost))
        {
            upgradeDatabase.stats[10].upgradeLevels++;
            upgradeDatabase.stats[10].upgradeTier = CheckLevel(upgradeDatabase.stats[10].upgradeLevels);
            upgradeDatabase.stats[10].float1 = upgradeDatabase.stats[10].baseFloat1 * upgradeDatabase.stats[10].upgradeLevels * Mathf.Pow(2,upgradeDatabase.stats[10].upgradeTier) * (1 + 0.02f * _gameManager.reputation) ;
            upgradeCount++;
            UpdateDamagePassive();
            UpdateUpgradeDescription($"Increase Self Forging Anvil Damage By {upgradeDatabase.stats[10].baseFloat1} per second",$"+{upgradeDatabase.stats[10].float1}/sec");
            UpdateUpgradePrice(upgradeDatabase.stats[10].upgradeCost);
            _soundManager.PlayOneShot(_soundManager.soundDatabase.GetSfx(SoundDatabase.SfxType.Upgrade)[0]);
        }
    }
    
    public void SelfForgingOre()
    {
        upgradeDatabase.stats[11].upgradeCost = upgradeDatabase.stats[11].upgradeBaseCost *
                                                Mathf.Pow(1.15f, upgradeDatabase.stats[11].upgradeLevels);
        if (_gameManager.HasMoney(upgradeDatabase.stats[11].upgradeCost))
        {
            upgradeDatabase.stats[11].upgradeLevels++;
            upgradeDatabase.stats[11].upgradeTier = CheckLevel(upgradeDatabase.stats[11].upgradeLevels);
            upgradeDatabase.stats[11].float1 = upgradeDatabase.stats[11].baseFloat1 * upgradeDatabase.stats[11].upgradeLevels * Mathf.Pow(2,upgradeDatabase.stats[11].upgradeTier) * (1 + 0.02f * _gameManager.reputation) ;
            upgradeCount++;
            UpdateDamagePassive();
            UpdateUpgradeDescription($"Increase Self Forging Ore Damage By {upgradeDatabase.stats[11].baseFloat1} per second",$"+{upgradeDatabase.stats[11].float1}/sec");
            UpdateUpgradePrice(upgradeDatabase.stats[11].upgradeCost);
            _soundManager.PlayOneShot(_soundManager.soundDatabase.GetSfx(SoundDatabase.SfxType.Upgrade)[0]);
        }
    }
    
    public void SentientSmithy()
    {
        upgradeDatabase.stats[12].upgradeCost = upgradeDatabase.stats[12].upgradeBaseCost *
                                                Mathf.Pow(1.15f, upgradeDatabase.stats[12].upgradeLevels);
        if (_gameManager.HasMoney(upgradeDatabase.stats[12].upgradeCost))
        {
            upgradeDatabase.stats[12].upgradeLevels++;
            upgradeDatabase.stats[12].upgradeTier = CheckLevel(upgradeDatabase.stats[12].upgradeLevels);
            upgradeDatabase.stats[12].float1 = upgradeDatabase.stats[12].baseFloat1 * upgradeDatabase.stats[12].upgradeLevels * Mathf.Pow(2,upgradeDatabase.stats[12].upgradeTier) * (1 + 0.02f * _gameManager.reputation) ;
            upgradeCount++;
            UpdateDamagePassive();
            UpdateUpgradeDescription($"Increase Sentient Smithy Damage By {upgradeDatabase.stats[12].baseFloat1} per second",$"+{upgradeDatabase.stats[12].float1}/sec");
            UpdateUpgradePrice(upgradeDatabase.stats[12].upgradeCost);
            _soundManager.PlayOneShot(_soundManager.soundDatabase.GetSfx(SoundDatabase.SfxType.Upgrade)[0]);
        }
    }
    
    public void AncestralSpirits()
    {
        upgradeDatabase.stats[13].upgradeCost = upgradeDatabase.stats[13].upgradeBaseCost *
                                                Mathf.Pow(1.15f, upgradeDatabase.stats[13].upgradeLevels);
        if (_gameManager.HasMoney(upgradeDatabase.stats[13].upgradeCost))
        {
            upgradeDatabase.stats[13].upgradeLevels++;
            upgradeDatabase.stats[13].upgradeTier = CheckLevel(upgradeDatabase.stats[13].upgradeLevels);
            upgradeDatabase.stats[13].float1 = upgradeDatabase.stats[13].baseFloat1 * upgradeDatabase.stats[13].upgradeLevels * Mathf.Pow(2,upgradeDatabase.stats[13].upgradeTier) * (1 + 0.02f * _gameManager.reputation) ;
            upgradeCount++;
            UpdateDamagePassive();
            UpdateUpgradeDescription($"Increase Ancestral Spirits Damage By {upgradeDatabase.stats[13].baseFloat1} per second",$"+{upgradeDatabase.stats[13].float1}/sec");
            UpdateUpgradePrice(upgradeDatabase.stats[13].upgradeCost);
            _soundManager.PlayOneShot(_soundManager.soundDatabase.GetSfx(SoundDatabase.SfxType.Upgrade)[0]);
        }
    }
    
    public void FutureGenerations()
    {
        upgradeDatabase.stats[14].upgradeCost = upgradeDatabase.stats[14].upgradeBaseCost *
                                                Mathf.Pow(1.15f, upgradeDatabase.stats[14].upgradeLevels);
        if (_gameManager.HasMoney(upgradeDatabase.stats[14].upgradeCost))
        {
            upgradeDatabase.stats[14].upgradeLevels++;
            upgradeDatabase.stats[14].upgradeTier = CheckLevel(upgradeDatabase.stats[14].upgradeLevels);
            upgradeDatabase.stats[14].float1 = upgradeDatabase.stats[14].baseFloat1 * upgradeDatabase.stats[14].upgradeLevels * Mathf.Pow(2,upgradeDatabase.stats[14].upgradeTier) * (1 + 0.02f * _gameManager.reputation) ;
            upgradeCount++;
            UpdateDamagePassive();
            UpdateUpgradeDescription($"Increase Future Generations Damage By {upgradeDatabase.stats[14].baseFloat1} per second",$"+{upgradeDatabase.stats[14].float1}/sec");
            UpdateUpgradePrice(upgradeDatabase.stats[14].upgradeCost);
            _soundManager.PlayOneShot(_soundManager.soundDatabase.GetSfx(SoundDatabase.SfxType.Upgrade)[0]);
        }
    }
    
    public void CsharpHammer()
    {
        upgradeDatabase.stats[15].upgradeCost = upgradeDatabase.stats[15].upgradeBaseCost *
                                                Mathf.Pow(1.15f, upgradeDatabase.stats[15].upgradeLevels);
        if (_gameManager.HasMoney(upgradeDatabase.stats[15].upgradeCost))
        {
            upgradeDatabase.stats[15].upgradeLevels++;
            upgradeDatabase.stats[15].upgradeTier = CheckLevel(upgradeDatabase.stats[15].upgradeLevels);
            upgradeDatabase.stats[15].float1 = upgradeDatabase.stats[15].baseFloat1 * upgradeDatabase.stats[15].upgradeLevels * Mathf.Pow(2,upgradeDatabase.stats[15].upgradeTier) * (1 + 0.02f * _gameManager.reputation) ;
            upgradeCount++;
            UpdateDamagePassive();
            UpdateUpgradeDescription($"Increase Csharp Hammer Damage By {upgradeDatabase.stats[15].baseFloat1} per second",$"+{upgradeDatabase.stats[15].float1}/sec");
            UpdateUpgradePrice(upgradeDatabase.stats[15].upgradeCost);
            _soundManager.PlayOneShot(_soundManager.soundDatabase.GetSfx(SoundDatabase.SfxType.Upgrade)[0]);
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
                                                Mathf.Pow(1.15f, upgradeDatabase.stats[16].upgradeLevels);
        if (_gameManager.HasMoney(upgradeDatabase.stats[16].upgradeCost))
        {
            upgradeDatabase.stats[16].upgradeLevels++;
            upgradeDatabase.stats[16].upgradeTier = CheckLevel(upgradeDatabase.stats[16].upgradeLevels);
            upgradeDatabase.stats[16].float1 = upgradeDatabase.stats[16].baseFloat1 * upgradeDatabase.stats[16].upgradeLevels * Mathf.Pow(2,upgradeDatabase.stats[16].upgradeTier) * (1 + 0.02f * _gameManager.reputation) ;
            upgradeCount++;
            UpdatePassiveMoney();
            UpdateUpgradeDescription($"Increase Passive Money By {upgradeDatabase.stats[16].baseFloat1} per second",$"+{upgradeDatabase.stats[16].float1}/sec");
            UpdateUpgradePrice(upgradeDatabase.stats[16].upgradeCost);
            _soundManager.PlayOneShot(_soundManager.soundDatabase.GetSfx(SoundDatabase.SfxType.Upgrade)[0]);
        }
    }
    
    public void CommunityService()
    {
        upgradeDatabase.stats[17].upgradeCost = upgradeDatabase.stats[17].upgradeBaseCost *
                                                Mathf.Pow(1.15f, upgradeDatabase.stats[17].upgradeLevels);
        if (_gameManager.HasMoney(upgradeDatabase.stats[17].upgradeCost))
        {
            upgradeDatabase.stats[17].upgradeLevels++;
            upgradeDatabase.stats[17].upgradeTier = CheckLevel(upgradeDatabase.stats[17].upgradeLevels);
            upgradeDatabase.stats[17].float1 = upgradeDatabase.stats[17].baseFloat1 * upgradeDatabase.stats[17].upgradeLevels * Mathf.Pow(2,upgradeDatabase.stats[17].upgradeTier) * (1 + 0.02f * _gameManager.reputation) ;
            upgradeCount++;
            UpdatePassiveMoney();
            UpdateUpgradeDescription($"Increase Passive Money By {upgradeDatabase.stats[17].baseFloat1} per second",$"+{upgradeDatabase.stats[17].float1}/sec");
            UpdateUpgradePrice(upgradeDatabase.stats[17].upgradeCost);
            _soundManager.PlayOneShot(_soundManager.soundDatabase.GetSfx(SoundDatabase.SfxType.Upgrade)[0]);
        }
    }
    
    public void SwordBetting()
    {
        upgradeDatabase.stats[18].upgradeCost = upgradeDatabase.stats[18].upgradeBaseCost *
                                                Mathf.Pow(1.15f, upgradeDatabase.stats[18].upgradeLevels);
        if (_gameManager.HasMoney(upgradeDatabase.stats[18].upgradeCost))
        {
            upgradeDatabase.stats[18].upgradeLevels++;
            upgradeDatabase.stats[18].upgradeTier = CheckLevel(upgradeDatabase.stats[18].upgradeLevels);
            upgradeDatabase.stats[18].float1 = upgradeDatabase.stats[18].baseFloat1 * upgradeDatabase.stats[18].upgradeLevels * Mathf.Pow(2,upgradeDatabase.stats[18].upgradeTier) * (1 + 0.02f * _gameManager.reputation) ;
            upgradeCount++;
            UpdatePassiveMoney();
            UpdateUpgradeDescription($"Increase Passive Money By {upgradeDatabase.stats[18].baseFloat1} per second",$"+{upgradeDatabase.stats[18].float1}/sec");
            UpdateUpgradePrice(upgradeDatabase.stats[18].upgradeCost);
            _soundManager.PlayOneShot(_soundManager.soundDatabase.GetSfx(SoundDatabase.SfxType.Upgrade)[0]);
        }
    }
    
    public void SwordReselling()
    {
        upgradeDatabase.stats[19].upgradeCost = upgradeDatabase.stats[19].upgradeBaseCost *
                                                Mathf.Pow(1.15f, upgradeDatabase.stats[19].upgradeLevels);
        if (_gameManager.HasMoney(upgradeDatabase.stats[19].upgradeCost))
        {
            upgradeDatabase.stats[19].upgradeLevels++;
            upgradeDatabase.stats[19].upgradeTier = CheckLevel(upgradeDatabase.stats[19].upgradeLevels);
            upgradeDatabase.stats[19].float1 = upgradeDatabase.stats[19].baseFloat1 * upgradeDatabase.stats[19].upgradeLevels * Mathf.Pow(2,upgradeDatabase.stats[19].upgradeTier) * (1 + 0.02f * _gameManager.reputation) ;
            upgradeCount++;
            UpdatePassiveMoney();
            UpdateUpgradeDescription($"Increase Passive Money By {upgradeDatabase.stats[19].baseFloat1} per second",$"+{upgradeDatabase.stats[19].float1}/sec");
            UpdateUpgradePrice(upgradeDatabase.stats[19].upgradeCost);
            _soundManager.PlayOneShot(_soundManager.soundDatabase.GetSfx(SoundDatabase.SfxType.Upgrade)[0]);
        }
    }
    
    public void SmithyNetwork()
    {
        upgradeDatabase.stats[20].upgradeCost = upgradeDatabase.stats[20].upgradeBaseCost *
                                                Mathf.Pow(1.15f, upgradeDatabase.stats[20].upgradeLevels);
        if (_gameManager.HasMoney(upgradeDatabase.stats[20].upgradeCost))
        {
            upgradeDatabase.stats[20].upgradeLevels++;
            upgradeDatabase.stats[20].upgradeTier = CheckLevel(upgradeDatabase.stats[20].upgradeLevels);
            upgradeDatabase.stats[20].float1 = upgradeDatabase.stats[20].baseFloat1 * upgradeDatabase.stats[20].upgradeLevels * Mathf.Pow(2,upgradeDatabase.stats[20].upgradeTier) * (1 + 0.02f * _gameManager.reputation) ;
            upgradeCount++;
            UpdatePassiveMoney();
            UpdateUpgradeDescription($"Increase Passive Money By {upgradeDatabase.stats[20].baseFloat1} per second",$"+{upgradeDatabase.stats[20].float1}/sec");
            UpdateUpgradePrice(upgradeDatabase.stats[20].upgradeCost);
            _soundManager.PlayOneShot(_soundManager.soundDatabase.GetSfx(SoundDatabase.SfxType.Upgrade)[0]);
        }
    }
    
    public void SmithyCooperation()
    {
        upgradeDatabase.stats[21].upgradeCost = upgradeDatabase.stats[21].upgradeBaseCost *
                                                Mathf.Pow(1.15f, upgradeDatabase.stats[21].upgradeLevels);
        if (_gameManager.HasMoney(upgradeDatabase.stats[21].upgradeCost))
        {
            upgradeDatabase.stats[21].upgradeLevels++;
            upgradeDatabase.stats[21].upgradeTier = CheckLevel(upgradeDatabase.stats[21].upgradeLevels);
            upgradeDatabase.stats[21].float1 = upgradeDatabase.stats[21].baseFloat1 * upgradeDatabase.stats[21].upgradeLevels * Mathf.Pow(2,upgradeDatabase.stats[21].upgradeTier) * (1 + 0.02f * _gameManager.reputation) ;
            upgradeCount++;
            UpdatePassiveMoney();
            UpdateUpgradeDescription($"Increase Passive Money By {upgradeDatabase.stats[21].baseFloat1} per second",$"+{upgradeDatabase.stats[21].float1}/sec");
            UpdateUpgradePrice(upgradeDatabase.stats[21].upgradeCost);
            _soundManager.PlayOneShot(_soundManager.soundDatabase.GetSfx(SoundDatabase.SfxType.Upgrade)[0]);
        }
    }
    
    public void InternationalSmithy()
    {
        upgradeDatabase.stats[22].upgradeCost = upgradeDatabase.stats[22].upgradeBaseCost *
                                                Mathf.Pow(1.15f, upgradeDatabase.stats[22].upgradeLevels);
        if (_gameManager.HasMoney(upgradeDatabase.stats[22].upgradeCost))
        {
            upgradeDatabase.stats[22].upgradeLevels++;
            upgradeDatabase.stats[22].upgradeTier = CheckLevel(upgradeDatabase.stats[22].upgradeLevels);
            upgradeDatabase.stats[22].float1 = upgradeDatabase.stats[22].baseFloat1 * upgradeDatabase.stats[22].upgradeLevels * Mathf.Pow(2,upgradeDatabase.stats[22].upgradeTier) * (1 + 0.02f * _gameManager.reputation) ;
            upgradeCount++;
            UpdatePassiveMoney();
            UpdateUpgradeDescription($"Increase Passive Money By {upgradeDatabase.stats[22].baseFloat1} per second",$"+{upgradeDatabase.stats[22].float1}/sec");
            UpdateUpgradePrice(upgradeDatabase.stats[22].upgradeCost);
            _soundManager.PlayOneShot(_soundManager.soundDatabase.GetSfx(SoundDatabase.SfxType.Upgrade)[0]);
        }
    }
    
    public void NonFungibleWeapons()
    {
        upgradeDatabase.stats[23].upgradeCost = upgradeDatabase.stats[23].upgradeBaseCost *
                                                Mathf.Pow(1.15f, upgradeDatabase.stats[23].upgradeLevels);
        if (_gameManager.HasMoney(upgradeDatabase.stats[23].upgradeCost))
        {
            upgradeDatabase.stats[23].upgradeLevels++;
            upgradeDatabase.stats[23].upgradeTier = CheckLevel(upgradeDatabase.stats[23].upgradeLevels);
            upgradeDatabase.stats[23].float1 = upgradeDatabase.stats[23].baseFloat1 * upgradeDatabase.stats[23].upgradeLevels * Mathf.Pow(2,upgradeDatabase.stats[23].upgradeTier) * (1 + 0.02f * _gameManager.reputation) ;
            upgradeCount++;
            UpdatePassiveMoney();
            UpdateUpgradeDescription($"Increase Passive Money By {upgradeDatabase.stats[23].baseFloat1} per second",$"+{upgradeDatabase.stats[23].float1}/sec");
            UpdateUpgradePrice(upgradeDatabase.stats[23].upgradeCost);
            _soundManager.PlayOneShot(_soundManager.soundDatabase.GetSfx(SoundDatabase.SfxType.Upgrade)[0]);
        }
    }
    
    public void CoinDuplicationGlitch()
    {
        upgradeDatabase.stats[24].upgradeCost = upgradeDatabase.stats[24].upgradeBaseCost *
                                                Mathf.Pow(1.15f, upgradeDatabase.stats[24].upgradeLevels);
        if (_gameManager.HasMoney(upgradeDatabase.stats[24].upgradeCost))
        {
            upgradeDatabase.stats[24].upgradeLevels++;
            upgradeDatabase.stats[24].upgradeTier = CheckLevel(upgradeDatabase.stats[24].upgradeLevels);
            upgradeDatabase.stats[24].float1 = upgradeDatabase.stats[24].baseFloat1 * upgradeDatabase.stats[24].upgradeLevels * Mathf.Pow(2,upgradeDatabase.stats[24].upgradeTier) * (1 + 0.02f * _gameManager.reputation) ;
            upgradeCount++;
            UpdatePassiveMoney();
            UpdateUpgradeDescription($"Increase Passive Money By {upgradeDatabase.stats[24].baseFloat1} per second",$"+{upgradeDatabase.stats[24].float1}/sec");
            UpdateUpgradePrice(upgradeDatabase.stats[24].upgradeCost);
            _soundManager.PlayOneShot(_soundManager.soundDatabase.GetSfx(SoundDatabase.SfxType.Upgrade)[0]);
        }
    }
    
    public void WeaponDuplicationGlitch()
    {
        upgradeDatabase.stats[25].upgradeCost = upgradeDatabase.stats[25].upgradeBaseCost *
                                                Mathf.Pow(1.15f, upgradeDatabase.stats[25].upgradeLevels);
        if (_gameManager.HasMoney(upgradeDatabase.stats[25].upgradeCost))
        {
            upgradeDatabase.stats[25].upgradeLevels++;
            upgradeDatabase.stats[25].upgradeTier = CheckLevel(upgradeDatabase.stats[25].upgradeLevels);
            upgradeDatabase.stats[25].float1 = upgradeDatabase.stats[25].baseFloat1 * upgradeDatabase.stats[25].upgradeLevels * Mathf.Pow(2,upgradeDatabase.stats[25].upgradeTier) * (1 + 0.02f * _gameManager.reputation) ;
            upgradeCount++;
            UpdatePassiveMoney();
            UpdateUpgradeDescription($"Increase Passive Money By {upgradeDatabase.stats[25].baseFloat1} per second",$"+{upgradeDatabase.stats[25].float1}/sec");
            UpdateUpgradePrice(upgradeDatabase.stats[25].upgradeCost);
            _soundManager.PlayOneShot(_soundManager.soundDatabase.GetSfx(SoundDatabase.SfxType.Upgrade)[0]);
        }
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
        if (level is >= 50 and 100)
        {
            return 3;
        }
        if (level is >= 100 and 250)
        {
            return 4;
        }
        if (level is >= 250 and 500)
        {
            return 5;
        }
        if (level is >= 500 and 1000)
        {
            return 6;
        }
        return 0;
    }
    
}
