using UnityEngine;
using UnityEngine.U2D.Animation;
using UnityEngine.UI;

public class Ore : Singleton<Ore>
{
    public SpriteResolver anvilSpriteResolver;
    public SpriteResolver assistantSpriteResolver;
    private Animator _animator;
    public OreDatabase oreDatabase;
    private UIManager _uiManager;
    private CollectionManager _collectionManager;
    
    private OreStats _thisOre;
    
    public int tempSelectOreIndex;
    public int selectedOreIndex;
    
    private void Awake()
    {
        _uiManager = UIManager.Instance;
        _collectionManager = CollectionManager.Instance;
    }

    private void Start()
    {
        _animator = GetComponent<Animator>();
        UpdateOre();
    }
    
    void Update()
    {
        _uiManager.UpdateHardnessSlider(_thisOre.currentHardness, _thisOre.defaultHardness);
    }
    
    //update ore
    public void UpdateOre()
    {
        _animator.enabled = false;
        selectedOreIndex = tempSelectOreIndex;
        anvilSpriteResolver.SetCategoryAndLabel("Common", oreDatabase.ores[selectedOreIndex].oreName);
        anvilSpriteResolver.ResolveSpriteToSpriteRenderer();
        assistantSpriteResolver.SetCategoryAndLabel("Common", oreDatabase.ores[selectedOreIndex].oreName);
        assistantSpriteResolver.ResolveSpriteToSpriteRenderer();
        DisableButtonIfNoNextOre();
        _thisOre = oreDatabase.ores[selectedOreIndex];
        name = _thisOre.oreName;
        _uiManager.UpdateMaxHardnessSlider(_thisOre.defaultHardness);
        _thisOre.currentHardness = _thisOre.defaultHardness;
        _uiManager.UpdateOreNameText(_thisOre.oreName);
        _collectionManager.UpdateItemSelection();
        _collectionManager.UpdateRandomSystem();
        _animator.enabled = true;
    }
    
    public void ModifySelectedOreIndex(int index)
    {
        if (index == -1 && tempSelectOreIndex - 1 >= 0)
        {
            if (oreDatabase.ores[tempSelectOreIndex - 1].isUnlocked)
            {
                tempSelectOreIndex--;
                _uiManager.nextOreButton.GetComponent<Image>().color = Color.white;
                DisableButtonIfNoNextOre();
            }
        }
        else if (index == 1 && tempSelectOreIndex + 1 < oreDatabase.ores.Length)
        {
            if (oreDatabase.ores[tempSelectOreIndex + 1].isUnlocked)
            {
                tempSelectOreIndex++;
                _uiManager.previousOreButton.GetComponent<Image>().color = Color.white;
                DisableButtonIfNoNextOre();
            }
        }
        CheckOreIndex();
    }
    public void ModifyHardness(int amount)
    {
        _thisOre.currentHardness -= amount;
        CheckHardness();
    }
    
    void DisableButtonIfNoNextOre()
    {
        if (tempSelectOreIndex - 1 < 0 || !oreDatabase.ores[tempSelectOreIndex - 1].isUnlocked)
        {
            _uiManager.previousOreButton.GetComponent<Image>().color = new Color(0.38f, 0.38f, 0.38f);
        }
        else if (tempSelectOreIndex + 1 > oreDatabase.ores.Length - 1 || !oreDatabase.ores[tempSelectOreIndex + 1].isUnlocked)
        {
            _uiManager.nextOreButton.GetComponent<Image>().color = new Color(0.38f, 0.38f, 0.38f);
        }
    }
    
    void CheckHardness()
    {
        if (_thisOre.currentHardness <= 0)
        {
            _thisOre.currentHardness = _thisOre.defaultHardness;
            _collectionManager.DropItem();
        }
        if (_thisOre.isPremium)
        {
            ModifyOreAmount(_thisOre, -1);
        }
    }

    void CheckOreIndex()
    {
        if (tempSelectOreIndex >= oreDatabase.ores.Length || tempSelectOreIndex < 0)
        {
            tempSelectOreIndex = 0;
        }
    }
    
    public void ModifyOreAmount(OreStats ore, int amount)
    {
        ore.amount += amount;
        if (ore.amount > 0)
        {
            ore.isUnlocked = true;
        }
        else
        {
            ore.isUnlocked = false;
            tempSelectOreIndex = 0;
            UpdateOre();
            _uiManager.UpdateOreDetails();
            _uiManager.UpdateOreImageHead();
            _uiManager.UpdateOreNameText(_thisOre.oreName);
        }
    }
    
    public OreStats GetOreStats()
    {
        return _thisOre;
    }
}