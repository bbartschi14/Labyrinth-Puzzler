using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "Events/Puzzle Game Event")]
public class PuzzleGameEvent : ScriptableObject
{
    private readonly List<PuzzleGameEventListener> eventListeners =
        new List<PuzzleGameEventListener>();

    public void Raise(Puzzle puzzle)
    {
        for (int i = eventListeners.Count - 1; i >= 0; i--)
        {
            eventListeners[i].OnEventRaised(puzzle);
        }
    }

    public void RegisterListener(PuzzleGameEventListener listener)
    {
        if (!eventListeners.Contains(listener))
            eventListeners.Add(listener);
    }

    public void UnregisterListener(PuzzleGameEventListener listener)
    {
        if (eventListeners.Contains(listener))
            eventListeners.Remove(listener);
    }
}
