using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UniRx.Triggers;
using UniRx;

public class GolfBallController : UIBehaviour, IDragHandler
{
    [SerializeField] private LineRenderer arrow;
    [SerializeField] private Rigidbody rb;
    [SerializeField] private float impulseStrength;
    [SerializeField] private float maxArrowLength = 2f;

    private Camera cam;

    private void Start()
    {
        cam = Camera.main;
        
        this.OnBeginDragAsObservable().Subscribe(_ => arrow.enabled = true).AddTo(this);
        
        this.OnDragAsObservable().Subscribe(pointer =>
        {
            var point = pointer.position;
            UpdateArrow(new Vector3(point.x, point.y, 0f));
        }).AddTo(this);
        
        this.OnEndDragAsObservable().Subscribe(_ =>
        {
            arrow.enabled = false;
            Launch();
        }).AddTo(this);
    }
    
    private void UpdateArrow(Vector3 mousePos)
    {
        arrow.SetPosition(0, this.transform.position);
        Ray ray = cam.ScreenPointToRay(mousePos);
        Plane hPlane = new Plane(Vector3.up , Vector3.zero + new Vector3(0f, .3f, 0f));
        float distance = 0; 
        if (hPlane.Raycast(ray, out distance)){
            arrow.SetPosition(1,ray.GetPoint(distance));
            if (Vector3.Distance(arrow.GetPosition(0), arrow.GetPosition(1)) > maxArrowLength)
            {
                Vector3 norm = this.transform.position + maxArrowLength * (arrow.GetPosition(1) - arrow.GetPosition(0)).normalized;
                arrow.SetPosition(1, norm);
            }
        }
    }
    
    private void Launch()
    {
        float strength = Vector3.Distance(arrow.GetPosition(1), arrow.GetPosition(0));
        Vector3 arrowForce = (arrow.GetPosition(1) - arrow.GetPosition(0)).normalized;
        arrowForce *= impulseStrength * strength;
        rb.AddForce(arrowForce[0], 0f, arrowForce[2], ForceMode.Impulse);
    }


    public void OnDrag(PointerEventData eventData)
    {
    }
}
