using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStartPanel : MonoBehaviour
{
    [SerializeField] private RectTransform rt;
    [SerializeField] private GameObject background;
    void Start()
    {
        rt.anchoredPosition = new Vector2(-2000f, rt.anchoredPosition.y);
        LeanTween.move(rt, new Vector3(0f, rt.anchoredPosition.y),1.25f)
            .setEaseOutQuart();    }
    
    public void ClosePanel()
    {
        background.SetActive(false);
            
        LeanTween.move(rt, new Vector3(2000f, rt.anchoredPosition.y),1.25f)
            .setEaseOutQuart()
            .setOnComplete(_ =>
            {
                gameObject.SetActive(false);
            });
    }
}
