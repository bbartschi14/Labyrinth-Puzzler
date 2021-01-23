using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "Events/Vector2 Game Event")]

public class Vector2GameEvent : ScriptableObject
{
    private readonly List<Vector2GameEventListener> eventListeners =
        new List<Vector2GameEventListener>();

    public void Raise(Vector2 vec2)
    {
        for (int i = eventListeners.Count - 1; i >= 0; i--)
        {
            eventListeners[i].OnEventRaised(vec2);
        }
    }

    public void RegisterListener(Vector2GameEventListener listener)
    {
        if (!eventListeners.Contains(listener))
            eventListeners.Add(listener);
    }

    public void UnregisterListener(Vector2GameEventListener listener)
    {
        if (eventListeners.Contains(listener))
            eventListeners.Remove(listener);
    }

}