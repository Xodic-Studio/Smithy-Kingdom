using System;
using UnityEngine;
using UnityEngine.UI;

public class PremiumSubMenuColor : MonoBehaviour
{
    [Header("Premium Sub-Menu")] 
    
    private bool selectedGacha;
    private bool selectedPackages;

    [SerializeField] private Button gachaButton;
    [SerializeField] private Button packagesButton;

    public Color normalColor, selectColor;
    
    void Start()
    {
        selectedGacha = true;
        selectedPackages = false;
        gachaButton.GetComponent<Image>().color = selectColor;
        packagesButton.GetComponent<Image>().color = normalColor;
    }

    public void GachaCheckPage()
    {
        if (!selectedGacha)
        {
            selectedGacha = true;
            selectedPackages = false;
            gachaButton.GetComponent<Image>().color = selectColor;
            packagesButton.GetComponent<Image>().color = normalColor;
        }
    }

    public void PackagesCheckPage()
    {
        if (!selectedPackages)
        {
            selectedGacha = false;
            selectedPackages = true;
            gachaButton.GetComponent<Image>().color = normalColor;
            packagesButton.GetComponent<Image>().color = selectColor;
        }
    }

    private void OnDisable()
    {
        selectedGacha = true;
        selectedPackages = false;
        gachaButton.GetComponent<Image>().color = selectColor;
        packagesButton.GetComponent<Image>().color = normalColor;
    }
}
