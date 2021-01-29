using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;

public class LadderController : Clickable
{
    [SerializeField] private PuzzleGameEvent onPuzzleActivated;
    [SerializeField] private int doorId;
    [SerializeField] private GameEvent gameOverEvent;
    [SerializeField] private GameObject lockPrefab;
    [SerializeField] private int levelConnection;
    [SerializeField] private IntGameEvent onAdvanceLevelEvent;
    [SerializeField] private StringGameEvent snackBarEvent;
    [Header("Puzzle Settings")]
    [SerializeField] private bool useSpecific;
    [SerializeField] private Puzzle specificPuzzle;
    [SerializeField] private PuzzleType puzzleType;
    
    [SerializeField ]private bool isOpen;
    private Puzzle puzzle;
    private GameObject lockObject;

    private void Start()
    {
        Debug.Log(levelConnection);
        puzzle = PuzzleGenerator.Instance.GeneratePuzzle(puzzleType, useSpecific, specificPuzzle);
        puzzle.doorId = doorId;
        puzzle.SetupPuzzle();
        if (!isOpen)
        {
            lockObject = Instantiate(lockPrefab, transform);
            lockObject.transform.position = transform.position;
            lockObject.transform.Translate(new Vector3(.5f, 1.25f, 0f));
            LeanTween.moveLocalY(lockObject, 1f, 1f).setLoopPingPong().setEaseInBack();  
        }
       
    }

    public override void Clicked()
    {
        if (!isOpen)
        {
            snackBarEvent.Raise("Unlocking this ladder opens up a new floor!");

            onPuzzleActivated.Raise(this.puzzle);
        }
        else
        {
            Transport();
        }
    }

    public void Transport()
    {
        Debug.Log("Transporting " + levelConnection);
        onAdvanceLevelEvent.Raise(levelConnection);
    }

    public void OpenChest(int i)
    {
        if (i == doorId)
        {
            if (lockObject != null) LeanTween.scale(lockObject, Vector3.zero, .15f)
                .setEaseInCubic().setOnComplete(_ => Destroy(lockObject));
            isOpen = true;
            gameOverEvent.Raise();

        }
    }

    
    public override void StartClick()
    {
    }
}
