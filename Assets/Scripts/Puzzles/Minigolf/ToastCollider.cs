using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToastCollider : MonoBehaviour
{
    [SerializeField] private StringGameEvent snackbar;
    [SerializeField] private string toast;
    private void OnTriggerEnter(Collider other)
    {
        if (!other.isTrigger)
        {
            Debug.Log("Toast raised");

            snackbar.Raise(toast);
        }
    }
}
