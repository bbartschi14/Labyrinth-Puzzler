using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorMessages : MonoBehaviour
{
    [SerializeField] private List<string> messages = new List<string>();

    [SerializeField] private StringGameEvent snackbarEvent;
    // Start is called before the first frame update
    void Start()
    {
        //SendMessage(0);
    }

    public void SendMessage(int i)
    {
        snackbarEvent.Raise(messages[i]);
    }
}
