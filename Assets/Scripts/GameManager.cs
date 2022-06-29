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
        Touch touch = Input.GetTouch(0);
        
        touchPosition = touch.position;
        worldCords = mainCamera.ScreenToWorldPoint(touchPosition);
        worldCords.z = 0f;
            

        Debug.Log(touchPosition);
        if (EventSystem.current.IsPointerOverGameObject())
        {
            Debug.Log("Touched UI");
        }
        else
        {
            TapTap(touchPosition);
        }
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
    private void AddDamageText(string text, Vector3 position)
    {
        var go = Instantiate(damageGameObject, position, Quaternion.identity);
        go.GetComponent<TMP_Text>().text = $"- {text}";
        go.transform.SetParent(_uiManager.baseCanvas.transform, false);
        StartCoroutine(FloatDelayDestroy(go));
    }
    
    IEnumerator FloatDelayDestroy(GameObject go)
    {
        for (float i = 0; i < 0.5f; i += 1 * Time.fixedDeltaTime)
        {
            go.transform.position += new Vector3(0, 0.01f, 0);
            yield return null;
        }
        Destroy(go);
    }

    
    
    
    
    
    
    
    
    //Hitting Function
    private void TapTap(Vector2 position)
    {
        _ore.ModifyHardness(hammerDamage);
        AddDamageText(position.ToString(), position);
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
