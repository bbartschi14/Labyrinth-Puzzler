using System;
using System.Collections;
using System.Collections.Generic;
using UniRx.Triggers;
using UnityEngine;

[CreateAssetMenu(menuName = "Puzzles/Minigolf")]
public class Minigolf : Puzzle
{
    public GameObject minigolfPrefab;
    public bool requireStrokeCount;
    public int maxStrokeCount;
    public override GameObject DisplayPuzzle(GameObject panel)
    {
        GameObject puzzleObj = Instantiate(minigolfPrefab, panel.transform);
        puzzleObj.GetComponent<GolfPanel>().FormatPanel(this);
        return puzzleObj;
    }

    public override void SetupPuzzle()
    {
    }
    
    public override void ShowToast()
    {
        
    }
    
}