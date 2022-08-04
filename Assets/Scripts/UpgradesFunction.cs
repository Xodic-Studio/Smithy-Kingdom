using System;
using GameDatabase;
using Manager;
using UnityEngine;
using UnityEngine.Events;

public class UpgradesFunction : Singleton<UpgradesFunction>
{
    Ore _ore;
    GameManager _gameManager;
    UIManager _uiManager;
    SoundManager _soundManager;
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
    
    private bool _hammerDamage1, _hammerDamage2, _hammerDamage3 = false;
    public int hammerTier = 0;
    
    public void ChangeHammerDamage1()
    {
        if (!_hammerDamage1)
        {
            if (_gameManager.HasMoney(HAMMER_DAMAGE1_COST))
            {
                _hammerDamage1 = true;
                _gameManager.ModifyHammerDamage(2);
                _soundManager.PlayOneShot(_soundManager.soundDatabase.GetSfx(SoundDatabase.SfxType.Upgrade)[0]);
                upgradeCount++;
                hammerTier ++;
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
                _soundManager.PlayOneShot(_soundManager.soundDatabase.GetSfx(SoundDatabase.SfxType.Upgrade)[0]);
                upgradeCount++;
                hammerTier ++;
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
                _soundManager.PlayOneShot(_soundManager.soundDatabase.GetSfx(SoundDatabase.SfxType.Upgrade)[0]);
                upgradeCount++;
                hammerTier ++;
            }
        }
    }

    public int hammerEnhancementLevel = 1;

    public void HammerEnhancement()
    {
        if (_gameManager.HasMoney(HAMMER_ENHANCEMENT_COST * Mathf.Pow(100, hammerEnhancementLevel)))
        {
            hammerEnhancementLevel++;
            _soundManager.PlayOneShot(_soundManager.soundDatabase.GetSfx(SoundDatabase.SfxType.Upgrade)[0]);
            upgradeCount++;
        }
    }
    
    public int hammerEnvironmentLevel = 1;
    public void HammerEnvironment()
    {
        if (_gameManager.HasMoney(HAMMER_ENVIRONMENT_COST * Mathf.Pow(2, hammerEnvironmentLevel)))
        {
            hammerEnvironmentLevel++;
            _soundManager.PlayOneShot(_soundManager.soundDatabase.GetSfx(SoundDatabase.SfxType.Upgrade)[0]);
            upgradeCount++;
        }
    }

    public float damagePassive;

    public int helpingHandLevel = 1;
    public int moreHelpersLevel = 1;
    public int lingeringFlameLevel = 1;
    public int selfForgingHammerLevel = 1;
    public int selfForgingAnvilLevel = 1;
    public int selfForgingOreLevel = 1;
    public int sentientSmithyLevel = 1;
    public int ancestralSpiritsLevel = 1;
    public int futureGenerationsLevel = 1;
    public int csharpHammerLevel = 1;
    
    public int helpingHandTier;
    public int moreHelpersTier;
    public int lingeringFlameTier;
    public int selfForgingHammerTier;
    public int selfForgingAnvilTier;
    public int selfForgingOreTier;
    public int sentientSmithyTier;
    public int ancestralSpiritsTier;
    public int futureGenerationsTier;
    public int csharpHammerTier;

    public void HelpingHand()
    {
        if (_gameManager.HasMoney(HELPING_HAND_COST * Mathf.Pow(1.15f, helpingHandLevel)))
        {
            helpingHandLevel++;
            damagePassive += HELPING_HAND_DAMAGE * helpingHandLevel * Mathf.Pow(2,helpingHandTier) *(1 + 0.02f * _gameManager.reputation) ;
            _soundManager.PlayOneShot(_soundManager.soundDatabase.GetSfx(SoundDatabase.SfxType.Upgrade)[0]);
            upgradeCount++;
            helpingHandTier = CheckLevel(helpingHandLevel);
        }
    }
    
    public void MoreHelpers()
    {
        if (_gameManager.HasMoney(MORE_HELPERS_COST * Mathf.Pow(1.15f, moreHelpersLevel)))
        {
            moreHelpersLevel++;
            damagePassive += MORE_HELPERS_DAMAGE * moreHelpersLevel * Mathf.Pow(2,moreHelpersTier) *(1 + 0.02f * _gameManager.reputation) ;
            _soundManager.PlayOneShot(_soundManager.soundDatabase.GetSfx(SoundDatabase.SfxType.Upgrade)[0]);
            upgradeCount++;
            moreHelpersTier = CheckLevel(moreHelpersLevel);
        }
    }
    
    public void LingeringFlame()
    {
        if (_gameManager.HasMoney(LINGERING_FLAME_COST * Mathf.Pow(1.15f, lingeringFlameLevel)))
        {
            lingeringFlameLevel++;
            damagePassive += LINGERING_FLAME_DAMAGE * lingeringFlameLevel * Mathf.Pow(2,lingeringFlameTier) *(1 + 0.02f * _gameManager.reputation) ;
            _soundManager.PlayOneShot(_soundManager.soundDatabase.GetSfx(SoundDatabase.SfxType.Upgrade)[0]);
            upgradeCount++;
            lingeringFlameTier = CheckLevel(lingeringFlameLevel);
        }
    }
    
