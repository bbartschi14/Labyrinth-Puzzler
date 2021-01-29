using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Puzzle : ScriptableObject
{
    public int doorId;
    public StringGameEvent snackBarEvent;
    public abstract GameObject DisplayPuzzle(GameObject panel);
    public abstract void SetupPuzzle();
    public abstract void ShowToast();
}

public enum PuzzleType
{
    Unscramble,
    WordSearch,
    Minigolf
}