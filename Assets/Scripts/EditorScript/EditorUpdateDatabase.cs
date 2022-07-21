#if UNITY_EDITOR
using TMPro;
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
                upt.ui.OpenCollectiblesMenu();
                UpdateCollectionUI();
            }

            #region UpdateUI

            void UpdateUpgradeUI()
            {
                var active = upt.ui.upgradeList.activeSelf;
                upt.ui.thisVerticalLayoutGroup = upt.ui.upgradeList.GetComponent<VerticalLayoutGroup>();
                upt.ui.prefabRectTransform = upt.ui.upgradeUIPrefab.GetComponent<RectTransform>();
                upt.ui.parentRectTransform = upt.ui.upgradeList.transform.parent.GetComponent<RectTransform>();
                upt.ui.thisRectTransform = upt.ui.upgradeList.GetComponent<RectTransform>();
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
                if (listTransform.childCount <= 5)
                {
                    upt.ui.thisRectTransform.sizeDelta = new Vector2(upt.ui.thisRectTransform.sizeDelta.x,
                        upt.ui.parentRectTransform.rect.height);
                }
                else
                {
                    var y = upt.ui.parentRectTransform.rect.height +
                            (listTransform.childCount - 5) *
                            (upt.ui.prefabRectTransform.rect.height +
                             upt.ui.thisVerticalLayoutGroup.spacing);
                    upt.ui.thisRectTransform.sizeDelta = new Vector2(upt.ui.thisRectTransform.sizeDelta.x, y);
                }
            }

            //Update the Premium Upgrade UI
            void UpdatePremiumUpgradeUI()
            {
                var active = upt.ui.premiumUpgradeList.activeSelf;
                upt.ui.thisVerticalLayoutGroup =
                    upt.ui.premiumUpgradeList.GetComponent<VerticalLayoutGroup>();
                upt.ui.prefabRectTransform = upt.ui.upgradeUIPrefab.GetComponent<RectTransform>();
                upt.ui.parentRectTransform =
                    upt.ui.premiumUpgradeList.transform.parent.GetComponent<RectTransform>();
                upt.ui.thisRectTransform = upt.ui.premiumUpgradeList.GetComponent<RectTransform>();
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
                if (listTransform.childCount <= 5)
                {
                    upt.ui.thisRectTransform.sizeDelta = new Vector2(upt.ui.thisRectTransform.sizeDelta.x,
                        upt.ui.parentRectTransform.rect.height);
                }
                else
                {
                    var y = upt.ui.parentRectTransform.rect.height +
                            (listTransform.childCount - 5) *
                            (upt.ui.prefabRectTransform.rect.height +
                             upt.ui.thisVerticalLayoutGroup.spacing);
                    upt.ui.thisRectTransform.sizeDelta = new Vector2(upt.ui.thisRectTransform.sizeDelta.x, y);
                }
            }

            void UpdateCollectionUI()
            {
                var active = upt.ui.collectionList.activeSelf;
                upt.ui.thisVerticalLayoutGroup = upt.ui.collectionList.GetComponent<VerticalLayoutGroup>();
                upt.ui.prefabRectTransform = upt.ui.collectionUIPrefab.GetComponent<RectTransform>();
                upt.ui.parentRectTransform =
                    upt.ui.collectionList.transform.parent.GetComponent<RectTransform>();
                upt.ui.thisRectTransform = upt.ui.collectionList.GetComponent<RectTransform>();
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
                if (listTransform.childCount <= 2)
                {
                    upt.ui.thisRectTransform.sizeDelta = new Vector2(upt.ui.thisRectTransform.sizeDelta.x,
                        upt.ui.parentRectTransform.rect.height);
                }
                else
                {
                    var y = upt.ui.parentRectTransform.rect.height +
                            (listTransform.childCount - 2) *
                            (upt.ui.prefabRectTransform.rect.height +
                             upt.ui.thisVerticalLayoutGroup.spacing);
                    upt.ui.thisRectTransform.sizeDelta = new Vector2(upt.ui.thisRectTransform.sizeDelta.x, y);
                }

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
                            var gameObject = Instantiate(upt.ui.itemUIPrefab, listTransform);
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


                    var spacing = listTransform.GetComponent<GridLayoutGroup>();
                    upt.ui.prefabRectTransform = upt.ui.itemUIPrefab.GetComponent<RectTransform>();
                    upt.ui.thisRectTransform = listTransform.GetComponent<RectTransform>();
                    var count = listTransform.childCount;
                    Debug.Log(Mathf.Ceil((float) count / 3));
                    var y = Mathf.Ceil((float) count / 3) *
                        (upt.ui.prefabRectTransform.rect.height + spacing.spacing.y) + spacing.padding.top;
                    upt.ui.thisRectTransform.sizeDelta = new Vector2(upt.ui.thisRectTransform.sizeDelta.x, y);
                    j++;
                }
            }

            #endregion
        }
    }
#endif
}