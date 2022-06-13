using System;
using UnityEngine;

public class Ore : MonoBehaviour
{
    OreInventory _oreInventory;
    OreStats _oreStats;
    
    private string _oreName;
    private string _oreDescription;
    private Sprite _oreSprite;
    private int _currentHardness;
    private int _defaultHardness;
    private bool _isPremium;
    

    void Start()
    {
        UpdateStats();
    }


    /// <summary>
    /// Update All Stats of the Ore
    /// </summary>
    private void UpdateStats()
    {
        _oreName = _oreStats.oreName;
        _oreDescription = _oreStats.oreDescription;
        _oreSprite = _oreStats.oreSprite;
        _currentHardness = _oreStats.currentHardness;
        _defaultHardness = _oreStats.defaultHardness;
        _isPremium = _oreStats.isPremium;
    }
    
    private void OnValidate()
    {
        gameObject.name = _oreName;
    }
    
    
    
    /// <summary>
    /// Method to Damage the ore Returns integer of points
    /// </summary>
    /// <param name="damage"> the damage that did to the ore</param>\
    /// <returns>return 1 if hardness reach 0, else return 0</returns>
    public int DamageOre(int damage)
    {
        _currentHardness -= damage;
        if (_currentHardness <= 0)
        {
            _currentHardness = _defaultHardness;
            return 1;
        }
        return 0;
    }
    
    
}
