using System;
using System.Collections;
using UnityEngine;
using UnityEditor;

namespace AnimationScript
{
    /*
    Noted:
    To use animation please use function "StartCoroutine(GetGachaResults(int dropCount, Sprite[] sprite))".
    dropCount could only be "1" or "10", also sprite argument need to be in array maximum of 10.
    IEnumerator function will not work if this GameObject is inactive.
    
    Examples:
    "StartCoroutine(GetGachaResults(1, spriteArray))" << sprite[0] will be use in this case.
    "StartCoroutine(GetGachaResults(10, spriteArray))" << sprite[0]...[9] will be use in this case.
    */
    
    public class GachaDrop : Singleton<GachaDrop>
    {
        [SerializeField] public GameObject gachaDropPrefab;
        [SerializeField] public GameObject gachaResultList;
        public bool isRolling;
        public Sprite[] dummySprite1;
        public Sprite[] dummySprite2;

        private void Start()
        {
            CloseResult();
        }

        public IEnumerator GetGachaResults(int dropCount, Sprite[] sprite)
        {
            if (isRolling)
            {
                Debug.Log("Gacha is still rolling wait for it to finish first!");
            }
            else if (!isRolling)
            {
                if (dropCount != sprite.Length)
                {
                    Debug.LogWarning("The Sprite[] given isn't match with dropCount? Try checking it again.");
                }
                else
                {
                    if (dropCount == 1)
                    {
                        ClearList();
                        isRolling = true;
                        GameObject drop = Instantiate(gachaDropPrefab, gachaResultList.transform);
                        drop.GetComponentInChildren<GachaImageController>().SetResultImage(sprite[0]);
                        yield return null;
                    }
                    else if (dropCount == 10)
                    {
                        ClearList();
                        isRolling = true;
                        for (int i = 0; i < dropCount; i++)
                        {
                            GameObject drop = Instantiate(gachaDropPrefab, gachaResultList.transform);
                            drop.GetComponentInChildren<GachaImageController>().SetResultImage(sprite[i]);
                            yield return new WaitForSeconds(0.07f);
                        }
                    }
                    else
                    {
                        Debug.LogWarning("Did you just set Gacha rolls to other than 'x1' or 'x10'?");
                    }
                    yield return new WaitForSeconds(6f);
                    isRolling = false;
                }
            }
        }

        public void ClearList()
        {
            foreach (Transform child in gachaResultList.transform)
            {
                Destroy(child.gameObject);
            }
        }
        
        public void OpenResult()
        {
            gameObject.SetActive(true);
            Debug.Log("GachaOpen");
        }
        
        public void CloseResult()
        {
            if (!isRolling)
            {
                ClearList();
                gameObject.SetActive(false);
            }
            else if (isRolling)
            {
                Debug.Log("Rolling animation is still running, closing it now would ruin the mood!");
            }
        }
    }

    #if UNITY_EDITOR
    [CustomEditor(typeof(GachaDrop))]
    public class SpawnGachaDrop : Editor
    {
        public override void OnInspectorGUI()
        {
            GUILayout.Label("Spawn Drop", EditorStyles.boldLabel);

            DrawDefaultInspector();

            var gachaResult = (GachaDrop) target;
            
            if (GUILayout.Button("Open Gacha Result"))
            {
                gachaResult.OpenResult();
            }
            else if (GUILayout.Button("Close Gacha Result"))
            {
                gachaResult.CloseResult();
            }

            if (GUILayout.Button("Spawn (x1)"))
            {
                gachaResult.StartCoroutine(gachaResult.GetGachaResults(1, gachaResult.dummySprite1));
            }
            else if (GUILayout.Button("Spawn (x10)"))
            {
                gachaResult.StartCoroutine(gachaResult.GetGachaResults(10, gachaResult.dummySprite2));
            }
            else if (GUILayout.Button("Clear List"))
            {
                gachaResult.isRolling = false;
                gachaResult.ClearList();
            }
        }
    }
    #endif
}