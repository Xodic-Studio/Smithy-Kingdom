using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : Singleton<UIManager>
{
    private GameManager _gameManager;
    
    [Header("AutoUI Scripts")]
    public Upgrade uiUpgrade;
    public Upgrade uiPremiumUpgrade;
    public CollectionType uiCollectionType;
    
    [Header("BaseUI")]
    public TMP_Text oreNameText;
    public TMP_Text moneyText;
    public GameObject previousOreButtonGo;
    public GameObject nextOreButtonGo;
    public Slider hardnessSlider;
    public TMP_Text hardnessText;
    private Button _previousOreButton;
    private Button _nextOreButton;
    

    private void Awake()
    {
        _gameManager = GameManager.Instance;
    }

    private void Start()
    {
        BaseStart();
        UpgradeStart();
        AddUpgradeButtons();
        AddMenuButtons();
    }

    /// <summary>
    /// Update the Money TMP text with the current money
    /// </summary>
    public void UpdateMoneyText()
    {
        moneyText.text = $"$: {_gameManager.money}";
    }
    
    
    /// <summary>
    /// Update the Ore Name TMP text with the current ore name
    /// </summary>
    /// <param name="newText"> is the Ore Name</param>
    public void UpdateOreNameText(string newText)
    {
        oreNameText.text = newText;
    }
    
    /// <summary>
    /// Update the Hardness Slider Max Value and TMP text with the default hardness
    /// </summary>
    /// <param name="defaultHardness">Default hardness of an ore</param>
    public void UpdateMaxHardnessSlider(int defaultHardness)
    {
        hardnessSlider.maxValue = defaultHardness;
        hardnessSlider.value = defaultHardness;
        
        
    }

    /// <summary>
    /// Update the Hardness Slider Value and TMP text with the current hardness
    /// </summary>
    /// <param name="hardness">Current Hardness of an ore</param>
    /// <param name="maxHardness">Default Hardness of an ore</param>
    public void UpdateHardnessSlider(int hardness, int maxHardness)
    {
        hardnessSlider.value = hardness;
        hardnessText.text = $"{hardness}/{maxHardness}";
    }

    #region Base

    void BaseStart()
    {
        _nextOreButton = nextOreButtonGo.GetComponent<Button>();
        _previousOreButton = previousOreButtonGo.GetComponent<Button>();
        _nextOreButton.onClick.AddListener(() => _gameManager.SelectNextOre());
        _previousOreButton.onClick.AddListener(() => _gameManager.SelectPreviousOre());
    }

    #endregion
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

    /// <summary>
    /// Add Functions to all buttons in the Main Menu
    /// </summary>
    private void AddMenuButtons()
    {
        upgradeMenuButton.onClick.AddListener(OpenUpgradeMenu);
        collectiblesMenuButton.onClick.AddListener(OpenCollectiblesMenu);
        premiumMenuButton.onClick.AddListener(OpenPremiumMenu);
        settingsMenuButton.onClick.AddListener(OpenSettingsMenu);
        closeButton.onClick.AddListener(CloseMenu);
    }


    /// <summary>
    /// Close Button Function
    /// Close Current UI and Open Base UI
    /// </summary>
    public void CloseMenu()
    {
        overrideCanvas.SetActive(false);
        baseCanvas.SetActive(true);
    }
    
    /// <summary>
    /// Open Upgrade Menu Function
    /// Open Upgrade Menu, Close Other Menu and Close Base UI
    /// </summary>
    public void OpenUpgradeMenu()
    {
        CheckCanvas();
        upgradeMenu.SetActive(true);
        collectiblesMenu.SetActive(false);
        premiumMenu.SetActive(false);
        settingsMenu.SetActive(false);
        TapNormalUpgradePanel();
    }

    /// <summary>
    /// Open Collectibles Menu Function
    /// Open Collectibles Menu, Close Other Menu and Close Base UI
    /// </summary>
    public void OpenCollectiblesMenu()
    {
        CheckCanvas();
        upgradeMenu.SetActive(false);
        collectiblesMenu.SetActive(true);
        premiumMenu.SetActive(false);
        settingsMenu.SetActive(false);
    }

    /// <summary>
    /// Open Premium Menu Function
    /// Open Premium Menu, Close Other Menu and Close Base UI
    /// </summary>
    public void OpenPremiumMenu()
    {
        CheckCanvas();
        upgradeMenu.SetActive(false);
        collectiblesMenu.SetActive(false);
        premiumMenu.SetActive(true);
        settingsMenu.SetActive(false);
    }

    /// <summary>
    /// Open Settings Menu Function
    /// Open Settings Menu, Close Other Menu and Close Base UI
    /// </summary>
    public void OpenSettingsMenu()
    {
        CheckCanvas();
        upgradeMenu.SetActive(false);
        collectiblesMenu.SetActive(false);
        premiumMenu.SetActive(false);
        settingsMenu.SetActive(true);
    }

    /// <summary>
    /// Check if the override canvas is already active or not
    /// </summary>
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

    /// <summary>
    /// This function needed to be called in start
    /// </summary>
    private void UpgradeStart()
    {
        _normalRectTransform = normalUpgradePanel.GetComponent<RectTransform>();
        _premiumRectTransform = premiumUpgradePanel.GetComponent<RectTransform>();
    }

    /// <summary>
    /// Add Functions to all buttons in the Upgrades Menu
    /// </summary>
    private void AddUpgradeButtons()
    {
        normalUpgradeButton.onClick.AddListener(TapNormalUpgradePanel);
        premiumUpgradeButton.onClick.AddListener(TapPremiumUpgradePanel);

    }

    /// <summary>
    /// Switch to the Normal Upgrade Panel
    /// </summary>
    public void TapNormalUpgradePanel()
    {
        normalUpgradePanel.SetActive(true);
        premiumUpgradePanel.SetActive(false);
        upgradesScrollRect.content = _normalRectTransform;
        
    }

    /// <summary>
    /// Switch to the Premium Upgrade Panel
    /// </summary>
    public void TapPremiumUpgradePanel()
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
        GUILayout.Label("Menu Quick Access", EditorStyles.boldLabel);
        GUILayout.BeginHorizontal(GUILayout.ExpandWidth(true));
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
        EditorGUILayout.Space();
        GUILayout.Label("Update All UI", EditorStyles.boldLabel);
        GUILayout.BeginHorizontal(GUILayout.ExpandWidth(true));
        if (GUILayout.Button("Update UIs", GUILayout.Width(100)))
        {
            uiManager.OpenUpgradeMenu();
            uiManager.TapNormalUpgradePanel();
            uiManager.uiUpgrade.UpdateUpgrades();
            uiManager.TapPremiumUpgradePanel();
            uiManager.uiPremiumUpgrade.UpdateUpgrades();
            uiManager.OpenCollectiblesMenu();
            uiManager.uiCollectionType.UpdateCollection();
        }
        GUILayout.Label("**Update All Data For UIs**");
        GUILayout.EndHorizontal();
    }
}

#endif
