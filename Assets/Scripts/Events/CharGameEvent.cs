using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "Events/Char Game Event")]
public class CharGameEvent : ScriptableObject
{
    private readonly List<CharGameEventListener> eventListeners =
        new List<CharGameEventListener>();

    public void Raise(char c)
    {
        for (int i = eventListeners.Count - 1; i >= 0; i--)
        {
            eventListeners[i].OnEventRaised(c);
        }
    }

    public void RegisterListener(CharGameEventListener listener)
    {
        if (!eventListeners.Contains(listener))
            eventListeners.Add(listener);
    }

    public void UnregisterListener(CharGameEventListener listener)
    {
        if (eventListeners.Contains(listener))
            eventListeners.Remove(listener);
    }

}