using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;

namespace AnimationScript
{
    /*
    Noted:
    To use animation please use function "Roll(int dropCount, Sprite[] sprite)".
    dropCount could only be "1" or "10", also sprite argument need to be in array maximum of 10.
    
    Examples:
    "Roll(1, spriteArray)" << sprite[0] will be use in this case.
    "Roll(10, spriteArray)" << sprite[0]...[9] will be use in this case.
    
    Bugs:
    Skipping function is broken, if "Roll(both 1 and 10, spriteArray)" function is called right after spinning wheel finished spinning.
    To not encounter these bugs please use "CloseResult()" before start a new "Roll(both 1 and 10, spriteArray)" function.
    As example from SpawnGachaDrop class >> SpawnExample region.
    */
    
    public class GachaDrop : Singleton<GachaDrop>
    {
        [SerializeField] public GameObject gachaDropPrefab;
        [SerializeField] public GameObject gachaResultList;
        [SerializeField] public GameObject mainAreaUi;
        [SerializeField] public RectTransform gachaHeaderTrans;
        [HideInInspector] public float currentHeight = 700f;
        [HideInInspector] public bool isRolling, skippable;
        [SerializeField] private Sprite[] gachaBackgroundSprite;
        public Sprite[] dummySprite1;
        public Sprite[] dummySprite2;
        private int rolls = 1;
        private SoundManagerr _soundManager;

        private void Awake()
        {
            _soundManager = SoundManagerr.Instance;
        }

        private void Start()
        {
            ClearList();
            CloseResult();
        }

        public void Roll(int dropCount, Sprite[] sprite)
        {
            if (gameObject.activeSelf)
            {
                StartCoroutine(GetGachaResults(dropCount, sprite));
            }
        }
        
        private IEnumerator GetGachaResults(int dropCount, Sprite[] sprite)
        {
            if (isRolling)
            {
                Debug.Log("Gacha's Spinning Wheel is still rolling wait for it to finish first to start a new rolls!");
            }
            else if (!isRolling)
            {
                if (dropCount != sprite.Length)
                {
                    Debug.LogWarning("The Sprite[] given isn't match with dropCount? Try checking it again.");
                }
                else
                {
                    StopCoroutine("CloseResult");
                    isRolling = false;
                    skippable = false;
                    
                    if (dropCount == 1)
                    {
                        _soundManager.PlaySound("Rollx1");
                        Debug.Log("Spawning 1 roll");
                        SetNewSize(700f);
                        ClearList();
                        rolls = 1;
                        isRolling = true;
                        GameObject drop = Instantiate(gachaDropPrefab, gachaResultList.transform);
                        drop.GetComponentInChildren<GachaImageController>().SetResultImage(sprite[0]);
                        skippable = true;
                        yield return new WaitForSeconds(6f);
                        isRolling = false;
                    }
                    else if (dropCount == 10)
                    {
                        _soundManager.PlaySound("Rollx10");
                        Debug.Log("Spawning 10 rolls");
                        SetNewSize(1400f);
                        ClearList();
                        rolls = 10;
                        isRolling = true;
                        for (int i = 0; i < dropCount; i++)
                        {
                            GameObject drop = Instantiate(gachaDropPrefab, gachaResultList.transform);
                            drop.GetComponentInChildren<GachaImageController>().SetResultImage(sprite[i]);
                            yield return new WaitForSeconds(0.07f);
                        }
                        skippable = true;
                        yield return new WaitForSeconds(6f);
                        isRolling = false;
                    }
                    else
                    {
                        Debug.LogWarning("Did you just set Gacha rolls to other than 'x1' or 'x10'?");
                    }
                    /*if (isRolling)
                    {
                        yield return new WaitForSeconds(6f);
                        isRolling = false;
                    }*/
                }
            }
        }

        public void ClearList()
        {
            //Debug.Log("Gacha list cleared");
            foreach (Transform child in gachaResultList.transform)
            {
                Destroy(child.gameObject);
            }
        }

        public void ResetSize()
        {
            //Debug.Log("Gacha panel size reset");
            SetNewSize(700f);
        }
        
        public void OpenResult()
        {
            if (!gameObject.activeSelf)
            {
                gameObject.SetActive(true);
                //Debug.Log("Opening Gacha Panel");
            }
        }

        public void CloseResult()
        {
            if (gameObject.activeSelf)
            {
                StartCoroutine(CloseResultOrSkip());
            }
        }
        
        private IEnumerator CloseResultOrSkip()
        {
            if (!isRolling)
            {
                //Debug.Log("Closing Gacha Panel");
                StopCoroutine("GetGachaResults");
                ResetSize();
                ClearList();
                gameObject.SetActive(false);
            }
            else if (isRolling)
            {
                //Skipping Function

                if (skippable)
                {
                    Debug.Log("Skipping Gacha Animation");
                    skippable = false;
                    int iMax = gachaResultList.transform.childCount;

                    for (int i = 0; i < iMax; i++)
                    {
                        gachaResultList.transform.GetChild(i).GetChild(1).GetComponent<GachaImageController>().Skip();
                        yield return new WaitForSeconds(0.07f);
                    }
                    
                    StopCoroutine("GetGachaResults");

                    if (rolls == 1)
                    {
                        yield return new WaitForSeconds(2.5f);
                        isRolling = false;
                    }
                    else if (rolls == 10)
                    {
                        yield return new WaitForSeconds(3f);
                        isRolling = false;
                    }
                }
            }
        }

        private void SetNewSize(float newHeight)
        {
            float oldHeight = currentHeight;
            float newY = 140f;
            //New Y Pos: 1 Roll = 235f, 10 Roll = 375f (+-140)
            
            //For size changing of MainAreaUI rect width & height
            
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
                    mainAreaUi.GetComponent<Image>().sprite = gachaBackgroundSprite[1];
                    gachaHeaderTrans.offsetMin += new Vector2(0, newY);
                    gachaHeaderTrans.offsetMax -= new Vector2(0, -newY);
                }
                else if (resultInt == 1)
                {
                    //Got Smaller then Move Down
                    mainAreaUi.GetComponent<Image>().sprite = gachaBackgroundSprite[0];
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
            
            //Unity GUI Area
            
            if (GUILayout.Button("Close Gacha Result (Skip if rolling)"))
            {
                gachaResult.CloseResult();
            }
            
            #region SpawnExample
            
            /*
             Here is "Roll(x1 or x10)" spawning example.
             Yes.. I know it's still not 100% bugs-proof if "Roll(x1 or x10)" is called again while spinning wheel still spinning.
             Just hope there isn't gonna be someone calling "Roll(x1 or x10)" function without "CloseResult()" function.
             */
            
            if (GUILayout.Button("Spawn (x1)"))
            {
                if (gachaResult.gameObject.activeSelf)
                {
                    gachaResult.CloseResult();
                }
                gachaResult.OpenResult();
                gachaResult.Roll(1, gachaResult.dummySprite1);
            }
            if (GUILayout.Button("Spawn (x10)"))
            {
                if (gachaResult.gameObject.activeSelf)
                {
                    gachaResult.CloseResult();
                }
                gachaResult.OpenResult();
                gachaResult.Roll(10, gachaResult.dummySprite2);
            }
            
            #endregion
            
            if (GUILayout.Button("Clear List"))
            {
                gachaResult.ResetSize();
                gachaResult.ClearList();
            }
        }
    }
    #endif
}