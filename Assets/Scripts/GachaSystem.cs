using System.Collections.Generic;
using GameDatabase;
using Manager;
using UnityEngine;
using Random = UnityEngine.Random;

public class GachaSystem : Singleton<GachaSystem>
{
    private Ore _ore;
    private UIManager _uiManager;
    private OreDatabase _oreDatabase;
    private OreStats[] PremiumOres => _ore.oreDatabase.premiumOres;

    private void Awake()
    {
        _ore = Ore.Instance;
        _uiManager = UIManager.Instance;
    }

    public List<Sprite> resultSprites = new();
    
    public void RandomGacha()
    {
        if (PremiumOres.Length != 0)
        {
            var randomNumber = Random.Range(0,PremiumOres.Length);
            _ore.ModifyOreAmount(PremiumOres[randomNumber], 1);
            resultSprites.Add(PremiumOres[randomNumber].oreSprite);
        }
        else
        {
            Debug.LogWarning("There are no Premium Ores in the database");
        }
    }
    
}
