using System;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    [SerializeField] private Ore ore;
    private UIManager _uiManager;
    
    
    
    //Hammer variables
    public int hammerDamage;


    private void Awake()
    {
        _uiManager = UIManager.Instance;
    }


    //Hitting Function
    void OnFire()
    {
        Debug.Log("Hammer hit");
        ore.ModifyHardness(hammerDamage);
    }
    
    // Ore Selection
    public void SelectPreviousOre()
    {
        ore.ModifySelectedOreIndex(-1);
        ore.UpdateOre();
        _uiManager.UpdateOreNameText(ore.GetOreStats().oreName);
    }
    
    public void SelectNextOre()
    {
        ore.ModifySelectedOreIndex(1);
        ore.UpdateOre();
        _uiManager.UpdateOreNameText(ore.GetOreStats().oreName);
    }



}
