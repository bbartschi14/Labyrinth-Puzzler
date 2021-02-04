using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class FloorsController : MonoBehaviour
{
    [SerializeField] private List<GameObject> floorPrefabs = new List<GameObject>();
    [SerializeField] private int currentFloor;
    [SerializeField] private UnityEvent<int> moveEvent;
    private GameObject currentGO;

    private void Start()
    {
    }

    private void LoadFloor(int floor)
    {
        currentFloor = floor;
        currentGO = Instantiate(floorPrefabs[floor], transform);
        currentGO.transform.Translate(Vector3.up*10f);
        LeanTween.moveY(currentGO, 0f, 1f).setEaseOutCubic();
    }

    public void ChangeToFloor(int floor)
    {
        Debug.Log("Moving to next floor " + floor);
        moveEvent.Invoke(floor);
        currentFloor = floor;
        floorPrefabs[floor].SetActive(true);
        LeanTween.moveY(gameObject, -60f*floor, 1f).
            setEaseOutCubic()
            .setOnComplete(_ => floorPrefabs[floor-1].SetActive(false));
        
    }

    public void NextFloor()
    {
        ChangeToFloor(currentFloor+1);
    }
}
