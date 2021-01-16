using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Clickable : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    public Material material;
    public MeshRenderer renderer;

    public ClickableType type;
    public GameObject rotationPoint;
    public AnimationCurve curve;
    public bool flipDirection = false;
    public GameObject potionPrefab;
    public Transform spawnPoint;
    public AnimationCurve spawnCurve;
    public Vector3 potionOffset;
    public GameObject particles;
    private bool interactable = true;

    private int itemsCount = 1;
    
    public void OnPointerClick(PointerEventData eventData)
    {
        /*if (interactable)
        {
            if (type == ClickableType.Box)
            {
                BoxClicked();
            } 
        }*/
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (interactable)
        {
            renderer.material.SetColor("_GlowColor", Color.white);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        renderer.material = material;
    }

    public void OnTriggerEnter(Collider other)
    {
        if (interactable)
        {
            if (type == ClickableType.Box)
            {
                renderer.material.SetColor("_GlowColor", Color.white);
                BoxClicked();
            }else if (type == ClickableType.Pot)
            {
                renderer.material.SetColor("_GlowColor", Color.white);
                PotHit();
            }
        }
    }

    private void BoxClicked()
    {
        interactable = false;
        int flip = flipDirection ? 1 : -1;
        LeanTween.rotate(rotationPoint, Vector3.forward * -20f * flip, 1f).setEase(curve)
            .setOnComplete(AnimationEnd);
        if ( itemsCount > 0)
        {
            SpawnPotion();
            itemsCount -= 1;
        }
    }

    private void PotHit()
    {
        interactable = false;
        LeanTween.rotate(gameObject, Vector3.up * -20f, 1f).setEase(curve)
            .setOnComplete(AnimationEnd);
        particles.SetActive(true);
        if ( itemsCount > 0)
        {
            SpawnPotion();
            itemsCount -= 1;
        }
    }

    private void AnimationEnd()
    {
        interactable = true;
        renderer.material = material;
    }

    private void SpawnPotion()
    {
        GameObject potion = Instantiate(potionPrefab, spawnPoint);
        potion.transform.position = transform.position + potionOffset;
        potion.GetComponent<Potion>().StartAnimating();
        LeanTween.moveX(potion, spawnPoint.position.x, .25f);
        LeanTween.moveZ(potion, spawnPoint.position.z, .25f);
        LeanTween.moveY(potion, 2f, .25f).setEase(spawnCurve);


    }
}

public enum ClickableType
{
    Box,
    Table,
    Pot
}

