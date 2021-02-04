using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockPanel : MonoBehaviour
{
    [SerializeField] private TMPro.TextMeshProUGUI mainText;
    public void FormatPanel(Rock puzzle)
    {
        mainText.text = "Complete the " + puzzle.color + " rock puzzle!";
    }
}
