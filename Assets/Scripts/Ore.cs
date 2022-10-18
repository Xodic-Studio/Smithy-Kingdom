using System;
using GameDatabase;
using Manager;
using UnityEngine;
using UnityEngine.U2D.Animation;
using UnityEngine.UI;

public class Ore : Singleton<Ore>
{
    private SoundManagerr soundManager;
    public SpriteResolver anvilSpriteResolver;
    public SpriteResolver assistantSpriteResolver;
    private Animator _animator;
    public OreDatabase oreDatabase;
    private UIManager _uiManager;
    private CollectionManager _collectionManager;
    
    private OreStats _thisOre;
    
    public int tempSelectOreIndex;
    public bool isPremium;
    public int selectedOreIndex;
    
    private bool _isDroppingItem;
    
    private void Awake()
    {
        soundManager = SoundManagerr.Instance;
        _uiManager = UIManager.Instance;
        _collectionManager = CollectionManager.Instance;
    }

    private void Start()
    {
        _animator = GetComponent<Animator>();
        UpdateCommonOre();
    }
    
    void Update()
    {
        _uiManager.UpdateHardnessSlider(_thisOre.currentHardness, _thisOre.defaultHardness);
    }
    
    //update ore
    public void UpdateCommonOre()
    {
        _animator.enabled = false;
        selectedOreIndex = tempSelectOreIndex;
        isPremium = false;
        //_uiManager.ConfirmPremiumOreButton.interactable = true;
        anvilSpriteResolver.SetCategoryAndLabel("Common", oreDatabase.ores[selectedOreIndex].oreName);
        anvilSpriteResolver.ResolveSpriteToSpriteRenderer();
        assistantSpriteResolver.SetCategoryAndLabel("Common", oreDatabase.ores[selectedOreIndex].oreName);
        assistantSpriteResolver.ResolveSpriteToSpriteRenderer();
        DisableButtonIfNoNextOre();
        _thisOre = oreDatabase.ores[selectedOreIndex];
        name = _thisOre.oreName;
        _uiManager.UpdateMaxHardnessSlider(_thisOre.defaultHardness);
        _thisOre.currentHardness = (int)_thisOre.defaultHardness;
        _uiManager.UpdateOreNameText(_thisOre.oreName);
        _collectionManager.UpdateItemSelection();
        _collectionManager.UpdateRandomSystem();
        _animator.enabled = true;
        _uiManager.UpdateOreImageHead();
    }

    public void UpdatePremiumOre()
    {
        _animator.enabled = false;
        selectedOreIndex = tempSelectOreIndex;
        isPremium = true;
        anvilSpriteResolver.SetCategoryAndLabel("Gacha", oreDatabase.premiumOres[selectedOreIndex].oreName);
        anvilSpriteResolver.ResolveSpriteToSpriteRenderer();
        assistantSpriteResolver.SetCategoryAndLabel("Gacha", oreDatabase.premiumOres[selectedOreIndex].oreName);
        assistantSpriteResolver.ResolveSpriteToSpriteRenderer();
        DisableButtonIfNoNextOre();
        _thisOre = oreDatabase.premiumOres[selectedOreIndex];
        name = _thisOre.oreName;
        _uiManager.UpdateMaxHardnessSlider((float)_thisOre.defaultHardness);
        _thisOre.currentHardness = (int)_thisOre.defaultHardness;
        _uiManager.UpdateOreNameText(_thisOre.oreName);
        _collectionManager.UpdatePremiumItemSelection();
        _collectionManager.UpdateRandomSystem();
        _animator.enabled = true;
        _uiManager.UpdatePremiumOreImageHead();
        _uiManager.ConfirmOreButton.interactable = false;
    }
    
