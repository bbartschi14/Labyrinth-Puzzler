using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;
using UnityEngine.UI;
public class UnscrambleButton : MonoBehaviour
{
    [SerializeField] private TMPro.TextMeshProUGUI buttonText;
    [SerializeField] private Button button;
    [SerializeField] private RectTransform rt;
    [SerializeField] private GameObject dottedBorder;
    private bool used;
    public void SetupButton(char c, UnscrambleAnswer field)
    {
        buttonText.text = c.ToString();
        button.OnClickAsObservable()
            .Subscribe(_ =>
            {
                if (!used)
                {
                    field.AddLetter(buttonText.text,this);
                    ToggleUsed(true);
                }
            })
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

    public void Reset()
    {
        //Debug.Log("Button reset: " + buttonText.text);
        ToggleUsed(false);
    }

    private void ToggleUsed(bool val)
    {
        used = val;
        buttonText.enabled = !val;
        button.enabled = !val;
    }
    
    public void ToggleAvailable(bool val)
    {
        ToggleUsed(!val);
        dottedBorder.SetActive(!val);
    }
}
