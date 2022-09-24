using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Hold : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    GameManager gameManager;
    UpgradesFunction upgradesFunction;
    public bool isPressed;
    bool isHolding = false;
    float holdTime = 0;
    private float timer;

    private void Awake()
    {
        gameManager = GameManager.Instance;
        upgradesFunction = UpgradesFunction.Instance;
    }

    //timer update for holding
    private void Update()
    {
        if (isPressed)
        {
            holdTime += Time.deltaTime;
            timer += Time.deltaTime;
            if (holdTime >= 1)
            {
                if (timer > 1f / upgradesFunction.premiumUpgradeDatabase.stats[0].upgradeLevel)
                {
                    gameManager.TapTap();
                    timer = 0;
                }
            }
            
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        isPressed = true;
    }
    
    public void OnPointerUp(PointerEventData eventData)
    {
        isPressed = false;
        holdTime = 0;
    }
}
