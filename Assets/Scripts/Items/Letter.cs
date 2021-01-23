using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Letter : Item
{
    [SerializeField] private char letter;
    [SerializeField] private CharGameEvent collectTileEvent;

    public override void Collect()
    {
        collectTileEvent.Raise(letter);
    }
}
