using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour
{
    [SerializeField] private PuzzleGameEvent onPuzzleActivated;
    private Puzzle puzzle;
    [SerializeField] private int doorId;
    [SerializeField] private Material glowMat;
    [SerializeField] private DoorController linkedDoor;
    [SerializeField] private TransformGameEvent onTransportCalled;
    [SerializeField] private GameEvent onPortalExit;
    [SerializeField] private PuzzleType puzzleType;
    private bool isOpen = false;
    private void Start()
    {
        puzzle = PuzzleGenerator.Instance.GeneratePuzzle(puzzleType);
        puzzle.doorId = doorId;
    }

    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log(other.gameObject.name + " has enter trigger of " + gameObject.name);
        if (!isOpen)
        {
            onPuzzleActivated.Raise(this.puzzle);
        }
        else
        {
            if (linkedDoor != null)
            {
                Transport();
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (isOpen)
        {
            onPortalExit.Raise();
        }
    }

    public void OpenDoor(int i)
    {
        if (i == doorId)
        {
            Debug.Log("Door " + i + " opened!");
            ForceOpen();
            if (linkedDoor != null) Transport();
        }
    }


    public void ForceOpen()
    {
        GetComponent<Renderer>().material = glowMat;
        isOpen = true;
    }

    private void Transport()
    {
        linkedDoor.ForceOpen();
        onTransportCalled.Raise(linkedDoor.transform);
    }
}
