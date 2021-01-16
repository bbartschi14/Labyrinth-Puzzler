using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "Variables/Int Variable")]
public class IntVariable : ScriptableObject, ISerializationCallbackReceiver
{
#if UNITY_EDITOR
    [Multiline]
    public string DeveloperDescription = "";
#endif
    public int Value;

    public bool notifyChange;

    public UnityEvent onChangeEvent;

    public bool resetToInitialOnPlay;

    public int initialValue;
    public void SetValue(int value)
    {
        Value = value;
        OnChange();
    }

    public void SetValue(IntVariable value)
    {
        Value = value.Value;
        OnChange();
    }

    public void ApplyChange(int amount)
    {
        Value += amount;
        OnChange();
    }

    public void ApplyChange(IntVariable amount)
    {
        Value += amount.Value;
        OnChange();
    }

    private void OnChange()
    {
        if (notifyChange)
        {
            onChangeEvent.Invoke();
        }
    }


    public void OnAfterDeserialize()
    {
        Value = initialValue;
    }

    public void OnBeforeSerialize() { }

   
}