using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class DoorController : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private PuzzleGameEvent onPuzzleActivated;
    [SerializeField] private int doorId;
    [SerializeField] private DirectionGameEvent onDirectionalMove;
    [SerializeField] private Direction direction;
    [Header("Visuals")]
    [SerializeField] private Material glowMat;
    [SerializeField] private Renderer renderer;
    [SerializeField] private DoorController linkedDoor;
    [SerializeField] private Color glowColor;
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
        LeanTween.moveY(lockObject, 1f, 1f).setLoopPingPong().setEaseInBack();
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
        if (lockObject != null) LeanTween.scale(lockObject, Vector3.zero, .15f)
            .setEaseInCubic().setOnComplete(_ => Destroy(lockObject));
        isOpen = true;
    }

    private void Transport()
    {
        linkedDoor.ForceOpen();
        onDirectionalMove.Raise(this.direction);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
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

    public void OnPointerEnter(PointerEventData eventData)
    {
        renderer.material.SetColor("_GlowColor", glowColor);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        renderer.material = glowMat;
    }
}
