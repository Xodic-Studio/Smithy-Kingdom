using System.Collections;
using AnimationScript;
using GameDatabase;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Manager
{
    public class UIManager : Singleton<UIManager>
    {
        private GameManager _gameManager;
        private Ore _ore;
        private SoundManagerr _soundManager;
        private GachaSystem _gachaSystem;
        private GachaDrop _gachaDrop;
        private AchievementDatabase _achievementDatabase;

        [Header("BaseUI")] public TMP_Text oreNameText;
        public TMP_Text moneyText;
        public TMP_Text gemText;
        public Slider hardnessSlider;
        public TMP_Text hardnessText;

        private void Awake()
        {
            _gachaDrop = GachaDrop.Instance;
            _ore = Ore.Instance;
            _soundManager = SoundManagerr.Instance;
            _gameManager = GameManager.Instance;
            _gachaSystem = GachaSystem.Instance;
            _achievementDatabase = _gameManager.achievementDatabase;

        }

        private void Start()
        {
            UpgradeStart();
            PremiumStart();
            AddMenuButtons();
            OreSelectionStart();
            OverlayStart();
            CollectiblesStart();
            NotificationsStart();
        }

        /// <summary>
        ///     Update the Money TMP text with the current money
        /// </summary>
        public void UpdateMoneyText()
        {
            moneyText.text = $"{_gameManager.NumberToString((decimal)_gameManager.GetMoney())}";
        }

        /// <summary>
        ///     Update the Gem TMP text with the current gem
        /// </summary>
        public void UpdateGemText()
        {
            gemText.text = $"{_gameManager.NumberToString((decimal)_gameManager.GetGems())}";
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
        public void UpdateMaxHardnessSlider(float defaultHardness)
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
            hardnessText.text = $"{_gameManager.NumberToString((decimal)hardness)}/{_gameManager.NumberToString((decimal)maxHardness)} ({Mathf.Round(hardness / maxHardness * 100):F2}%)";
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
            collectiblesMenuButton.onClick.AddListener(OpenCollectionMenu);
            premiumMenuButton.onClick.AddListener(OpenPremiumMenu);
            settingsMenuButton.onClick.AddListener(OpenSettingsMenu);
            oreSelectButton.onClick.AddListener(OpenNormalOreMenu);
            closeButton.onClick.AddListener(CloseMenu);
            prestigeMenuButton.onClick.AddListener(OpenPrestigeMenu);
        }

        /// <summary>
        /// Close popup Canvas
        /// </summary>
        public void ClosePopup()
        {
            popup.SetActive(false);
            okButton.onClick.RemoveListener(_gameManager.MailReward);
            //_soundManager.PlayOneShot(_soundManager.soundDatabase.GetSfx(SoundDatabase.SfxType.ChangePage)[0]);
            _soundManager.PlaySound("ChangePage");
        }

        /// <summary>
        ///     Open Popup
        /// </summary>
        public void OpenPopup()
        {
            popup.SetActive(true);
            //_soundManager.PlayOneShot(_soundManager.soundDatabase.GetSfx(SoundDatabase.SfxType.OpenPopup)[0]);
            _soundManager.PlaySound("OpenPopup");
        }

        /// <summary>
        ///     Close Button Function
        ///     Close Current UI and Open Base UI
        /// </summary>
        public void CloseMenu()
        {
            overrideCanvas.SetActive(false);
            baseCanvas.SetActive(true);
            //_soundManager.PlayOneShot(_soundManager.soundDatabase.GetSfx(SoundDatabase.SfxType.ChangePage)[0]);
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
        public void OpenCollectionMenu()
        {
            CheckCanvas();
            CloseAllMenus();
            collectiblesMenu.SetActive(true);
            TapCollectionMenu();
            RemoveNotification(NotificationType.Collectible, collectibleNotificationCount);
            //_soundManager.PlayOneShot(_soundManager.soundDatabase.GetSfx(SoundDatabase.SfxType.ChangePage)[0]);
            _soundManager.PlaySound("ChangePage");


        }
 
        /// <summary>
        /// open an Achievement Menu
        /// </summary>
        public void OpenAchievementMenu()
        {
            CheckCanvas();
            CloseAllMenus();
            collectiblesMenu.SetActive(true);
            TapAchievementMenu();
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
            //_soundManager.PlayOneShot(_soundManager.soundDatabase.GetSfx(SoundDatabase.SfxType.ChangePage)[0]);
            _soundManager.PlaySound("ChangePage");

        }

        /// <summary>
        ///     Open Ore Menu Function
        ///     Open Ore Menu, Close Other Menu and Close Base UI
        /// </summary>
        public void OpenNormalOreMenu()
        {
            CheckCanvas();
            CloseAllMenus();
            oreSelectionPanel.SetActive(true);
            TapNormalOreMenu();
            _ore.tempSelectOreIndex = 0;
            UpdateOreDetails();
            _ore.DisableButtonIfNoNextOre();
            RemoveNotification(NotificationType.Ore, oreNotificationCount);
        }

        public void OpenPremiumOreMenu()
        {
            CheckCanvas();
            CloseAllMenus();
            oreSelectionPanel.SetActive(true);
            RemoveNotification(NotificationType.Ore, oreNotificationCount);
            TapPremiumOreMenu();
            _ore.tempSelectOreIndex = _ore.selectedOreIndex;
            UpdatePremiumOreDetails();
            _ore.DisableButtonIfNoNextPremiumOre();
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
            //_soundManager.PlayOneShot(_soundManager.soundDatabase.GetSfx(SoundDatabase.SfxType.ChangePage)[0]);
            _soundManager.PlaySound("ChangePage");

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
        private void CloseAllMenus()
        {
            prestigeMenuPanel.SetActive(false);
            oreSelectionPanel.SetActive(false);
            upgradeMenu.SetActive(false);
            collectiblesMenu.SetActive(false);
            premiumMenu.SetActive(false);
            settingsMenu.SetActive(false);
        
            normalUpgradePanel.SetActive(false);
            premiumUpgradePanel.SetActive(false);
            collectionMenuPanel.SetActive(false);
            achievementMenuPanel.SetActive(false);
            gachaMenuPanel.SetActive(false);
            packageMenuPanel.SetActive(false);
            normalOrePanel.SetActive(false);
            premiumOrePanel.SetActive(false);
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
        public Image premiumOreImageBody;
        public TMP_Text oreName;
        public TMP_Text premiumOreName;
        public TMP_Text oreDescription;
        public TMP_Text premiumOreDescription;
        [SerializeField] private Button normalOreButton;
        [SerializeField] private Button premiumOreButton;
        [SerializeField] private GameObject normalOrePanel;
        [SerializeField] private GameObject premiumOrePanel;
        [SerializeField] public Button previousOreButton;
        [SerializeField] public Button nextOreButton;
        [SerializeField] public Button previousPremiumOreButton;
        [SerializeField] public Button nextPremiumOreButton;
        [SerializeField] public GameObject confirmOreButtonGo;
        [SerializeField] public GameObject confirmPremiumOreButtonGo;
        public Button ConfirmOreButton { get; private set; }
        public Button ConfirmPremiumOreButton { get; private set; }
        public Image ConfirmOreButtonImage { get; private set; }
        public Image ConfirmPremiumOreButtonImage { get; private set; }
        private TMP_Text _confirmOreButtonText;
        private TMP_Text _confirmPremiumOreButtonText;
        
        /// <summary>
        ///     Start Method Of the Ore Selection UI
        /// </summary>
        private void OreSelectionStart()
        {
            normalOreButton.onClick.AddListener(TapNormalOreMenu);
            premiumOreButton.onClick.AddListener(TapPremiumOreMenu);
            _confirmOreButtonText = confirmOreButtonGo.GetComponentInChildren<TMP_Text>();
            _confirmPremiumOreButtonText = confirmPremiumOreButtonGo.GetComponentInChildren<TMP_Text>();
            ConfirmOreButtonImage = confirmOreButtonGo.GetComponent<Image>();
            ConfirmPremiumOreButtonImage = confirmPremiumOreButtonGo.GetComponent<Image>();
            ConfirmOreButton = confirmOreButtonGo.GetComponent<Button>();
            ConfirmPremiumOreButton = confirmPremiumOreButtonGo.GetComponent<Button>();
            ConfirmOreButton.onClick.AddListener(SelectOre);
            nextOreButton.onClick.AddListener(PreviewNextOre);
            previousOreButton.onClick.AddListener(PreviewPreviousOre);
            nextPremiumOreButton.onClick.AddListener(PreviewNextPremiumOre);
            previousPremiumOreButton.onClick.AddListener(PreviewPreviousPremiumOre);
            ConfirmPremiumOreButton.onClick.AddListener(SelectPremiumOre);
            UpdateOreDetails();
            UpdatePremiumOreDetails();
            premiumOreImageBody.color = Color.gray;
        }

        /// <summary>
        ///     update the ore details(In The Selector)
        /// </summary>
        public void UpdateOreDetails()
        {
            oreImageBody.sprite = _ore.oreDatabase.ores[_ore.tempSelectOreIndex].oreSprite;
            oreName.text = _ore.oreDatabase.ores[_ore.tempSelectOreIndex].oreName;
            oreDescription.text = _ore.oreDatabase.ores[_ore.tempSelectOreIndex].oreDescription;
            if (_ore.selectedOreIndex == _ore.tempSelectOreIndex && !_ore.isPremium)
            {
                ConfirmOreButton.interactable = false;
                _confirmOreButtonText.text = "Selected";
            }
            else
            {
                _confirmOreButtonText.text = "Select";
            }
        }
        
        public void UpdatePremiumOreDetails()
        {
            premiumOreImageBody.sprite = _ore.oreDatabase.premiumOres[_ore.tempSelectOreIndex].oreSprite;
            premiumOreName.text = _ore.oreDatabase.premiumOres[_ore.tempSelectOreIndex].oreName;
            premiumOreDescription.text = _ore.oreDatabase.premiumOres[_ore.tempSelectOreIndex].oreDescription;
            if (_ore.selectedOreIndex == _ore.tempSelectOreIndex && _ore.isPremium)
            {
                ConfirmPremiumOreButton.interactable = false;
                _confirmPremiumOreButtonText.text = "Selected";
            }
            else
            {
                _confirmPremiumOreButtonText.text = "Select";
            }
        }

        /// <summary>
        ///     update Currently Selected Ore Image (Outside the Selector)
        /// </summary>
        public void UpdateOreImageHead()
        {
            oreImageHead.sprite = _ore.oreDatabase.ores[_ore.tempSelectOreIndex].oreSprite;
        }
        
        public void UpdatePremiumOreImageHead()
        {
            oreImageHead.sprite = _ore.oreDatabase.premiumOres[_ore.tempSelectOreIndex].oreSprite;
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
        
        private void PreviewPreviousPremiumOre()
        {
            _ore.ModifyPremiumOreIndex(-1);
            UpdatePremiumOreDetails();
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
        
        private void PreviewNextPremiumOre()
        {
            _ore.ModifyPremiumOreIndex(1);
            UpdatePremiumOreDetails();
            UpdateOreNameText(_ore.GetOreStats().oreName);
        }

        /// <summary>
        ///     Select the Ore and Update the Ore Details in the game
        /// </summary>
        private void SelectOre()
        {
            _ore.UpdateCommonOre();
            UpdateOreDetails();
            UpdateOreImageHead();
            UpdateOreNameText(_ore.GetOreStats().oreName);
            //_soundManager.RandomSoundEffect(_soundManager.soundDatabase.GetSfx(SoundDatabase.SfxType.SelectOre));
            _soundManager.PlaySound("SelectOre");
        }
        
        private void SelectPremiumOre()
        {
            _ore.UpdatePremiumOre();
            UpdatePremiumOreDetails();
            UpdatePremiumOreImageHead();
            UpdateOreNameText(_ore.GetOreStats().oreName);
           // _soundManager.RandomSoundEffect(_soundManager.soundDatabase.GetSfx(SoundDatabase.SfxType.SelectOre));
           _soundManager.PlaySound("SelectOre");

        }
        
        private void TapNormalOreMenu()
        {
            normalOrePanel.SetActive(true);
            premiumOrePanel.SetActive(false);
            _ore.tempSelectOreIndex = 0;
            UpdateOreDetails();
            _ore.DisableButtonIfNoNextOre();
            //_soundManager.PlayOneShot(_soundManager.soundDatabase.GetSfx(SoundDatabase.SfxType.ChangePage)[0]);
            _soundManager.PlaySound("ChangePage");
            if(_confirmOreButtonText.text == "Select")
                ConfirmOreButton.interactable = true;

        } 
        
        private void TapPremiumOreMenu()
        {
            normalOrePanel.SetActive(false);
            premiumOrePanel.SetActive(true);
            _ore.tempSelectOreIndex = 0;
            UpdatePremiumOreDetails();
            _ore.DisableButtonIfNoNextPremiumOre();
            //_soundManager.PlayOneShot(_soundManager.soundDatabase.GetSfx(SoundDatabase.SfxType.ChangePage)[0]);
            _soundManager.PlaySound("ChangePage");

            _ore.FirstPremiumOreIndex();

        }

        #endregion

        #region Overlay UI

        [Header("Overlay")] 
        public GameObject popup;
        public TMP_Text popupTitle;
        public TMP_Text popupDescription;
        public Image popupImage;
        public Button okButton;
        public Sprite denySprite;

        private void OverlayStart()
        {
            okButton.onClick.AddListener(ClosePopup);
        }
    
        public void AssignPopupValue(string textTitle, string textDescription, Sprite sprite)
        {
            popupTitle.text = textTitle;
            popupDescription.text = textDescription;
            popupImage.sprite = sprite;
        }

        public void NotEnoughMoney()
        {
            AssignPopupValue("Not Enough Money", "You don't have enough money to buy this", denySprite);
            OpenPopup();
        }
        
        public void NotEnoughGems()
        {
            AssignPopupValue("Not Enough Gems", "You don't have enough gems to buy this", denySprite);
            OpenPopup();
        }

        #endregion

        #region Mail UI
        [Header("Mail")] 
        public GameObject mailPanel;
        public Image mailIcon;
        public Button mailButton;

        /// <summary>
        /// Add New Mail to Screen Notification
        /// </summary>
        /// <param name="mail"> Mail Class From MailDatabase</param>
        public void AddNewMail(Mail mail)
        {
            mailButton.onClick.RemoveAllListeners();
            Debug.Log("New Mail");
            mailPanel.SetActive(true);
            mailIcon.sprite = mail.icon;
            mailButton.onClick.AddListener(() => OpenMail(mail));
            //_soundManager.PlayOneShot(_soundManager.soundDatabase.GetSfx(SoundDatabase.SfxType.MailPopup)[0]);
            _soundManager.PlaySound("MailPopup");

            StartCoroutine(MailTimer());
        }

        /// <summary>
        /// Open Mail Assigned to the button
        /// </summary>
        /// /// <param name="mail"> Mail Class From MailDatabase</param>
        void OpenMail(Mail mail)
        {
            _gameManager.MailReward();
            _achievementDatabase.ModifyProgress("You’ve got Mail!",1);
            StopCoroutine(MailTimer());
            popupTitle.text = mail.title;
            popupDescription.text = mail.content + "\n\n" + $"You Received: {_gameManager.GetMailReward()} Coins";
            popupImage.sprite = mail.icon;
            popup.SetActive(true);
            mailPanel.SetActive(false);
            //_soundManager.PlayOneShot(_soundManager.soundDatabase.GetSfx(SoundDatabase.SfxType.MailRead)[0]);
            _soundManager.PlaySound("MailRead");

        }

        IEnumerator MailTimer()
        {
            yield return new WaitForSeconds(60f);
            mailPanel.SetActive(false);
        }

        [Header("Notifications")] 
        public GameObject oreNotificationGo;
        public GameObject collectiblesNotificationGo;
        public GameObject achievementNotificationGo;
        public GameObject mailNotificationGo;
        public TMP_Text oreNotificationText;
        public TMP_Text collectiblesNotificationText;
        public TMP_Text achievementNotificationText;
        public TMP_Text mailNotificationText;
        public int oreNotificationCount;
        public int collectibleNotificationCount;
        public int achievementNotificationCount;
        public int mailNotificationCount;

        private void NotificationsStart()
        {
            oreNotificationText = oreNotificationGo.GetComponentInChildren<TMP_Text>();
            collectiblesNotificationText = collectiblesNotificationGo.GetComponentInChildren<TMP_Text>();
            //achievementNotificationText = achievementNotificationGo.GetComponentInChildren<TMP_Text>();
            //mailNotificationText = mailNotificationGo.GetComponentInChildren<TMP_Text>();
            oreNotificationGo.SetActive(false);
            collectiblesNotificationGo.SetActive(false);
            //achievementNotificationGo.SetActive(false);
            //mailNotificationGo.SetActive(false);
        }
        
        
        /// <summary>
        /// Add a notification to count
        /// </summary>
        /// <param name="type">Type of notification</param>
        /// <param name="count">Numbers of notification</param>
        public void AddNotification(NotificationType type, int count)
        {
            switch (type)
            {
                case NotificationType.Ore:
                    oreNotificationCount += count;
                    oreNotificationText.text = oreNotificationCount.ToString();
                    oreNotificationGo.SetActive(true);
                    break;
                case NotificationType.Collectible:
                    collectibleNotificationCount += count;
                    collectiblesNotificationText.text = collectibleNotificationCount.ToString();
                    collectiblesNotificationGo.SetActive(true);
                    break;
                case NotificationType.Achievement:
                    achievementNotificationCount += count;
                    achievementNotificationText.text = achievementNotificationCount.ToString();
                    achievementNotificationGo.SetActive(true);               
                    break;
                case NotificationType.Mail:
                    mailNotificationCount += count;
                    mailNotificationText.text = mailNotificationCount.ToString();
                    mailNotificationGo.SetActive(true);                   
                    break;
            }
        }

        /// <summary>
        /// Remove Notification if clicked
        /// </summary>
        /// <param name="type"></param>
        /// <param name="count"></param>
        public void RemoveNotification(NotificationType type, int count)
        {
            switch (type)
            {
                case NotificationType.Ore:
                    oreNotificationCount -= count;
                    oreNotificationText.text = oreNotificationCount.ToString();
                    CheckNotificationCount();
                    break;
                case NotificationType.Collectible:
                    collectibleNotificationCount -= count;
                    collectiblesNotificationText.text = collectibleNotificationCount.ToString();
                    CheckNotificationCount();
                    break;
                case NotificationType.Achievement:
                    achievementNotificationCount -= count;
                    achievementNotificationText.text = achievementNotificationCount.ToString();
                    CheckNotificationCount();
                    break;
                case NotificationType.Mail:
                    mailNotificationCount -= count;
                    mailNotificationText.text = mailNotificationCount.ToString();
                    CheckNotificationCount();
                    break;
            }
        }
        
        public void CheckNotificationCount()
        {
            if (oreNotificationCount >= 0)
            {
                oreNotificationCount = 0;
                oreNotificationGo.SetActive(false);
            }
            if (collectibleNotificationCount >= 0)
            {
                collectiblesNotificationGo.SetActive(false);
            }
            // if (achievementNotificationCount >= 0)
            // {
            //     achievementNotificationGo.SetActive(false);
            // }
            // if (mailNotificationCount >= 0)
            // {
            //     mailNotificationGo.SetActive(false);
            // }
        }



        public enum NotificationType
        {
            Ore,
            Collectible,
            Achievement,
            Mail
        }



        #endregion

        #region Prestige

        [Header("Prestige")] public TMP_Text prestigeText;

        #endregion

        #region Upgrades

        [Header("Upgrades Tab")] public ScrollRect mainScrollRect;
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
            mainScrollRect.content = _normalRectTransform;
            //_soundManager.PlayOneShot(_soundManager.soundDatabase.GetSfx(SoundDatabase.SfxType.ChangePage)[0]);
            _soundManager.PlaySound("ChangePage");

        }

        /// <summary>
        ///     Switch to the Premium Upgrade Panel
        /// </summary>
        private void TapPremiumUpgradePanel()
        {
            normalUpgradePanel.SetActive(false);
            premiumUpgradePanel.SetActive(true);
            mainScrollRect.content = _premiumRectTransform;
            //_soundManager.PlayOneShot(_soundManager.soundDatabase.GetSfx(SoundDatabase.SfxType.ChangePage)[0]);
            _soundManager.PlaySound("ChangePage");

        }

        #endregion

        #region Collectibles

        [Header("Collectibles Tab")]
        public Button achievementButton;
        public Button collectibleButton;
        public GameObject achievementMenuPanel;
        public GameObject collectionMenuPanel;
    
        private RectTransform _collectiblesRectTransform;
        private RectTransform _achievementRectTransform;
    
        void CollectiblesStart()
        {
            _collectiblesRectTransform = collectionMenuPanel.GetComponent<RectTransform>();
            _achievementRectTransform = achievementMenuPanel.GetComponent<RectTransform>();
            AddCollectibleButtons();
        }
    
        void AddCollectibleButtons()
        {
            collectibleButton.onClick.AddListener(TapCollectionMenu);
            achievementButton.onClick.AddListener(TapAchievementMenu);
        }
    
    
        void TapAchievementMenu()
        {
            achievementMenuPanel.SetActive(true);
            collectionMenuPanel.SetActive(false);
            mainScrollRect.content = _achievementRectTransform;
           // _soundManager.PlayOneShot(_soundManager.soundDatabase.GetSfx(SoundDatabase.SfxType.ChangePage)[0]);
           _soundManager.PlaySound("ChangePage");

        }
    
        void TapCollectionMenu()
        {
            achievementMenuPanel.SetActive(false);
            collectionMenuPanel.SetActive(true);
            mainScrollRect.content = _collectiblesRectTransform;
            //_soundManager.PlayOneShot(_soundManager.soundDatabase.GetSfx(SoundDatabase.SfxType.ChangePage)[0]);
            _soundManager.PlaySound("ChangePage");

        }


        #endregion

        #region Premium

        [Header("Premium Tab")]
        public Button gachaMenuButton;
        public Button packageMenuButton;
        public Button gacha1Button;
        public Button gacha10Button;
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
            gacha10Button.onClick.AddListener(delegate
            {
                OpenGacha(10);
            });
        }
    
        /// <summary>
        /// open gacha menu
        /// </summary>
        public void OpenGachaMenu()
        {
            gachaMenuPanel.SetActive(true);
            packageMenuPanel.SetActive(false);
            //_soundManager.PlayOneShot(_soundManager.soundDatabase.GetSfx(SoundDatabase.SfxType.ChangePage)[0]);
            _soundManager.PlaySound("ChangePage");

        }

        public void OpenGacha(int amount)
        {
            if (_gameManager.HasGems(amount * 100))
            {
                for (int i = 0 ; i < amount ; i++)
                {
                    _gachaSystem.RandomGacha();
                }
                _gachaDrop.OpenResult();
                _gachaDrop.Roll(amount, _gachaSystem.resultSprites.ToArray());
                _gachaSystem.resultSprites.Clear();
                Debug.Log(_gachaDrop.gameObject.activeSelf);
            }
        }
    
        /// <summary>
        /// open package menu
        /// </summary>
        public void OpenPackageMenu()
        {
            gachaMenuPanel.SetActive(false);
            packageMenuPanel.SetActive(true);
            mainScrollRect.content = packageMenuPanel.GetComponent<RectTransform>();
           // _soundManager.PlayOneShot(_soundManager.soundDatabase.GetSfx(SoundDatabase.SfxType.ChangePage)[0]);
           _soundManager.PlaySound("ChangePage");

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
        public GameObject collectionList;
        public GameObject collectionUIPrefab;
        public GameObject itemIconUIPrefab;
    
        public GameObject achievementsList;
        #endregion
    }
}

