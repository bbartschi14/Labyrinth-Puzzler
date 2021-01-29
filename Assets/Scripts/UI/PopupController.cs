using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopupController : MonoBehaviour
{
   [SerializeField] private GameObject puzzlePanel;
   [SerializeField] private GameObject displayArea;
   [SerializeField] private GameObject closePanel;
   [SerializeField] private RectTransform panelRt;
   private GameObject currentSpawnedPuzzle;
   private bool closing = false;
   public void StartPuzzle(Puzzle puzzle)
   {
      if (closing)
      {
         LeanTween.cancel(panelRt);
         CompleteClose();
      }
      puzzlePanel.SetActive(true);
      currentSpawnedPuzzle = puzzle.DisplayPuzzle(displayArea);
      puzzle.ShowToast();
      panelRt.anchoredPosition = new Vector2(panelRt.anchoredPosition.x, 1000f);
      LeanTween.move(panelRt, new Vector3(panelRt.anchoredPosition.x, 0f),.5f)
         .setEaseOutQuart();
   }

   public void EndPuzzle()
   {
      if (puzzlePanel.activeSelf)
      {
         closing = true;
         closePanel.SetActive(false);
         LeanTween.move(panelRt, new Vector3(panelRt.anchoredPosition.x, 1000f),.5f)
            .setEaseOutQuart()
            .setOnComplete(_ =>
            {
               CompleteClose();
            });
      }
   }

   private void CompleteClose()
   {
      puzzlePanel.SetActive(false);
      if (currentSpawnedPuzzle != null)
      {
         currentSpawnedPuzzle.transform.parent = null;
         closePanel.SetActive(true);
         Destroy(currentSpawnedPuzzle);
         closing = false;
      }
   }
}
