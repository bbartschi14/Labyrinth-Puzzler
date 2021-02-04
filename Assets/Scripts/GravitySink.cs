using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravitySink : MonoBehaviour
{
    private Rigidbody obj;
    private Transform objT;
    [SerializeField] private float strength = 8f;
    

    private void OnTriggerStay(Collider other)
    {
        if (!other.isTrigger)
        {
            obj = other.gameObject.GetComponent<Rigidbody>();
            objT = other.transform;
            if (obj != null)
            {
                obj.AddForce((transform.position-objT.position)*strength);
            }
        }
        

    }

    private void OnTriggerExit(Collider other)
    {
        obj = null;
        objT = null;
    }
}
