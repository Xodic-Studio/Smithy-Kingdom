using System;
using UnityEngine;

public class AssistantSkin : MonoBehaviour
{
    [HideInInspector] public GameObject assistantSweat;
    [HideInInspector] public GameObject assistantDepression1;
    [HideInInspector] public GameObject assistantDepression2;
    [SerializeField] public bool isTired;

    private void Update()
    {
        if (isTired)
        {
            assistantSweat.SetActive(true);
            assistantDepression1.SetActive(true);
            assistantDepression2.SetActive(true);
        }
        else if (!isTired)
        {
            assistantSweat.SetActive(false);
            assistantDepression1.SetActive(false);
            assistantDepression2.SetActive(false);
        }
    }
}
