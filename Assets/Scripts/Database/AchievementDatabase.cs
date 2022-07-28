using System;
using UnityEngine;

[CreateAssetMenu(fileName = "NewDatabase", menuName = "Game/Database/Achievement")]
public class AchievementDatabase : ScriptableObject
{
    public Achievement[] achievements;
}

[Serializable]
public class Achievement
{
    public string achievementName;
    public string description;
    public Sprite icon;
    public bool isUnlocked;
    public string dateAchieved;
    public int progress;
    public int requirement;
}
