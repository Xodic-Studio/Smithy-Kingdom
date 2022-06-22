using System.Collections;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class Upgrade : Singleton<Upgrade>
{
    public UpgradesDatabase upgradesDatabase;
    public GameObject upgradePrefab;


    public void OnValidate()
    {
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
        var i = 0;
        foreach (var VARIABLE in upgradesDatabase.stats)
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
    }

    private void UpdateScrollViewSize()
    {
        RectTransform rt = GetComponent<RectTransform>();
        if (transform.childCount <= 5)
        {
            rt.sizeDelta = new Vector2(rt.sizeDelta.x, transform.parent.GetComponent<RectTransform>().rect.height);
        }
        else
        {
            float y = transform.parent.GetComponent<RectTransform>().rect.height + (transform.childCount - 5) * (upgradePrefab.GetComponent<RectTransform>().rect.height + GetComponent<VerticalLayoutGroup>().spacing);
            rt.sizeDelta = new Vector2(rt.sizeDelta.x, y);
        }
    }
}



[CustomEditor(typeof(Upgrade))]
public class UpgradeEditor : Editor
{
    public override void OnInspectorGUI()
    {
        Upgrade upgrade = (Upgrade) target;
        base.OnInspectorGUI();
        if (GUILayout.Button("Update Upgrades"))
        {
            //Debug.Log("Update Pressed");
            upgrade.UpdateUpgrades();
        }
    }
}
