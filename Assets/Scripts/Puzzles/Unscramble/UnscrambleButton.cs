using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;
using UnityEngine.UI;
public class UnscrambleButton : MonoBehaviour
{
    public TMPro.TextMeshProUGUI buttonText;
    public Button button;
    public RectTransform rt;
    public void SetupButton(char c, UnscrambleAnswer field)
    {
        buttonText.text = c.ToString();
        button.OnClickAsObservable()
            .Subscribe(_ => field.AddLetter(buttonText.text))
            .AddTo(this);
    }

    public void Resize(int numLetters)
    {
        if (numLetters > 6)
        {
            rt.sizeDelta = new Vector2(80, 80);
            buttonText.fontSize = 50;
        }
    }
}
