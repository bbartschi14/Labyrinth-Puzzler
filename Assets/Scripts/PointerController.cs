using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PointerController : MonoBehaviour, IPointerDownHandler
{
    [SerializeField] private GameObject pointer;
    [SerializeField] private bool horizontal;
    private void Start()
    {
        if (horizontal)
        {
            LeanTween.moveLocalX(pointer, 1.25f, 2f).setEaseOutCubic().setLoopClamp();
        }
        else
        {
            LeanTween.moveLocalY(pointer, 1f, 2f).setEaseOutCubic().setLoopClamp();
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        pointer.SetActive(false);
    }
}
