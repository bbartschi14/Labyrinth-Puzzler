using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Collider))]
public class MinigolfHole : MonoBehaviour
{
    public int doorId;
    public IntGameEvent onPuzzleCorrect;

    private void OnTriggerEnter(Collider other)
    {
        onPuzzleCorrect.Raise(doorId);
    }

    public void SetId(int i)
    {
        doorId = i;
    }
}
