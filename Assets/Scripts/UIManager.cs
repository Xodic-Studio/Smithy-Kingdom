using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : Singleton<UIManager>
{
    private GameManager _gameManager;
    private Ore _ore;
    private SoundManager _soundManager;
    private GachaSystem _gachaSystem;

    [Header("BaseUI")] public TMP_Text oreNameText;
    public TMP_Text moneyText;
    public TMP_Text gemText;
    public Slider hardnessSlider;
    public TMP_Text hardnessText;

    private void Awake()
    {
        _ore = Ore.Instance;
        _soundManager = SoundManager.Instance;
        _gameManager = GameManager.Instance;
        _gachaSystem = GachaSystem.Instance;
        
    }

    private void Start()
    {
        UpgradeStart();
        PremiumStart();
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
        #if !UNITY_EDITOR
        _soundManager.PlayOneShot(_soundManager.soundDatabase.GetSfx(SoundDatabase.SfxType.Back)[0]);
        #endif
    }

    /// <summary>
    ///     Open Upgrade Menu Function
    ///     Open Upgrade Menu, Close Other Menu and Close Base UI
    /// </summary>
    public void OpenUpgradeMenu()
    {
        CheckCanvas();
        CloseAllMenus();
        upgradeMenu.SetActive(true);
        #if !UNITY_EDITOR
        _soundManager.PlayOneShot(_soundManager.soundDatabase.GetSfx(SoundDatabase.SfxType.ChangePage)[0]);
        #endif
        TapNormalUpgradePanel();
    }

    /// <summary>
    ///     Open Premium Upgrade Menu
    /// </summary>
    public void OpenPremiumUpgradeMenu()
    {
        CheckCanvas();
        CloseAllMenus();
        upgradeMenu.SetActive(true);
        TapPremiumUpgradePanel();
    }

    /// <summary>
    ///     Open Collectibles Menu Function
    ///     Open Collectibles Menu, Close Other Menu and Close Base UI
    /// </summary>
    public void OpenCollectiblesMenu()
    {
        CheckCanvas();
        CloseAllMenus();
        collectiblesMenu.SetActive(true);
        #if !UNITY_EDITOR
        _soundManager.PlayOneShot(_soundManager.soundDatabase.GetSfx(SoundDatabase.SfxType.ChangePage)[0]);
        #endif
    }

    /// <summary>
    ///     Open Premium Menu Function
    ///     Open Premium Menu, Close Other Menu and Close Base UI
    /// </summary>
    public void OpenPremiumMenu()
    {
        CheckCanvas();
        CloseAllMenus();
        premiumMenu.SetActive(true);
        #if !UNITY_EDITOR
        _soundManager.PlayOneShot(_soundManager.soundDatabase.GetSfx(SoundDatabase.SfxType.ChangePage)[0]);
        #endif
        OpenGachaMenu();
    }

    /// <summary>
    ///     Open Settings Menu Function
    ///     Open Settings Menu, Close Other Menu and Close Base UI
    /// </summary>
    public void OpenSettingsMenu()
    {
        CheckCanvas();
        CloseAllMenus();
        settingsMenu.SetActive(true);
        #if !UNITY_EDITOR
        _soundManager.PlayOneShot(_soundManager.soundDatabase.GetSfx(SoundDatabase.SfxType.ChangePage)[0]);
        #endif
    }

    /// <summary>
    ///     Open Ore Menu Function
    ///     Open Ore Menu, Close Other Menu and Close Base UI
    /// </summary>
    public void OpenOreMenu()
    {
        CheckCanvas();
        CloseAllMenus();
        oreSelectionPanel.SetActive(true);
        #if !UNITY_EDITOR
        _ore.tempSelectOreIndex = _ore.selectedOreIndex;
        _soundManager.PlayOneShot(_soundManager.soundDatabase.GetSfx(SoundDatabase.SfxType.ChangePage)[0]);
        #endif
    }

    /// <summary>
    ///     Open Prestige Menu Function
    ///     Open Prestige Menu, Close Other Menu and Close Base UI
    /// </summary>
    public void OpenPrestigeMenu()
    {
        CheckCanvas();
        CloseAllMenus();
        prestigeMenuPanel.SetActive(true);
        #if !UNITY_EDITOR
        _soundManager.PlayOneShot(_soundManager.soundDatabase.GetSfx(SoundDatabase.SfxType.ChangePage)[0]);
        #endif
    }

    /// <summary>
    ///     Check if the override canvas is already active or not
    /// </summary>
    private void CheckCanvas()
    {
        if (!overrideCanvas.activeSelf) overrideCanvas.SetActive(true);
        if (baseCanvas.activeSelf) baseCanvas.SetActive(false);
    }
    /// <summary>
    /// Close All Menus
    /// </summary>
    public void CloseAllMenus()
    {
        prestigeMenuPanel.SetActive(false);
        oreSelectionPanel.SetActive(false);
        upgradeMenu.SetActive(false);
        collectiblesMenu.SetActive(false);
        premiumMenu.SetActive(false);
        settingsMenu.SetActive(false);
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
    public void UpdateOreDetails()
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
    public void UpdateOreImageHead()
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
        _soundManager.RandomSoundEffect(_soundManager.soundDatabase.GetSfx(SoundDatabase.SfxType.SelectOre));
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
        AddUpgradeButtons();
        AssignEventToUpgradeButton(database.upgradeDatabase);
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
    private void TapNormalUpgradePanel()
    {
        normalUpgradePanel.SetActive(true);
        premiumUpgradePanel.SetActive(false);
        upgradesScrollRect.content = _normalRectTransform;
    }

    /// <summary>
    ///     Switch to the Premium Upgrade Panel
    /// </summary>
    private void TapPremiumUpgradePanel()
    {
        normalUpgradePanel.SetActive(false);
        premiumUpgradePanel.SetActive(true);
        upgradesScrollRect.content = _premiumRectTransform;
    }
    
    /// <summary>
    /// 
    /// </summary>
    /// <param name="button"></param>
    /// <param name="upgrade"></param>
    public void AssignEventToUpgradeButton(UpgradeDatabase upgrade)
    {
        var i = 0;
        foreach (var variable in upgrade.stats)
        {
            var button = upgradeList.transform.GetChild(i).GetComponentInChildren<Button>();
            button.onClick.AddListener(delegate
            {
                variable.upgradeEvent.Invoke();
                _soundManager.PlayOneShot(_soundManager.soundDatabase.GetSfx(SoundDatabase.SfxType.Upgrade)[0]);
            });
            i++;
        }
    }

    #endregion

    #region Collectibles

    [Header("Collectibles Tab")]
    public ScrollRect collectiblesScrollRect;


    #endregion

    #region Premium

    [Header("Premium Tab")]
    public Button gachaMenuButton;
    public Button packageMenuButton;
    public Button gacha1Button;
    public Button gacha11Button;
    public GameObject gachaMenuPanel;
    public GameObject packageMenuPanel;
    
    /// <summary>
    /// Collectibles Start Method
    /// </summary>
    private void PremiumStart()
    {
        gachaMenuButton.onClick.AddListener(OpenGachaMenu);
        packageMenuButton.onClick.AddListener(OpenPackageMenu);
        gacha1Button.onClick.AddListener(delegate
        {
            OpenGacha(1);
        });
        gacha11Button.onClick.AddListener(delegate
        {
            OpenGacha(11);
        });
    }
    
    /// <summary>
    /// open gacha menu
    /// </summary>
    public void OpenGachaMenu()
    {
        gachaMenuPanel.SetActive(true);
        packageMenuPanel.SetActive(false);
    }

    public void OpenGacha(int amount)
    {
        if (_gameManager.GetGems() >= amount * 100)
        {
            for (int i = 0 ; i < amount ; i++)
            {
                _gachaSystem.RandomGacha();
                _gameManager.ModifyGems(-100);
            }
        }
        else
        {
            Debug.Log("Not Enough Gems");
        }
    }
    
    /// <summary>
    /// open package menu
    /// </summary>
    public void OpenPackageMenu()
    {
        gachaMenuPanel.SetActive(false);
        packageMenuPanel.SetActive(true);
    }
    
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

    [Header("Update UI Data")] 
    public Database database;
    public GameObject upgradeList;
    public GameObject premiumUpgradeList;
    public GameObject upgradeUIPrefab;

    public RectTransform thisRectTransform;
    public RectTransform parentRectTransform;
    public RectTransform prefabRectTransform;
    public VerticalLayoutGroup thisVerticalLayoutGroup;

    public GameObject collectionList;
    public GameObject collectionUIPrefab;
    public GameObject itemUIPrefab;
    
    public GameObject achievementsList;
    #endregion
}

