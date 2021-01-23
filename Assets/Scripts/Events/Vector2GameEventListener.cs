using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class Vector2GameEventListener : MonoBehaviour
{
    [Tooltip("Event to register with.")]
    public Vector2GameEvent Event;

    [Tooltip("Response to invoke when Event is raised.")]
    public UnityEvent<Vector2> Response;

    private void OnEnable()
    {
        Event.RegisterListener(this);
    }

    private void OnDisable()
    {
        Event.UnregisterListener(this);
    }

    public void OnEventRaised(Vector2 vec2)
    {
        Response.Invoke(vec2);
    }
}