using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorsController : MonoBehaviour
{
    [SerializeField] private List<GameObject> floorPrefabs = new List<GameObject>();
    [SerializeField] private int currentFloor;
    private GameObject currentGO;

    private void Start()
    {
        LoadFloor(currentFloor);
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
        Debug.Log("Moving to floor " + floor);
        GameObject old = currentGO;
        LeanTween.moveY(old, -60f, 1f).
            setEaseOutCubic().
            setOnComplete(_ => Destroy(old));
        
        LoadFloor(floor);
    }

    public void NextFloor()
    {
        ChangeToFloor(currentFloor+1);
    }
}