    public void ModifySelectedOreIndex(int index)
    {
        if (index == -1 && tempSelectOreIndex - 1 >= 0)
        {
            if (!oreDatabase.ores[tempSelectOreIndex - 1].isUnlocked)
            {
                _uiManager.oreImageBody.color = Color.gray;
                _uiManager.ConfirmOreButton.interactable = false;
                _uiManager.ConfirmOreButtonImage.color = Color.gray;
            } else if (oreDatabase.ores[tempSelectOreIndex - 1].isUnlocked)
            {
                _uiManager.oreImageBody.color = Color.white;
                _uiManager.ConfirmOreButton.interactable = true;
                _uiManager.ConfirmOreButtonImage.color = Color.white;
            }
            tempSelectOreIndex--;
            //soundManager.PlayOneShot(soundManager.soundDatabase.GetSfx(SoundDatabase.SfxType.SelectOre)[0]);
            soundManager.PlaySound("SelectOre");
            DisableButtonIfNoNextOre();
        }
        else if (index == 1 && tempSelectOreIndex + 1 < oreDatabase.ores.Length)
        {
            if (!oreDatabase.ores[tempSelectOreIndex + 1].isUnlocked)
            {
                _uiManager.oreImageBody.color = Color.gray;
                _uiManager.ConfirmOreButton.interactable = false;
                _uiManager.ConfirmOreButtonImage.color = Color.gray;
            } else if (oreDatabase.ores[tempSelectOreIndex + 1].isUnlocked)
            {
                _uiManager.oreImageBody.color = Color.white;
                _uiManager.ConfirmOreButton.interactable = true;
                _uiManager.ConfirmOreButtonImage.color = Color.white;
            }
            tempSelectOreIndex++;
            //soundManager.PlayOneShot(soundManager.soundDatabase.GetSfx(SoundDatabase.SfxType.SelectOre)[0]);
            soundManager.PlaySound("SelectOre");

            DisableButtonIfNoNextOre();
        }
        CheckOreIndex();
    }
    
    public void ModifyPremiumOreIndex(int index)
    {
        if (index == -1 && tempSelectOreIndex - 1 >= 0)
        {
            if (!oreDatabase.premiumOres[tempSelectOreIndex - 1].isUnlocked || oreDatabase.premiumOres[tempSelectOreIndex - 1].amount == 0)
            {
                _uiManager.premiumOreImageBody.color = Color.gray;
                _uiManager.ConfirmPremiumOreButton.interactable = false;
                _uiManager.ConfirmPremiumOreButtonImage.color = Color.gray;
            } else if (oreDatabase.premiumOres[tempSelectOreIndex - 1].isUnlocked && oreDatabase.premiumOres[tempSelectOreIndex - 1].amount != 0)
            {
                _uiManager.premiumOreImageBody.color = Color.white;
                _uiManager.ConfirmPremiumOreButton.interactable = true;
                _uiManager.ConfirmPremiumOreButtonImage.color = Color.white;
            }
            tempSelectOreIndex--;
            //soundManager.PlayOneShot(soundManager.soundDatabase.GetSfx(SoundDatabase.SfxType.SelectOre)[0]);
            soundManager.PlaySound("SelectOre");

            DisableButtonIfNoNextPremiumOre();
        }
        else if (index == 1 && tempSelectOreIndex + 1 < oreDatabase.premiumOres.Length)
        {
            if (!oreDatabase.premiumOres[tempSelectOreIndex + 1].isUnlocked || oreDatabase.premiumOres[tempSelectOreIndex + 1].amount == 0)
            {
                _uiManager.premiumOreImageBody.color = Color.gray;
                _uiManager.ConfirmPremiumOreButton.interactable = false;
                _uiManager.ConfirmPremiumOreButtonImage.color = Color.gray;
            } else if (oreDatabase.premiumOres[tempSelectOreIndex + 1].isUnlocked && oreDatabase.premiumOres[tempSelectOreIndex + 1].amount != 0)
            {
                _uiManager.premiumOreImageBody.color = Color.white;
                _uiManager.ConfirmPremiumOreButton.interactable = true;
                _uiManager.ConfirmPremiumOreButtonImage.color = Color.white;
            }
            tempSelectOreIndex++;
            //soundManager.PlayOneShot(soundManager.soundDatabase.GetSfx(SoundDatabase.SfxType.SelectOre)[0]);
            soundManager.PlaySound("SelectOre");

            DisableButtonIfNoNextPremiumOre();
        }
        CheckPremiumOreIndex();
    }

