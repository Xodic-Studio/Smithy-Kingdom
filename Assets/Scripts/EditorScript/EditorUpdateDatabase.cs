#if UNITY_EDITOR
using Manager;
using TMPro;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

namespace EditorScript
{
    public class EditorUpdateDatabase : MonoBehaviour
    {
        public UIManager ui { get; private set; }

        private void OnValidate()
        {
            ui = GetComponent<UIManager>();
        }
    }

    [CustomEditor(typeof(EditorUpdateDatabase))]
    public class UpdateDatabase : Editor
    {
        public override void OnInspectorGUI()
        {
            var upt = (EditorUpdateDatabase) target;
            EditorGUILayout.Space();
            GUILayout.Label("Update Database", EditorStyles.boldLabel);
            if (GUILayout.Button("UpdateUpgrade",GUILayout.Width(250)))
            {
                upt.ui.OpenUpgradeMenu();
                UpdateUpgradeUI();
            }

            if (GUILayout.Button("UpdatePremiumUpgrade",GUILayout.Width(250)))
            {
                upt.ui.OpenPremiumUpgradeMenu();
                UpdatePremiumUpgradeUI();
            }

            if (GUILayout.Button("UpdateCollection",GUILayout.Width(250)))
            {
                upt.ui.OpenCollectionMenu();
                UpdateCollectionUI();
            }
            
            if (GUILayout.Button("UpdateAchievement",GUILayout.Width(250)))
            {
                upt.ui.OpenAchievementMenu();
                UpdateAchievementUI();
            }

            #region UpdateUI

            void UpdateUpgradeUI()
            {
                var active = upt.ui.upgradeList.activeSelf;
                if (!active) upt.ui.upgradeList.SetActive(true);

                var databaseStats = upt.ui.database.upgradeDatabase.stats;
                var listTransform = upt.ui.upgradeList.transform;
                var i = 0;
                foreach (var unused in databaseStats)
                {
                    try
                    {
                        listTransform.GetChild(i);
                    }
                    catch (UnityException)
                    {
                        var newUpgrade = Instantiate(upt.ui.upgradeUIPrefab, listTransform);
                        newUpgrade.transform.SetParent(listTransform);
                    }

                    listTransform.GetChild(i).name = databaseStats[i].upgradeName;
                    listTransform.GetChild(i).Find("UpgradeTextArea/UpgradeName").GetComponent<TMP_Text>().text =
                        databaseStats[i].upgradeName;
                    listTransform.GetChild(i).Find("UpgradeTextArea/UpgradeDescription").GetComponent<TMP_Text>()
                        .text = databaseStats[i].upgradeDescription;
                    i++;
                }

                var childCount = listTransform.childCount;
                var needDestroy = childCount - databaseStats.Length;

                for (i = childCount; i > childCount - needDestroy; i--)
                    DestroyImmediate(listTransform.GetChild(i - 1).gameObject);


            }

            //Update the Premium Upgrade UI
            void UpdatePremiumUpgradeUI()
            {
                var active = upt.ui.premiumUpgradeList.activeSelf;
                if (!active) upt.ui.premiumUpgradeList.SetActive(true);

                var databaseStats = upt.ui.database.premiumUpgradeDatabase.stats;
                var listTransform = upt.ui.premiumUpgradeList.transform;
                var i = 0;
                foreach (var unused in databaseStats)
                {
                    try
                    {
                        listTransform.GetChild(i);
                    }
                    catch (UnityException)
                    {
                        var newUpgrade = Instantiate(upt.ui.upgradeUIPrefab, listTransform);
                        newUpgrade.transform.SetParent(listTransform);
                    }

                    listTransform.GetChild(i).name = databaseStats[i].upgradeName;
                    listTransform.GetChild(i).Find("UpgradeTextArea/UpgradeName").GetComponent<TMP_Text>().text =
                        databaseStats[i].upgradeName;
                    listTransform.GetChild(i).Find("UpgradeTextArea/UpgradeDescription").GetComponent<TMP_Text>()
                        .text = databaseStats[i].upgradeDescription;
                    i++;
                }

                var childCount = listTransform.childCount;
                var needDestroy = childCount - databaseStats.Length;
                for (i = childCount; i > childCount - needDestroy; i--)
                    DestroyImmediate(listTransform.GetChild(i - 1).gameObject);
            }

            void UpdateCollectionUI()
            {
                var active = upt.ui.collectionList.activeSelf;
                if (!active) upt.ui.collectionList.SetActive(true);

                var databaseStats = upt.ui.database.itemsDatabase.collections;
                var listTransform = upt.ui.collectionList.transform;
                var i = 0;
                foreach (var unused in databaseStats)
                {
                    try
                    {
                        listTransform.GetChild(i);
                    }
                    catch (UnityException)
                    {
                        var newUpgrade = Instantiate(upt.ui.collectionUIPrefab, listTransform);
                        newUpgrade.transform.SetParent(listTransform);
                    }

                    listTransform.GetChild(i).name = databaseStats[i].collectionName;
                    listTransform.GetChild(i).GetComponent<TMP_Text>().text = databaseStats[i].collectionName;
                    i++;
                }

                var childCount = listTransform.childCount;
                var needDestroy = childCount - databaseStats.Length;
                for (i = childCount; i > childCount - needDestroy; i--)
                    DestroyImmediate(listTransform.GetChild(i - 1).gameObject);

                var j = 0;
                foreach (var collection in databaseStats)
                {
                    listTransform = upt.ui.collectionList.transform.GetChild(j);
                    i = 0;
                    foreach (var unused in collection.items)
                    {
                        try
                        {
                            listTransform.GetChild(i);
                        }
                        catch (UnityException)
                        {
                            var gameObject = Instantiate(upt.ui.itemIconUIPrefab, listTransform);
                            gameObject.transform.SetParent(listTransform);
                        }

                        listTransform.GetChild(i).name = databaseStats[j].items[i].itemName;
                        listTransform.GetChild(i).GetChild(0).GetComponent<Image>().sprite =
                            databaseStats[j].items[i].itemSprite;
                        listTransform.GetChild(i).GetChild(0).GetComponent<Image>().color =
                            new Color(0.22f, 0.22f, 0.22f);
                        i++;
                    }

                    childCount = listTransform.childCount;
                    needDestroy = childCount - databaseStats[j].items.Length;
                    for (i = childCount; i > childCount - needDestroy; i--)
                        DestroyImmediate(listTransform.GetChild(i - 1).gameObject);
                }
            }

            void UpdateAchievementUI()
            {
            var active = upt.ui.achievementsList.activeSelf;
            if (!active) upt.ui.achievementsList.SetActive(true);

                var databaseStats = upt.ui.database.achievementDatabase.achievements;
                var listTransform = upt.ui.achievementsList.transform;
                var i = 0;
                foreach (var unused in databaseStats)
                {
                    try
                    {
                        listTransform.GetChild(i);
                    }
                    catch (UnityException)
                    {
                        var newUpgrade = Instantiate(upt.ui.itemIconUIPrefab, listTransform);
                        newUpgrade.transform.SetParent(listTransform);
                    }

                    listTransform.GetChild(i).name = databaseStats[i].achievementName; 
                    listTransform.GetChild(i).GetComponentInChildren<Image>().sprite =
                        databaseStats[i].icon;
                    listTransform.GetChild(i).GetChild(0).GetComponentInChildren<Image>().color =
                        new Color(0.22f, 0.22f, 0.22f);
                    i++;
                }

                var childCount = listTransform.childCount;
                var needDestroy = childCount - databaseStats.Length;
                for (i = childCount; i > childCount - needDestroy; i--)
                    DestroyImmediate(listTransform.GetChild(i - 1).gameObject);
            }
            #endregion
        }
        
    }
}
#endif