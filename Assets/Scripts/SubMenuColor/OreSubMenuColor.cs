using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OreSubMenuColor : MonoBehaviour
{
    [Header("Ore Sub-Menu")] 
    
    private bool selectedNormal;
    private bool selectedPremium;

    [SerializeField] private Button normalOreButton;
    [SerializeField] private Button premiumOreButton;

    public Color normalColor, selectColor;
    
    void Start()
    {
        selectedNormal = true;
        selectedPremium = false;
        normalOreButton.GetComponent<Image>().color = selectColor;
        premiumOreButton.GetComponent<Image>().color = normalColor;
    }

    public void NormalOreCheckPage()
    {
        if (!selectedNormal)
        {
            selectedNormal = true;
            selectedPremium = false;
            normalOreButton.GetComponent<Image>().color = selectColor;
            premiumOreButton.GetComponent<Image>().color = normalColor;
        }
    }

    public void PremiumOreCheckPage()
    {
        if (!selectedPremium)
        {
            selectedNormal = false;
            selectedPremium = true;
            normalOreButton.GetComponent<Image>().color = normalColor;
            premiumOreButton.GetComponent<Image>().color = selectColor;
        }
    }
    
}
