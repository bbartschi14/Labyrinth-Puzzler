using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "Events/Transform Game Event")]

public class TransformGameEvent : ScriptableObject
{
    private readonly List<TransformGameEventListener> eventListeners =
        new List<TransformGameEventListener>();

    public void Raise(Transform t)
    {
        for (int i = eventListeners.Count - 1; i >= 0; i--)
        {
            eventListeners[i].OnEventRaised(t);
        }
    }

    public void RegisterListener(TransformGameEventListener listener)
    {
        if (!eventListeners.Contains(listener))
            eventListeners.Add(listener);
    }

    public void UnregisterListener(TransformGameEventListener listener)
    {
        if (eventListeners.Contains(listener))
            eventListeners.Remove(listener);
    }
}