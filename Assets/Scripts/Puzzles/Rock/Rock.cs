using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Puzzles/Rock")]
public class Rock : Puzzle
{
    public GameObject rockPrefab;
    public string color;
    public override GameObject DisplayPuzzle(GameObject panel)
    {
        GameObject puzzleObj = Instantiate(rockPrefab, panel.transform);
        puzzleObj.GetComponent<RockPanel>().FormatPanel(this);
        return puzzleObj;
    }

    public override void SetupPuzzle()
    {
    }
    
    public override void ShowToast()
    {
        
    }
}