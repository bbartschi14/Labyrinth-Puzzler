using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnscrambleAnswer : MonoBehaviour
{
    [SerializeField] private UnscramblePanel panel;
    public TMPro.TextMeshProUGUI answerText;
    public int currentIndex = 0;
    public int maxLength;
    
    public void InitializeBlank(int length)
    {
        answerText.text = new String('_', length);
        currentIndex = 0;
        this.maxLength = length;
        if (length > 6)
        {
            answerText.fontSize = 60;
        }
    }

    public void AddLetter(string c)
    {
        if (currentIndex >= maxLength)
        {
            return;
        }
        
        //Debug.Log("adding " + c + " at " + currentIndex);
        answerText.text = Overwrite(answerText.text, currentIndex, c);
        currentIndex++;

        if (currentIndex == maxLength)
        {
            panel.CheckAnswer();
        }
    }
    
    public void RemoveLetter()
    {
        if (currentIndex < 1)
        {
            return;
        }
        currentIndex--;
        //Debug.Log("adding " + c + " at " + currentIndex);
        answerText.text = Overwrite(answerText.text, currentIndex, "_");
    }

    public string Overwrite(string text, int position, string new_text)
    {
        return text.Substring(0, position) + new_text + text.Substring(position + new_text.Length);
    }
}


