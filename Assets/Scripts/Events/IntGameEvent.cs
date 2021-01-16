using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "Events/Int Game Event")]

public class IntGameEvent : ScriptableObject
{
    private readonly List<IntGameEventListener> eventListeners =
           new List<IntGameEventListener>();

    public void Raise(int number)
    {
        for (int i = eventListeners.Count - 1; i >= 0; i--)
        {
            eventListeners[i].OnEventRaised(number);
        }
    }

    public void RegisterListener(IntGameEventListener listener)
    {
        if (!eventListeners.Contains(listener))
            eventListeners.Add(listener);
    }

    public void UnregisterListener(IntGameEventListener listener)
    {
        if (eventListeners.Contains(listener))
            eventListeners.Remove(listener);
    }

}