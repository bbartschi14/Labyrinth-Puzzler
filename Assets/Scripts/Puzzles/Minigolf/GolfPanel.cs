using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GolfPanel : MonoBehaviour
{
    [SerializeField] private TMPro.TextMeshProUGUI mainText;
    public void FormatPanel(Minigolf puzzle)
    {
        
        if (puzzle.requireStrokeCount)
        {
            string desc = "strokes";
            if (puzzle.maxStrokeCount == 1) desc = "stroke";
            mainText.text = "Complete the minigolf hole in " + puzzle.maxStrokeCount + " " + desc + " or less!";
        }
    }
}
