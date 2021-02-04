using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Collider))]
public class MinigolfHole : MonoBehaviour
{
    public int doorId;
    public IntGameEvent onPuzzleCorrect;
    public bool isTreasure;
    public int maxStrokes;
    private void OnTriggerEnter(Collider other)
    {
        if (!other.isTrigger)
        {
            if (isTreasure)
            {
                if (other.GetComponent<GolfBallController>().GetStrokes() <= maxStrokes)
                {
                    onPuzzleCorrect.Raise(doorId);
                }
            } else
            {
                onPuzzleCorrect.Raise(doorId);
            }
        }
    }

    public void SetId(int i)
    {
        doorId = i;
    }
}
