using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class CharGameEventListener : MonoBehaviour
{
    [Tooltip("Event to register with.")]
    public CharGameEvent Event;

    [Tooltip("Response to invoke when Event is raised.")]
    public UnityEvent<char> Response;

    private void OnEnable()
    {
        Event.RegisterListener(this);
    }

    private void OnDisable()
    {
        Event.UnregisterListener(this);
    }

    public void OnEventRaised(char c)
    {
        Response.Invoke(c);
    }
}
