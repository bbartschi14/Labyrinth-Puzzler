using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;

public class UnscramblePanel : MonoBehaviour
{
    [SerializeField] private TMPro.TextMeshProUGUI clueTextField;
    [SerializeField] private UnscrambleAnswer answerField;
    [SerializeField] private GameObject lettersContainer;
    [SerializeField] private GameObject letterButton;
    [SerializeField] private GameObject hintButton;
    [SerializeField] private Button eraseButton;
    [SerializeField] private IntVariable magicCount;
    [SerializeField] private IntGameEvent onPuzzleCorrect;
    
    private Unscramble puzzle;
    private List<int> scrambleIndices;
    private List<Transform> letterButtons = new List<Transform>();
    private UnscrambleButton missingButton;

    public void FormatPanel(Unscramble puzzle)
    {
        this.puzzle = puzzle;
        puzzle.answer = puzzle.answer.ToUpper();
        clueTextField.text = "\"" + puzzle.clue + "\"";
        ResetAnswerField();
        List<int> shuffledIndices = Shuffle(puzzle.answer);
        scrambleIndices = shuffledIndices;
        bool removed = false;
        
        foreach (int i in shuffledIndices)
        {
            char c = puzzle.answer[i];
            GameObject button = Instantiate(letterButton, lettersContainer.transform);
            button.SetActive(true);
            UnscrambleButton puzzleButton = button.GetComponent<UnscrambleButton>();
            puzzleButton.SetupButton(c, answerField);
            puzzleButton.Resize(puzzle.answer.Length);
            letterButtons.Add(puzzleButton.transform);

            if (puzzle.isMissingLetter && puzzle.missingLetter == c && !removed)
            {
                removed = true;
                puzzleButton.ToggleAvailable(false);
                missingButton = puzzleButton;
            }
        }

        eraseButton.OnClickAsObservable()
            .Subscribe(_ =>
            {
                answerField.RemoveLetter();
            })
            .AddTo(this);
        
        if (puzzle.isMissingLetter)
        {
            CheckForMissingLetter();
        }
    }

    private void CheckForMissingLetter()
    {
        GameObject tileNeeded = ItemContainer.Instance.GetTile(puzzle.missingLetter);
        if (tileNeeded != null)
        {
            ItemContainer.Instance.RemoveTile(tileNeeded);
            puzzle.isMissingLetter = false; 
            Image im = missingButton.GetComponent<Image>();
            Color toColor = im.color;
            LeanTween.value(gameObject, color => im.color = color, Color.green, toColor, .8f)
                .setEaseOutBounce().setOnComplete(_ =>
                {
                    missingButton.ToggleAvailable(true);
                    LeanTween.value(gameObject, color => im.color = color, Color.green, toColor, .6f)
                        .setEaseOutCubic();
                });        }
    }

    private void UseHint()
    {
        if (magicCount.Value > 1)
        {
            magicCount.ApplyChange(-2);
            foreach (var t in  letterButtons)
            {
                if (t.gameObject.activeSelf)
                {
                    t.parent = null;
                }
            }
            
            for (int i = 0; i < puzzle.answer.Length; i++)
            {
                for (int j = 0; j < scrambleIndices.Count; j++)
                {
                    if (scrambleIndices[j] == i)
                    {
                        letterButtons[j].parent = lettersContainer.transform;
                        Image im = letterButtons[j].GetComponent<Image>();
                        LeanTween.value(this.gameObject, val => im.color = val,
                            Color.green, im.color, .35f).setEaseInCubic();
                    }
                }
                
            }
        }
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
        float newSize = 1.2f * answerField.answerText.fontSize;
        answerField.answerText.color = Color.green;
        LeanTween.value(this.gameObject, size => answerField.answerText.fontSize = size, newSize,
            answerField.answerText.fontSize, .35f)
            .setEase(LeanTweenType.easeOutBounce)
            .setOnComplete(_ => onPuzzleCorrect.Raise(puzzle.doorId));
    }

    private void OnIncorrectAnswer()
    {
        LeanTween.value(this.gameObject, WrongAnswerColor, Color.red, answerField.answerText.color, .35f)
            .setOnComplete(ResetAnswerField);
    }

    private void WrongAnswerColor(Color val)
    {
        answerField.answerText.color = val;
    }
    
    private void ResetAnswerField()
    {
        answerField.InitializeBlank(puzzle.answer.Length);
    }
    
    private List<int> Shuffle(String str)
    {
        //string result = "";
        List<int> usedIndices = new List<int>();
        for (int i = 0; i < str.Length; i++)
        {
            int index = UnityEngine.Random.Range(0, str.Length);
            while (usedIndices.Contains(index))
            {
                index = UnityEngine.Random.Range(0, str.Length);
            }
            usedIndices.Add(index);
            //result += str[index];
        }

        return usedIndices;

    }
    
    
    
}
