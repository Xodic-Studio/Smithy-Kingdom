using System.Collections;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class Upgrade : Singleton<Upgrade>
{
    public UpgradesDatabase upgradesDatabase;
    public GameObject upgradePrefab;
    
    private RectTransform _thisRectTransform;
    private RectTransform _parentRectTransform;
    private RectTransform _prefabRectTransform;
    private VerticalLayoutGroup _thisVerticalLayoutGroup;

    

    public void OnValidate()
    {   
        _thisVerticalLayoutGroup = GetComponent<VerticalLayoutGroup>();
        _prefabRectTransform = upgradePrefab.GetComponent<RectTransform>();
        _parentRectTransform = transform.parent.GetComponent<RectTransform>();
        _thisRectTransform = GetComponent<RectTransform>();
        UpdateUpgrades();
    }
    IEnumerator Destroy(GameObject go)
    {
        yield return new WaitForEndOfFrame();
        DestroyImmediate(go);
    }

    
    // ReSharper disable Unity.PerformanceAnalysis
    public void UpdateUpgrades()
    {
        var active = gameObject.activeSelf;
        if (!active)
        {
            gameObject.SetActive(true);
        }

        var i = 0;
        foreach (var variable in upgradesDatabase.stats)
        {
            try
            {
                transform.GetChild(i);
            }
            catch (UnityException)
            {
                var newUpgrade = Instantiate(upgradePrefab, transform);
                newUpgrade.transform.SetParent(transform);
            }
            transform.GetChild(i).name = upgradesDatabase.stats[i].upgradeName;
            transform.GetChild(i).Find("UpgradeTextArea/UpgradeName").GetComponent<TMP_Text>().text = upgradesDatabase.stats[i].upgradeName;
            transform.GetChild(i).Find("UpgradeTextArea/UpgradeDescription").GetComponent<TMP_Text>().text = upgradesDatabase.stats[i].upgradeDescription;
            i++;
        }
        
        var needDestroy = transform.childCount - upgradesDatabase.stats.Length;
                
        for (i = transform.childCount; i > transform.childCount - needDestroy; i--)
        {
            StartCoroutine(Destroy(transform.GetChild(i-1).gameObject));
        }
        UpdateScrollViewSize();
        gameObject.SetActive(active);
    }

    private void UpdateScrollViewSize()
    {
        if (transform.childCount <= 5)
        {
            _thisRectTransform.sizeDelta = new Vector2(_thisRectTransform.sizeDelta.x, _parentRectTransform.rect.height);
        }
        else
        {
            float y = _parentRectTransform.rect.height + (transform.childCount - 5) * (_prefabRectTransform.rect.height + _thisVerticalLayoutGroup.spacing);
            _thisRectTransform.sizeDelta = new Vector2(_thisRectTransform.sizeDelta.x, y);
        }
    }
}


#if UNITY_EDITOR
[CustomEditor(typeof(Upgrade))]
public class UpgradeEditor : Editor
{
    public override void OnInspectorGUI()
    {
        Upgrade upgrade = (Upgrade) target;
        base.OnInspectorGUI();
        EditorGUILayout.Space();
        GUILayout.Label("Update UI", EditorStyles.boldLabel);
        GUILayout.BeginHorizontal();
        if (GUILayout.Button("Update", GUILayout.Width(100)))
        {
            upgrade.UpdateUpgrades();
        }
        GUILayout.Label("*Only Use in Editor");
        GUILayout.EndHorizontal();
    }
}
#endif

