using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    private Ore _ore;
    private UIManager _uiManager;

    public float money;
    
    
    
    //Hammer variables
    public int hammerDamage;


    private void Awake()
    {
        _uiManager = UIManager.Instance;
        _ore = Ore.Instance;
    }

    private void Start()
    {
        _uiManager.UpdateMoneyText();
    }


    //Modify money
    public void ModifyMoney(float amount)
    {
        money += amount;
        _uiManager.UpdateMoneyText();
    }
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    

    //Hitting Function
    public void TapTap()
    {
        Debug.Log("Hammer hit");
        _ore.ModifyHardness(hammerDamage);
    }
    
    // Ore Selection
    public void SelectPreviousOre()
    {
        _ore.ModifySelectedOreIndex(-1);
        _ore.UpdateOre();
        _uiManager.UpdateOreNameText(_ore.GetOreStats().oreName);
    }
    
    public void SelectNextOre()
    {
        _ore.ModifySelectedOreIndex(1);
        _ore.UpdateOre();
        _uiManager.UpdateOreNameText(_ore.GetOreStats().oreName);
    }

    


}
