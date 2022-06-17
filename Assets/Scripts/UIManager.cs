using System;
using TMPro;
using UnityEngine;

public class UIManager : Singleton<UIManager>
{
    public TMP_Text oreNameText;
    public GameManager gameManager;

    private void Awake()
    {
        gameManager = GameManager.Instance;
    }

    public void UpdateOreNameText(string newText)
    {
        oreNameText.text = newText;
    }
    
}
