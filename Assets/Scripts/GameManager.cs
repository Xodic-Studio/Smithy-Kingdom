using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class GameManager : Singleton<GameManager>
{
    private Ore _ore;
    private UIManager _uiManager;
    public GameObject damageGameObject;
    TMP_Text _damageText;
    [SerializeField] Camera mainCamera;
    public float money;

    void OnFire()
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
    }


    //Hammer variables
    public int hammerDamage;


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
        // if (Touchscreen.current != null)
        // {
        //     touchPosition = Touchscreen.current.position.ReadValue();
        // }

        if (Mouse.current != null)
        {
            touchPosition = Mouse.current.position.ReadValue();
        }
        else
        {
            return;
        }
        worldCords = mainCamera.ScreenToWorldPoint(touchPosition);
        worldCords.z = 0f;
        var go = Instantiate(damageGameObject, worldCords, Quaternion.identity);
        RectTransform goRect = go.GetComponent<RectTransform>();
        go.GetComponent<TMP_Text>().text = $"- {text}";
        go.transform.SetParent(_uiManager.baseCanvas.transform, false);
        goRect.position = worldCords;
        StartCoroutine(FloatDelayDestroy(goRect));
    }
    
    IEnumerator FloatDelayDestroy(RectTransform goRect)
    {
        for (float i = 0; i < 0.5f; i += 1 * Time.fixedDeltaTime)
        {
            goRect.position += new Vector3(0, 0.01f, 0);
            yield return null;
        }
        Destroy(goRect.gameObject);
    }

    
    
    
    
    
    
    
    
    //Hitting Function
    private void TapTap()
    {
        _ore.ModifyHardness(hammerDamage);
        AddDamageText(hammerDamage.ToString());
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
