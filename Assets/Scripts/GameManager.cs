using System;
using System.Collections;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameManager : Singleton<GameManager>
{
    private Ore _ore;
    private UIManager _uiManager;
    public GameObject damageGameObject;
    private TMP_Text _damageText;
    [SerializeField] Camera mainCamera;
    public float money;

    public Animator smithy;
    public Animator anvil;
    


    //Hammer variables
    public int hammerDamage;
    private float _timer;
    private int _clickPerSec;
    private int _click;


    private void Awake()
    {
        _uiManager = UIManager.Instance;
        _ore = Ore.Instance;
    }

    private void Start()
    {
        _damageText = damageGameObject.GetComponent<TMP_Text>();
        _uiManager.UpdateMoneyText();
    }

    private void Update()
    {
        CheckTimer();
    }


    void CheckTimer()
    {
        _timer += Time.deltaTime;
        if (_timer > 1)
        {
            _clickPerSec = _click;
            _click = 0;
            _timer = 0;
            if (_clickPerSec is < 10 and > 5)
            {
                smithy.SetFloat("Speed", 1.25f);
                anvil.SetFloat("Speed", 1.25f);
            } else if (_clickPerSec > 10)
            {
                smithy.SetFloat("Speed", 1.5f);
                anvil.SetFloat("Speed", 1.5f);
            }
            else
            {
                smithy.SetFloat("Speed", 1f);
                anvil.SetFloat("Speed", 1f);
            }
        }
        

    }

    //Modify money
    public void ModifyMoney(float amount)
    {
        money += amount;
        _uiManager.UpdateMoneyText();
    }
    
    //Instantiate Damage text
    private void AddDamageText(string text)
    {
        Vector2 touchPosition;
        Vector3 worldCords;
        if (Touchscreen.current != null)
        {
            touchPosition = Touchscreen.current.position.ReadValue();
        }

        // if (Mouse.current != null)
        // {
        //     touchPosition = Mouse.current.position.ReadValue();
        // }
        else 
        {
            return;
        }
        worldCords = mainCamera.ScreenToWorldPoint(touchPosition);
        worldCords.z = 0f;
        var go = Instantiate(damageGameObject, worldCords, Quaternion.identity);
        RectTransform goRect = go.GetComponent<RectTransform>();
        TMP_Text goText = go.GetComponent<TMP_Text>();
        goText.text = $"- {text}";
        go.transform.SetParent(_uiManager.baseCanvas.transform, false);
        goRect.position = worldCords;
        StartCoroutine(FloatDelayDestroy(goRect, goText));
    }
    
    IEnumerator FloatDelayDestroy(RectTransform goRect, TMP_Text goText)
    {
        for (float i = 0; i < 0.75f; i += 1 * Time.fixedDeltaTime)
        {
            goRect.position += new Vector3(0, 0.01f, 0);
            if (i > 0.1f)
            {
                goText.color -= new Color(0, 0, 0, 0.1f);
            }
            yield return null;
        }
        Destroy(goRect.gameObject);
    }

    
    
    
    
    
    
    
    
    //Hitting Function
    private void TapTap()
    {
        _ore.ModifyHardness(hammerDamage);
        AddDamageText(hammerDamage.ToString());
        smithy.SetTrigger("Hit");
        anvil.SetTrigger("Hit");
        _click +=5;
    }
    
    // Ore Selection
    public void SelectPreviousOre()
    {
        _ore.ModifySelectedOreIndex(-1);
        _ore.UpdateOre();
        _uiManager.UpdateOreNameText(_ore.GetOreStats().oreName);
    }
    public void SelectNextOre()
    {
        _ore.ModifySelectedOreIndex(1);
        _ore.UpdateOre();
        _uiManager.UpdateOreNameText(_ore.GetOreStats().oreName);
    }

    


}
