using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Events/Direction Game Event")]
public class DirectionGameEvent : ScriptableObject
{
    private readonly List<DirectionGameEventListener> eventListeners =
        new List<DirectionGameEventListener>();

    public void Raise(Direction dir)
    {
        for (int i = eventListeners.Count - 1; i >= 0; i--)
        {
            eventListeners[i].OnEventRaised(dir);
        }
    }

    public void RegisterListener(DirectionGameEventListener listener)
    {
        if (!eventListeners.Contains(listener))
            eventListeners.Add(listener);
    }

    public void UnregisterListener(DirectionGameEventListener listener)
    {
        if (eventListeners.Contains(listener))
            eventListeners.Remove(listener);
    }

}
