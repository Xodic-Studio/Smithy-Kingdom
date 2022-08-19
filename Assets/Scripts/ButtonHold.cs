using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class ButtonHold : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    private bool pointerDown;
    float timer;

    public float holdTime;
    public bool allowDelay;

    public UnityEvent onHold;
    public void OnPointerDown(PointerEventData eventData)
    {
        pointerDown = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        Reset();
    }

    private void Update()
    { 
        timer += Time.deltaTime;
        if (pointerDown)
        {
            if (timer >= holdTime)
            {
                if (onHold != null)
                {
                    onHold.Invoke();
                }
            }
        }
        else
        {
            timer = 0;
        }
    }


    private void Reset()
    {
        pointerDown = false;
        timer = 0;
    }
}
