using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MinigolfReset : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private GolfBallController gb;

    public void OnPointerClick(PointerEventData eventData)
    {
        gb.ResetPosition();
    }
}
