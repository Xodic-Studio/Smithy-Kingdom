using System.IO;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

[Serializable]
public class Save
{
    public 
    
    
    
    
    
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
            CloseSave();
        }
        LoadGame();
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
        _saveFile.cats = CatStorageManager.Instance.catLists.ToArray();
        _saveFile.boughtSlot = CatStorageManager.Instance.boughtSlot;
        _saveFile.totalCat = CatStorageManager.Instance.totalCat;

        _saveFile.items = ItemManager.Instance.items;
        _saveFile.gold = ItemManager.Instance.Gold;
        _saveFile.diamond = ItemManager.Instance.Diamond;

        _saveFile.collects = BubbleManager.Instance.bubbles.ToArray();

        CloseSave();

        // print("Saved");
    }
    
    
    
    
    
}
