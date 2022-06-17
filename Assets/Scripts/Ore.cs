using System;
using Unity.VisualScripting.ReorderableList;
using UnityEngine;

public class Ore : Singleton<Ore>
{
    [SerializeField] private OreDatabase oreDatabase;
    private UIManager _uiManager;
    private GameManager _gameManager;
    
    private OreStats _thisOre;
    private SpriteRenderer _oreSprite;
    public int selectedOreIndex;
    
    

    private void Awake()
    {
        _uiManager = UIManager.Instance;
        _gameManager = GameManager.Instance;
    }

    private void Start()
    {
        _oreSprite = GetComponent<SpriteRenderer>();
        UpdateOre();
    }
    
    

    void Update()
    {
        CheckHardness();
        CheckOreIndex();
    }
    

    //update ore
    public void UpdateOre()
    {
        _thisOre = oreDatabase.ores[selectedOreIndex];
        name = _thisOre.oreName;
        _thisOre.currentHardness = _thisOre.defaultHardness;
        _oreSprite.sprite = _thisOre.oreSprite;
        _uiManager.UpdateOreNameText(_thisOre.oreName);
    }
    
    public void ModifySelectedOreIndex(int index)
    {
        if (index == -1 && selectedOreIndex - 1 >= 0)
        {
            selectedOreIndex--;
        }
        else if (index == 1 && selectedOreIndex + 1 < oreDatabase.ores.Length)
        {
            selectedOreIndex++;
        }
        Debug.Log("You selected ore " + selectedOreIndex);
        
    }
    public void ModifyHardness(int amount)
    {
        _thisOre.currentHardness -= amount;
    }
    void CheckHardness()
    {
        if (_thisOre.currentHardness <= 0)
        {
            _thisOre.currentHardness = _thisOre.defaultHardness;
            _gameManager.ModifyMoney(100);
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
