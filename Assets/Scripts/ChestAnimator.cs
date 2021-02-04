using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestAnimator : MonoBehaviour
{
    [SerializeField] private GameObject lid;

    [SerializeField] private ParticleSystem goldParticles;

    public void Animate()
    {
        LeanTween.rotateAround(lid, lid.transform.right, -70f,3f).setEaseOutCubic();
        goldParticles.gameObject.SetActive(true);
    }
}
