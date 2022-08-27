using UnityEngine;
using UnityEngine.UI;

public class UpgradeSubMenuColor : MonoBehaviour
{
    [Header("Upgrade Sub-Menu Tab")] 
    private bool selectedNormal;
    private bool selectedPremium;

    [SerializeField] private Button normalUpgrade;
    [SerializeField] private Button premiumUpgrade;

    public Color normalColor, selectColor;

    private void Start()
    {
        selectedNormal = true;
        selectedPremium = false;
        normalUpgrade.GetComponent<Image>().color = selectColor;
        premiumUpgrade.GetComponent<Image>().color = normalColor;
    }

    public void NormalCheckPage()
    {
        if (!selectedNormal)
        {
            selectedNormal = true;
            selectedPremium = false;
            normalUpgrade.GetComponent<Image>().color = selectColor;
            premiumUpgrade.GetComponent<Image>().color = normalColor;
        }
    }

    public void PremiumCheckPage()
    {
        if (!selectedPremium)
        {
            selectedNormal = false;
            selectedPremium = true;
            normalUpgrade.GetComponent<Image>().color = normalColor;
            premiumUpgrade.GetComponent<Image>().color = selectColor;
        }
    }
}
