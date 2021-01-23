using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlowController : MonoBehaviour
{
    [SerializeField] private List<Material> glowMaterials;
    
    void Start()
    {
        foreach (Material mat in glowMaterials)
        {
            mat.SetColor("_GlowColor", Color.white);
        }
    }

    
}
