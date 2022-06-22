using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : Singleton<UIManager>
{
    public TMP_Text oreNameText;
    public TMP_Text moneyText;
    public Slider hardnessSlider;
    public TMP_Text hardnessText;
    public GameObject overideCanvas;
    public GameObject baseCanvas;
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


    public void CloseOveride()
    {
        overideCanvas.SetActive(false);
        baseCanvas.SetActive(true);
    }

    public void OpenOveride()
    {
        overideCanvas.SetActive(true);
        baseCanvas.SetActive(false);
    }
    
    
}
#if UNITY_EDITOR
[CustomEditor(typeof(UIManager))]
public class UIManagerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        UIManager uiManager = (UIManager) target;
        base.OnInspectorGUI();
        if (GUILayout.Button("OpenBaseCanvas"))
        {
            uiManager.baseCanvas.SetActive(true);
            uiManager.overideCanvas.SetActive(false);
        }

        if (GUILayout.Button("OpenOverideCanvas"))
        {
            uiManager.baseCanvas.SetActive(false);
            uiManager.overideCanvas.SetActive(true);
        }
    }
}

#endif
