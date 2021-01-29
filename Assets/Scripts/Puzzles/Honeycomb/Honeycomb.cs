using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "Puzzles/Honeycomb")]
public class Honeycomb : Puzzle
{
    public GameObject honeycombPrefab;
    public string letters;
    public List<string> answers;

    public override GameObject DisplayPuzzle(GameObject panel)
    {
        GameObject puzzleObj = Instantiate(honeycombPrefab, panel.transform);
        HoneycombPanel panelComponent = puzzleObj.GetComponent<HoneycombPanel>();
        panelComponent.FormatPanel(this);
        return puzzleObj;
    }

    public override void SetupPuzzle()
    {
    }
    
    public override void ShowToast()
    {
        
    }
}
