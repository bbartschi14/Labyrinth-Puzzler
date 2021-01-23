using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;
public class HoneycombButton : MonoBehaviour
{
    [SerializeField] private TMPro.TextMeshProUGUI buttonText;
    [SerializeField] private Button button;
    
    public void SetupButton(char c, TMPro.TextMeshProUGUI field)
    {
        buttonText.text = c.ToString();
        button.OnClickAsObservable()
            .Subscribe(_ =>
            {
                field.text += c.ToString();
            })
            .AddTo(this);
    }
}
