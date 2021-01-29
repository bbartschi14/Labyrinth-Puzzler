using System;
using System.Collections;
using System.Collections.Generic;
using UniRx.Triggers;
using UnityEngine;

[CreateAssetMenu(menuName = "Puzzles/Minigolf")]
public class Minigolf : Puzzle
{
    public GameObject minigolfPrefab;

    public override GameObject DisplayPuzzle(GameObject panel)
    {
        GameObject puzzleObj = Instantiate(minigolfPrefab, panel.transform);
        return puzzleObj;
    }

    public override void SetupPuzzle()
    {
    }
    
    public override void ShowToast()
    {
        
    }
    
}