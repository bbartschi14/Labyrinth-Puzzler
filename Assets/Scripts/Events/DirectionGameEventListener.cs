using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DirectionGameEventListener : MonoBehaviour
{
    [Tooltip("Event to register with.")]
    public DirectionGameEvent Event;

    [Tooltip("Response to invoke when Event is raised.")]
    public UnityEvent<Direction> Response;

    private void OnEnable()
    {
        Event.RegisterListener(this);
    }

    private void OnDisable()
    {
        Event.UnregisterListener(this);
    }

    public void OnEventRaised(Direction dir)
    {
        Response.Invoke(dir);
    }
}