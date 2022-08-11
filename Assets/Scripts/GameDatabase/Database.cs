using UnityEngine;

namespace GameDatabase
{
    [CreateAssetMenu (fileName = "Database", menuName = "Database")]
    public class Database : ScriptableObject
    {
        public UpgradeDatabase upgradeDatabase;
        public UpgradeDatabase premiumUpgradeDatabase;
        public ItemDatabase itemsDatabase;
        public AchievementDatabase achievementDatabase;
        public OreDatabase oresDatabase;
    
        public void ResetAllDatabase()
        {
            itemsDatabase.ResetDatabase();
            oresDatabase.ResetDatabase();
            achievementDatabase.ResetProgress();
        }
    }
}