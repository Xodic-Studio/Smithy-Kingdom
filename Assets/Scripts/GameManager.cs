using System.Collections;
using GameDatabase;
using Manager;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class GameManager : Singleton<GameManager>
{
    public Database database;
    public AchievementDatabase achievementDatabase;
    private Ore _ore;
    private UIManager _uiManager;
    private SoundManagerr _soundManager;
    private UpgradesFunction _upgradesFunction;

    public float reputation;
    private int _hammerDamageRaw = 1;
    private float _hammerDamageCombined;
    private float _damageTextTimer;
    private float _cpsTimer;
    private float _cps;
    private float _dps;
    private float _lastTime;
    private bool _isClicking;

    [SerializeField] public float money;
    [SerializeField] public float allMoney;
    [SerializeField] public float gems;
    [SerializeField] private MailDatabase mailDatabase;

    public Animator smithy;
    public Animator anvil;
    public GameObject damageGameObject;

    private static readonly int Click = Animator.StringToHash("Click");
    private static readonly int Speed = Animator.StringToHash("Speed");
    private static readonly int Hit = Animator.StringToHash("Hit");

    private void Awake()
    {
        _uiManager = UIManager.Instance;
        _ore = Ore.Instance;
        _soundManager = SoundManagerr.Instance;
        _upgradesFunction = UpgradesFunction.Instance;
    }

    private void Start()
    {
        //_soundManager.EffectsSource = GetComponent<AudioSource>();
        _uiManager.UpdateMoneyText();
        _uiManager.UpdateGemText();
        Invoke(nameof(MailTimer),Random.Range(180,301));
        Invoke("OneSecondInterval", 1f);
        ResetIsClicking();
        achievementDatabase = database.achievementDatabase;
        //_soundManager.PlayMusic(_soundManager.soundDatabase.bgm[0]);
        _soundManager.PlaySound("bgm");
    }

    private void Update()
    {
        CheckTimer();
    }
    //TODO: REWORK THIS
    void CheckTimer()
    {
        _cpsTimer += Time.deltaTime;
        _damageTextTimer += Time.deltaTime;
        if (_cpsTimer > 1)
        {
            //smithy.SetFloat(Speed,CpsToSpeed((int) _cps));
        }
    }

    float _lastSecondDps;
    //TODO: REWORK THIS
    void OneSecondInterval()
    {
        if (_upgradesFunction.passiveDamage > 0 && !_ore.GetIsDroppingItem())
        {
            _ore.ModifyHardness(Mathf.Floor(_upgradesFunction.passiveDamage));
            AddDamageText(NumberToString((decimal)_upgradesFunction.passiveDamage));
        }
        LuckyAchievement();
        ModifyMoney(_upgradesFunction.passiveMoney);
        _dps -= _lastSecondDps;
        _lastSecondDps = _dps;
        Debug.Log(_dps);
        Invoke("OneSecondInterval", 1f);

        void LuckyAchievement()
        {
            var random = Random.Range(1, 10000000);
            if (random == 1)
            {
                achievementDatabase.ModifyProgress("Lucky!", 1);
            }
        }
    }
    //TODO: REWORK THIS
    void ResetIsClicking()
    {
        if (!_isClicking)
        {
            //smithy.SetFloat(Click,0);
        }
        _isClicking = false;
        Invoke(nameof(ResetIsClicking),1f);
    }
    
    void MailTimer()
    {
        _uiManager.AddNewMail(mailDatabase.GetRandomMail());
        Invoke(nameof(MailTimer),Random.Range(180,301));
    }
    
    public void MailReward()
    {
        var rewardCurrentCoin = 100 + money * 0.15f;
        var rewardCps = 100 + _upgradesFunction.passiveDamage * 900;

        mailReward = Mathf.Floor(rewardCurrentCoin > rewardCps ? rewardCps : rewardCurrentCoin).ToString();
        ModifyMoney(Mathf.Floor(rewardCurrentCoin > rewardCps ? rewardCps : rewardCurrentCoin));
    }

    private string mailReward;
    public string GetMailReward()
    {
        return mailReward;
    }
    float CpsToSpeed(int cps)
    {
        if (cps > 3)
        {
            return 1.25f;
        }
        return 1f;
    }
    
    public bool HasMoney(float amount)
    {
        if (GetMoney() < amount)
        {
            _uiManager.NotEnoughMoney();
            return false;
        }
        ModifyMoney(-amount);
        return true;
    }
    
    public bool HasGems(int amount)
    {
        if (GetGems() < amount)
        {
            _uiManager.NotEnoughGems();
            return false;
        }
        ModifyGems(-amount);
        return true;
    }
    
    //Instantiate Damage text
    private void AddDamageText(string text)
    {
        if (_damageTextTimer > .2f)
        {
            float randomX = Random.Range(-.5f, .5f);
            var go = Instantiate(damageGameObject, _ore.transform.position + new Vector3(randomX, 1f), Quaternion.identity);
            RectTransform goRect = (RectTransform) go.transform;
            TMP_Text goText = go.GetComponent<TMP_Text>();
            goText.text = $"- {text}";
            goRect.SetParent(_uiManager.GetCanvas().transform, true);
            StartCoroutine(FloatDelayDestroy(goRect, goText));
            _damageTextTimer = 0;
            _hammerDamageCombined = 0;
            goRect.localScale = new Vector3(1, 1, 1);
        }
    }
    
    IEnumerator FloatDelayDestroy(RectTransform goRect, TMP_Text goText)
    {
        for (float i = 0; i < 1.5f; i += 1 * Time.fixedDeltaTime)
        {
            goRect.position += new Vector3(0, 0.05f, 0);
            if (i > 0.5f)
            {
                goText.color -= new Color(0, 0, 0, 0.1f);
            }
            yield return null;
        }
        Destroy(goRect.gameObject);
    }

    private float _finalDamage;
    //Hitting Function
    private void TapTap()
    {
        if (!_ore.GetIsDroppingItem())
        {
            _dps += _hammerDamageCombined;
            smithy.SetTrigger(Hit);
            anvil.SetTrigger(Hit);
            float currentTime = Time.time;
            float diff = currentTime - _lastTime;
            _lastTime = currentTime;
            _cps = 1f / diff;
            //smithy.SetFloat(Click, _cps);
            _isClicking = true;
            _hammerDamageCombined = Mathf.Round(Mathf.Pow(2, _upgradesFunction.hammerTier) +
                                                _upgradesFunction.upgradeDatabase.stats[5].upgradeLevel * 0.1f * _upgradesFunction.upgradeCount *
                                                (1 + 0.02f * reputation));
            _finalDamage = _hammerDamageCombined + _upgradesFunction.upgradeDatabase.stats[4].upgradeLevel * 0.01f * _dps;
            AddDamageText(NumberToString((decimal)_finalDamage));
            _ore.ModifyHardness(_finalDamage);
            
            //_soundManager.RandomSoundEffect(_soundManager.soundDatabase.GetSfx(SoundDatabase.SfxType.HammerHit));
            _soundManager.PlaySound("Hammer");
            
            var random = Random.Range(1, 10);
            if (random == 1)
            {
               // _soundManager.RandomSoundEffect(_soundManager.soundDatabase.GetSfx(SoundDatabase.SfxType.SmithVoice));
            }
        }
    }

    
    
    #region Getter Setter
    public float GetGems()
    {
        return gems;
    }
    
    public void ModifyGems(float amount)
    {
        gems += amount;
        _uiManager.UpdateGemText();
    }
    
    public void ModifyHammerDamage(int amount)
    {
        _hammerDamageRaw += amount;
        if (_hammerDamageRaw <= 1)
        {
            _hammerDamageRaw = 1;
        }
    }
    
    public void ModifyMoney(float amount)
    {
        money += Mathf.Round(amount);
        if(amount > 0)
        {
            allMoney += Mathf.Round(amount);
        }
        _uiManager.UpdateMoneyText();
        CheckMoneyForUpgrades();
        achievementDatabase.ModifyProgress("World-famous smithy",amount, true);
    }

    public void CheckMoneyForUpgrades()
    {
        var i = 0;
        foreach (Transform upgrade in _uiManager.upgradeList.transform)
        {
            if (money > _upgradesFunction.upgradeDatabase.stats[i].upgradeCost)
            {
                upgrade.GetChild(2).GetChild(0).GetComponent<Image>().color = Color.white;
                upgrade.GetChild(2).GetChild(0).GetComponent<Button>().interactable = true;
            } else
            {
                upgrade.GetChild(2).GetChild(0).GetComponent<Image>().color = Color.red;
                //upgrade.GetChild(2).GetChild(0).GetComponent<Button>().interactable = false;
            }
            i++;
        }
    }
    
    public float GetMoney()
    {
        return money;
    }

    public string NumberToString(decimal number)
    {
        if (number < 1000)
        {
            return number.ToString("F0");
        }
        if (number < 1000000)
        {
            return (number / 1000).ToString("0.##") + "k";
        }
        if (number < 1000000000)
        {
            return (number / 1000000).ToString("0.##") + "m";
        }
        if (number < 1000000000000)
        {
            return (number / 1000000000).ToString("0.##") + "b";
        }
        if (number < 1000000000000000)
        {
            return (number / 1000000000000).ToString("0.##") + "t";
        }
        if (number < 1000000000000000000)
        {
            return (number / 1000000000000000).ToString("0.##") + "q";
        } 
        if (number < 1000000000000000000000m)
        {
            return (number / 1000000000000000000m).ToString("0.##") + "Q";
        } 
        if (number < 1000000000000000000000000m)
        {
            return (number / 1000000000000000000000m).ToString("0.##") + "s";
        }
        if (number < 1000000000000000000000000000m)
        {
            return (number / 1000000000000000000000000m).ToString("0.##") + "S";
        }

        return number.ToString();
    }
    
    public float GetReputation()
    {
        return reputation;
    }

    public void ModifyReputation(float amount)
    {
        reputation += amount;
    }
    
    public void Prestige()
    {
        if (allMoney >= 50000000 * (Mathf.Pow(reputation + 1, 3) - Mathf.Pow(reputation, 3)))
        {
            ModifyMoney(-money);
            money = 0;
            var result = Mathf.Ceil(Mathf.Pow(allMoney / 50000000, (float) 1 / 3));
            Debug.Log(result);
            ModifyReputation(result);
            database.oresDatabase.ResetDatabase();
            database.itemsDatabase.ResetDatabase();
            database.upgradeDatabase.ResetDatabase();
            _upgradesFunction.passiveDamage = 0;
            _upgradesFunction.passiveMoney = 0;
            _uiManager.CloseMenu();
            
            CollectionManager.Instance.CheckEveryCollection();
            allMoney = 0;
            //_soundManager.PlayOneShot(_soundManager.soundDatabase.GetSfx(SoundDatabase.SfxType.Prestige)[0]);
            _soundManager.PlaySound("Prestige");
        }
        else
        {
            _uiManager.AssignPopupValue("Can't Prestige Yet", "You need to make at least 50M to prestige", _uiManager.denySprite);
            _uiManager.OpenPopup();
        }
        
    }
    
    #endregion


}