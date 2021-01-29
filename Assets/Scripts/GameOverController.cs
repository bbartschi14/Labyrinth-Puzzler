using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GameOverController : MonoBehaviour
{
    [SerializeField] private GameObject gameWonPanel;
    [SerializeField] private RectTransform contentRt;
    private Image im;

    private void Start()
    {
        im = gameWonPanel.GetComponent<Image>();
    }

    public void GameWon()
    {
        gameWonPanel.SetActive(true);
        contentRt.anchoredPosition = new Vector2(contentRt.anchoredPosition.x, 1000f);
        LeanTween.move(contentRt, new Vector3(contentRt.anchoredPosition.x, 0f),.5f)
            .setEaseOutQuart();
    }

    public void CloseWindow()
    {
        im.enabled = false;
        LeanTween.move(contentRt, new Vector3(contentRt.anchoredPosition.x, 1000f),.5f)
            .setEaseOutQuart()
            .setOnComplete(_ =>
            {
                gameWonPanel.SetActive(false);
                im.enabled = true;

            });
    }
}
