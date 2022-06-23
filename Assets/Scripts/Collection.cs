using System.Collections;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class Collection : Singleton<Collection>
{
    public ItemDatabase itemDatabase;
    public GameObject collectionPrefab;
    
    private RectTransform _thisRectTransform;
    private RectTransform _parentRectTransform;
    private RectTransform _prefabRectTransform;
    private VerticalLayoutGroup _thisVerticalLayoutGroup;

    

    private void OnValidate()
    {
        _thisVerticalLayoutGroup = GetComponent<VerticalLayoutGroup>();
        _prefabRectTransform = collectionPrefab.GetComponent<RectTransform>();
        _parentRectTransform = transform.parent.GetComponent<RectTransform>();
        _thisRectTransform = GetComponent<RectTransform>();
        UpdateCollection();
    }

    IEnumerator Destroy(GameObject go)
    {
        yield return new WaitForEndOfFrame();
        DestroyImmediate(go);
    }
    
    public void UpdateCollection()
    {
        var i = 0;
        foreach (var collection in itemDatabase.collections)
        {
            try
            {
                transform.GetChild(i);
            }
            catch (UnityException)
            {
                var newCollection = Instantiate(collectionPrefab, transform);
                newCollection.transform.SetParent(transform);
            }
            transform.GetChild(i).name = itemDatabase.collections[i].collectionName;
            transform.GetChild(i).GetComponent<TMP_Text>().text = itemDatabase.collections[i].collectionName;
            i++;
        }
        var needDestroy = transform.childCount - itemDatabase.collections.Length;
                
        for (i = transform.childCount; i > transform.childCount - needDestroy; i--)
        {
            StartCoroutine(Destroy(transform.GetChild(i-1).gameObject));
        }
        UpdateScrollViewSize();
    }
    
    private void UpdateScrollViewSize()
    {
        if (transform.childCount <= 2)
        {
            _thisRectTransform.sizeDelta = new Vector2(_thisRectTransform.sizeDelta.x, _parentRectTransform.rect.height);
        }
        else
        {
            float y = _parentRectTransform.rect.height + (transform.childCount - 2) * (_prefabRectTransform.rect.height + _thisVerticalLayoutGroup.spacing);
            _thisRectTransform.sizeDelta = new Vector2(_thisRectTransform.sizeDelta.x, y);
        }
    }
}
#if UNITY_EDITOR
[CustomEditor(typeof(Collection))]
public class CollectionEditor : Editor
{
    public override void OnInspectorGUI()
    {
        Collection collection = (Collection) target;
        base.OnInspectorGUI();
        EditorGUILayout.Space();
        GUILayout.Label("Update UI", EditorStyles.boldLabel);
        GUILayout.BeginHorizontal();
        if (GUILayout.Button("Update", GUILayout.Width(100)))
        {
            collection.UpdateCollection();
        }
        GUILayout.Label("*Only Use in Editor");
        GUILayout.EndHorizontal();
    }
}
#endif
