using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = System.Random;
using UniRx;

public class WordSearchPanel : MonoBehaviour
{
    [SerializeField] private TMPro.TextMeshProUGUI clueText;
    [SerializeField] private GameObject hintButton;
    [SerializeField] private IntVariable magicCount;
    [SerializeField] private GameObject letterPrefab;
    [SerializeField] private GameObject searchArea;
    [SerializeField] private UILineRenderer line;
    [SerializeField] private IntGameEvent onPuzzleCorrect;

    private WordSearch puzzle;
    private Vector2Int linePoints;
    private Vector2Int solutionPoints;
    private List<Vector2Int> solution;
    private int size = 4;

    public void FormatPanel(WordSearch puzzle)
    {
        this.puzzle = puzzle;
        clueText.text += puzzle.wordToFind;
        string word = puzzle.wordToFind;

        for (int row = 0; row < size; row++)
        {
            for (int col = 0; col < size; col++)
            {
                GameObject tile = Instantiate(letterPrefab, searchArea.transform);
                tile.SetActive(true);
                tile.GetComponent<LayoutElement>().ignoreLayout = false;
                WordSearchTile tileComp = tile.GetComponent<WordSearchTile>();
                tileComp.SetText(RandomLetter());
                tileComp.Setup();
            }
        }
        
        List<Vector2Int> wordLocations = GetWordIndices();
        solution = wordLocations;
        while (wordLocations == null)
        {
            wordLocations = GetWordIndices();
        }

        int letterIndex = 0;
        foreach (Vector2Int loc in wordLocations)
        {
            //Debug.Log(loc);
            searchArea.transform.GetChild(CoordToIndex(loc)).GetComponent<WordSearchTile>().SetText(word[letterIndex]);
            letterIndex++;
        }
        solutionPoints = new Vector2Int(CoordToIndex(wordLocations[0]), CoordToIndex(wordLocations[wordLocations.Count - 1]));
        
        hintButton.GetComponent<Button>().OnClickAsObservable()
            .Subscribe(_ => UseHint())
            .AddTo(this);
    }

    public void CheckPuzzle()
    {
        //Debug.Log("Checking puzzle, Line Points: " + linePoints + " , solution:" + solutionPoints );
        if (linePoints.x == solutionPoints.x && linePoints.y == solutionPoints.y
            || linePoints.y == solutionPoints.x && linePoints.x == solutionPoints.y)
        {
            OnCorrectAnswer();
        } else
        {
            OnIncorrectAnswer();
        }
    }

    private void UseHint()
    {
        if (magicCount.Value > 1)
        {
            magicCount.ApplyChange(-2);
            foreach (var loc in solution)
            {
                searchArea.transform.GetChild(CoordToIndex(loc)).GetComponent<WordSearchTile>().SetTextColor(Color.green);
            }
        }
    }
    
    private void OnCorrectAnswer()
    {
        Image im = searchArea.GetComponent<Image>();
        LeanTween.value(this.gameObject, color => im.color = color, Color.green, im.color, .35f)
            .setEase(LeanTweenType.easeOutCubic)
            .setOnComplete(_ => onPuzzleCorrect.Raise(puzzle.doorId));
    }

    private void OnIncorrectAnswer()
    {
        Image im = searchArea.GetComponent<Image>();
        LeanTween.value(this.gameObject, color => im.color = color, Color.red, im.color, .35f)
            .setEase(LeanTweenType.easeOutCubic);
    }

    public void SetLine(bool val)
    {
        line.enabled = val;
    }

    public void UpdateLine(int startIndex, int endIndex)
    {
        // Check if vertical, horizontal, or diagonal line. Others are not valid
        Vector2Int firstCoord = IndexToCoord(startIndex);
        Vector2Int secondCoord = IndexToCoord(endIndex);
        int diff1 = Mathf.Abs(firstCoord.x - secondCoord.x);
        int diff2 = Mathf.Abs(firstCoord.y - secondCoord.y);
        if (diff1 != 0 && diff2 != 0)
        {
            if (diff1 != diff2) return;
        }
        
        // Update line position
        linePoints = new Vector2Int(startIndex, endIndex);
        List<Vector2> points = new List<Vector2>();
        points.Add(IndexToLineCoord(startIndex));
        points.Add(IndexToLineCoord(endIndex));
        line.points = points;
        line.SetAllDirty();
    }

    private List<Vector2Int> GetWordIndices()
    {
        List<Vector2Int> values = new List<Vector2Int>();
        Vector2Int startingPoint = new Vector2Int(UnityEngine.Random.Range(0,size),UnityEngine.Random.Range(0,size));
        values.Add(startingPoint);
        List<Vector2Int> offsets = new List<Vector2Int>() {Vector2Int.right,Vector2Int.down,Vector2Int.left,Vector2Int.up};
        foreach (Vector2Int offset in offsets)
        {
            if (TryWordDirection(startingPoint, offset))
            {
                for (int i = 1; i < puzzle.wordToFind.Length; i++)
                {
                    values.Add(startingPoint + offset * i);
                }
                return values;
            }
        }
        return null;
    }

    private bool TryWordDirection(Vector2Int start, Vector2Int direction)
    {
        //Debug.Log("Trying: " + direction + " from " + start);
        Vector2Int newVec = start + direction * (puzzle.wordToFind.Length - 1);
        //Debug.Log("Ending point: " + newVec);
        if (newVec.x >= size || newVec.y >= size || newVec.x < 0 || newVec.y < 0)
        {
            return false;
        }
        else
        {
            return true;
        }
    }
    private char RandomLetter()
    {
        string st = "abcdefghijklmnopqrstuvwxyz";
        return st[UnityEngine.Random.Range(0,st.Length)];
    }

    private int CoordToIndex(Vector2Int loc)
    {
        return loc.x + size*loc.y;
    }
    private Vector2 IndexToLineCoord(int i)
    {
        return new Vector2((i % size)*100f,(i / size)*-100f );
    }
    
    private Vector2Int IndexToCoord(int i)
    {
        return new Vector2Int((int)(i % size),(int)(i / size) );
    }
    
    
    
}
