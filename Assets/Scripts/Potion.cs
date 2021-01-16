using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Potion : MonoBehaviour
{
    [SerializeField] private Vector2 rotateTimeRange; 
    [SerializeField] private Vector2 bounceTimeRange; 
    [SerializeField] private IntVariable magicCount;

    public void StartAnimating()
    {
        LeanTween.rotateAround(gameObject, Vector3.up, 360, Random.Range(rotateTimeRange.x, rotateTimeRange.y)).setLoopClamp();
    }

    private void OnTriggerEnter(Collider other)
    {
        magicCount.ApplyChange(1);
        LeanTween.scale(gameObject, Vector3.zero, .15f).setEaseInCubic().setOnComplete(_ => Destroy(this.gameObject));
    }
    
}
