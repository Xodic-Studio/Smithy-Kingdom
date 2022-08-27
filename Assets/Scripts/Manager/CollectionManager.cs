using System;
using AnimationScript;
using GameDatabase;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

namespace Manager
{
    public class CollectionManager : Singleton<CollectionManager>
    {
        private GameManager _gameManager;
        private UIManager _uiManager;
        private SoundManagerr _soundManager;
        private Ore _ore;
        private EquipmentDrop _equipmentDrop;

        public AchievementDatabase achievementDatabase;
        public ItemDatabase itemDatabase;
        private ItemCollection _itemCollection;
        private ItemStats _itemStats;

        private float[] _cumulativeDropChances;

        public int itemCollectionIndex;
        public int itemStatsIndex;

        private bool once;

        private void Awake()
        {
            _equipmentDrop = EquipmentDrop.Instance;
            _uiManager = UIManager.Instance;
            _gameManager = GameManager.Instance;
            _soundManager = SoundManagerr.Instance;
            _ore = Ore.Instance;
            achievementDatabase = _gameManager.achievementDatabase;
        }

        private void Start()
        {
            UpdateItemSelection();
            CheckEveryCollection();
        }

        public void UpdateItemSelection()
        {
            try
            {
                _itemCollection = itemDatabase.collections[itemCollectionIndex];
                _itemStats = _itemCollection.items[itemStatsIndex];
            }
            catch (IndexOutOfRangeException)
            {
                Debug.LogWarning("Item Collection for This Ore Does Not Exist");
            }
        }
        
        public void UpdatePremiumItemSelection()
        {
            try
            {
                _itemCollection = itemDatabase.collections[itemCollectionIndex + _ore.oreDatabase.ores.Length];
                _itemStats = _itemCollection.items[itemStatsIndex];
            }
            catch (IndexOutOfRangeException)
            {
                Debug.LogWarning("Item Collection for This Ore Does Not Exist");
            }
        }

        //Method to get the cumulative drop rate
        private void CalculateCumDropRate()
        {
            if (_itemCollection.items.Length != 0)
            {
                Array.Resize(ref _cumulativeDropChances, _itemCollection.items.Length);
                var i = 0;
                float dropChance = 0;
                foreach (var item in _itemCollection.items)
                {
                    dropChance += item.dropChance;
                    _cumulativeDropChances[i] = dropChance;
                    i++;
                }
            }
            else
            {
                Debug.LogWarning("No items in the item collection");
            }
        }

        //Method to drop an item
        public void DropItem()
        {
            if (_itemCollection.items.Length != 0)
            {
                var randomNumber = Random.Range(0f, 100f);
                var i = 0;
                foreach (var variable in _cumulativeDropChances)
                {
                    if (randomNumber <= variable)
                    {
                        itemStatsIndex = i;
                        UpdateItemSelection();
                        CheckCollection();
                        CheckLegendary();
                        break;
                    }
                    i++;
                }
            }
            else
            {
                Debug.LogWarning("No items in the item collection");
            }
        }

        public void DropItem(bool isPremium)
        {
            if (isPremium)
            {
                if (_itemCollection.items.Length != 0)
                {
                    var randomNumber = Random.Range(0f, 100f);
                    var i = 0;
                    foreach (var variable in _cumulativeDropChances)
                    {
                        if (randomNumber <= variable)
                        {
                            itemStatsIndex = i;
                            UpdatePremiumItemSelection();
                            CheckPremiumCollection();
                            CheckLegendary();
                            break;
                        }
                        i++;
                    }
                }
            }
            else
            {
                DropItem();
            }
        }
        
        void CheckLegendary()
        {
            if (_itemStats.itemRarity == ItemStats.Rarity.Legendary)
            {
                //_soundManager.PlaySound(SoundManager.Sound.Legendary);
                achievementDatabase.ModifyProgress("Jackpot!", 1);
            }
        }

        //Method to add Item to collection otherwise Give Money to player
        private void CheckCollection()
        {
            _itemStats.timesForged++;
            var itemButton = _uiManager.collectionList.transform.GetChild(itemCollectionIndex).GetChild(itemStatsIndex)
                .GetComponent<Button>();
            var displayDescription = $"Selling Price: {_gameManager.NumberToString((decimal)_itemStats.itemPrice)}\n" +
                                     $"Rarity: {_itemStats.itemRarity}\n" +
                                     $"First Forged: {_itemStats.itemFirstForged}\n" +
                                     $"Times Forged: {_itemStats.timesForged}\n" +
                                     "\n" +
                                     $"{_itemStats.itemDescription}";
            var itemName = _itemStats.itemName;
            var itemSprite = _itemStats.itemSprite;
        
            if (!_itemStats.isUnlocked)
            {
                _uiManager.AddNotification(UIManager.NotificationType.Collectible,1);
                achievementDatabase.ModifyProgress("A New Beginning", 1);
                _itemStats.isUnlocked = true;
                _uiManager.collectionList.transform.GetChild(itemCollectionIndex).GetChild(itemStatsIndex).GetChild(0)
                    .GetComponent<Image>().color = Color.white;
                _itemStats.itemFirstForged = DateTime.Today.ToString("d");
                displayDescription = $"Selling Price: {_gameManager.NumberToString((decimal)_itemStats.itemPrice)}\n" +
                                     $"Rarity: {_itemStats.itemRarity}\n" +
                                     $"First Forged: {_itemStats.itemFirstForged}\n" +
                                     $"Times Forged: {_itemStats.timesForged}\n" +
                                     "\n" +
                                     $"{_itemStats.itemDescription}";
                _equipmentDrop.GetEquipmentResult(true, _itemStats.itemSprite);
                //_soundManager.PlayOneShot(_soundManager.soundDatabase.GetSfx(SoundDatabase.SfxType.GetNewItem)[0]);
                _soundManager.PlaySound("GetNewItem");
            }
            else
            {
                _equipmentDrop.GetEquipmentResult(false, _itemStats.itemSprite);
                //_soundManager.PlayOneShot(_soundManager.soundDatabase.GetSfx(SoundDatabase.SfxType.RecieveCoin)[0]);
                _soundManager.PlaySound("RecieveCoin");

            }
            itemButton.onClick.RemoveAllListeners();

            itemButton.onClick.AddListener(
                delegate
                {
                    _uiManager.AssignPopupValue(itemName, displayDescription, itemSprite);
                    _uiManager.OpenPopup();
                });
            _gameManager.ModifyMoney(_itemStats.itemPrice);
        }

