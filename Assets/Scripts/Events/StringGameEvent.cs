using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "Events/String Game Event")]

public class StringGameEvent : ScriptableObject
{
    private readonly List<StringGameEventListener> eventListeners =
        new List<StringGameEventListener>();

    public void Raise(string str)
    {
        for (int i = eventListeners.Count - 1; i >= 0; i--)
        {
            eventListeners[i].OnEventRaised(str);
        }
    }

    public void RegisterListener(StringGameEventListener listener)
    {
        if (!eventListeners.Contains(listener))
            eventListeners.Add(listener);
    }

    public void UnregisterListener(StringGameEventListener listener)
    {
        if (eventListeners.Contains(listener))
            eventListeners.Remove(listener);
    }

}