using GameDatabase;
using Manager;
using UnityEngine;
using UnityEngine.UI;

public class AchievementManager : Singleton<AchievementManager>
{
    private UIManager _uiManager;
    
    
    public AchievementDatabase achievementDatabase;

    private void Awake()
    {
        _uiManager = UIManager.Instance;
    }

    private void Start()
    {
        CheckEveryAchievement();
    }

    /// <summary>
    /// Check If Achievement Is Unlocked at Game Start
    /// </summary>
    private void CheckEveryAchievement()
    {
        var i = 0;
        foreach (var achievement in achievementDatabase.achievements)
        {
            var achievementButton = _uiManager.achievementsList.transform.GetChild(i).GetComponent<Button>();
            var displayDescription = "\n" +
                                     $"{achievement.description}\n" + 
                                     "\n" +
                                     $"Achieved On: {achievement.dateAchieved}";
            if (achievement.isUnlocked)
            {
                    _uiManager.achievementsList.transform.GetChild(i).GetChild(0).GetComponent<Image>()
                        .color = Color.white;
                    achievementButton.onClick.AddListener(delegate
                    {
                        _uiManager.AssignPopupValue(achievement.achievementName, displayDescription, achievement.icon);
                        _uiManager.OpenPopup();
                    });
            }
            i++;
        }
    }
}
