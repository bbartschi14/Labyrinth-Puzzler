using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickableCauldron : Clickable
{
    [Header("Animation Properties")] 
    [SerializeField] private float rotateTo;
    [SerializeField] private AnimationCurve curve;
    [SerializeField] private GameObject cauldronParticles;
    public override void Clicked()
    {
        cauldronParticles.SetActive(true);
        LeanTween.rotate(gameObject, Vector3.up * rotateTo, 1f).setEase(curve)
            .setOnComplete(AnimationEnd);
    }
}
