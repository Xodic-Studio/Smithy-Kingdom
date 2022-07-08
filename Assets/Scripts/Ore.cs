using UnityEngine;

public class Ore : Singleton<Ore>
{
    [SerializeField] private OreDatabase oreDatabase;
    private UIManager _uiManager;
    private CollectionManager _collectionManager;
    
    private OreStats _thisOre;
    public int selectedOreIndex;
    
    private void Awake()
    {
        _uiManager = UIManager.Instance;
        _collectionManager = CollectionManager.Instance;
    }

    private void Start()
    {
        UpdateOre();
    }
    
    void Update()
    {
        _uiManager.UpdateHardnessSlider(_thisOre.currentHardness, _thisOre.defaultHardness);
    }
    
    //update ore
    public void UpdateOre()
    {
        DisableButtonIfNoNextOre();
        _thisOre = oreDatabase.ores[selectedOreIndex];
        name = _thisOre.oreName;
        _uiManager.UpdateMaxHardnessSlider(_thisOre.defaultHardness);
        _thisOre.currentHardness = _thisOre.defaultHardness;
        _uiManager.UpdateOreNameText(_thisOre.oreName);
        _collectionManager.UpdateItemSelection();
        _collectionManager.UpdateRandomSystem();
    }
    
    public void ModifySelectedOreIndex(int index)
    {
        if (index == -1 && selectedOreIndex - 1 >= 0)
        {
            if (oreDatabase.ores[selectedOreIndex - 1].isUnlocked)
            {
                selectedOreIndex--;
                _uiManager.nextOreButtonGo.SetActive(true);
                DisableButtonIfNoNextOre();
            }
        }
        else if (index == 1 && selectedOreIndex + 1 < oreDatabase.ores.Length)
        {
            if (oreDatabase.ores[selectedOreIndex + 1].isUnlocked)
            {
                selectedOreIndex++;
                _uiManager.previousOreButtonGo.SetActive(true);
                DisableButtonIfNoNextOre();
            }
        }
        CheckOreIndex();
        Debug.Log("You selected ore " + oreDatabase.ores[selectedOreIndex].oreName);
    }
    public void ModifyHardness(int amount)
    {
        _thisOre.currentHardness -= amount;
        CheckHardness();
    }
    
    void DisableButtonIfNoNextOre()
    {
        if (selectedOreIndex - 1 < 0 || !oreDatabase.ores[selectedOreIndex - 1].isUnlocked)
        {
            _uiManager.previousOreButtonGo.SetActive(false);
        }
        else if (selectedOreIndex + 1 > oreDatabase.ores.Length - 1 || !oreDatabase.ores[selectedOreIndex + 1].isUnlocked)
        {
            _uiManager.nextOreButtonGo.SetActive(false);
        }
    }
    
    void CheckHardness()
    {
        if (_thisOre.currentHardness <= 0)
        {
            _thisOre.currentHardness = _thisOre.defaultHardness;
            _collectionManager.DropItem();
        }
    }

    void CheckOreIndex()
    {
        if (selectedOreIndex >= oreDatabase.ores.Length || selectedOreIndex < 0)
        {
            selectedOreIndex = 0;
        }
    }
    
    public OreStats GetOreStats()
    {
        return _thisOre;
    }
}