        void CheckPremiumCollection()
        {
            _itemStats.timesForged++;
            var itemButton = _uiManager.collectionList.transform.GetChild(itemCollectionIndex + _ore.oreDatabase.ores.Length).GetChild(itemStatsIndex)
                .GetComponent<Button>();
            var displayDescription = $"Selling Price: {_gameManager.NumberToString((decimal)_itemStats.itemPrice)}\n" +
                                     $"Rarity: {_itemStats.itemRarity}\n" +
                                     $"First Forged: {_itemStats.itemFirstForged}\n" +
                                     $"Times Forged: {_itemStats.timesForged}\n" +
                                     "\n" +
                                     $"{_itemStats.itemDescription}";
            var itemName = _itemStats.itemName;
            var itemSprite = _itemStats.itemSprite;
        
            if (!_itemStats.isUnlocked)
            {
                _uiManager.AddNotification(UIManager.NotificationType.Collectible,1);
                achievementDatabase.ModifyProgress("A New Beginning", 1);
                _itemStats.isUnlocked = true;
                _uiManager.collectionList.transform.GetChild(itemCollectionIndex + _ore.oreDatabase.ores.Length).GetChild(itemStatsIndex).GetChild(0)
                    .GetComponent<Image>().color = Color.white;
                _itemStats.itemFirstForged = DateTime.Today.ToString("d");
                displayDescription = $"Selling Price: {_gameManager.NumberToString((decimal)_itemStats.itemPrice)}\n" +
                                     $"Rarity: {_itemStats.itemRarity}\n" +
                                     $"First Forged: {_itemStats.itemFirstForged}\n" +
                                     $"Times Forged: {_itemStats.timesForged}\n" +
                                     "\n" +
                                     $"{_itemStats.itemDescription}";
                _equipmentDrop.GetEquipmentResult(true, _itemStats.itemSprite);
                //_soundManager.PlayOneShot(_soundManager.soundDatabase.GetSfx(SoundDatabase.SfxType.GetNewItem)[0]);
                _soundManager.PlaySound("GetNewItem");
            }
            else
            {
                _equipmentDrop.GetEquipmentResult(false, _itemStats.itemSprite);
                //_soundManager.PlayOneShot(_soundManager.soundDatabase.GetSfx(SoundDatabase.SfxType.RecieveCoin)[0]);
                _soundManager.PlaySound("RecieveCoin");

            }
            itemButton.onClick.RemoveAllListeners();

            itemButton.onClick.AddListener(
                delegate
                {
                    _uiManager.AssignPopupValue(itemName, displayDescription, itemSprite);
                    _uiManager.OpenPopup();
                });
            _gameManager.ModifyMoney(_itemStats.itemPrice);
        }

        public void CheckEveryCollection()
        {
            var j = 0;
            foreach (var collection in itemDatabase.collections)
            {
                var i = 0;
                foreach (var items in collection.items)
                {
                    var itemButton = _uiManager.collectionList.transform.GetChild(j).GetChild(i).GetComponent<Button>();
                    var displayDescription = $"Selling Price: {_gameManager.NumberToString((decimal)items.itemPrice)}\n" +
                                             $"Rarity: {items.itemRarity}\n" +
                                             $"First Forged: {items.itemFirstForged}\n" +
                                             $"Times Forged: {items.timesForged}\n" +
                                             "\n" +
                                             $"{items.itemDescription}";
                    if (items.isUnlocked)
                    {
                        _uiManager.collectionList.transform.GetChild(j).GetChild(i).GetChild(0).GetComponent<Image>()
                            .color = Color.white;
                        itemButton.onClick.AddListener(delegate
                        {
                            _uiManager.AssignPopupValue(items.itemName, displayDescription, items.itemSprite);
                            _uiManager.OpenPopup();
                        });
                    }
                    else
                    {
                        _uiManager.collectionList.transform.GetChild(j).GetChild(i).GetChild(0).GetComponent<Image>()
                            .color = new Color(0.22f, 0.22f, 0.22f);
                        itemButton.onClick.RemoveAllListeners();
                    }
                    i++;
                }
                j++;
            }
        }

        public void UpdateRandomSystem()
        {
            itemCollectionIndex = _ore.selectedOreIndex;
            if (_itemCollection.items.Length != 0)
            {
                CalculateCumDropRate();
                UpdateItemSelection();
            }
            else
            {
                Debug.LogWarning("No items in the item collection");
            }
        }
    }
}