    public void FirstPremiumOreIndex()
    {
        if (!oreDatabase.premiumOres[0].isUnlocked || oreDatabase.premiumOres[0].amount == 0)
        {
            _uiManager.premiumOreImageBody.color = Color.gray;
            _uiManager.ConfirmPremiumOreButton.interactable = false;
            _uiManager.ConfirmPremiumOreButtonImage.color = Color.gray;
        }
        else
        {
            _uiManager.premiumOreImageBody.color = Color.white;
            _uiManager.ConfirmPremiumOreButton.interactable = true;
            _uiManager.ConfirmPremiumOreButtonImage.color = Color.white;
        }
    }
    public void ModifyHardness(float amount)
    {
        _thisOre.currentHardness -= Convert.ToInt32(amount);
        CheckHardness();
    }
    
    public void DisableButtonIfNoNextOre()
    {
        if (tempSelectOreIndex - 1 < 0)
        {
            _uiManager.previousOreButton.GetComponent<Image>().color = new Color(0.38f, 0.38f, 0.38f);
        }
        if (tempSelectOreIndex + 1 > oreDatabase.ores.Length - 1)
        {
            _uiManager.nextOreButton.GetComponent<Image>().color = new Color(0.38f, 0.38f, 0.38f);
        }
        if (tempSelectOreIndex + 1 <= oreDatabase.ores.Length - 1)
        {
            _uiManager.nextOreButton.GetComponent<Image>().color = Color.white;
        }
        if (tempSelectOreIndex - 1 >= 0)
        {
            _uiManager.previousOreButton.GetComponent<Image>().color = Color.white;
        }
    }
    
    public void DisableButtonIfNoNextPremiumOre()
    {
        if (tempSelectOreIndex - 1 < 0)
        {
            _uiManager.previousPremiumOreButton.GetComponent<Image>().color = new Color(0.38f, 0.38f, 0.38f);
        }
        if (tempSelectOreIndex + 1 > oreDatabase.premiumOres.Length - 1)
        {
            _uiManager.nextPremiumOreButton.GetComponent<Image>().color = new Color(0.38f, 0.38f, 0.38f);
        }
        if (tempSelectOreIndex + 1 <= oreDatabase.premiumOres.Length - 1)
        {
            _uiManager.nextPremiumOreButton.GetComponent<Image>().color = Color.white;
        }
        if (tempSelectOreIndex - 1 >= 0)
        {
            _uiManager.previousPremiumOreButton.GetComponent<Image>().color = Color.white;
        }
    }
    void CheckHardness()
    {
        if (_thisOre.currentHardness <= 0)
        {
            _thisOre.currentHardness = 0;
            _isDroppingItem = true;
            _collectionManager.DropItem(isPremium);
            if (_thisOre.isPremium)
            {
                ModifyOreAmount(_thisOre, -1);
            }
            Invoke("DropItemDelay",.1f);
        }
    }
    
    void DropItemDelay()
    {
        _thisOre.currentHardness = (int)_thisOre.defaultHardness;
        _isDroppingItem = false;
    }
    

    void CheckOreIndex()
    {
        if (tempSelectOreIndex >= oreDatabase.ores.Length || tempSelectOreIndex < 0)
        {
            tempSelectOreIndex = 0;
        }
    }
    
    void CheckPremiumOreIndex()
    {
        if (tempSelectOreIndex >= oreDatabase.premiumOres.Length || tempSelectOreIndex < 0)
        {
            tempSelectOreIndex = 0;
        }
    }
    
    public void ModifyOreAmount(OreStats ore, int amount)
    {
        ore.amount += amount;
        if (ore.amount <= 0)
        {
            ore.amount = 0;
            tempSelectOreIndex = 0;
            UpdateCommonOre();
            _uiManager.UpdateOreDetails();
            _uiManager.UpdateOreNameText(_thisOre.oreName);
        }

        if (ore.amount > 0 && ore.isUnlocked == false)
        {
            ore.isUnlocked = true;
            _uiManager.AddNotification(UIManager.NotificationType.Ore,1);
        }
    }
    
    public OreStats GetOreStats()
    {
        return _thisOre;
    }
    
    public bool GetIsDroppingItem()
    {
        return _isDroppingItem;
    }
}