using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ChestController : Clickable
{
    [SerializeField] private PuzzleGameEvent onPuzzleActivated;
    [SerializeField] private int doorId;
    [SerializeField] private bool isFinalChest = false;
    [SerializeField] private GameEvent gameOverEvent;
    [SerializeField] private GameObject lockPrefab;
    
    [Header("Puzzle Settings")]
    [SerializeField] private bool useSpecific;
    [SerializeField] private Puzzle specificPuzzle;
    [SerializeField] private PuzzleType puzzleType;
    
    private bool isOpen = false;
    private Puzzle puzzle;
    private GameObject lockObject;

    private void Start()
    {
        puzzle = PuzzleGenerator.Instance.GeneratePuzzle(puzzleType, useSpecific, specificPuzzle);
        puzzle.doorId = doorId;
        puzzle.SetupPuzzle();
        lockObject = Instantiate(lockPrefab, transform);
        lockObject.transform.position = transform.position;
        lockObject.transform.Translate(new Vector3(-.5f, 1.25f, 0f));
        LeanTween.moveLocalY(lockObject, 1f, 1f).setLoopPingPong().setEaseInBack();
    }

    public override void Clicked()
    {
        if (!isOpen)
        {
            onPuzzleActivated.Raise(this.puzzle);
        }
        else
        {
            if ( itemsCount > 0)
            {
                SpawnItem();
                itemsCount -= 1;
            }
        }
    }

    public void OpenChest(int i)
    {
        if (i == doorId)
        {
            if (lockObject != null) LeanTween.scale(lockObject, Vector3.zero, .15f)
                .setEaseInCubic().setOnComplete(_ => Destroy(lockObject));
            isOpen = true;
            if (isFinalChest)
            {
                gameOverEvent.Raise();
            }
            if ( itemsCount > 0)
            {
                SpawnItem();
                itemsCount -= 1;
            }
        }
    }

    
    public override void StartClick()
    {
    }
}