    public void SelfForgingHammer()
    {
        if (_gameManager.HasMoney(SELF_FORGING_HAMMER_COST * Mathf.Pow(1.15f, selfForgingHammerLevel)))
        {
            selfForgingHammerLevel++;
            damagePassive += SELF_FORGING_HAMMER_DAMAGE * selfForgingHammerLevel * Mathf.Pow(2,selfForgingHammerTier) *(1 + 0.02f * _gameManager.reputation) ;
            _soundManager.PlayOneShot(_soundManager.soundDatabase.GetSfx(SoundDatabase.SfxType.Upgrade)[0]);
            upgradeCount++;
            selfForgingHammerTier = CheckLevel(selfForgingHammerLevel);
        }
    }
    
    public void SelfForgingAnvil()
    {
        if (_gameManager.HasMoney(SELF_FORGING_ANVIL_COST * Mathf.Pow(1.15f, selfForgingAnvilLevel)))
        {
            selfForgingAnvilLevel++;
            damagePassive += SELF_FORGING_ANVIL_DAMAGE * selfForgingAnvilLevel * Mathf.Pow(2,selfForgingAnvilTier) *(1 + 0.02f * _gameManager.reputation) ;
            _soundManager.PlayOneShot(_soundManager.soundDatabase.GetSfx(SoundDatabase.SfxType.Upgrade)[0]);
            upgradeCount++;
            selfForgingAnvilTier = CheckLevel(selfForgingAnvilLevel);
        }
    }
    
    public void SelfForgingOre()
    {
        if (_gameManager.HasMoney(SELF_FORGING_ORE_COST * Mathf.Pow(1.15f, selfForgingOreLevel)))
        {
            selfForgingOreLevel++;
            damagePassive += SELF_FORGING_ORE_DAMAGE * selfForgingOreLevel * Mathf.Pow(2,selfForgingOreTier) *(1 + 0.02f * _gameManager.reputation) ;
            _soundManager.PlayOneShot(_soundManager.soundDatabase.GetSfx(SoundDatabase.SfxType.Upgrade)[0]);
            upgradeCount++;
            selfForgingOreTier = CheckLevel(selfForgingOreLevel);
        }
    }
    
    public void SentientSmithy()
    {
        if (_gameManager.HasMoney(SENTIENT_SMITHY_COST * Mathf.Pow(1.15f, sentientSmithyLevel)))
        {
            sentientSmithyLevel++;
            damagePassive += SENTIENT_SMITHY_DAMAGE * sentientSmithyLevel * Mathf.Pow(2,sentientSmithyTier) *(1 + 0.02f * _gameManager.reputation) ;
            _soundManager.PlayOneShot(_soundManager.soundDatabase.GetSfx(SoundDatabase.SfxType.Upgrade)[0]);
            upgradeCount++;
            sentientSmithyTier = CheckLevel(sentientSmithyLevel);
        }
    }
    
    public void AncestralSpirits()
    {
        if (_gameManager.HasMoney(ANCESTRAL_SPIRITS_COST * Mathf.Pow(1.15f, ancestralSpiritsLevel)))
        {
            ancestralSpiritsLevel++;
            damagePassive += ANCESTRAL_SPIRITS_DAMAGE * ancestralSpiritsLevel * Mathf.Pow(2,ancestralSpiritsTier) *(1 + 0.02f * _gameManager.reputation) ;
            _soundManager.PlayOneShot(_soundManager.soundDatabase.GetSfx(SoundDatabase.SfxType.Upgrade)[0]);
            upgradeCount++;
            ancestralSpiritsTier = CheckLevel(ancestralSpiritsLevel);
        }
    }
    
    public void FutureGenerations()
    {
        if (_gameManager.HasMoney(FUTURE_GENERATIONS_COST * Mathf.Pow(1.15f, futureGenerationsLevel)))
        {
            futureGenerationsLevel++;
            damagePassive += FUTURE_GENERATIONS_DAMAGE * futureGenerationsLevel * Mathf.Pow(2,futureGenerationsTier) *(1 + 0.02f * _gameManager.reputation) ;
            _soundManager.PlayOneShot(_soundManager.soundDatabase.GetSfx(SoundDatabase.SfxType.Upgrade)[0]);
            upgradeCount++;
            futureGenerationsTier = CheckLevel(futureGenerationsLevel);
        }
    }
    
    public void CsharpHammer()
    {
        if (_gameManager.HasMoney(CSHARP_HAMMER_COST * Mathf.Pow(1.15f, csharpHammerLevel)))
        {
            csharpHammerLevel++;
            damagePassive += CSHARP_HAMMER_DAMAGE * csharpHammerLevel * Mathf.Pow(2,csharpHammerTier) *(1 + 0.02f * _gameManager.reputation) ;
            _soundManager.PlayOneShot(_soundManager.soundDatabase.GetSfx(SoundDatabase.SfxType.Upgrade)[0]);
            upgradeCount++;
            csharpHammerTier = CheckLevel(csharpHammerLevel);
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
