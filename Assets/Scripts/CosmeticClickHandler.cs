using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
public class CosmeticClickHandler : MonoBehaviour
{
    [SerializeField] private GameObject particles;
    private Camera cam;
    
    void Start()
    {
        cam = Camera.main;
        var clickStream = Observable.EveryUpdate()
            .Where(_ => Input.GetMouseButtonDown(0))
            .Subscribe(_ => SendClick(Input.mousePosition));
    }

    private void SendClick(Vector3 mousePos)
    {
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 100))
        {
            GameObject instance = Instantiate(particles, hit.point, Quaternion.identity);
            instance.transform.up = hit.normal;
            Destroy(instance,.75f);
        }
    }

    
}
