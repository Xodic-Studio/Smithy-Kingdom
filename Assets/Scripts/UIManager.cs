using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : Singleton<UIManager>
{
    private GameManager _gameManager;
    
    [Header("BaseUI")]
    public TMP_Text oreNameText;
    public TMP_Text moneyText;
    public Slider hardnessSlider;
    public TMP_Text hardnessText;
    

    private void Awake()
    {
        _gameManager = GameManager.Instance;
    }

    private void Start()
    {
        UpgradeStart();
        AddUpgradeButtons();
        AddMenuButtons();
    }

    //update money text
    public void UpdateMoneyText()
    {
        moneyText.text = $"$: {_gameManager.money}";
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

    #region MainMenu
    
    [Header("Main Menu")]
    public Button upgradeMenuButton;
    public Button collectiblesMenuButton;
    public Button premiumMenuButton;
    public Button settingsMenuButton;
    public Button closeButton;
    
    public GameObject overrideCanvas;
    public GameObject baseCanvas;
    public GameObject upgradeMenu;
    public GameObject collectiblesMenu;
    public GameObject premiumMenu;
    public GameObject settingsMenu;

    private void AddMenuButtons()
    {
        upgradeMenuButton.onClick.AddListener(OpenUpgradeMenu);
        collectiblesMenuButton.onClick.AddListener(OpenCollectiblesMenu);
        premiumMenuButton.onClick.AddListener(OpenPremiumMenu);
        settingsMenuButton.onClick.AddListener(OpenSettingsMenu);
        closeButton.onClick.AddListener(CloseMenu);
    }


    public void CloseMenu()
    {
        overrideCanvas.SetActive(false);
        baseCanvas.SetActive(true);
    }
    
    public void OpenUpgradeMenu()
    {
        CheckCanvas();
        upgradeMenu.SetActive(true);
        collectiblesMenu.SetActive(false);
        premiumMenu.SetActive(false);
        settingsMenu.SetActive(false);
        TapNormalUpgradePanel();
    }

    public void OpenCollectiblesMenu()
    {
        CheckCanvas();
        upgradeMenu.SetActive(false);
        collectiblesMenu.SetActive(true);
        premiumMenu.SetActive(false);
        settingsMenu.SetActive(false);
    }

    public void OpenPremiumMenu()
    {
        CheckCanvas();
        upgradeMenu.SetActive(false);
        collectiblesMenu.SetActive(false);
        premiumMenu.SetActive(true);
        settingsMenu.SetActive(false);
    }

    public void OpenSettingsMenu()
    {
        CheckCanvas();
        upgradeMenu.SetActive(false);
        collectiblesMenu.SetActive(false);
        premiumMenu.SetActive(false);
        settingsMenu.SetActive(true);
    }

    private void CheckCanvas()
    {
        if (!overrideCanvas.activeSelf)
        {
            overrideCanvas.SetActive(true);
        } 
        if (baseCanvas.activeSelf)
        {
            baseCanvas.SetActive(false);
        }
    }
    
    
    #endregion
    
    #region Upgrades

    [Header("Upgrades Tab")]
    public ScrollRect upgradesScrollRect;
    public Button normalUpgradeButton;
    public Button premiumUpgradeButton;
    public GameObject normalUpgradePanel;
    public GameObject premiumUpgradePanel;
    private RectTransform _normalRectTransform;
    private RectTransform _premiumRectTransform;

    private void UpgradeStart()
    {
        _normalRectTransform = normalUpgradePanel.GetComponent<RectTransform>();
        _premiumRectTransform = premiumUpgradePanel.GetComponent<RectTransform>();
    }

    private void AddUpgradeButtons()
    {
        normalUpgradeButton.onClick.AddListener(TapNormalUpgradePanel);
        premiumUpgradeButton.onClick.AddListener(TapPremiumUpgradePanel);

    }

    private void TapNormalUpgradePanel()
    {
        normalUpgradePanel.SetActive(true);
        premiumUpgradePanel.SetActive(false);
        upgradesScrollRect.content = _normalRectTransform;
        
    }

    private void TapPremiumUpgradePanel()
    {
        normalUpgradePanel.SetActive(false);
        premiumUpgradePanel.SetActive(true);
        upgradesScrollRect.content = _premiumRectTransform;
    }

    #endregion
    
    #region Collectibles
    [Header("Collectibles Tab")]
    public ScrollRect collectiblesScrollRect;
    
    #endregion
    
    #region Premium
    [Header("Premium Tab")]
    public ScrollRect premiumScrollRect;
    #endregion
    
    #region Settings
    [Header("Settings Tab")]
    public ScrollRect settingsScrollRect;

    #endregion





}
#if UNITY_EDITOR
[CustomEditor(typeof(UIManager))]
public class UIManagerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        UIManager uiManager = (UIManager) target;
        base.OnInspectorGUI();
        EditorGUILayout.Space();
        if (GUILayout.Button("Close Menu"))
        {
            uiManager.CloseMenu();
        }
        EditorGUILayout.Space();
        GUILayout.Label("Menu Quick Access");
        GUILayout.BeginHorizontal( GUILayout.ExpandWidth(true));
        if (GUILayout.Button("UpgradeMenu"))
        {
            uiManager.OpenUpgradeMenu();
        }
        if (GUILayout.Button("CollectiblesMenu"))
        {
            uiManager.OpenCollectiblesMenu();
        }
        if (GUILayout.Button("PremiumMenu"))
        {
            uiManager.OpenPremiumMenu();
        }
        if (GUILayout.Button("SettingsMenu"))
        {
            uiManager.OpenSettingsMenu();
        }
        GUILayout.EndHorizontal();
        
    }
}

#endif
