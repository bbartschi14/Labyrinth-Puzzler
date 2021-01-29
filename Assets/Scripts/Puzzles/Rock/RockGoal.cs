using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockGoal : MonoBehaviour
{
    [SerializeField] public Vector2 position;
    [SerializeField] public int goalID;
    [SerializeField] public int doorID;

    private bool solved = false;
    public void Complete()
    {
        Debug.Log("Goal #" + goalID + " complete");
        Renderer renderer = GetComponent<Renderer>();
        renderer.material.color = Color.white;

        solved = true;
    }

    public bool IsSolved()
    {
        return solved;
    }
}
