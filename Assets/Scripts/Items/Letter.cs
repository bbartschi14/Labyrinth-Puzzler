using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Letter : Item
{
    [SerializeField] private char letter;
    [SerializeField] private CharGameEvent collectTileEvent;
    [SerializeField] private StringGameEvent snackbarEvent;

    public override void Collect()
    {
        collectTileEvent.Raise(letter);
        snackbarEvent.Raise("A letter! Use it to solve puzzles with missing letters");
    }
}
