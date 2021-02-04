using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GolfballRespawner : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (!other.isTrigger)
        {
            GolfBallController gb = other.gameObject.GetComponent<GolfBallController>();
            if (gb != null)
            {
                gb.ResetPosition();
            }
        }
        
    }
}
