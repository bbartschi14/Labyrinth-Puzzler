using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "Puzzles/Floor Letter")]

public class FloorLetter : Puzzle
{
    public GameObject floorPanelPrefab;
    public string clue;
    public string answer;
    public override GameObject DisplayPuzzle(GameObject panel)
    {
        GameObject puzzleObj = Instantiate(floorPanelPrefab, panel.transform);
        FloorLetterPanel panelComponent = puzzleObj.GetComponent<FloorLetterPanel>();
        panelComponent.FormatPanel(this);
        return puzzleObj;
    }

    public override void SetupPuzzle()
    {
    }
}
