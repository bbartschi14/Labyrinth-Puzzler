using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnackbarController : MonoBehaviour
{
    [SerializeField] private RectTransform rt;
    [SerializeField] private TMPro.TextMeshProUGUI textField;
    [SerializeField] private List<string> messages;
    private void Start()
    {
        CloseBar();

    }

    public void DisplayMessage(string msg)
    {
        if (!messages.Contains(msg))
        {
            messages.Add(msg);
            textField.text = msg;
            LeanTween.moveY(gameObject, 0f, .4f).setEaseOutCubic();
        }
    }

    public void CloseBar()
    {
        LeanTween.moveY(gameObject, -rt.sizeDelta.y*1.5f, .3f);
    }
}
