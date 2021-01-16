using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Puzzle : ScriptableObject
{
    public int doorId;
    public abstract GameObject DisplayPuzzle(GameObject panel);
}

public enum PuzzleType
{
    Unscramble,
    WordSearch
}