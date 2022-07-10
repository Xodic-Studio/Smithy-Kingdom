using UnityEngine;

public class Ore : Singleton<Ore>
{
    public  OreDatabase oreDatabase;
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
        UpdateOre();
    }
    
    void Update()
    {
        _uiManager.UpdateHardnessSlider(_thisOre.currentHardness, _thisOre.defaultHardness);
    }
    
    //update ore
    public void UpdateOre()
    {
        selectedOreIndex = tempSelectOreIndex;
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
        if (index == -1 && tempSelectOreIndex - 1 >= 0)
        {
            if (oreDatabase.ores[tempSelectOreIndex - 1].isUnlocked)
            {
                tempSelectOreIndex--;
                _uiManager.nextOreButton.gameObject.SetActive(true);
                DisableButtonIfNoNextOre();
            }
        }
        else if (index == 1 && tempSelectOreIndex + 1 < oreDatabase.ores.Length)
        {
            if (oreDatabase.ores[tempSelectOreIndex + 1].isUnlocked)
            {
                tempSelectOreIndex++;
                _uiManager.previousOreButton.gameObject.SetActive(true);
                DisableButtonIfNoNextOre();
            }
        }
        CheckOreIndex();
        Debug.Log("You selected ore " + oreDatabase.ores[tempSelectOreIndex].oreName);
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
            _uiManager.previousOreButton.gameObject.SetActive(false);
        }
        else if (tempSelectOreIndex + 1 > oreDatabase.ores.Length - 1 || !oreDatabase.ores[tempSelectOreIndex + 1].isUnlocked)
        {
            _uiManager.nextOreButton.gameObject.SetActive(false);
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
        if (tempSelectOreIndex >= oreDatabase.ores.Length || tempSelectOreIndex < 0)
        {
            tempSelectOreIndex = 0;
        }
    }
    
    public OreStats GetOreStats()
    {
        return _thisOre;
    }
}