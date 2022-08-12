using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEditor;
using UnityEditor.SceneTemplate;

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
        [SerializeField] public RectTransform mainAreaUiTrans;
        [SerializeField] public RectTransform gachaHeaderTrans;
        [HideInInspector] public float currentHeight = 700f;
        [HideInInspector] public bool isRolling;
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
                        SetNewSize(mainAreaUiTrans, 700f);
                        ClearList();
                        isRolling = true;
                        GameObject drop = Instantiate(gachaDropPrefab, gachaResultList.transform);
                        drop.GetComponentInChildren<GachaImageController>().SetResultImage(sprite[0]);
                        yield return null;
                    }
                    else if (dropCount == 10)
                    {
                        SetNewSize(mainAreaUiTrans, 1400f);
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

        private void SetNewSize(RectTransform gachaBackgroundTrans, float newHeight)
        {
            float oldHeight = currentHeight;
            float newY = 200f; //Y Pos: 1 Roll = 175f, 10 Roll = 375f
            
            gachaBackgroundTrans.sizeDelta = new Vector2(gachaBackgroundTrans.rect.width, newHeight);
            
            if (oldHeight - newHeight == 0)
            {
                // Do nothing
            }
            else if (oldHeight - newHeight != 0)
            {
                float result = Mathf.Sign(oldHeight - newHeight);
                int resultInt = result.ConvertTo<int>();
                
                if (resultInt == -1)
                {
                    //Got Bigger then Move Up
                    gachaHeaderTrans.offsetMin += new Vector2(0, newY);
                    gachaHeaderTrans.offsetMax -= new Vector2(0, -newY);
                }
                else if (resultInt == 1)
                {
                    //Got Smaller then Move Down
                    gachaHeaderTrans.offsetMin += new Vector2(0, -newY);
                    gachaHeaderTrans.offsetMax -= new Vector2(0, newY);
                }
            }
            
            currentHeight = newHeight;
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