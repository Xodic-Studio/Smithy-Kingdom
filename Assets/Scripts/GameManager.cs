using System.Collections;
using TMPro;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    private Ore _ore;
    private UIManager _uiManager;
    public GameObject damageGameObject;
    TMP_Text _damageText;

    public float money;
    
    
    
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
        for (int i = 0; i < 10; i++)
        {
            go.transform.position += new Vector3(0, 0.1f, 0);
            yield return new WaitForSeconds(0.1f);
        }
        Destroy(go);
    }

    
    
    
    
    
    
    
    
    //Hitting Function
    public void TapTap()
    {
        _ore.ModifyHardness(hammerDamage);
        AddDamageText(hammerDamage.ToString(), _ore.transform.position);
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
