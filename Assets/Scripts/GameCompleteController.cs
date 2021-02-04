using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameCompleteController : MonoBehaviour
{
    [SerializeField] private GameObject panel;
    [SerializeField] private TMPro.TextMeshProUGUI timeField;
    [SerializeField] private TMPro.TextMeshProUGUI goldField;
    [SerializeField] private IntVariable gold;
    private RectTransform rt;
    private float time;

    private void Start()
    {
        time = Time.time;
        rt = panel.transform.GetChild(0).GetComponent<RectTransform>();
    }

    public void GameComplete()
    {
        panel.SetActive(true);
        
        rt.anchoredPosition = new Vector2(-2000f, rt.anchoredPosition.y);
        LeanTween.move(rt, new Vector3(0f, rt.anchoredPosition.y),1.25f)
            .setEaseOutQuart();    
        
    
        float deltaTime = Time.time - time;
        int minutes = (int)deltaTime / 60;
        int seconds = (int)(deltaTime - (minutes * 60));
        timeField.text = minutes.ToString() + " minutes and " + seconds + " seconds";
        goldField.text = "with " + gold.Value + " gold!";
    }

    public void Reload()
    {
        gold.SetValue(0);
        SceneManager.LoadScene("Main Game");
    }
}
