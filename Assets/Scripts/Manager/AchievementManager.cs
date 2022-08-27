using System;
using GameDatabase;
using UnityEngine;
using UnityEngine.UI;

namespace Manager
{
    public class AchievementManager : Singleton<AchievementManager>
    {
        private UIManager _uiManager;
        private SoundManagerr _soundManager;
    
    
        public AchievementDatabase achievementDatabase;

        private void Awake()
        {
            _soundManager = SoundManagerr.Instance;
            _uiManager = UIManager.Instance;
        }

        private void Start()
        {
            CheckEveryAchievement();
        }

        /// <summary>
        /// Check If Achievement Is Unlocked at Game Start
        /// </summary>
        public void CheckEveryAchievement()
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
                else
                {
               
                    _uiManager.achievementsList.transform.GetChild(i).GetChild(0).GetComponent<Image>()
                        .color = new Color(0.22f, 0.22f, 0.22f);
                    achievementButton.onClick.RemoveAllListeners();
                    achievementButton.onClick.AddListener(delegate
                    {
                        _uiManager.AssignPopupValue("???", "Hint: " + achievement.hint, achievement.icon);
                        _uiManager.OpenPopup();
                    });
                }
                i++;
            }
        }
    
        public void UnlockAchievement(Achievement achievement)
        {
            achievement.isUnlocked = true;
            achievement.dateAchieved = DateTime.Now.ToString();
            _uiManager.achievementsList.transform.GetChild(achievement.id).GetChild(0).GetComponent<Image>()
                .color = Color.white;
            _uiManager.achievementsList.transform.GetChild(achievement.id).GetComponent<Button>().onClick.RemoveAllListeners();
            _uiManager.achievementsList.transform.GetChild(achievement.id).GetComponent<Button>().onClick.AddListener(
                delegate
                {
                    _uiManager.AssignPopupValue(achievement.achievementName, achievement.description, achievement.icon);
                    _uiManager.OpenPopup();
                });
            //_soundManager.PlayOneShot(_soundManager.soundDatabase.GetSfx(SoundDatabase.SfxType.AchievementUnlock)[0]);
            _soundManager.PlaySound("_soundManager");
        }
    }
}
