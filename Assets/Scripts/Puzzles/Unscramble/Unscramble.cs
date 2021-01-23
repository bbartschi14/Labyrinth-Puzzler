using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Puzzles/Unscramble")]
public class Unscramble : Puzzle
{
    public GameObject unscramblePrefab;
    public string clue;
    public string answer;

    public bool isMissingLetter;
    public char missingLetter;
    public override GameObject DisplayPuzzle(GameObject panel)
    {
        GameObject puzzleObj = Instantiate(unscramblePrefab, panel.transform);
        UnscramblePanel panelComponent = puzzleObj.GetComponent<UnscramblePanel>();
        panelComponent.FormatPanel(this);
        return puzzleObj;
    }

    public override void SetupPuzzle()
    {
    }
}
