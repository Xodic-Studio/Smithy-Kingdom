using System.IO;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using GameDatabase;
using Manager;
using Unity.VisualScripting;
using UnityEngine;

[Serializable]
public class Save
{
    //Achievements
    public bool[] isUnlockedAchievements;
    public float[] progressAchievements;
    public string[] dateAchievements;
    
    //Item
    public CollectionSave[] collectionSaves;
    
    //orestats
    public bool[] isUnlockedOre;
    
    public bool[] isUnlockedPremiumOre;
    public int[] oreAmountPremiumOre;
    public int money, gems, reputation;
}
[Serializable]
public class CollectionSave
{
    public bool[] isUnlockedItem;
    public int[] timesForgedItem;
    public string[] dateItem;
}



public class SaveSystem : Singleton<SaveSystem>
{
    private string _saveFolder, _saveName;
    private Save _saveFile;
    private void Awake()
    {
        // saveFolder = Path.GetDirectoryName(Application.dataPath) + "/Save"; // BUILD USE THIS!!!
        _saveFolder = @"C:\Users\puree\Desktop\TempSave";
        _saveName = _saveFolder + "/SaveFile";
        if (!Directory.Exists(_saveFolder))
        {
            Directory.CreateDirectory(_saveFolder);
        }
        if (!File.Exists(_saveName))
        {
            _saveFile = new Save();
            Debug.Log("1");
            CloseSave();
            Debug.Log("2");
        }
        LoadGame();
        Debug.Log("3");
    }
    
    private void CloseSave()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = new FileStream(_saveName, FileMode.Create);
        bf.Serialize(file, _saveFile);
        file.Close();
    }
    
    public void SaveGame()
    {
        _saveFile = new Save();

        var j = 0;
        var i = 0;
        foreach (var VARIABLE in AchievementManager.Instance.achievementDatabase.achievements)
        {
            _saveFile.progressAchievements[i] = VARIABLE.progress;
            _saveFile.isUnlockedAchievements[i] = VARIABLE.isUnlocked;
            _saveFile.dateAchievements[i] = VARIABLE.dateAchieved;
            i++;
        }
        
        i = 0;
        foreach (var collection in CollectionManager.Instance.itemDatabase.collections)
        {
            foreach (var item in collection.items)
            {
                _saveFile.collectionSaves[j].isUnlockedItem[i] = item.isUnlocked;
                _saveFile.collectionSaves[j].timesForgedItem[i] = item.timesForged;
                _saveFile.collectionSaves[j].dateItem[i] = item.itemFirstForged;
                i++;
            }
            j++;
        }
        
        i = 0;
        foreach (var ore in Ore.Instance.oreDatabase.ores)
        {
            _saveFile.isUnlockedOre[i] = ore.isUnlocked;
            i++;
        }
        
        i = 0;
        foreach (var ore in Ore.Instance.oreDatabase.premiumOres)
        {
            _saveFile.isUnlockedPremiumOre[i] = ore.isUnlocked;
            _saveFile.oreAmountPremiumOre[i] = ore.amount;
            i++;
        }
        
        _saveFile.money = (int) GameManager.Instance.GetMoney();
        _saveFile.gems = (int) GameManager.Instance.GetGems();
        _saveFile.reputation = GameManager.Instance.GetReputation();
        CloseSave();
        // print("Saved");
    }
    
    public void LoadGame()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Open(_saveName, FileMode.Open);
        _saveFile = (Save)bf.Deserialize(file);
        file.Close();

        bool _save = false;
        
        GameManager.Instance.money = _saveFile.money;
        GameManager.Instance.gems = _saveFile.gems;
        GameManager.Instance.reputation = _saveFile.reputation;

        var i = 0;
        var j = 0;
        if(_saveFile.isUnlockedAchievements == null || _saveFile.progressAchievements == null || _saveFile.dateAchievements == null)
        {
            print("SAVE: New Achievements");
            _saveFile.isUnlockedAchievements = new bool[AchievementManager.Instance.achievementDatabase.achievements.Length];
            _saveFile.progressAchievements = new float[AchievementManager.Instance.achievementDatabase.achievements.Length];
            _saveFile.dateAchievements = new string[AchievementManager.Instance.achievementDatabase.achievements.Length];
            _save = true;
        }
        foreach (var VARIABLE in _saveFile.isUnlockedAchievements)
        {
            AchievementManager.Instance.achievementDatabase.achievements[i].isUnlocked = VARIABLE;
            AchievementManager.Instance.achievementDatabase.achievements[i].dateAchieved = _saveFile.dateAchievements[i];
            AchievementManager.Instance.achievementDatabase.achievements[i].progress = _saveFile.progressAchievements[i];
            i++;
        }
        
        i = 0;
        if(_saveFile.collectionSaves == null)
        {
            print("SAVE: New Collections");
            _saveFile.collectionSaves = new CollectionSave[CollectionManager.Instance.itemDatabase.collections.Length];
            Debug.Log(_saveFile.collectionSaves.Length);
            foreach (var VARIABLE in _saveFile.collectionSaves)
            {
                VARIABLE.dateItem = new string[CollectionManager.Instance.itemDatabase.collections[i].items.Length];
                VARIABLE.timesForgedItem = new int[CollectionManager.Instance.itemDatabase.collections[i].items.Length];
                VARIABLE.isUnlockedItem = new bool[CollectionManager.Instance.itemDatabase.collections[i].items.Length];
                i++;
            }
            _save = true;
        }
        i = 0;
        foreach (var variable in _saveFile.collectionSaves)
        {
            foreach (var item in variable.isUnlockedItem)
            {
                CollectionManager.Instance.itemDatabase.collections[j].items[i].isUnlocked = _saveFile.collectionSaves[j].isUnlockedItem[i];
                CollectionManager.Instance.itemDatabase.collections[j].items[i].timesForged = _saveFile.collectionSaves[j].timesForgedItem[i];
                CollectionManager.Instance.itemDatabase.collections[j].items[i].itemFirstForged = _saveFile.collectionSaves[j].dateItem[i];
                i++;
            }
            j++;
        }
        
        i = 0;
        if( _saveFile.isUnlockedOre == null)
        {
            print("SAVE: New Ores");
            _saveFile.isUnlockedOre = new bool[Ore.Instance.oreDatabase.ores.Length];
            _save = true;
        }
        foreach (var VARIABLE in _saveFile.isUnlockedOre)
        {
            Ore.Instance.oreDatabase.ores[i].isUnlocked = VARIABLE;
            i++;
        }
        
        i = 0;
        if(_saveFile.isUnlockedPremiumOre == null || _saveFile.oreAmountPremiumOre == null)
        {
            print("SAVE: New Premium Ores");
            _saveFile.isUnlockedPremiumOre = new bool[Ore.Instance.oreDatabase.premiumOres.Length];
            _saveFile.oreAmountPremiumOre = new int[Ore.Instance.oreDatabase.premiumOres.Length];
            _save = true;
        }
        foreach (var VARIABLE in _saveFile.isUnlockedPremiumOre)
        {
            Ore.Instance.oreDatabase.premiumOres[i].isUnlocked = VARIABLE;
            Ore.Instance.oreDatabase.premiumOres[i].amount = _saveFile.oreAmountPremiumOre[i];
            i++;
        }
        
        if(_save)
        {
            SaveGame();
        }
        print("Loaded");
    }

    private void OnApplicationQuit()
    { 
        SaveGame();
    }
}
