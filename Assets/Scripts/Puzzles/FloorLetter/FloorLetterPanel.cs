using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorLetterPanel : MonoBehaviour
{
    [SerializeField] private TMPro.TextMeshProUGUI clueTextField;
    [SerializeField] private FloorLetterAnswer answerField;
    private FloorLetter puzzle;
    public void FormatPanel(FloorLetter puzzle)
    {
        this.puzzle = puzzle;
        clueTextField.text = puzzle.clue;
        answerField.InitializeBlank(puzzle.answer.Length);


    }
    
    public void CheckAnswer()
    {
        if (puzzle.answer == answerField.answerText.text)
        {
            OnCorrectAnswer();
        } else
        {
            OnIncorrectAnswer();
        }
    }
    
    private void OnCorrectAnswer()
    {
        
    }

    private void OnIncorrectAnswer()
    {
        
    }
}
