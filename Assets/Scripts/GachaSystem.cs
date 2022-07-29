
using System;
using GameDatabase;
using Manager;
using UnityEngine;
using Random = UnityEngine.Random;

public class GachaSystem : Singleton<GachaSystem>
{
    private Ore _ore;
    private UIManager _uiManager;
    private OreDatabase _oreDatabase;
    private OreStats[] _premiumOres;

    private void Awake()
    {
        _ore = Ore.Instance;
        _uiManager = UIManager.Instance;
    }

    private void Start()
    {
        _oreDatabase = _ore.oreDatabase;
        
        int i = 0;
        Array.Resize(ref _premiumOres, _oreDatabase.premiumOres.Length);
        foreach (var VARIABLE in _oreDatabase.premiumOres)
        {
            if (VARIABLE.isPremium)
            {
                _premiumOres[i] = VARIABLE;
                i++;
            }
        }
    }

    public void RandomGacha()
    {
        if (_premiumOres.Length != 0)
        {
            var randomNumber = Random.Range(1,6);
            switch (randomNumber)
            {
                case 1:
                    Debug.Log("You got a " + _premiumOres[0].oreName);
                    _ore.ModifyOreAmount(_premiumOres[0], 1);
                    _uiManager.AddRewardImage(_premiumOres[0].oreSprite);
                    _uiManager.OpenGachaResults();
                    break;
                case 2:
                    Debug.Log("You got a " + _premiumOres[1].oreName);
                    _ore.ModifyOreAmount(_premiumOres[1], 1);
                    _uiManager.AddRewardImage(_premiumOres[1].oreSprite);
                    _uiManager.OpenGachaResults();
                    break;
                case 3:
                    Debug.Log("You got a " + _premiumOres[2].oreName);
                    _ore.ModifyOreAmount(_premiumOres[2], 1);
                    _uiManager.AddRewardImage(_premiumOres[2].oreSprite);
                    _uiManager.OpenGachaResults();
                    break;
                case 4:
                    Debug.Log("You got a " + _premiumOres[3].oreName);
                    _ore.ModifyOreAmount(_premiumOres[3], 1);
                    _uiManager.AddRewardImage(_premiumOres[3].oreSprite);
                    _uiManager.OpenGachaResults();
                    break;
                case 5:
                    Debug.Log("You got a " + _premiumOres[4].oreName);
                    _ore.ModifyOreAmount(_premiumOres[4], 1);
                    _uiManager.AddRewardImage(_premiumOres[4].oreSprite);
                    _uiManager.OpenGachaResults();
                    break;
                case 6:
                    Debug.Log("You got a " + _premiumOres[5].oreName);
                    _ore.ModifyOreAmount(_premiumOres[5], 1);
                    _uiManager.AddRewardImage(_premiumOres[5].oreSprite);
                    _uiManager.OpenGachaResults();
                    break;
            }
        }
        else
        {
            Debug.LogWarning("There are no Premium Ores in the database");
        }
    }
    
}
