using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class CollectionManager : Singleton<CollectionManager>
{
    private GameManager _gameManager;
    private Ore _ore;
    
    
    
    [SerializeField] ItemDatabase itemDatabase;
    ItemCollection _itemCollection;
    ItemStats _itemStats;
    
    private float[] _cumulativeDropChances;

    public int itemCollectionIndex;
    public int itemStatsIndex;

    private void Awake()
    {
        _gameManager = GameManager.Instance;
        _ore = Ore.Instance;
    }

    private void Start()
    {
        UpdateItemSelection();
    }

    private void UpdateItemSelection()
    {
        try
        {
            _itemCollection = itemDatabase.items[itemCollectionIndex];
            _itemStats = _itemCollection.items[itemStatsIndex];
        } catch (IndexOutOfRangeException)
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
            foreach(ItemStats item in _itemCollection.items) 
            {
                dropChance += item.dropChance;
                _cumulativeDropChances[i] = dropChance ;
                i++;
            }
        }

    }
    
    //Method to drop an item
    public void DropItem()
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
                break;
            }
            i++;
        }
    }
    
    //Method to add Item to collection otherwise Give Money to player
    private void CheckCollection()
    {
        if (!_itemStats.isUnlocked)
        {
            _itemStats.isUnlocked = true;
            Debug.Log($"{_itemStats.itemName} has been added to your collection");
        }
        _gameManager.ModifyMoney(_itemStats.itemPrice);
        Debug.Log($"{_itemStats.itemPrice} has been added to your wallet");
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

