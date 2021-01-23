using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopupController : MonoBehaviour
{
   [SerializeField] private GameObject puzzlePanel;
   private GameObject currentSpawnedPuzzle;
   public void StartPuzzle(Puzzle puzzle)
   {
      puzzlePanel.SetActive(true);
      currentSpawnedPuzzle = puzzle.DisplayPuzzle(puzzlePanel.transform.GetChild(2).gameObject);
   }

   public void EndPuzzle()
   {
      puzzlePanel.SetActive(false);
      if (currentSpawnedPuzzle != null)
      {
         currentSpawnedPuzzle.transform.parent = null;
         Destroy(currentSpawnedPuzzle);
      }

      
   }
}
