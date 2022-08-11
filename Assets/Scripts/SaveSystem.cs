using System.IO;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using Manager;
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
        _saveFolder = Path.GetDirectoryName(Application.persistentDataPath) + "/Save"; // BUILD USE THIS!!!
        //_saveFolder = @"C:\Users\puree\Desktop\TempSave";
        _saveName = _saveFolder + "/SaveFile";
        if (!Directory.Exists(_saveFolder))
        {
            Directory.CreateDirectory(_saveFolder);
        }

        if (!File.Exists(_saveName))
        {
            _saveFile = new Save();
            CloseSave();
        }

        LoadGame();
    }

    private void Start()
    {
        Debug.Log(Application.dataPath);

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
        var j = 0;
        var i = 0;
        foreach (var VARIABLE in AchievementManager.Instance.achievementDatabase.achievements)
        {
            _saveFile.progressAchievements[i] = VARIABLE.progress;
            _saveFile.isUnlockedAchievements[i] = VARIABLE.isUnlocked;
            _saveFile.dateAchievements[i] = VARIABLE.dateAchieved;
            i++;
        }

        foreach (var collection in CollectionManager.Instance.itemDatabase.collections)
        {
            i = 0;
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
        print("Saved");
    }

    public void LoadGame()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Open(_saveName, FileMode.Open);
        _saveFile = (Save) bf.Deserialize(file);
        file.Close();

        bool save = false;

        GameManager.Instance.money = _saveFile.money;
        GameManager.Instance.gems = _saveFile.gems;
        GameManager.Instance.reputation = _saveFile.reputation;

        var i = 0;
        var j = 0;
        if (_saveFile.isUnlockedAchievements == null || _saveFile.progressAchievements == null ||
            _saveFile.dateAchievements == null)
        {
            print("SAVE: New Achievements");
            _saveFile.isUnlockedAchievements =
                new bool[AchievementManager.Instance.achievementDatabase.achievements.Length];
            _saveFile.progressAchievements =
                new float[AchievementManager.Instance.achievementDatabase.achievements.Length];
            _saveFile.dateAchievements =
                new string[AchievementManager.Instance.achievementDatabase.achievements.Length];
            foreach (var VARIABLE in _saveFile.isUnlockedAchievements)
            {
                _saveFile.isUnlockedAchievements[i] = false;
                _saveFile.progressAchievements[i] = 0;
                _saveFile.dateAchievements[i] = "";
                i++;
            }

            save = true;
        }

        i = 0;
        foreach (var VARIABLE in _saveFile.isUnlockedAchievements)
        {
            AchievementManager.Instance.achievementDatabase.achievements[i].isUnlocked = VARIABLE;
            AchievementManager.Instance.achievementDatabase.achievements[i].dateAchieved =
                _saveFile.dateAchievements[i];
            AchievementManager.Instance.achievementDatabase.achievements[i].progress =
                _saveFile.progressAchievements[i];
            i++;
        }

        i = 0;
        if (_saveFile.collectionSaves == null)
        {
            print("SAVE: New Collections");
            _saveFile.collectionSaves = new CollectionSave[CollectionManager.Instance.itemDatabase.collections.Length];
            foreach (var variable in _saveFile.collectionSaves)
            {

                _saveFile.collectionSaves[j] = new CollectionSave
                {
                    dateItem = new string[CollectionManager.Instance.itemDatabase.collections[j].items.Length],
                    timesForgedItem = new int[CollectionManager.Instance.itemDatabase.collections[j].items.Length],
                    isUnlockedItem = new bool[CollectionManager.Instance.itemDatabase.collections[j].items.Length]
                };
                i = 0;
                foreach (var VARIABLE in _saveFile.collectionSaves[j].isUnlockedItem)
                {
                    Debug.Log(_saveFile.collectionSaves[j].isUnlockedItem.Length);
                    Debug.Log(_saveFile.collectionSaves[j].isUnlockedItem[i]);
                    _saveFile.collectionSaves[j].isUnlockedItem[i] = false;
                    _saveFile.collectionSaves[j].timesForgedItem[i] = 0;
                    _saveFile.collectionSaves[j].dateItem[i] = "";
                    i++;
                }

                j++;
            }

            save = true;
        }

        j = 0;
        foreach (var variable in _saveFile.collectionSaves)
        {
            i = 0;
            Debug.Log("Collection: " + j);
            foreach (var item in variable.isUnlockedItem)
            {
                Debug.Log("Item: " + i);
                CollectionManager.Instance.itemDatabase.collections[j].items[i].isUnlocked =
                    _saveFile.collectionSaves[j].isUnlockedItem[i];
                CollectionManager.Instance.itemDatabase.collections[j].items[i].timesForged =
                    _saveFile.collectionSaves[j].timesForgedItem[i];
                CollectionManager.Instance.itemDatabase.collections[j].items[i].itemFirstForged =
                    _saveFile.collectionSaves[j].dateItem[i];
                i++;
            }

            j++;
        }

        i = 0;
        if (_saveFile.isUnlockedOre == null)
        {
            print("SAVE: New Ores");
            _saveFile.isUnlockedOre = new bool[Ore.Instance.oreDatabase.ores.Length];
            foreach (var VARIABLE in _saveFile.isUnlockedOre)
            {
                _saveFile.isUnlockedOre[i] = false;
                i++;
            }

            _saveFile.isUnlockedOre[0] = true;
            save = true;
        }

        i = 0;
        foreach (var VARIABLE in _saveFile.isUnlockedOre)
        {
            Ore.Instance.oreDatabase.ores[i].isUnlocked = VARIABLE;
            i++;
        }

        i = 0;
        if (_saveFile.isUnlockedPremiumOre == null || _saveFile.oreAmountPremiumOre == null)
        {
            print("SAVE: New Premium Ores");
            _saveFile.isUnlockedPremiumOre = new bool[Ore.Instance.oreDatabase.premiumOres.Length];
            _saveFile.oreAmountPremiumOre = new int[Ore.Instance.oreDatabase.premiumOres.Length];
            foreach (var VARIABLE in _saveFile.isUnlockedPremiumOre)
            {
                _saveFile.isUnlockedPremiumOre[i] = false;
                _saveFile.oreAmountPremiumOre[i] = 0;
                i++;
            }

            save = true;
        }

        i = 0;
        foreach (var VARIABLE in _saveFile.isUnlockedPremiumOre)
        {
            Ore.Instance.oreDatabase.premiumOres[i].isUnlocked = VARIABLE;
            Ore.Instance.oreDatabase.premiumOres[i].amount = _saveFile.oreAmountPremiumOre[i];
            i++;
        }

        if (save)
        {
            SaveGame();
        }
    }

    void OnApplicationPause(bool pauseStatus)
    {
        if (pauseStatus)
        {
            SaveGame();
        }
    }
}