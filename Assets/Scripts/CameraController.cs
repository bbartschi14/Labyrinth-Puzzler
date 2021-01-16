using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : Singleton<CameraController>
{
    [SerializeField] private float gridSize;
    private Vector3 currentRoomPoint = new Vector3(8f, 0f, 8f);
    public void MoveToNewRoom(Transform newRoom)
    {
        Vector3 posDelta = newRoom.position - currentRoomPoint;
        int maxIndex = MaxOfVec3(posDelta);
        Vector3 delta = Vector3.zero;
        delta[maxIndex] = 1.0f * (posDelta[maxIndex]/Mathf.Abs(posDelta[maxIndex])); 
        Vector3 newPos = transform.position + delta * gridSize;
        currentRoomPoint += delta * gridSize;
        LeanTween.move(gameObject, newPos, .35f).setEaseInOutCubic();
    }

    private int MaxOfVec3(Vector3 vec)
    {
        float max = vec[0];
        int maxIndex = 0;
        for (int i = 1; i < 3; i++)
        {
            if (Mathf.Abs(vec[i]) > Mathf.Abs(max))
            {
                max = vec[i];
                maxIndex = i;
            }
        }

        return maxIndex;
    }
    
}


