using System;
using UnityEngine;

[CreateAssetMenu(fileName = "NewDatabase", menuName = "Game/Database/Achievement")]
public class AchienvementDatabase : ScriptableObject
{
    public Achievement[] achievements;
}

[Serializable]
public class Achievement
{
    public string name;
    public string description;
    public Sprite icon;
    public bool isUnlocked;
    public int progress;
    public int requirement;
}
