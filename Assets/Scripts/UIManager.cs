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
    
    #region UpdateUI
    public enum DatabaseType
    {
        Collection,
        Item,
        Upgrades,
        PremiumUpgrade,
    }
    
    [Header("Update UI")]
    [SerializeField]
    internal Database database;

    [SerializeField] internal GameObject upgradeList;
    [SerializeField] internal GameObject premiumUpgradeList;
    [SerializeField] internal GameObject upgradeUIPrefab;

    internal RectTransform ThisRectTransform;
    internal RectTransform ParentRectTransform;
    internal RectTransform PrefabRectTransform;
    internal VerticalLayoutGroup ThisVerticalLayoutGroup;

    [SerializeField] internal GameObject collectionList;
    [SerializeField] internal GameObject collectionUIPrefab;

    #endregion
}

#if UNITY_EDITOR
[CustomEditor(typeof(UIManager))]
public class UIManagerEditor : Editor
{
    //TODO: Refactor this to be more readable
    public override void OnInspectorGUI()
    {
        #region Navigation

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
        #endregion
        EditorGUILayout.Space();
        GUILayout.BeginHorizontal(GUILayout.ExpandWidth(true));
        if (GUILayout.Button("UpdateUpgrade"))
        {
            uiManager.OpenUpgradeMenu();
            UpdateUI(UIManager.DatabaseType.Upgrades);
        }
        if (GUILayout.Button("UpdatePremiumUpgrade"))
        {
            uiManager.OpenPremiumMenu();
            UpdateUI(UIManager.DatabaseType.Upgrades);
        }
        if (GUILayout.Button("UpdateCollection"))
        {
            uiManager.OpenCollectiblesMenu();
            UpdateUI(UIManager.DatabaseType.Collection);
        }
        GUILayout.EndHorizontal();

        #region UpdateUI
        
        void UpdateUI(UIManager.DatabaseType databaseType)
        {
            void CheckIfActive()
            {
                if (databaseType == UIManager.DatabaseType.Upgrades)

            {
                    var active = uiManager.upgradeList.activeSelf;
                    uiManager.ThisVerticalLayoutGroup = uiManager.upgradeList.GetComponent<VerticalLayoutGroup>();
                    uiManager.PrefabRectTransform = uiManager.upgradeUIPrefab.GetComponent<RectTransform>();
                    uiManager.ParentRectTransform = uiManager.upgradeList.transform.parent.GetComponent<RectTransform>();
                    uiManager.ThisRectTransform = uiManager.upgradeList.GetComponent<RectTransform>();
                    if (!active)
                    {
                        uiManager.upgradeList.SetActive(true);
                    }
                } 
                else if (databaseType == UIManager.DatabaseType.Collection)
                {
                    var active = uiManager.collectionList.activeSelf;
                    uiManager.ThisVerticalLayoutGroup = uiManager.collectionList.GetComponent<VerticalLayoutGroup>();
                    uiManager.PrefabRectTransform = uiManager.collectionUIPrefab.GetComponent<RectTransform>();
                    uiManager.ParentRectTransform = uiManager.collectionList.transform.parent.GetComponent<RectTransform>();
                    uiManager.ThisRectTransform = uiManager.collectionList.GetComponent<RectTransform>();
                    if (!active)
                    {
                        uiManager.collectionList.SetActive(true);
                    }
                }
                else
                {
                    var active = uiManager.upgradeList.activeSelf;
                    if (!active)
                    {
                        uiManager.upgradeList.SetActive(true);
                    }
                }
            }
            void EditUIs()
            {
                if (databaseType == UIManager.DatabaseType.Upgrades)
                {
                    var databaseStats = uiManager.database.upgradeDatabase.stats;
                    var listTransform = uiManager.upgradeList.transform;
                    var i = 0;
                    foreach (var unused in databaseStats)
                    {  
                        try
                        {
                            listTransform.transform.GetChild(i);
                        }
                        catch (UnityException)
                        {
                            var newUpgrade = Instantiate(uiManager.upgradeUIPrefab, listTransform);
                            newUpgrade.transform.SetParent(listTransform);
                        }
                        listTransform.GetChild(i).name = databaseStats[i].upgradeName;
                        listTransform.GetChild(i).Find("UpgradeTextArea/UpgradeName").GetComponent<TMP_Text>().text = databaseStats[i].upgradeName;
                        listTransform.GetChild(i).Find("UpgradeTextArea/UpgradeDescription").GetComponent<TMP_Text>().text = databaseStats[i].upgradeDescription;
                        i++;
                    }

                    var childCount = listTransform.childCount;
                    var needDestroy = childCount - databaseStats.Length;

                    for (i = childCount; i > childCount - needDestroy; i--)
                    {
                        DestroyImmediate(listTransform.GetChild(i-1).gameObject);
                    }
                    if (listTransform.transform.childCount <= 5)
                    {
                        uiManager.ThisRectTransform.sizeDelta = new Vector2(uiManager.ThisRectTransform.sizeDelta.x,
                            uiManager.ParentRectTransform.rect.height);
                    }
                    else
                    {
                        float y = uiManager.ParentRectTransform.rect.height +
                                  (listTransform.childCount - 5) *
                                  (uiManager.PrefabRectTransform.rect.height +
                                   uiManager.ThisVerticalLayoutGroup.spacing);
                        uiManager.ThisRectTransform.sizeDelta = new Vector2(uiManager.ThisRectTransform.sizeDelta.x, y);
                    }
                } 
                else if (databaseType == UIManager.DatabaseType.Collection)
                {
                    var databaseStats = uiManager.database.itemsDatabase.collections;
                    var listTransform = uiManager.collectionList.transform;
                    var i = 0;
                    foreach (var unused in databaseStats)
                    {  
                        try
                        {
                            listTransform.transform.GetChild(i);
                        }
                        catch (UnityException)
                        {
                            var newUpgrade = Instantiate(uiManager.collectionUIPrefab, listTransform);
                            newUpgrade.transform.SetParent(listTransform);
                        }
                        listTransform.GetChild(i).name = databaseStats[i].collectionName;
                        listTransform.GetChild(i).GetComponent<TMP_Text>().text = databaseStats[i].collectionName;
                        i++;
                    }
                    var childCount = listTransform.childCount;                       
                    var needDestroy = childCount - databaseStats.Length;
                    

                    for (i = childCount; i > childCount - needDestroy; i--)
                    {
                        DestroyImmediate(listTransform.GetChild(i-1).gameObject);
                    }
                    if (listTransform.transform.childCount <= 2)
                    {
                        uiManager.ThisRectTransform.sizeDelta = new Vector2(uiManager.ThisRectTransform.sizeDelta.x,
                            uiManager.ParentRectTransform.rect.height);
                    }
                    else
                    {
                        float y = uiManager.ParentRectTransform.rect.height +
                                  (listTransform.childCount - 2) *
                                  (uiManager.PrefabRectTransform.rect.height +
                                   uiManager.ThisVerticalLayoutGroup.spacing);
                        uiManager.ThisRectTransform.sizeDelta = new Vector2(uiManager.ThisRectTransform.sizeDelta.x, y);
                    }
                }
            }
            
            
            CheckIfActive();
            EditUIs();


            
        }


        #endregion
    }
}

#endif
