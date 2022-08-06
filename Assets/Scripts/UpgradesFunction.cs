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
        var upgradePriceText = EventSystem.current.currentSelectedGameObject.transform.parent.parent.GetChild(0).GetChild(2)
            .GetComponent<TMP_Text>();
        upgradePriceText.text = price.ToString("F0");
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
            if (_gameManager.HasMoney(5000) )
            {
                _ore.oreDatabase.ores[_oreUpgradeLevel].isUnlocked = true;
                if (_ore.oreDatabase.ores[_oreUpgradeLevel].oreName == _ore.oreDatabase.ores[3].oreName)
                {
                    achievementDatabase.ModifyProgress("Power of the Falling Star",1);
                }
                _uiManager.AddNotification(UIManager.NotificationType.Ore, 1);
                _oreUpgradeLevel++;
                _soundManager.PlayOneShot(_soundManager.soundDatabase.GetSfx(SoundDatabase.SfxType.Upgrade)[0]);
                upgradeCount++;
            }
        }
    }

    private const float HAMMER_DAMAGE1_COST = 500f;
    private const float HAMMER_DAMAGE2_COST = 25000f;
    private const float HAMMER_DAMAGE3_COST = 50000f;
    private const float HAMMER_ENHANCEMENT_COST = 200000f;
    private const float HAMMER_ENVIRONMENT_COST = 500000f;
    
    private const float HELPING_HAND_DAMAGE = 1f;
    private const float MORE_HELPERS_DAMAGE = 8f;
    private const float LINGERING_FLAME_DAMAGE = 37f;
    private const float SELF_FORGING_HAMMER_DAMAGE = 202f;
    private const float SELF_FORGING_ANVIL_DAMAGE = 1090f;
    private const float SELF_FORGING_ORE_DAMAGE = 5850f;
    private const float SENTIENT_SMITHY_DAMAGE = 31400f;
    private const float ANCESTRAL_SPIRITS_DAMAGE = 168000f;
    private const float FUTURE_GENERATIONS_DAMAGE = 904000f;
    private const float CSHARP_HAMMER_DAMAGE = 4950000f;
    
    private const float HELPING_HAND_COST = 250f;
    private const float MORE_HELPERS_COST = 2750f;
    private const float LINGERING_FLAME_COST = 30300f;
    private const float SELF_FORGING_HAMMER_COST = 333000f;
    private const float SELF_FORGING_ANVIL_COST = 3670000f;
    private const float SELF_FORGING_ORE_COST = 40400000f;
    private const float SENTIENT_SMITHY_COST = 444000000f;
    private const float ANCESTRAL_SPIRITS_COST = 4880000000f;
    private const float FUTURE_GENERATIONS_COST = 53700000000f;
    private const float CSHARP_HAMMER_COST = 591000000000f;
    
    private bool _hammerDamage1, _hammerDamage2, _hammerDamage3;
    public int hammerTier;
    
    public void ChangeHammerDamage1()
    {
        if (!_hammerDamage1)
        {
            if (_gameManager.HasMoney(HAMMER_DAMAGE1_COST))
            {
                _hammerDamage1 = true;
                _gameManager.ModifyHammerDamage(2);
                upgradeCount++;
                hammerTier ++;
                _soundManager.PlayOneShot(_soundManager.soundDatabase.GetSfx(SoundDatabase.SfxType.Upgrade)[0]);
            }
        }
    }
    
    public void ChangeHammerDamage2()
    {
        if (!_hammerDamage2)
        {
            if (_gameManager.HasMoney(HAMMER_DAMAGE2_COST))
            {
                _hammerDamage2 = true;
                _gameManager.ModifyHammerDamage(4);
                upgradeCount++;
                hammerTier ++;
                _soundManager.PlayOneShot(_soundManager.soundDatabase.GetSfx(SoundDatabase.SfxType.Upgrade)[0]);
            }
        }
    }
    
    public void ChangeHammerDamage3()
    {
        if (!_hammerDamage3)
        {
            if (_gameManager.HasMoney(HAMMER_DAMAGE3_COST))
            {
                _hammerDamage3 = true;
                _gameManager.ModifyHammerDamage(8);
                upgradeCount++;
                hammerTier ++;
                _soundManager.PlayOneShot(_soundManager.soundDatabase.GetSfx(SoundDatabase.SfxType.Upgrade)[0]);
            }
        }
    }

    public int hammerEnhancementLevel;

    public void HammerEnhancement()
    {
        if (_gameManager.HasMoney(HAMMER_ENHANCEMENT_COST * Mathf.Pow(100, hammerEnhancementLevel)))
        {
            hammerEnhancementLevel++;
            upgradeCount++;
            UpdateUpgradePrice(HAMMER_ENHANCEMENT_COST * Mathf.Pow(100, hammerEnhancementLevel));
            _soundManager.PlayOneShot(_soundManager.soundDatabase.GetSfx(SoundDatabase.SfxType.Upgrade)[0]);
        }
    }
    
    public int hammerEnvironmentLevel;
    public void HammerEnvironment()
    {
        if (_gameManager.HasMoney(HAMMER_ENVIRONMENT_COST * Mathf.Pow(2, hammerEnvironmentLevel)))
        {
            hammerEnvironmentLevel++;
            upgradeCount++;
            UpdateUpgradePrice(HAMMER_ENVIRONMENT_COST * Mathf.Pow(2, hammerEnvironmentLevel));
            _soundManager.PlayOneShot(_soundManager.soundDatabase.GetSfx(SoundDatabase.SfxType.Upgrade)[0]);
        }
    }

    public float passiveDamage;

    void UpdateDamagePassive()
    {
        passiveDamage = _helpingHandDamage + _moreHelpersDamage + _lingeringFlameDamage + _selfForgingHammerDamage + _selfForgingAnvilDamage + _selfForgingOreDamage + _sentientSmithyDamage + _ancestralSpiritsDamage + _futureGenerationsDamage + _csharpHammerDamage;
        achievementDatabase.ModifyProgress("Enthusiastic Helper", passiveDamage);
    }
    
    private int _helpingHandLevel;
    private int _moreHelpersLevel;
    private int _lingeringFlameLevel;
    private int _selfForgingHammerLevel;
    private int _selfForgingAnvilLevel;
    private int _selfForgingOreLevel;
    private int _sentientSmithyLevel;
    private int _ancestralSpiritsLevel;
    private int _futureGenerationsLevel;
    private int _csharpHammerLevel;
    
    private int _helpingHandTier;
    private int _moreHelpersTier;
    private int _lingeringFlameTier;
    private int _selfForgingHammerTier;
    private int _selfForgingAnvilTier;
    private int _selfForgingOreTier;
    private int _sentientSmithyTier;
    private int _ancestralSpiritsTier;
    private int _futureGenerationsTier;
    private int _csharpHammerTier;

    private float _helpingHandDamage;
    private float _moreHelpersDamage;
    private float _lingeringFlameDamage;
    private float _selfForgingHammerDamage;
    private float _selfForgingAnvilDamage;
    private float _selfForgingOreDamage;
    private float _sentientSmithyDamage;
    private float _ancestralSpiritsDamage;
    private float _futureGenerationsDamage;
    private float _csharpHammerDamage;
    

    public void HelpingHand()
    {
        if (_gameManager.HasMoney(HELPING_HAND_COST * Mathf.Pow(1.15f, _helpingHandLevel)))
        {
            _helpingHandLevel++;
            _helpingHandTier = CheckLevel(_helpingHandLevel);
            _helpingHandDamage = HELPING_HAND_DAMAGE * _helpingHandLevel * Mathf.Pow(2,_helpingHandTier) * (1 + 0.02f * _gameManager.reputation) ;
            upgradeCount++;
            UpdateDamagePassive();
            UpdateUpgradeDescription($"Decrease Ore Hp By {HELPING_HAND_DAMAGE} per second",$"-{_helpingHandDamage}/sec");
            UpdateUpgradePrice(HELPING_HAND_COST * Mathf.Pow(1.15f, _helpingHandLevel));
            _soundManager.PlayOneShot(_soundManager.soundDatabase.GetSfx(SoundDatabase.SfxType.Upgrade)[0]);
        }
    }
    
    public void MoreHelpers()
    {
        if (_gameManager.HasMoney(MORE_HELPERS_COST * Mathf.Pow(1.15f, _moreHelpersLevel)))
        {
            _moreHelpersLevel++;
            _moreHelpersTier = CheckLevel(_moreHelpersLevel);
            _moreHelpersDamage = MORE_HELPERS_DAMAGE * _moreHelpersLevel * Mathf.Pow(2,_moreHelpersTier) * (1 + 0.02f * _gameManager.reputation) ;
            upgradeCount++;
            UpdateDamagePassive();
            UpdateUpgradeDescription($"Decrease Ore Hp By {MORE_HELPERS_DAMAGE} per second",$"-{_moreHelpersDamage}/sec");
            UpdateUpgradePrice(MORE_HELPERS_COST * Mathf.Pow(1.15f, _moreHelpersLevel));
            _soundManager.PlayOneShot(_soundManager.soundDatabase.GetSfx(SoundDatabase.SfxType.Upgrade)[0]);
        }
    }
    
    public void LingeringFlame()
    {
        if (_gameManager.HasMoney(LINGERING_FLAME_COST * Mathf.Pow(1.15f, _lingeringFlameLevel)))
        {
            _lingeringFlameLevel++;
            _lingeringFlameTier = CheckLevel(_lingeringFlameLevel);
            _lingeringFlameDamage = LINGERING_FLAME_DAMAGE * _lingeringFlameLevel * Mathf.Pow(2,_lingeringFlameTier) *(1 + 0.02f * _gameManager.reputation) ;
            upgradeCount++;
            UpdateDamagePassive();
            UpdateUpgradeDescription($"Decrease Ore Hp By {LINGERING_FLAME_DAMAGE} per second",$"-{_lingeringFlameDamage}/sec");
            UpdateUpgradePrice(LINGERING_FLAME_COST * Mathf.Pow(1.15f, _lingeringFlameLevel));
            _soundManager.PlayOneShot(_soundManager.soundDatabase.GetSfx(SoundDatabase.SfxType.Upgrade)[0]);
        }
    }
    
    public void SelfForgingHammer()
    {
        if (_gameManager.HasMoney(SELF_FORGING_HAMMER_COST * Mathf.Pow(1.15f, _selfForgingHammerLevel)))
        {
            _selfForgingHammerLevel++;
            _selfForgingHammerTier = CheckLevel(_selfForgingHammerLevel);
            _selfForgingHammerDamage = SELF_FORGING_HAMMER_DAMAGE * _selfForgingHammerLevel * Mathf.Pow(2,_selfForgingHammerTier) *(1 + 0.02f * _gameManager.reputation) ;
            upgradeCount++;
            UpdateDamagePassive();
            UpdateUpgradeDescription($"Decrease Ore Hp By {SELF_FORGING_HAMMER_DAMAGE} per second",$"-{_selfForgingHammerDamage}/sec");
            UpdateUpgradePrice(SELF_FORGING_HAMMER_COST * Mathf.Pow(1.15f, _selfForgingHammerLevel));
            _soundManager.PlayOneShot(_soundManager.soundDatabase.GetSfx(SoundDatabase.SfxType.Upgrade)[0]);
        }
    }
    
    public void SelfForgingAnvil()
    {
        if (_gameManager.HasMoney(SELF_FORGING_ANVIL_COST * Mathf.Pow(1.15f, _selfForgingAnvilLevel)))
        {
            _selfForgingAnvilLevel++;
            _selfForgingAnvilTier = CheckLevel(_selfForgingAnvilLevel);
            _selfForgingAnvilDamage = SELF_FORGING_ANVIL_DAMAGE * _selfForgingAnvilLevel * Mathf.Pow(2,_selfForgingAnvilTier) *(1 + 0.02f * _gameManager.reputation) ;
            upgradeCount++;
            UpdateDamagePassive();
            UpdateUpgradeDescription($"Decrease Ore Hp By {SELF_FORGING_ANVIL_DAMAGE} per second",$"-{_selfForgingAnvilDamage}/sec");
            UpdateUpgradePrice(SELF_FORGING_ANVIL_COST * Mathf.Pow(1.15f, _selfForgingAnvilLevel));
            _soundManager.PlayOneShot(_soundManager.soundDatabase.GetSfx(SoundDatabase.SfxType.Upgrade)[0]);
        }
    }
    
    public void SelfForgingOre()
    {
        if (_gameManager.HasMoney(SELF_FORGING_ORE_COST * Mathf.Pow(1.15f, _selfForgingOreLevel)))
        {
            _selfForgingOreLevel++;
            _selfForgingOreTier = CheckLevel(_selfForgingOreLevel);
            _selfForgingOreDamage = SELF_FORGING_ORE_DAMAGE * _selfForgingOreLevel * Mathf.Pow(2,_selfForgingOreTier) *(1 + 0.02f * _gameManager.reputation) ;
            upgradeCount++;
            UpdateDamagePassive();
            UpdateUpgradeDescription($"Decrease Ore Hp By {SELF_FORGING_ORE_DAMAGE} per second",$"-{_selfForgingOreDamage}/sec");
            UpdateUpgradePrice(SELF_FORGING_ORE_COST * Mathf.Pow(1.15f, _selfForgingOreLevel));
            _soundManager.PlayOneShot(_soundManager.soundDatabase.GetSfx(SoundDatabase.SfxType.Upgrade)[0]);
        }
    }
    
    public void SentientSmithy()
    {
        if (_gameManager.HasMoney(SENTIENT_SMITHY_COST * Mathf.Pow(1.15f, _sentientSmithyLevel)))
        {
            _sentientSmithyLevel++;
            _sentientSmithyTier = CheckLevel(_sentientSmithyLevel);
            _sentientSmithyDamage = SENTIENT_SMITHY_DAMAGE * _sentientSmithyLevel * Mathf.Pow(2,_sentientSmithyTier) *(1 + 0.02f * _gameManager.reputation) ;
            upgradeCount++;
            UpdateDamagePassive();
            UpdateUpgradeDescription($"Decrease Ore Hp By {SENTIENT_SMITHY_DAMAGE} per second",$"-{_sentientSmithyDamage}/sec");
            UpdateUpgradePrice(SENTIENT_SMITHY_COST * Mathf.Pow(1.15f, _sentientSmithyLevel));
            _soundManager.PlayOneShot(_soundManager.soundDatabase.GetSfx(SoundDatabase.SfxType.Upgrade)[0]);
        }
    }
    
    public void AncestralSpirits()
    {
        if (_gameManager.HasMoney(ANCESTRAL_SPIRITS_COST * Mathf.Pow(1.15f, _ancestralSpiritsLevel)))
        {
            _ancestralSpiritsLevel++;
            _ancestralSpiritsTier = CheckLevel(_ancestralSpiritsLevel);
            _ancestralSpiritsDamage = ANCESTRAL_SPIRITS_DAMAGE * _ancestralSpiritsLevel * Mathf.Pow(2,_ancestralSpiritsTier) *(1 + 0.02f * _gameManager.reputation) ;
            upgradeCount++;
            UpdateDamagePassive();
            UpdateUpgradeDescription($"Decrease Ore Hp By {ANCESTRAL_SPIRITS_DAMAGE} per second",$"-{_ancestralSpiritsDamage}/sec");
            UpdateUpgradePrice(ANCESTRAL_SPIRITS_COST * Mathf.Pow(1.15f, _ancestralSpiritsLevel));
            _soundManager.PlayOneShot(_soundManager.soundDatabase.GetSfx(SoundDatabase.SfxType.Upgrade)[0]);
        }
    }
    
    public void FutureGenerations()
    {
        if (_gameManager.HasMoney(FUTURE_GENERATIONS_COST * Mathf.Pow(1.15f, _futureGenerationsLevel)))
        {
            _futureGenerationsLevel++;
            _futureGenerationsTier = CheckLevel(_futureGenerationsLevel);
            _futureGenerationsDamage = FUTURE_GENERATIONS_DAMAGE * _futureGenerationsLevel * Mathf.Pow(2,_futureGenerationsTier) *(1 + 0.02f * _gameManager.reputation) ;
            upgradeCount++;
            UpdateDamagePassive();
            UpdateUpgradeDescription($"Decrease Ore Hp By {FUTURE_GENERATIONS_DAMAGE} per second",$"-{_futureGenerationsDamage}/sec");
            UpdateUpgradePrice(FUTURE_GENERATIONS_COST * Mathf.Pow(1.15f, _futureGenerationsLevel));
            _soundManager.PlayOneShot(_soundManager.soundDatabase.GetSfx(SoundDatabase.SfxType.Upgrade)[0]);
        }
    }
    
    public void CsharpHammer()
    {
        if (_gameManager.HasMoney(CSHARP_HAMMER_COST * Mathf.Pow(1.15f, _csharpHammerLevel)))
        {
            _csharpHammerLevel++;
            _csharpHammerTier = CheckLevel(_csharpHammerLevel);
            _csharpHammerDamage = CSHARP_HAMMER_DAMAGE * _csharpHammerLevel * Mathf.Pow(2,_csharpHammerTier) *(1 + 0.02f * _gameManager.reputation) ;
            upgradeCount++;
            UpdateDamagePassive();
            UpdateUpgradeDescription($"Decrease Ore Hp By {CSHARP_HAMMER_DAMAGE} per second",$"-{_csharpHammerDamage}/sec");
            UpdateUpgradePrice(CSHARP_HAMMER_COST * Mathf.Pow(1.15f, _csharpHammerLevel));
            _soundManager.PlayOneShot(_soundManager.soundDatabase.GetSfx(SoundDatabase.SfxType.Upgrade)[0]);
        }
    }

    public float passiveMoney;
    
    public float donationBoxMoney;
    public float communityServiceMoney;
    public float swordBettingMoney;
    public float swordResellingMoney;
    public float smithyNetworkMoney;
    public float smithyCooperationMoney;
    public float internationalSmithyMoney;
    public float nonFungibleWeaponsMoney;
    public float coinDuplicationGlitchMoney;
    public float weaponDuplicationGlitchMoney;
    
    private const float DONATION_BOX_MONEY = 1f;
    private const float COMMUNITY_SERVICE_MONEY = 8f;
    private const float SWORD_BETTING_MONEY = 37f;
    private const float SWORD_RESELLING_MONEY = 202f;
    private const float SMITHY_NETWORK_MONEY = 1090f;
    private const float SMITHY_COOPERATION_MONEY = 5850f;
    private const float INTERNATIONAL_SMITHY_MONEY = 31400f;
    private const float NON_FUNGIBLE_WEAPONS_MONEY = 168000f;
    private const float COIN_DUPLICATION_GLITCH_MONEY = 904000f;
    private const float WEAPON_DUPLICATION_GLITCH_MONEY = 4950000f;
    
    private const float DONATION_BOX_COST = 100f;
    private const float COMMUNITY_SERVICE_COST = 1100f;
    private const float SWORD_BETTING_COST = 12100f;
    private const float SWORD_RESELLING_COST = 133000f;
    private const float SMITHY_NETWORK_COST = 1460000f;
    private const float SMITHY_COOPERATION_COST = 16100000f;
    private const float INTERNATIONAL_SMITHY_COST = 177000000f;
    private const float NON_FUNGIBLE_WEAPONS_COST = 1950000000f;
    private const float COIN_DUPLICATION_GLITCH_COST = 21500000000f;
    private const float WEAPON_DUPLICATION_GLITCH_COST = 236000000000f;
    
    private int _donationBoxLevel;
    private int _communityServiceLevel;
    private int _swordBettingLevel;
    private int _swordResellingLevel;
    private int _smithyNetworkLevel;
    private int _smithyCooperationLevel;
    private int _internationalSmithyLevel;
    private int _nonFungibleWeaponsLevel;
    private int _coinDuplicationGlitchLevel;
    private int _weaponDuplicationGlitchLevel;
    
    private int _donationBoxTier;
    private int _communityServiceTier;
    private int _swordBettingTier;
    private int _swordResellingTier;
    private int _smithyNetworkTier;
    private int _smithyCooperationTier;
    private int _internationalSmithyTier;
    private int _nonFungibleWeaponsTier;
    private int _coinDuplicationGlitchTier;
    private int _weaponDuplicationGlitchTier;

    private void UpdatePassiveMoney()
    {
        passiveMoney = 0;
        passiveMoney += donationBoxMoney;
        passiveMoney += communityServiceMoney;
        passiveMoney += swordBettingMoney;
        passiveMoney += swordResellingMoney;
        passiveMoney += smithyNetworkMoney;
        passiveMoney += smithyCooperationMoney;
        passiveMoney += internationalSmithyMoney;
        passiveMoney += nonFungibleWeaponsMoney;
        passiveMoney += coinDuplicationGlitchMoney;
        passiveMoney += weaponDuplicationGlitchMoney;
    }
    
    public void DonationBox()
    {
        if (_gameManager.HasMoney(DONATION_BOX_COST * Mathf.Pow(1.15f, _donationBoxLevel)))
        {
            _donationBoxLevel++;
            _donationBoxTier = CheckLevel(_donationBoxLevel);
            donationBoxMoney = DONATION_BOX_MONEY * _donationBoxLevel * Mathf.Pow(2,_donationBoxTier) *(1 + 0.02f * _gameManager.reputation) ;
            upgradeCount++;
            UpdatePassiveMoney();
            UpdateUpgradeDescription($"Increase Passive Money By {DONATION_BOX_MONEY} per second",$"+{donationBoxMoney}/sec");
            UpdateUpgradePrice(DONATION_BOX_COST * Mathf.Pow(1.15f, _donationBoxLevel));
        }
    }
    
    public void CommunityService()
    {
        if (_gameManager.HasMoney(COMMUNITY_SERVICE_COST * Mathf.Pow(1.15f, _communityServiceLevel)))
        {
            _communityServiceLevel++;
            _communityServiceTier = CheckLevel(_communityServiceLevel);
            communityServiceMoney = COMMUNITY_SERVICE_MONEY * _communityServiceLevel * Mathf.Pow(2,_communityServiceTier) *(1 + 0.02f * _gameManager.reputation) ;
            upgradeCount++;
            UpdatePassiveMoney();
            UpdateUpgradeDescription($"Increase Passive Money By {COMMUNITY_SERVICE_MONEY} per second",$"+{communityServiceMoney}/sec");
            UpdateUpgradePrice(COMMUNITY_SERVICE_COST * Mathf.Pow(1.15f, _communityServiceLevel));
        }
    }
    
    public void SwordBetting()
    {
        if (_gameManager.HasMoney(SWORD_BETTING_COST * Mathf.Pow(1.15f, _swordBettingLevel)))
        {
            _swordBettingLevel++;
            _swordBettingTier = CheckLevel(_swordBettingLevel);
            swordBettingMoney = SWORD_BETTING_MONEY * _swordBettingLevel * Mathf.Pow(2,_swordBettingTier) *(1 + 0.02f * _gameManager.reputation) ;
            upgradeCount++;
            UpdatePassiveMoney();
            UpdateUpgradeDescription($"Increase Passive Money By {SWORD_BETTING_MONEY} per second",$"+{swordBettingMoney}/sec");
            UpdateUpgradePrice(SWORD_BETTING_COST * Mathf.Pow(1.15f, _swordBettingLevel));
        }
    }
    
    public void SwordReselling()
    {
        if (_gameManager.HasMoney(SWORD_RESELLING_COST * Mathf.Pow(1.15f, _swordResellingLevel)))
        {
            _swordResellingLevel++;
            _swordResellingTier = CheckLevel(_swordResellingLevel);
            swordResellingMoney = SWORD_RESELLING_MONEY * _swordResellingLevel * Mathf.Pow(2,_swordResellingTier) *(1 + 0.02f * _gameManager.reputation) ;
            upgradeCount++;
            UpdatePassiveMoney();
            UpdateUpgradeDescription($"Increase Passive Money By {SWORD_RESELLING_MONEY} per second",$"+{swordResellingMoney}/sec");
            UpdateUpgradePrice(SWORD_RESELLING_COST * Mathf.Pow(1.15f, _swordResellingLevel));
        }
    }
    
    public void SmithyNetwork()
    {
        if (_gameManager.HasMoney(SMITHY_NETWORK_COST * Mathf.Pow(1.15f, _smithyNetworkLevel)))
        {
            _smithyNetworkLevel++;
            _smithyNetworkTier = CheckLevel(_smithyNetworkLevel);
            smithyNetworkMoney = SMITHY_NETWORK_MONEY * _smithyNetworkLevel * Mathf.Pow(2,_smithyNetworkTier) *(1 + 0.02f * _gameManager.reputation) ;
            upgradeCount++;
            UpdatePassiveMoney();
            UpdateUpgradeDescription($"Increase Passive Money By {SMITHY_NETWORK_MONEY} per second",$"+{smithyNetworkMoney}/sec");
            UpdateUpgradePrice(SMITHY_NETWORK_COST * Mathf.Pow(1.15f, _smithyNetworkLevel));
        }
    }
    
    public void SmithyCooperation()
    {
        if (_gameManager.HasMoney(SMITHY_COOPERATION_COST * Mathf.Pow(1.15f, _smithyCooperationLevel)))
        {
            _smithyCooperationLevel++;
            _smithyCooperationTier = CheckLevel(_smithyCooperationLevel);
            smithyCooperationMoney = SMITHY_COOPERATION_MONEY * _smithyCooperationLevel * Mathf.Pow(2,_smithyCooperationTier) *(1 + 0.02f * _gameManager.reputation) ;
            upgradeCount++;
            UpdatePassiveMoney();
            UpdateUpgradeDescription($"Increase Passive Money By {SMITHY_COOPERATION_MONEY} per second",$"+{smithyCooperationMoney}/sec");
            UpdateUpgradePrice(SMITHY_COOPERATION_COST * Mathf.Pow(1.15f, _smithyCooperationLevel));
        }
    }
    
    public void InternationalSmithy()
    {
        if (_gameManager.HasMoney(INTERNATIONAL_SMITHY_COST * Mathf.Pow(1.15f, _internationalSmithyLevel)))
        {
            _internationalSmithyLevel++;
            _internationalSmithyTier = CheckLevel(_internationalSmithyLevel);
            internationalSmithyMoney = INTERNATIONAL_SMITHY_MONEY * _internationalSmithyLevel * Mathf.Pow(2,_internationalSmithyTier) *(1 + 0.02f * _gameManager.reputation) ;
            upgradeCount++;
            UpdatePassiveMoney();
            UpdateUpgradeDescription($"Increase Passive Money By {INTERNATIONAL_SMITHY_MONEY} per second",$"+{internationalSmithyMoney}/sec");
            UpdateUpgradePrice(INTERNATIONAL_SMITHY_COST * Mathf.Pow(1.15f, _internationalSmithyLevel));
        }
    }
    
    public void NonFungibleWeapons()
    {
        if (_gameManager.HasMoney(NON_FUNGIBLE_WEAPONS_COST * Mathf.Pow(1.15f, _nonFungibleWeaponsLevel)))
        {
            _nonFungibleWeaponsLevel++;
            _nonFungibleWeaponsTier = CheckLevel(_nonFungibleWeaponsLevel);
            nonFungibleWeaponsMoney = NON_FUNGIBLE_WEAPONS_MONEY * _nonFungibleWeaponsLevel * Mathf.Pow(2,_nonFungibleWeaponsTier) *(1 + 0.02f * _gameManager.reputation) ;
            upgradeCount++;
            UpdatePassiveMoney();
            UpdateUpgradeDescription($"Increase Passive Money By {NON_FUNGIBLE_WEAPONS_MONEY} per second",$"+{nonFungibleWeaponsMoney}/sec");
            UpdateUpgradePrice(NON_FUNGIBLE_WEAPONS_COST * Mathf.Pow(1.15f, _nonFungibleWeaponsLevel));
        }
    }
    
    public void CoinDuplicationGlitch()
    {
        if (_gameManager.HasMoney(COIN_DUPLICATION_GLITCH_COST * Mathf.Pow(1.15f, _coinDuplicationGlitchLevel)))
        {
            _coinDuplicationGlitchLevel++;
            _coinDuplicationGlitchTier = CheckLevel(_coinDuplicationGlitchLevel);
            coinDuplicationGlitchMoney = COIN_DUPLICATION_GLITCH_MONEY * _coinDuplicationGlitchLevel * Mathf.Pow(2,_coinDuplicationGlitchTier) *(1 + 0.02f * _gameManager.reputation) ;
            upgradeCount++;
            UpdatePassiveMoney();
            UpdateUpgradeDescription($"Increase Passive Money By {COIN_DUPLICATION_GLITCH_MONEY} per second",$"+{coinDuplicationGlitchMoney}/sec");
            UpdateUpgradePrice(COIN_DUPLICATION_GLITCH_COST * Mathf.Pow(1.15f, _coinDuplicationGlitchLevel));
        }
    }
    
    public void WeaponDuplicationGlitch()
    {
        if (_gameManager.HasMoney(WEAPON_DUPLICATION_GLITCH_COST * Mathf.Pow(1.15f, _weaponDuplicationGlitchLevel)))
        {
            _weaponDuplicationGlitchLevel++;
            _weaponDuplicationGlitchTier = CheckLevel(_weaponDuplicationGlitchLevel);
            weaponDuplicationGlitchMoney = WEAPON_DUPLICATION_GLITCH_MONEY * _weaponDuplicationGlitchLevel * Mathf.Pow(2,_weaponDuplicationGlitchTier) *(1 + 0.02f * _gameManager.reputation) ;
            upgradeCount++;
            UpdatePassiveMoney();
            UpdateUpgradeDescription($"Increase Passive Money By {WEAPON_DUPLICATION_GLITCH_MONEY} per second",$"+{weaponDuplicationGlitchMoney}/sec");
            UpdateUpgradePrice(WEAPON_DUPLICATION_GLITCH_COST * Mathf.Pow(1.15f, _weaponDuplicationGlitchLevel));
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
