using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class GameManager : Singleton<GameManager>
{
    private Ore _ore;
    private UIManager _uiManager;
    private SoundManager _soundManager;
    
    public GameObject damageGameObject;

    [SerializeField] private float money;
    [SerializeField] private float gems;

    public Animator smithy;
    public Animator anvil;
    
    private int _hammerDamage = 1;
    private int _hammerDamageCombined;
    private float _damageTextTimer;
    private float _cpsTimer;
    private int _clickPerSec;
    private int _click;
    
    private static readonly int Speed = Animator.StringToHash("Speed");
    private static readonly int Hit = Animator.StringToHash("Hit");

    private void Awake()
    {
        _uiManager = UIManager.Instance;
        _ore = Ore.Instance;
        _soundManager = SoundManager.Instance;
    }

    private void Start()
    {
        _soundManager.EffectsSource = GetComponent<AudioSource>();
        _soundManager.PlayMusic(_soundManager.soundDatabase.bgm[0]);
        _uiManager.UpdateMoneyText();
        _uiManager.UpdateGemText();
    }

    private void Update()
    {
        CheckTimer();
    }
    
    void CheckTimer()
    {
        _cpsTimer += Time.deltaTime;
        _damageTextTimer += Time.deltaTime;
        if (_cpsTimer > 1)
        {
            _clickPerSec = _click;
            _click = 0;
            _cpsTimer = 0;
            if (_clickPerSec is < 10 and > 5)
            {
                smithy.SetFloat(Speed, 1.25f);
                anvil.SetFloat(Speed, 1.25f);
            } else if (_clickPerSec > 10)
            {
                smithy.SetFloat(Speed, 1.5f);
                anvil.SetFloat(Speed, 1.5f);
            }
            else
            {
                smithy.SetFloat(Speed, 1f);
                anvil.SetFloat(Speed, 1f);
            }
        }
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
    
    //Hitting Function
    private void TapTap()
    {
        if (!_ore.GetIsDroppingItem())
        {
            _ore.ModifyHardness(_hammerDamage);
            _hammerDamageCombined += _hammerDamage;
            CombineDamageText();
            smithy.SetTrigger(Hit);
            anvil.SetTrigger(Hit);
            _click +=5;
            _soundManager.RandomSoundEffect(_soundManager.soundDatabase.GetSfx(SoundDatabase.SfxType.HammerHit));
        }
    }

    private void CombineDamageText()
    {
        AddDamageText(_hammerDamageCombined.ToString());
    }


    public void UpdateMusicVolume(Slider slider)
    {
        _soundManager.musicVolume = slider.value;
        _soundManager.UpdateMusicVolume();
    }
    
    public void UpdateMasterVolume(Slider slider)
    {
        _soundManager.masterVolume = slider.value;
        _soundManager.UpdateMusicVolume();
    }
    
    public void UpdateSfxVolume(Slider slider)
    {
        _soundManager.sfxVolume = slider.value;
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
        _hammerDamage += amount;
        if (_hammerDamage <= 1)
        {
            _hammerDamage = 1;
        }
    }
    
    public void ModifyMoney(float amount)
    {
        money += amount;
        _uiManager.UpdateMoneyText();
    }
    
    public float GetMoney()
    {
        return money;
    }

    #endregion
}