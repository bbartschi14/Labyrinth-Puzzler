using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Clickable : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private Material material;
    [SerializeField] public Material nonGlowMaterial;
    [SerializeField] public MeshRenderer renderer;
    [SerializeField] private GameObject itemPrefab;
    [SerializeField] private IntVariable magicCount;
    [SerializeField] public int itemsCount = 1;

    private bool interactable = true;

    public void OnPointerClick(PointerEventData eventData)
    {
        if (!interactable) return;
        StartClick();
        Clicked();
    }

    public virtual void StartClick()
    {
        interactable = false;
        if ( itemsCount > 0)
        {
            SpawnItem();
            itemsCount -= 1;
        }
        if (itemsCount == 0)
        {
            renderer.material = nonGlowMaterial;
        }
    }
    public virtual void Clicked()
    {
        Debug.Log("Base clicked");
    }

    public void SpawnItem()
    {
        GameObject item = Instantiate(itemPrefab, transform.position, itemPrefab.transform.rotation);
        item.GetComponent<Item>().Collect();
        LeanTween.rotateAround(item, Vector3.up, 360, 1f).setLoopClamp();
        LeanTween.moveY(item, 3f, .5f).setEaseOutCubic().setOnComplete(_ =>
        {
            Destroy(item);
        });
        ;
    }
    
    public void AnimationEnd()
    {
        interactable = true;
        if (itemsCount > 0)
        {
            renderer.material = material;
        }

    }

    public void OnPointerEnter(PointerEventData eventData)
    {
    }
    public void OnPointerExit(PointerEventData eventData)
    {
    }

    
}


