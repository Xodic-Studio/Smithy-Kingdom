using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CollectionSubMenuColor : MonoBehaviour
{
    [Header("Collection Sub-Menu Tab")] 
    
    private bool selectedCollection;
    private bool selectedAchievement;

    [SerializeField] private Button collectionButton;
    [SerializeField] private Button achievementButton;

    public Color normalColor, selectColor;
    
    void Start()
    {
        selectedCollection = true;
        selectedAchievement = false;
        collectionButton.GetComponent<Image>().color = selectColor;
        achievementButton.GetComponent<Image>().color = normalColor;
    }

    public void CollectionCheckPage()
    {
        if (!selectedCollection)
        {
            selectedCollection = true;
            selectedAchievement = false;
            collectionButton.GetComponent<Image>().color = selectColor;
            achievementButton.GetComponent<Image>().color = normalColor;
        }
    }

    public void AchievementCheckPage()
    {
        if (!selectedAchievement)
        {
            selectedCollection = false;
            selectedAchievement = true;
            collectionButton.GetComponent<Image>().color = normalColor;
            achievementButton.GetComponent<Image>().color = selectColor;
        }
    }
}
