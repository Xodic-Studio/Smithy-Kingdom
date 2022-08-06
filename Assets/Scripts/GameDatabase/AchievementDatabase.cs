using System;
using UnityEngine;

namespace GameDatabase
{
    [CreateAssetMenu(fileName = "NewDatabase", menuName = "Game/Database/Achievement")]
    public class AchievementDatabase : ScriptableObject
    {
        public Achievement[] achievements;
        
        public Achievement GetAchievement(string achievementName)
        {
            foreach (var achievement in achievements)
            {
                if (achievement.achievementName == achievementName)
                {
                    return achievement;
                }
            }
            
            return null;
        }

        private void CheckUnlocked(Achievement achievement)
        {
            if (achievement.isUnlocked) return;
            if (achievement.progress >= achievement.requirement)
            {
                AchievementManager.Instance.UnlockAchievement(achievement);
            }
        }


        public void ModifyProgress(string achievementName, float progress)
        {
            Debug.Log(achievementName + ": " + progress);
            var achievement = GetAchievement(achievementName);
            if (achievement == null) return;
            achievement.progress += progress;
            CheckUnlocked(achievement);
        }
        
        public void ModifyProgress(string achievementName, float progress, bool setProgress)
        {
            if (setProgress)
            {
                var achievement = GetAchievement(achievementName);
                if (achievement == null) return;
                achievement.progress = progress;
                CheckUnlocked(achievement);
            }
            else
            {
                ModifyProgress(achievementName, progress);
            }
        }

        private void OnValidate()
        {
            foreach (var achievement in achievements)
            {
                achievement.id = Array.IndexOf(achievements, achievement);
            }
        }
        
        public void ResetProgress()
        {
            foreach (var achievement in achievements)
            {
                achievement.progress = 0;
                achievement.isUnlocked = false;
            }
        }
    }

    [Serializable]
    public class Achievement
    {
        public string achievementName;
        public string description;
        public Sprite icon;
        public bool isUnlocked;
        public string dateAchieved;
        public float progress;
        public float requirement;
        public int id;
        
        public void Unlock()
        {
            isUnlocked = true;
        }
    }
}