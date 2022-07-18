using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : Singleton<UIManager>
{
    private GameManager _gameManager;
    private Ore _ore;

    [Header("BaseUI")] public TMP_Text oreNameText;
    public TMP_Text moneyText;
    public TMP_Text gemText;
    public Slider hardnessSlider;
    public TMP_Text hardnessText;

    private void Awake()
    {
        _ore = Ore.Instance;
        _gameManager = GameManager.Instance;
    }

    private void Start()
    {
        UpgradeStart();
        AddUpgradeButtons();
        AddMenuButtons();
        OreSelectionStart();
        OverlayStart();
    }

    /// <summary>
    ///     Update the Money TMP text with the current money
    /// </summary>
    public void UpdateMoneyText()
    {
        moneyText.text = $"$: {_gameManager.GetMoney()}";
    }

    /// <summary>
    ///     Update the Gem TMP text with the current gem
    /// </summary>
    public void UpdateGemText()
    {
        gemText.text = $"{_gameManager.GetGems()}";
    }

    /// <summary>
    ///     Update the Ore Name TMP text with the current ore name
    /// </summary>
    /// <param name="newText"> is the Ore Name</param>
    public void UpdateOreNameText(string newText)
    {
        oreNameText.text = newText;
    }

    /// <summary>
    ///     Update the Hardness Slider Max Value and TMP text with the default hardness
    /// </summary>
    /// <param name="defaultHardness">Default hardness of an ore</param>
    public void UpdateMaxHardnessSlider(int defaultHardness)
    {
        hardnessSlider.maxValue = defaultHardness;
        hardnessSlider.value = defaultHardness;
    }

    /// <summary>
    ///     Update the Hardness Slider Value and TMP text with the current hardness
    /// </summary>
    /// <param name="hardness">Current Hardness of an ore</param>
    /// <param name="maxHardness">Default Hardness of an ore</param>
    public void UpdateHardnessSlider(float hardness, float maxHardness)
    {
        hardnessSlider.value = hardness;
        hardnessText.text = $"{hardness}/{maxHardness} ({hardness / maxHardness * 100}%)";
    }

    #region MainMenu

    [Header("Main Menu")] [SerializeField] private Button oreSelectButton;
    [SerializeField] private Button prestigeMenuButton;
    [SerializeField] private Button upgradeMenuButton;
    [SerializeField] private Button collectiblesMenuButton;
    [SerializeField] private Button premiumMenuButton;
    [SerializeField] private Button settingsMenuButton;
    [SerializeField] private Button closeButton;

    [SerializeField] private GameObject overrideCanvas;
    [SerializeField] private GameObject oreSelectionPanel;
    [SerializeField] private GameObject prestigeMenuPanel;
    [SerializeField] private GameObject upgradeMenu;
    [SerializeField] private GameObject collectiblesMenu;
    [SerializeField] private GameObject premiumMenu;
    [SerializeField] private GameObject settingsMenu;

    [SerializeField] private GameObject baseCanvas;
    [SerializeField] private GameObject overlayCanvas;

    /// <summary>
    ///     Add Functions to all buttons in the Main Menu
    /// </summary>
    private void AddMenuButtons()
    {
        upgradeMenuButton.onClick.AddListener(OpenUpgradeMenu);
        collectiblesMenuButton.onClick.AddListener(OpenCollectiblesMenu);
        premiumMenuButton.onClick.AddListener(OpenPremiumMenu);
        settingsMenuButton.onClick.AddListener(OpenSettingsMenu);
        oreSelectButton.onClick.AddListener(OpenOreMenu);
        closeButton.onClick.AddListener(CloseMenu);
        prestigeMenuButton.onClick.AddListener(OpenPrestigeMenu);
    }

    /// <summary>
    ///     Close Overlay Canvas
    /// </summary>
    public void CloseOverlay()
    {
        overlayCanvas.SetActive(false);
        overlayCanvas.GetComponentInChildren<Image>().color = new Color(0, 0, 0, 0);
    }

    /// <summary>
    ///     Open Overlay Canvas
    /// </summary>
    public void OpenOverlay()
    {
        overlayCanvas.SetActive(true);
        overlayCanvas.GetComponentInChildren<Image>().color = new Color(0, 0, 0, 0.4f);
    }

    /// <summary>
    ///     Close Button Function
    ///     Close Current UI and Open Base UI
    /// </summary>
    public void CloseMenu()
    {
        overrideCanvas.SetActive(false);
        baseCanvas.SetActive(true);
    }

    /// <summary>
    ///     Open Upgrade Menu Function
    ///     Open Upgrade Menu, Close Other Menu and Close Base UI
    /// </summary>
    public void OpenUpgradeMenu()
    {
        CheckCanvas();
        prestigeMenuPanel.SetActive(false);
        oreSelectionPanel.SetActive(false);
        upgradeMenu.SetActive(true);
        collectiblesMenu.SetActive(false);
        premiumMenu.SetActive(false);
        settingsMenu.SetActive(false);
        TapNormalUpgradePanel();
    }

    /// <summary>
    ///     Open Premium Upgrade Menu
    /// </summary>
    public void OpenPremiumUpgradeMenu()
    {
        CheckCanvas();
        prestigeMenuPanel.SetActive(false);
        oreSelectionPanel.SetActive(false);
        upgradeMenu.SetActive(true);
        collectiblesMenu.SetActive(false);
        premiumMenu.SetActive(false);
        settingsMenu.SetActive(false);
        TapPremiumUpgradePanel();
    }

    /// <summary>
    ///     Open Collectibles Menu Function
    ///     Open Collectibles Menu, Close Other Menu and Close Base UI
    /// </summary>
    public void OpenCollectiblesMenu()
    {
        CheckCanvas();
        prestigeMenuPanel.SetActive(false);
        oreSelectionPanel.SetActive(false);
        upgradeMenu.SetActive(false);
        collectiblesMenu.SetActive(true);
        premiumMenu.SetActive(false);
        settingsMenu.SetActive(false);
    }

    /// <summary>
    ///     Open Premium Menu Function
    ///     Open Premium Menu, Close Other Menu and Close Base UI
    /// </summary>
    public void OpenPremiumMenu()
    {
        CheckCanvas();
        prestigeMenuPanel.SetActive(false);
        oreSelectionPanel.SetActive(false);
        upgradeMenu.SetActive(false);
        collectiblesMenu.SetActive(false);
        premiumMenu.SetActive(true);
        settingsMenu.SetActive(false);
    }

    /// <summary>
    ///     Open Settings Menu Function
    ///     Open Settings Menu, Close Other Menu and Close Base UI
    /// </summary>
    public void OpenSettingsMenu()
    {
        CheckCanvas();
        prestigeMenuPanel.SetActive(false);
        oreSelectionPanel.SetActive(false);
        upgradeMenu.SetActive(false);
        collectiblesMenu.SetActive(false);
        premiumMenu.SetActive(false);
        settingsMenu.SetActive(true);
    }

    /// <summary>
    ///     Open Ore Menu Function
    ///     Open Ore Menu, Close Other Menu and Close Base UI
    /// </summary>
    public void OpenOreMenu()
    {
        CheckCanvas();
        prestigeMenuPanel.SetActive(false);
        upgradeMenu.SetActive(false);
        collectiblesMenu.SetActive(false);
        premiumMenu.SetActive(false);
        settingsMenu.SetActive(false);
        oreSelectionPanel.SetActive(true);
        _ore.tempSelectOreIndex = _ore.selectedOreIndex;
    }

    /// <summary>
    ///     Open Prestige Menu Function
    ///     Open Prestige Menu, Close Other Menu and Close Base UI
    /// </summary>
    public void OpenPrestigeMenu()
    {
        CheckCanvas();
        oreSelectionPanel.SetActive(false);
        upgradeMenu.SetActive(false);
        collectiblesMenu.SetActive(false);
        premiumMenu.SetActive(false);
        settingsMenu.SetActive(false);
        prestigeMenuPanel.SetActive(true);
    }

    /// <summary>
    ///     Check if the override canvas is already active or not
    /// </summary>
    private void CheckCanvas()
    {
        if (!overrideCanvas.activeSelf) overrideCanvas.SetActive(true);
        if (baseCanvas.activeSelf) baseCanvas.SetActive(false);
    }

    #region Getters and Setters

    /// <summary>
    ///     Get Base Canvas
    /// </summary>
    /// <returns>Returns Base Canvas</returns>
    public GameObject GetCanvas()
    {
        return baseCanvas;
    }

    #endregion

    #endregion

    #region OreSelection

    [Header("Ore Selection")] public Image oreImageHead;
    public Image oreImageBody;
    public TMP_Text oreName;
    public TMP_Text oreDescription;
    [SerializeField] public Button previousOreButton;
    [SerializeField] public Button nextOreButton;
    [SerializeField] public GameObject confirmOreButtonGo;
    private Button _confirmOreButton;
    private Image _confirmOreButtonImage;
    private TMP_Text _confirmOreButtonText;

    /// <summary>
    ///     Start Method Of the Ore Selection UI
    /// </summary>
    private void OreSelectionStart()
    {
        _confirmOreButtonText = confirmOreButtonGo.GetComponentInChildren<TMP_Text>();
        _confirmOreButtonImage = confirmOreButtonGo.GetComponent<Image>();
        _confirmOreButton = confirmOreButtonGo.GetComponent<Button>();
        _confirmOreButton.onClick.AddListener(SelectOre);
        nextOreButton.onClick.AddListener(PreviewNextOre);
        previousOreButton.onClick.AddListener(PreviewPreviousOre);
        UpdateOreDetails();
    }

    /// <summary>
    ///     update the ore details(In The Selector)
    /// </summary>
    private void UpdateOreDetails()
    {
        oreImageBody.sprite = _ore.oreDatabase.ores[_ore.tempSelectOreIndex].oreSprite;
        oreName.text = _ore.oreDatabase.ores[_ore.tempSelectOreIndex].oreName;
        oreDescription.text = _ore.oreDatabase.ores[_ore.tempSelectOreIndex].oreDescription;
        if (_ore.selectedOreIndex == _ore.tempSelectOreIndex)
        {
            //_confirmOreButtonImage.color = new Color(0.41f, 0.41f, 0.41f);
            _confirmOreButton.interactable = false;
            _confirmOreButtonText.text = "Selected";
        }
        else
        {
            //_confirmOreButtonImage.color = new Color(0.45f, 0.69f, 1f);
            _confirmOreButton.interactable = true;
            _confirmOreButtonText.text = "Select";
        }
    }

    /// <summary>
    ///     update Currently Selected Ore Image (Outside the Selector)
    /// </summary>
    private void UpdateOreImageHead()
    {
        oreImageHead.sprite = _ore.oreDatabase.ores[_ore.tempSelectOreIndex].oreSprite;
    }

    /// <summary>
    ///     Preview Previous Ore and Update the Ore Details
    /// </summary>
    private void PreviewPreviousOre()
    {
        _ore.ModifySelectedOreIndex(-1);
        UpdateOreDetails();
        UpdateOreNameText(_ore.GetOreStats().oreName);
    }

    /// <summary>
    ///     Preview Next Ore and Update the Ore Details
    /// </summary>
    private void PreviewNextOre()
    {
        _ore.ModifySelectedOreIndex(1);
        UpdateOreDetails();
        UpdateOreNameText(_ore.GetOreStats().oreName);
    }

    /// <summary>
    ///     Select the Ore and Update the Ore Details in the game
    /// </summary>
    private void SelectOre()
    {
        _ore.UpdateOre();
        UpdateOreDetails();
        UpdateOreImageHead();
        UpdateOreNameText(_ore.GetOreStats().oreName);
    }

    #endregion

    #region Overlay UI

    [Header("Overlay")]
    public TMP_Text overlayTitle;
    public TMP_Text overlayDescription;
    public Image overlayImage;
    public Button okButton;

    private void OverlayStart()
    {
        okButton.onClick.AddListener(CloseOverlay);
    }
    
    public void AssignOverlayValue(string textTitle, string textDescription, Sprite sprite)
    {
        overlayTitle.text = textTitle;
        overlayDescription.text = textDescription;
        overlayImage.sprite = sprite;
    }

    #endregion

    #region Prestige

    [Header("Prestige")] public TMP_Text prestigeText;

    #endregion

    #region Upgrades

    [Header("Upgrades Tab")] public ScrollRect upgradesScrollRect;
    public Button normalUpgradeButton;
    public Button premiumUpgradeButton;
    public GameObject normalUpgradePanel;
    public GameObject premiumUpgradePanel;
    private RectTransform _normalRectTransform;
    private RectTransform _premiumRectTransform;

    /// <summary>
    ///     This function needed to be called in start
    /// </summary>
    private void UpgradeStart()
    {
        _normalRectTransform = normalUpgradePanel.GetComponent<RectTransform>();
        _premiumRectTransform = premiumUpgradePanel.GetComponent<RectTransform>();
    }

    /// <summary>
    ///     Add Functions to all buttons in the Upgrades Menu
    /// </summary>
    private void AddUpgradeButtons()
    {
        normalUpgradeButton.onClick.AddListener(TapNormalUpgradePanel);
        premiumUpgradeButton.onClick.AddListener(TapPremiumUpgradePanel);
    }

    /// <summary>
    ///     Switch to the Normal Upgrade Panel
    /// </summary>
    public void TapNormalUpgradePanel()
    {
        normalUpgradePanel.SetActive(true);
        premiumUpgradePanel.SetActive(false);
        upgradesScrollRect.content = _normalRectTransform;
    }

    /// <summary>
    ///     Switch to the Premium Upgrade Panel
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

    [Header("Premium Tab")] public ScrollRect premiumScrollRect;

    #endregion

    #region Settings

    [Header("Settings Tab")] public ScrollRect settingsScrollRect;

    #endregion

    #region UpdateUI

    public enum DatabaseType
    {
        Collection,
        Item,
        Upgrades,
        PremiumUpgrade
    }

    [Header("Update UI")] [SerializeField] internal Database database;
    [SerializeField] internal GameObject upgradeList;
    [SerializeField] internal GameObject premiumUpgradeList;
    [SerializeField] internal GameObject upgradeUIPrefab;

    internal RectTransform ThisRectTransform;
    internal RectTransform ParentRectTransform;
    internal RectTransform PrefabRectTransform;
    internal VerticalLayoutGroup ThisVerticalLayoutGroup;

    [SerializeField] internal GameObject collectionList;
    [SerializeField] internal GameObject collectionUIPrefab;
    [SerializeField] internal GameObject itemUIPrefab;

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

        var uiManager = (UIManager) target;
        base.OnInspectorGUI();
        EditorGUILayout.Space();
        if (GUILayout.Button("Close Menu")) uiManager.CloseMenu();

        EditorGUILayout.Space();
        GUILayout.Label("Menu Quick Access", EditorStyles.boldLabel);
        GUILayout.BeginHorizontal(GUILayout.ExpandWidth(true));
        if (GUILayout.Button("UpgradeMenu")) uiManager.OpenUpgradeMenu();

        if (GUILayout.Button("CollectiblesMenu")) uiManager.OpenCollectiblesMenu();

        if (GUILayout.Button("PremiumMenu")) uiManager.OpenPremiumMenu();

        if (GUILayout.Button("SettingsMenu")) uiManager.OpenSettingsMenu();

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
            uiManager.OpenPremiumUpgradeMenu();
            UpdateUI(UIManager.DatabaseType.PremiumUpgrade);
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
                    uiManager.ParentRectTransform =
                        uiManager.upgradeList.transform.parent.GetComponent<RectTransform>();
                    uiManager.ThisRectTransform = uiManager.upgradeList.GetComponent<RectTransform>();
                    if (!active) uiManager.upgradeList.SetActive(true);
                }

                else if (databaseType == UIManager.DatabaseType.PremiumUpgrade)
                {
                    var active = uiManager.premiumUpgradeList.activeSelf;
                    uiManager.ThisVerticalLayoutGroup =
                        uiManager.premiumUpgradeList.GetComponent<VerticalLayoutGroup>();
                    uiManager.PrefabRectTransform = uiManager.upgradeUIPrefab.GetComponent<RectTransform>();
                    uiManager.ParentRectTransform =
                        uiManager.premiumUpgradeList.transform.parent.GetComponent<RectTransform>();
                    uiManager.ThisRectTransform = uiManager.premiumUpgradeList.GetComponent<RectTransform>();
                    if (!active) uiManager.premiumUpgradeList.SetActive(true);
                }

                else if (databaseType == UIManager.DatabaseType.Collection)
                {
                    var active = uiManager.collectionList.activeSelf;
                    uiManager.ThisVerticalLayoutGroup = uiManager.collectionList.GetComponent<VerticalLayoutGroup>();
                    uiManager.PrefabRectTransform = uiManager.collectionUIPrefab.GetComponent<RectTransform>();
                    uiManager.ParentRectTransform =
                        uiManager.collectionList.transform.parent.GetComponent<RectTransform>();
                    uiManager.ThisRectTransform = uiManager.collectionList.GetComponent<RectTransform>();
                    if (!active) uiManager.collectionList.SetActive(true);
                }
                else
                {
                    var active = uiManager.upgradeList.activeSelf;
                    if (!active) uiManager.upgradeList.SetActive(true);
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
                            listTransform.GetChild(i);
                        }
                        catch (UnityException)
                        {
                            var newUpgrade = Instantiate(uiManager.upgradeUIPrefab, listTransform);
                            newUpgrade.transform.SetParent(listTransform);
                        }

                        listTransform.GetChild(i).name = databaseStats[i].upgradeName;
                        listTransform.GetChild(i).Find("UpgradeTextArea/UpgradeName").GetComponent<TMP_Text>().text =
                            databaseStats[i].upgradeName;
                        listTransform.GetChild(i).Find("UpgradeTextArea/UpgradeDescription").GetComponent<TMP_Text>()
                            .text = databaseStats[i].upgradeDescription;
                        i++;
                    }

                    var childCount = listTransform.childCount;
                    var needDestroy = childCount - databaseStats.Length;

                    for (i = childCount; i > childCount - needDestroy; i--)
                        DestroyImmediate(listTransform.GetChild(i - 1).gameObject);
                    if (listTransform.childCount <= 5)
                    {
                        uiManager.ThisRectTransform.sizeDelta = new Vector2(uiManager.ThisRectTransform.sizeDelta.x,
                            uiManager.ParentRectTransform.rect.height);
                    }
                    else
                    {
                        var y = uiManager.ParentRectTransform.rect.height +
                                (listTransform.childCount - 5) *
                                (uiManager.PrefabRectTransform.rect.height +
                                 uiManager.ThisVerticalLayoutGroup.spacing);
                        uiManager.ThisRectTransform.sizeDelta = new Vector2(uiManager.ThisRectTransform.sizeDelta.x, y);
                    }
                }
                else if (databaseType == UIManager.DatabaseType.PremiumUpgrade)
                {
                    var databaseStats = uiManager.database.premiumUpgradeDatabase.stats;
                    var listTransform = uiManager.premiumUpgradeList.transform;
                    var i = 0;
                    foreach (var unused in databaseStats)
                    {
                        try
                        {
                            listTransform.GetChild(i);
                        }
                        catch (UnityException)
                        {
                            var newUpgrade = Instantiate(uiManager.upgradeUIPrefab, listTransform);
                            newUpgrade.transform.SetParent(listTransform);
                        }

                        listTransform.GetChild(i).name = databaseStats[i].upgradeName;
                        listTransform.GetChild(i).Find("UpgradeTextArea/UpgradeName").GetComponent<TMP_Text>().text =
                            databaseStats[i].upgradeName;
                        listTransform.GetChild(i).Find("UpgradeTextArea/UpgradeDescription").GetComponent<TMP_Text>()
                            .text = databaseStats[i].upgradeDescription;
                        i++;
                    }

                    var childCount = listTransform.childCount;
                    var needDestroy = childCount - databaseStats.Length;
                    for (i = childCount; i > childCount - needDestroy; i--)
                        DestroyImmediate(listTransform.GetChild(i - 1).gameObject);
                    if (listTransform.childCount <= 5)
                    {
                        uiManager.ThisRectTransform.sizeDelta = new Vector2(uiManager.ThisRectTransform.sizeDelta.x,
                            uiManager.ParentRectTransform.rect.height);
                    }
                    else
                    {
                        var y = uiManager.ParentRectTransform.rect.height +
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
                            listTransform.GetChild(i);
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
                        DestroyImmediate(listTransform.GetChild(i - 1).gameObject);
                    if (listTransform.childCount <= 2)
                    {
                        uiManager.ThisRectTransform.sizeDelta = new Vector2(uiManager.ThisRectTransform.sizeDelta.x,
                            uiManager.ParentRectTransform.rect.height);
                    }
                    else
                    {
                        var y = uiManager.ParentRectTransform.rect.height +
                                (listTransform.childCount - 2) *
                                (uiManager.PrefabRectTransform.rect.height +
                                 uiManager.ThisVerticalLayoutGroup.spacing);
                        uiManager.ThisRectTransform.sizeDelta = new Vector2(uiManager.ThisRectTransform.sizeDelta.x, y);
                    }

                    var j = 0;
                    foreach (var collection in databaseStats)
                    {
                        listTransform = uiManager.collectionList.transform.GetChild(j);
                        i = 0;
                        foreach (var unused in collection.items)
                        {
                            try
                            {
                                listTransform.GetChild(i);
                            }
                            catch (UnityException)
                            {
                                var gameObject = Instantiate(uiManager.itemUIPrefab, listTransform);
                                gameObject.transform.SetParent(listTransform);
                            }

                            listTransform.GetChild(i).name = databaseStats[j].items[i].itemName;
                            listTransform.GetChild(i).GetChild(0).GetComponent<Image>().sprite =
                                databaseStats[j].items[i].itemSprite;
                            listTransform.GetChild(i).GetChild(0).GetComponent<Image>().color =
                                new Color(0.22f, 0.22f, 0.22f);
                            i++;
                        }

                        childCount = listTransform.childCount;
                        needDestroy = childCount - databaseStats[j].items.Length;
                        for (i = childCount; i > childCount - needDestroy; i--)
                            DestroyImmediate(listTransform.GetChild(i - 1).gameObject);


                        var spacing = listTransform.GetComponent<GridLayoutGroup>();
                        uiManager.PrefabRectTransform = uiManager.itemUIPrefab.GetComponent<RectTransform>();
                        uiManager.ThisRectTransform = listTransform.GetComponent<RectTransform>();
                        var count = listTransform.childCount;
                        Debug.Log(Mathf.Ceil((float) count / 3));
                        var y = Mathf.Ceil((float) count / 3) *
                            (uiManager.PrefabRectTransform.rect.height + spacing.spacing.y) + spacing.padding.top;
                        uiManager.ThisRectTransform.sizeDelta = new Vector2(uiManager.ThisRectTransform.sizeDelta.x, y);
                        j++;
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