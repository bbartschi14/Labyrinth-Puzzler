using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickableBox : Clickable
{
    [Header("Animation Properties")] 
    [SerializeField] private GameObject rotationPoint;
    [SerializeField] private float rotateTo;
    [SerializeField] private AnimationCurve curve;

    public override void Clicked()
    {
        LeanTween.rotate(rotationPoint, Vector3.forward * rotateTo, 1f).setEase(curve)
            .setOnComplete(AnimationEnd);
    }
}
