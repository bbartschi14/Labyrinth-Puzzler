using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Puzzles/Rock")]
public class Rock : Puzzle
{
    public GameObject rockPrefab;

    public override GameObject DisplayPuzzle(GameObject panel)
    {
        GameObject puzzleObj = Instantiate(rockPrefab, panel.transform);
        return puzzleObj;
    }

    public override void SetupPuzzle()
    {
    }
    
    public override void ShowToast()
    {
        
    }
}