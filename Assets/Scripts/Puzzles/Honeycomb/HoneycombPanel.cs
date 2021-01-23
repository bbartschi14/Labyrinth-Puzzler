using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;
public class HoneycombPanel : MonoBehaviour
{
    [SerializeField] private TMPro.TextMeshProUGUI clueTextField;
    [SerializeField] private GameObject letterContainer;
    [SerializeField] private GameObject letterButtonPrefab;
    [SerializeField] private TMPro.TextMeshProUGUI  inputText;
    [SerializeField] private Button enterButton;
    [SerializeField] private Button eraseButton;
    [SerializeField] private GameObject wordContainer;
    [SerializeField] private GameObject wordPrefab;
    [SerializeField] private IntGameEvent onPuzzleCorrect;

    private Honeycomb puzzle;
    private List<string> wordsFound = new List<string>();

    public void FormatPanel(Honeycomb puzzle)
    {
        this.puzzle = puzzle;
        Debug.Log("Formatting");
        foreach (char c in puzzle.letters)
        {
            Debug.Log(c);
            GameObject letterButton = Instantiate(letterButtonPrefab, letterContainer.transform);
            letterButton.SetActive(true);
            HoneycombButton button = letterButton.GetComponent<HoneycombButton>();
            button.SetupButton(c, inputText);

        }
        
        eraseButton.OnClickAsObservable()
            .Subscribe(_ =>
            {
                if (inputText.text.Length > 0)
                {
                    inputText.text = inputText.text.Substring(0, inputText.text.Length - 1);
                }
            })
            .AddTo(this);
        
        enterButton.OnClickAsObservable()
            .Subscribe(_ =>
            {
                CheckWord();
            })
            .AddTo(this);
    }

    private void CheckWord()
    {
        if (puzzle.answers.Contains(inputText.text) && !wordsFound.Contains(inputText.text))
        {
            GameObject newWord = Instantiate(wordPrefab, wordContainer.transform);
            newWord.SetActive(true);
            newWord.GetComponent<TMPro.TextMeshProUGUI>().text = inputText.text;
            wordsFound.Add(inputText.text);
        }
        inputText.text = "";

        CheckPuzzle();
    }

    private void CheckPuzzle()
    {
        if (wordsFound.Count >= 3)
        {
            OnCorrectAnswer();
        } else
        {
            OnIncorrectAnswer();
        }
    }
    
    private void OnCorrectAnswer()
    {
        onPuzzleCorrect.Raise(puzzle.doorId);
    }

    private void OnIncorrectAnswer()
    {
       
    }
}
