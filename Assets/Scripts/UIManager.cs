using TMPro;
using UnityEngine.UI;

public class UIManager : Singleton<UIManager>
{
    public TMP_Text oreNameText;
    public TMP_Text moneyText;
    public Slider hardnessSlider;
    public TMP_Text hardnessText;
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
    
    public void UpdateMaxHardnessSlider(int hardness)
    {
        hardnessSlider.maxValue = hardness;
        hardnessSlider.value = hardness;
        
        
    }

    public void UpdateHardnessSlider(int hardness, int maxHardness)
    {
        hardnessSlider.value = hardness;
        hardnessText.text = $"{hardness}/{maxHardness}";
    }
    
    
    
    
}
