using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;
using UnityEngine.EventSystems;
using UniRx.Triggers;
using UniRx;
public class RockObject : UIBehaviour, IDragHandler
{
    [SerializeField] public Vector2 position;
    [SerializeField] public int rockID;

    [SerializeField] private LineRenderer arrow;
    [SerializeField] private float lineLength;
    [SerializeField] private RockArea area;
    [SerializeField] private GameObject sphere;
    private Camera cam;
    private Vector2 direction;
    private bool interactable = true;
    private void Start()
    {
        cam = Camera.main;
        
        this.OnBeginDragAsObservable().Subscribe(_ =>
            {
                if (interactable) arrow.enabled = true;
            }
        ).AddTo(this);
        
        this.OnDragAsObservable().Subscribe(pointer =>
        {
            if (interactable)
            {
                var point = pointer.position;
                UpdateArrow(new Vector3(point.x, point.y, 0f));
            }
        }).AddTo(this);

        this.OnEndDragAsObservable().Subscribe(_ =>
        {
            if (interactable)
            {
                arrow.enabled = false;
                Move();
            }
        }).AddTo(this);
    }

    private void Move()
    {
        Debug.Log("Moving in " + direction + " from " + position);
        Vector2 newPos = area.GetNewPosition(position, direction);
        Vector2 delta = newPos - position;
        position = newPos;
        Vector3 finalPos = new Vector3(delta.x * area.gridSize + transform.localPosition.x,
            transform.localPosition.y,
            delta.y * area.gridSize + transform.localPosition.z);
        interactable = false;
        float timeFactor = .3f;
        float time = Math.Abs(delta.x * timeFactor + Math.Abs(delta.y * timeFactor));
        Rotate(time);
        LeanTween.moveLocal(gameObject, finalPos, time).setOnComplete(_ =>
        {
            interactable = true;
            area.CheckSolved(this);
        });
    }

    private void Rotate(float time)
    {
        float turnSpeed = 450f;
        Vector3 axis = transform.right;
        if (direction == Vector2.right)
        {
            axis = -transform.forward;
        } else if (direction == Vector2.left)
        {
            axis = transform.forward;
        } else if (direction == Vector2.down)
        {
            axis = -transform.right;
        }

        LeanTween.rotateAround(sphere, axis, time*turnSpeed, time);

    }

    public void SetInteractable(bool value)
    {
        interactable = value;
    }
    
    private void UpdateArrow(Vector3 mousePos)
    {
        arrow.SetPosition(0,transform.position);
        Ray ray = cam.ScreenPointToRay(mousePos);
        Plane hPlane = new Plane(Vector3.up , Vector3.zero);
        float distance = 0; 
        if (hPlane.Raycast(ray, out distance)){
            Vector3 endPoint = ray.GetPoint(distance) - new Vector3(transform.position.x, 0f, transform.position.z);
            direction = GetDirection(endPoint);
            arrow.SetPosition(1,
                transform.position + new Vector3(direction.x, 0f, direction.y) * lineLength);
        }
    }

    // Angle to horizontal = inverse tangent of O/A
    private Vector2 GetDirection(Vector3 point)
    {
        float angle = Mathf.Atan(point.z / point.x) * Mathf.Rad2Deg;
        if (angle >= 0)
        {
            if (point.x < 0)
            {
                angle += 180f;
            }
        }
        else
        {
            if (point.x < 0)
            {
                angle += 180f;
            }
            else
            {
                angle += 360f;
            }
        }
        List<float> cutoffs = new List<float>() { 0f, 45f, 135f, 225f, 315f, 360f };
        List<Vector2> directions = new List<Vector2>() {Vector2.right, Vector2.up, Vector2.left, Vector2.down};
        for (int i = 0; i < directions.Count; i++)
        {
            if (angle >= cutoffs[i] && angle <= cutoffs[i + 1])
            {
                return directions[i];
            } 
        }
        
        return Vector2.right;

    } 
    
    public void OnDrag(PointerEventData eventData)
    {
    }
}
