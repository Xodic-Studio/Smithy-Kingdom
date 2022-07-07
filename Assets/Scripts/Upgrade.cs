using System.Collections;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class Upgrade : Singleton<Upgrade>
{
    /// <summary>
    /// All Database Types
    /// </summary>
    public enum DatabaseType
    {
        Ore,
        Upgrade,
        Item,
    }
    public Database database;
    public DatabaseType databaseType;
    [SerializeField] private GameObject upgradePrefab;
    [SerializeField] private GameObject orePrefab;
    [SerializeField] private GameObject itemPrefab;


    private RectTransform _thisRectTransform;
    private RectTransform _parentRectTransform;
    private RectTransform _prefabRectTransform;
    private VerticalLayoutGroup _thisVerticalLayoutGroup;
    private HorizontalLayoutGroup _thisHorizontalLayoutGroup;

    

    public void OnValidate()
    {   
        _thisVerticalLayoutGroup = GetComponent<VerticalLayoutGroup>();
        _prefabRectTransform = upgradePrefab.GetComponent<RectTransform>();
        _parentRectTransform = transform.parent.GetComponent<RectTransform>();
        _thisRectTransform = GetComponent<RectTransform>();
    }
    IEnumerator Destroy(GameObject go)
    {
        yield return new WaitForEndOfFrame();
        DestroyImmediate(go);
    }

    
    public void UpdateUpgrades()
    {
        var active = gameObject.activeSelf;
        if (!active)
        {
            gameObject.SetActive(true);
        }

        var i = 0;
        foreach (var unused in database.stats)
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
            transform.GetChild(i).name = database.stats[i].upgradeName;
            transform.GetChild(i).Find("UpgradeTextArea/UpgradeName").GetComponent<TMP_Text>().text = database.stats[i].upgradeName;
            transform.GetChild(i).Find("UpgradeTextArea/UpgradeDescription").GetComponent<TMP_Text>().text = database.stats[i].upgradeDescription;
            i++;
        }
        
        var needDestroy = transform.childCount - database.stats.Length;
                
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

