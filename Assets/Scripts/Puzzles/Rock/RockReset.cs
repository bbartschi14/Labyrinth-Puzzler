using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class RockReset : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private RockArea area;
    public void OnPointerClick(PointerEventData eventData)
    {
        area.ResetAll();
    }
}
