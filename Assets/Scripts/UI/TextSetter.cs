using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextSetter : MonoBehaviour
{
    public IntVariable intVariable;

    public TMPro.TextMeshProUGUI textField;
    public Color flashColor;
    private void Start()
    {
        UpdateText();
    }
    public void UpdateText()
    {
        textField.text = intVariable.Value.ToString();
        LeanTween.value(this.gameObject, color => textField.color = color, flashColor, textField.color, .35f);
        LeanTween.value(this.gameObject, size => textField.fontSize = size, 100f,
                textField.fontSize, .35f)
            .setEase(LeanTweenType.easeOutCubic);
    }
}