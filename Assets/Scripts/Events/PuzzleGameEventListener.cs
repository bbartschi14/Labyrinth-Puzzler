using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PuzzleGameEventListener : MonoBehaviour
{
    [Tooltip("Event to register with.")]
    public PuzzleGameEvent Event;

    [Tooltip("Response to invoke when Event is raised.")]
    public UnityEvent<Puzzle> Response;

    private void OnEnable()
    {
        Event.RegisterListener(this);
    }

    private void OnDisable()
    {
        Event.UnregisterListener(this);
    }

    public void OnEventRaised(Puzzle puzzle)
    {
        Response.Invoke(puzzle);
    }
}
