using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "Puzzles/Word Search")]
public class WordSearch : Puzzle
{
    public GameObject wordSearchPrefab;
    public string wordToFind;
    public override GameObject DisplayPuzzle(GameObject panel)
    {
        GameObject puzzleObj = Instantiate(wordSearchPrefab, panel.transform);
        WordSearchPanel panelComponent = puzzleObj.GetComponent<WordSearchPanel>();
        panelComponent.FormatPanel(this);
        return puzzleObj;
    }
}
