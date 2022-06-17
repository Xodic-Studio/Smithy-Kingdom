using System;
using TMPro;
using UnityEngine;

public class UIManager : Singleton<UIManager>
{
    public TMP_Text oreNameText;
    public TMP_Text moneyText;
    private GameManager _gameManager;

    private void Awake()
    {
        _gameManager = GameManager.Instance;
    }

    //update money text
    public void UpdateMoneyText()
    {
        moneyText.text = $"$: {_gameManager.money.ToString()}";
    }
    
    
    
    public void UpdateOreNameText(string newText)
    {
        oreNameText.text = newText;
    }
    
}
