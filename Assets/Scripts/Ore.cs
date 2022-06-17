using System;
using UnityEngine;

public class Ore : MonoBehaviour
{
    [SerializeField] private OreDatabase oreDatabase;
    private UIManager _uiManager;
    
    
    
    private OreStats _thisOre;
    private SpriteRenderer _oreSprite;
    
    public int selectedOreIndex;
    
    
    
    
    
    
    
    
    
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


    private void Awake()
    {
        _uiManager = UIManager.Instance;
    }

    private void Start()
    {
        _oreSprite = GetComponent<SpriteRenderer>();
        UpdateOre();
    }
    
    //update ore
    public void UpdateOre()
    {
        _thisOre = oreDatabase.ores[selectedOreIndex];
        _oreSprite.sprite = _thisOre.oreSprite;
        _uiManager.UpdateOreNameText(_thisOre.oreName);
    }

    void Update()
    {
        CheckHardness();
        CheckOreIndex();
    }
    void CheckHardness()
    {
        if (_thisOre.currentHardness <= 0)
        {
            _thisOre.currentHardness = _thisOre.defaultHardness;
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
